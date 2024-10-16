using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Org.BouncyCastle.Cms;
using Project.Extensions;
using Project.Models;
using Project.Models.Dtos;
using Project.Models.Groups;
using Project.Models.ViewModels;
using Project.Repositorys.IRepository;
using Project.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Project.Areas.Customers.SignalR
{
    [Area("Customers")]
    
    public class MessageHub : Hub
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userReponsitory;
        private readonly IAuctionRepository _auctionRepository;
        private readonly IMapper _mapper;
        
        public MessageHub(IMessageRepository messageRepository, IUserRepository userReponsitory, IAuctionRepository auctionRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _userReponsitory = userReponsitory;
            _auctionRepository = auctionRepository;
            _mapper = mapper;
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var OtherProductId = httpContext.Request.Query["RecipientProductId"].ToString();
            var groupName = GetGroupName(OtherProductId.ToString());
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await AddToGroup(groupName);

            var messages = await _messageRepository.
                GetMessageThread(Context.User.GetUsername(), OtherProductId.ToString());

            await Clients.Caller.SendAsync("ReceiveMessageThread", messages);

        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var group = await RemoveFromMessageGroup();
            await base.OnDisconnectedAsync(exception);
        }


        [Authorize(Roles = UserRole.Customers)]
        public async Task SendMessageProduct(CreateMessageDtos createMessageDto)
        {

            Auction recipient = await _auctionRepository.GetAuctionByIdAsync(createMessageDto.RecipientProductId);

            if (recipient.Product_Status == false)
                throw new HubException("Error Input");

            if(recipient.Time_End < DateTime.Now)
                throw new HubException("Error Input");

            if(recipient.Product_StatusBuy == true)
                throw new HubException("Error Input");

            ApplicationUser sender = await _userReponsitory.getUserByUsernameAsync(Context.User.GetUsername());
            
            if(sender.coin < createMessageDto.Content)
                throw new HubException("You can't money");

            string groupName = GetGroupName(recipient.Id);

            if (recipient.Price_End == createMessageDto.Content)
            {
                Message messageFinal = new Message();
                messageFinal = await _messageRepository.GetBacktoMoney(createMessageDto.RecipientProductId);
                if (messageFinal == null)
                {
                    var message = new Message
                    {
                        SenderId = sender.Id,
                        SenderUsername = sender.UserName,
                        RecipientId = recipient.Id,
                        Content = createMessageDto.Content
                    };
                    _messageRepository.AddMessage(message);

                    if (await _messageRepository.SaveAllAsync())
                    {
                        recipient.Product_StatusBuy = true;
                        recipient.Time_End = DateTime.Now;
                        _auctionRepository.UpdateAuction(recipient);


                        sender.coin -= createMessageDto.Content;
                        _userReponsitory.UpdateAppUser(sender);

                        Message messageBuy = new Message();
                        messageFinal = await _messageRepository.GetBacktoMoney(createMessageDto.RecipientProductId);
                        messageBuy = await _messageRepository.GetWinner(messageFinal.RecipientId);
                        if (!_auctionRepository.AuctionIdExistWinner(messageBuy.RecipientId))
                        {
                            CreateAuctionWinnerDtos auctionWinnerDtos = new CreateAuctionWinnerDtos()
                            {
                                auctionId = messageBuy.RecipientId,
                                applicationUserId = messageBuy.SenderId,
                                MessageId = messageBuy.Id,
                                DeliveryStatus = false
                            };

                            var result = _mapper.Map<AuctionWinner>(auctionWinnerDtos);

                            _auctionRepository.CreateAuctionWinner(result);
                            ApplicationUser backcoin = new ApplicationUser();
                            backcoin = await _userReponsitory.getUserByIdAsync(recipient.ApplicationUserId);
                            backcoin.coin += (createMessageDto.Content * 0.95);
                            _userReponsitory.UpdateAppUser(backcoin);
                        }
                        Message username = await _messageRepository.GetMessage(createMessageDto.RecipientProductId);
                        await Clients.Group(groupName).SendAsync("NewMessage", _mapper.Map<MessageDtos>(username));
                    }
                }
                else
                {
                    recipient.Product_StatusBuy = true;
                    recipient.Time_End = DateTime.Now;
                    _auctionRepository.UpdateAuction(recipient);

                    sender.coin -= createMessageDto.Content;

                    _userReponsitory.UpdateAppUser(sender);

                    ApplicationUser recipientID = new ApplicationUser();
                    recipientID = await _userReponsitory.getUserByUsernameAsync(messageFinal.SenderUsername);

                    recipientID.coin += messageFinal.Content;

                    _userReponsitory.UpdateAppUser(recipientID);

                    messageFinal.MoneyBack = true;
                    _messageRepository.UpdateMessage(messageFinal);
                    await _messageRepository.SaveAllAsync();


                    var message = new Message
                    {
                        SenderId = sender.Id,
                        SenderUsername = sender.UserName,
                        RecipientId = recipient.Id,
                        Content = createMessageDto.Content
                    };
                    _messageRepository.AddMessage(message);

                    if (await _messageRepository.SaveAllAsync())
                    {
                        sender.coin += createMessageDto.Content;
                        sender.coin -= createMessageDto.Content;
                        _userReponsitory.UpdateAppUser(sender);
                    }

                    Message messageBuy = new Message();
                    messageBuy = await _messageRepository.GetWinner(messageFinal.RecipientId);
                    if (!_auctionRepository.AuctionIdExistWinner(messageBuy.RecipientId))
                    {
                        CreateAuctionWinnerDtos auctionWinnerDtos = new CreateAuctionWinnerDtos()
                        {
                            auctionId = messageBuy.RecipientId,
                            applicationUserId = messageBuy.SenderId,
                            MessageId = messageBuy.Id,
                            DeliveryStatus = false
                        };

                        var result = _mapper.Map<AuctionWinner>(auctionWinnerDtos);

                        _auctionRepository.CreateAuctionWinner(result);
                        ApplicationUser backcoin = new ApplicationUser();
                        backcoin = await _userReponsitory.getUserByIdAsync(recipient.ApplicationUserId);
                        backcoin.coin += (createMessageDto.Content * 0.95);
                        _userReponsitory.UpdateAppUser(backcoin);
                    }
                    Message username = await _messageRepository.GetMessage(createMessageDto.RecipientProductId);
                    await Clients.Group(groupName).SendAsync("NewMessage", _mapper.Map<MessageDtos>(username));

                }
            }
            else { 

                Message messagefirst = new Message();
                messagefirst = await _messageRepository.GetMessagePrice(createMessageDto.RecipientProductId);

                double check = 0.0;
                double checkInputEnd = 0.0;
                if (messagefirst == null)
                {
                    check = recipient.Price_Start * 1.2;
                }
                else
                {
                    check = messagefirst.Content;
                }
                checkInputEnd = (createMessageDto.Content);
                var checkvalueinput = (check > recipient.Price_End) ? (createMessageDto.Content < recipient.Price_End && createMessageDto.Content >= checkInputEnd) : (createMessageDto.Content < recipient.Price_End && createMessageDto.Content >= check);

                if (!checkvalueinput)
                    throw new HubException("Error Input");


                if (recipient == null) throw new HubException("Not found user");

                if (_messageRepository.CheckTop1(Context.User.GetUserId(), createMessageDto.RecipientProductId))
                    throw new HubException("You is Top 1");

                Message messagefour = new Message();
                messagefour = await _messageRepository.GetBacktoMoney(createMessageDto.RecipientProductId);
                UpdateCoinUserDtos backtoCoinDtos = new UpdateCoinUserDtos();
                if (messagefour != null)
                {
                    ApplicationUser recipientID = await _userReponsitory.getUserByUsernameAsync(messagefour.SenderUsername);

                    backtoCoinDtos.coin = recipientID.coin;
                    backtoCoinDtos.coin += messagefour.Content;
                    _mapper.Map(backtoCoinDtos, recipientID);

                    _userReponsitory.UpdateAppUser(recipientID);

                    UpdateMessageContentDtos updateMessageContentDtos = new UpdateMessageContentDtos();
                    updateMessageContentDtos.MoneyBack = true;

                    _mapper.Map(updateMessageContentDtos, messagefour);

                    _messageRepository.UpdateMessage(messagefour);
                }

                var message = new Message
                {
                    SenderId = sender.Id,
                    SenderUsername = sender.UserName,
                    RecipientId = recipient.Id,
                    Content = createMessageDto.Content
                };
                _messageRepository.AddMessage(message);

                if (await _messageRepository.SaveAllAsync())
                {

                    backtoCoinDtos.coin = sender.coin;
                    backtoCoinDtos.coin -= createMessageDto.Content;
                    _mapper.Map(backtoCoinDtos, sender);

                    _userReponsitory.UpdateAppUser(sender);
                    Message username = await _messageRepository.GetMessage(createMessageDto.RecipientProductId);
                    await Clients.Group(groupName).SendAsync("NewMessage", _mapper.Map<MessageDtos>(username));
                }
            
            }

        }

        private async Task<Group> AddToGroup(string groupName)
        {
            var group = await _messageRepository.GetMessageGroup(groupName);
            var connection = new Connection(Context.ConnectionId, Context.User.GetUsername());

            if (group == null)
            {
                group = new Group(groupName);
                _messageRepository.AddGroup(group);
            }

            group.Connections.Add(connection);

            if (await _messageRepository.SaveAllAsync()) return group;

            throw new HubException("Failed to join group");
        }

        private async Task<Group> RemoveFromMessageGroup()
        {
            var group = await _messageRepository.GetGroupForConnection(Context.ConnectionId);
            var connection = group.Connections.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            _messageRepository.RemoveConnection(connection);
            if (await _messageRepository.SaveAllAsync()) return group;

            throw new HubException("Failed to remove from group");
        }

        private string GetGroupName(string ProductNameId)
        {
            return $"{ProductNameId}";
        }
    }
}
