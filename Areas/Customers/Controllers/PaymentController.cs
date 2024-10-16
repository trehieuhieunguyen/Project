using AutoMapper;
using BraintreeHttp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PayPal.Core;
using PayPal.v1.Payments;
using Project.Extensions;
using Project.Helpers;
using Project.Models;
using Project.Models.Dtos;
using Project.Repositorys.IRepository;
using Project.Utilities;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Areas.Customers.Controllers
{
    [Area("Customers")]
    [Authorize(Roles = UserRole.Customers)]
    [Route("[controller]/")]
    public class PaymentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IOrderCoinHeaderRepos _orderCoinHeaderRepos;
        private readonly IOrderCoinDetailRepos _orderCoinDetailRepos;
        private readonly IUserRepository _userRepository;
        private readonly string _clientId;
        private readonly string _secretKey;
        public PaymentController(IConfiguration configuration, IMapper mapper, IOrderCoinHeaderRepos orderCoinHeaderRepos, IOrderCoinDetailRepos orderCoinDetailRepos, IUserRepository userRepository)
        {
            _mapper = mapper;
            _orderCoinHeaderRepos = orderCoinHeaderRepos;
            _orderCoinDetailRepos = orderCoinDetailRepos;
            _userRepository = userRepository;
            _clientId = configuration["Paypal:ClientId"];
            _secretKey = configuration["Paypal:SecretKey"];
        }
        [Route("CheckOut")]
        public async Task<IActionResult> CheckOut()
        {
            if (SessionHelper.GetObjectFromJson<string>(HttpContext.Session, "url") == null)
            {
                return await Task.Run(() => RedirectToAction("Index", "Coin", new { area = "Customers" }));
            }

            return await Task.Run(() => View());
        }

        private int GetIDCoin(string namecoin)
        {
            int i;
            switch (namecoin)
            {
                case "100 Xu":
                    i = 1;
                    break;
                case "200 Xu":
                    i = 2;
                    break;
                case "500 Xu":
                    i = 3;
                    break;
                case "1000 Xu":
                    i = 4;
                    break;
                case "2000 Xu":
                    i = 5;
                    break;
                case "5000 Xu":
                    i = 6;
                    break;
                default: 
                    i = 7;
                    break;
            }
            return i;
        }

        private PayPalHttpClient Client()
        {
            var environment = new SandboxEnvironment(_clientId, _secretKey);
            var client = new PayPalHttpClient(environment);
            return client;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PaypalCheckout()
        {
            List<OrderCoinDetail> cart = SessionHelper.GetObjectFromJson<List<OrderCoinDetail>>(HttpContext.Session, "cart");
            if (cart is null)
            {
                return RedirectToAction("Index", "Coin", new { area = "Customers" });
            }

            var client = Client();

            var itemList = new ItemList()
            {
                Items = new List<Item>()
            };

            double total = 0.0;
            if (cart is not null)
            {
                total = cart.Sum(item => item.CoinProducts.Price * item.Quantity);
                for (int i = 0; i < cart.Count; i++)
                {
                    if (cart[i].CoinProducts.Id == 7)
                    {
                        total += cart[i].Price;
                    }
                }
            }

            foreach (var item in cart)
            {
                if (item.CoinProducts.Id == 7)
                {
                    itemList.Items.Add(new Item()
                    {
                        Name = item.CoinProducts.CoinName,
                        Currency = "USD",
                        Price = item.Price.ToString(CultureInfo.InvariantCulture),
                        Quantity = item.Quantity.ToString(),
                        Sku = "sku",
                        Tax = "0"
                    });
                }
                else {
                    itemList.Items.Add(new Item()
                    {
                        Name = item.CoinProducts.CoinName,
                        Currency = "USD",
                        Price = item.CoinProducts.Price.ToString(CultureInfo.InvariantCulture),
                        Quantity = item.Quantity.ToString(),
                        Sku = "sku",
                        Tax = "0"
                    });
                }
            }

            var hostname = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            var paypalOrderId = Guid.NewGuid();

            var payment = new Payment()
            {
                Intent = "sale",
                Transactions = new List<Transaction>()
                {
                    new Transaction()
                    {
                        Amount = new Amount()
                        {
                            Total = total.ToString(CultureInfo.InvariantCulture),
                            Currency = "USD"
                        },
                        ItemList = itemList,
                        Description = $"Invoice #{paypalOrderId}",
                        InvoiceNumber = paypalOrderId.ToString()
                    }
                },
                RedirectUrls = new RedirectUrls()
                {
                    CancelUrl = $"{hostname}/Payment/Fail?orderId={paypalOrderId}",
                    ReturnUrl = $"{hostname}/Payment/PaypalSuccess?orderId={paypalOrderId}"
                },
                Payer = new Payer()
                {
                    PaymentMethod = "paypal"
                }
            };


            PaymentCreateRequest request = new PaymentCreateRequest();
            request.RequestBody(payment);

            try
            {
                var response = await client.Execute(request);
                var statusCode = response.StatusCode;
                Payment result = response.Result<Payment>();

                var links = result.Links.GetEnumerator();
                string paypalRedirectUrl = null;
                while (links.MoveNext())
                {
                    LinkDescriptionObject lnk = links.Current;
                    if (lnk.Rel.ToLower().Trim().Equals("approval_url"))
                    {
                        //saving the payapalredirect URL to which user will be redirected for payment  
                        paypalRedirectUrl = lnk.Href;
                    }
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "url", paypalRedirectUrl);
                return RedirectToAction("CheckOut", "Payment", new { area = "Customers" });
            }
            catch (HttpException httpException)
            {
                var statusCode = httpException.StatusCode;
                var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();

                //Process when Checkout with Paypal fails
                return Redirect("/Customers/Payment/Fail");
            }

        }
        [Route("PaypalSuccess")]
        public async Task<IActionResult> PaypalSuccess([FromQuery(Name = "orderId")] string orderId, [FromQuery(Name = "paymentId")] string paymentId, [FromQuery(Name = "PayerID")] string payerID)
        {
            if (payerID is null)
            {
                return RedirectToAction("Index", "Home", new { area = "Customers" });
            }
            var request = new PaymentExecuteRequest(paymentId);

            request.RequestBody(new PaymentExecution()
            {
                PayerId = payerID
            });


            var client = Client();

            var response = await client.Execute(request);
            var payment = response.Result<Payment>();

            if (payment.State != "approved")
            { // no blance or something else?
                return RedirectToAction(nameof(Fail));
            }


            var items = new List<Item>();

            foreach (var cartitem in payment.Transactions)
            {
                foreach (var itemlist in cartitem.ItemList.Items)
                {
                    items.Add(new Item()
                    {
                        Name = itemlist.Name,
                        Price = itemlist.Price,
                        Quantity = itemlist.Quantity
                    });
                }
            }

            OrderCoinHeaderDtos model = new OrderCoinHeaderDtos
            {
                Id = orderId,
                Date = DateTime.Now,
                PaymentType = "PayPal",
                PaymentStatus = true,
                TransactionId = paymentId,
                ApplicationUserId = User.GetUserId()
            };

            var CreateOrderHeaderObj = _mapper.Map<OrderCoinHeader>(model);
            if (!_orderCoinHeaderRepos.CreateOderHeader(CreateOrderHeaderObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {CreateOrderHeaderObj.Id}");
                return StatusCode(500, ModelState);
            }
            var userFromDb = await _userRepository.getUserByIdAsync(User.GetUserId());
            OrderCoinDetail CreateOrderDetailObj;
            double coin123 = 0;
            foreach (var cart in items)
            {
                CreateOrderCoinDetailDtos orderCoinDetail = new CreateOrderCoinDetailDtos(){};
                orderCoinDetail.OrderHeaderId = orderId;

                orderCoinDetail.CoinProductsId = GetIDCoin(cart.Name);

                orderCoinDetail.Quantity = Convert.ToInt32(cart.Quantity);
                orderCoinDetail.Price = Convert.ToDouble(cart.Price);
                

                CreateOrderDetailObj = _mapper.Map<OrderCoinDetail>(orderCoinDetail);
                if (!_orderCoinDetailRepos.CreateOderDetails(CreateOrderDetailObj))
                {
                    ModelState.AddModelError("", $"Something went wrong when saving the record {CreateOrderDetailObj.Id}");
                    return StatusCode(500, ModelState);
                }
                
                coin123 += (Convert.ToInt32(cart.Quantity) * Convert.ToDouble(cart.Price));
                
            }
            userFromDb.coin = userFromDb.coin + coin123;
            _userRepository.UpdateAppUser(userFromDb);

            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", null);


            return await Task.Run(() => RedirectToAction("OrderDetails", "Order", new { area = "Customers", id = orderId }));
        }
        [Route("Fail")]
        public async Task<IActionResult> Fail([FromQuery(Name = "orderId")] string orderId)
        {
            List<OrderCoinDetail> lstcart = SessionHelper.GetObjectFromJson<List<OrderCoinDetail>>(HttpContext.Session, "cart");
            if (lstcart is null)
            {
                return RedirectToAction("Index", "Home", new { area = "Customers" });
            }
            OrderCoinHeaderDtos model = new OrderCoinHeaderDtos
            {
                Id = orderId,
                Date = DateTime.Now,
                PaymentType = "PayPal",
                PaymentStatus = false,
                ApplicationUserId = User.GetUserId()
            };

            var CreateOrderHeaderObj = _mapper.Map<OrderCoinHeader>(model);
            if (!_orderCoinHeaderRepos.CreateOderHeader(CreateOrderHeaderObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {CreateOrderHeaderObj.Id}");
                return StatusCode(500, ModelState);
            }

            OrderCoinDetail CreateOrderDetailObj;
            foreach (var cart in lstcart)
            {
                CreateOrderCoinDetailDtos orderCoinDetail = new CreateOrderCoinDetailDtos() { };
                orderCoinDetail.OrderHeaderId = orderId;
                orderCoinDetail.CoinProductsId = cart.CoinProducts.Id;
                orderCoinDetail.Quantity = cart.Quantity;

                if (cart.CoinProducts.Id == 7)
                {
                    orderCoinDetail.Price = cart.Price;
                }
                else
                {
                    orderCoinDetail.Price = cart.CoinProducts.Price;
                }

                CreateOrderDetailObj = _mapper.Map<OrderCoinDetail>(orderCoinDetail);
                if (!_orderCoinDetailRepos.CreateOderDetails(CreateOrderDetailObj))
                {
                    ModelState.AddModelError("", $"Something went wrong when saving the record {CreateOrderDetailObj.Id}");
                    return StatusCode(500, ModelState);
                }
            }

            lstcart.Clear();
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", lstcart);
            return await Task.Run(() => RedirectToAction("OrderDetails", "Order", new { area = "Customers", id = orderId }));
        }
        [Route("GenerateQRCode")]
        public async Task<IActionResult> GenerateQRCode()
        {
            string Url = SessionHelper.GetObjectFromJson<string>(HttpContext.Session, "url");
            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(Url, QRCodeGenerator.ECCLevel.Q);
            QRCode qRCode = new QRCode(qRCodeData);
            Bitmap bitmap = qRCode.GetGraphic(15);
            var bitmapBytes = await ConvertBitmapToBytes(bitmap);
            return await Task.Run(() => (File(bitmapBytes, "image/jpeg")));
        }

        private async Task<byte[]> ConvertBitmapToBytes(Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                return await Task.Run(() => ms.ToArray());
            }
        }
    }
}
