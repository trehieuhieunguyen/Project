//Migrated from _Library
window._wpemojiSettings = { "baseUrl": "https:\/\/s.w.org\/images\/core\/emoji\/12.0.0-1\/72x72\/", "ext": ".png", "svgUrl": "https:\/\/s.w.org\/images\/core\/emoji\/12.0.0-1\/svg\/", "svgExt": ".svg", "source": { "concatemoji": "\/wp-includes\/js\/wp-emoji-release.min.js?ver=5.3" } };
!function (e, a, t) { var r, n, o, i, p = a.createElement("canvas"), s = p.getContext && p.getContext("2d"); function c(e, t) { var a = String.fromCharCode; s.clearRect(0, 0, p.width, p.height), s.fillText(a.apply(this, e), 0, 0); var r = p.toDataURL(); return s.clearRect(0, 0, p.width, p.height), s.fillText(a.apply(this, t), 0, 0), r === p.toDataURL() } function l(e) { if (!s || !s.fillText) return !1; switch (s.textBaseline = "top", s.font = "600 32px Arial", e) { case "flag": return !c([127987, 65039, 8205, 9895, 65039], [127987, 65039, 8203, 9895, 65039]) && (!c([55356, 56826, 55356, 56819], [55356, 56826, 8203, 55356, 56819]) && !c([55356, 57332, 56128, 56423, 56128, 56418, 56128, 56421, 56128, 56430, 56128, 56423, 56128, 56447], [55356, 57332, 8203, 56128, 56423, 8203, 56128, 56418, 8203, 56128, 56421, 8203, 56128, 56430, 8203, 56128, 56423, 8203, 56128, 56447])); case "emoji": return !c([55357, 56424, 55356, 57342, 8205, 55358, 56605, 8205, 55357, 56424, 55356, 57340], [55357, 56424, 55356, 57342, 8203, 55358, 56605, 8203, 55357, 56424, 55356, 57340]) }return !1 } function d(e) { var t = a.createElement("script"); t.src = e, t.defer = t.type = "text/javascript", a.getElementsByTagName("head")[0].appendChild(t) } for (i = Array("flag", "emoji"), t.supports = { everything: !0, everythingExceptFlag: !0 }, o = 0; o < i.length; o++)t.supports[i[o]] = l(i[o]), t.supports.everything = t.supports.everything && t.supports[i[o]], "flag" !== i[o] && (t.supports.everythingExceptFlag = t.supports.everythingExceptFlag && t.supports[i[o]]); t.supports.everythingExceptFlag = t.supports.everythingExceptFlag && !t.supports.flag, t.DOMReady = !1, t.readyCallback = function () { t.DOMReady = !0 }, t.supports.everything || (n = function () { t.readyCallback() }, a.addEventListener ? (a.addEventListener("DOMContentLoaded", n, !1), e.addEventListener("load", n, !1)) : (e.attachEvent("onload", n), a.attachEvent("onreadystatechange", function () { "complete" === a.readyState && t.readyCallback() })), (r = t.source || {}).concatemoji ? d(r.concatemoji) : r.wpemoji && r.twemoji && (d(r.twemoji), d(r.wpemoji))) }(window, document, window._wpemojiSettings);

/* <![CDATA[ */
var dokan = { "ajaxurl": "\/wp-admin\/admin-ajax.php", "nonce": "6b8e6296dd", "ajax_loader": "\/wp-content\/plugins\/dokan-lite\/assets\/images\/ajax-loader.gif", "seller": { "available": "Available", "notAvailable": "Not Available" }, "delete_confirm": "Are you sure?", "wrong_message": "Something went wrong. Please try again.", "vendor_percentage": "90", "commission_type": "percentage", "rounding_precision": "6", "mon_decimal_point": ".", "product_types": ["simple"], "rest": { "root": "\/wp-json\/", "nonce": "b6bc898de6", "version": "dokan\/v1" }, "api": null, "libs": [], "routeComponents": { "default": null }, "routes": [], "urls": { "assetsUrl": "\/wp-content\/plugins\/dokan-lite\/assets" } };
/* ]]> */

/* <![CDATA[ */
var wc_add_to_cart_params = { /*"ajax_url": "\/real-estate\/wp-admin\/admin-ajax.php", */"wc_ajax_url": "\/real-estate\/?wc-ajax=%%endpoint%%", "i18n_view_cart": "View cart", "cart_url": "\/cart\/", "is_cart": "", "cart_redirect_after_add": "no" };
/* ]]> */

/* <![CDATA[ */
var countdown_language_data = { "labels": { "Years": "Năm", "Months": "Tháng", "Weeks": "Tuần", "Days": "Ngày", "Hours": "Giờ", "Minutes": "Phút", "Seconds": "Giây" }, "labels1": { "Year": "Năm", "Month": "Tháng", "Week": "Tuần", "Day": "Ngày", "Hour": "Giờ", "Minute": "Phút", "Second": "Giây" }, "compactLabels": { "y": "y", "m": "m", "w": "w", "d": "d" } };
/* ]]> */

/* <![CDATA[ */
var data = { "finished": "Phiên đấu giá kết thúc!", "checking": "Vui lòng đợi trong khi xử lý trạng thái phiên!", "gtm_offset": "0", "started": "Auction has started! Please refresh your page.", "no_need": "No need to bid. Your bid is winning! ", "compact_counter": "no", "outbid_message": "<ul class=\"woocommerce-error\" role=\"alert\">\n\t\t\t<li>\n\t\t\tYou've been outbid!\t\t<\/li>\n\t<\/ul>\n", "interval": "1" };
var SA_Ajax = { "ajaxurl": "\/real-estate\/?wsa-ajax", "najax": "1", "last_activity": "1574864091", "focus": "yes" };
/* ]]> */

(function () {
    window.lvca_fs = { can_use_premium_code: false };
})();

function setREVStartSize(t) { try { var h, e = document.getElementById(t.c).parentNode.offsetWidth; if (e = 0 === e || isNaN(e) ? window.innerWidth : e, t.tabw = void 0 === t.tabw ? 0 : parseInt(t.tabw), t.thumbw = void 0 === t.thumbw ? 0 : parseInt(t.thumbw), t.tabh = void 0 === t.tabh ? 0 : parseInt(t.tabh), t.thumbh = void 0 === t.thumbh ? 0 : parseInt(t.thumbh), t.tabhide = void 0 === t.tabhide ? 0 : parseInt(t.tabhide), t.thumbhide = void 0 === t.thumbhide ? 0 : parseInt(t.thumbhide), t.mh = void 0 === t.mh || "" == t.mh || "auto" === t.mh ? 0 : parseInt(t.mh, 0), "fullscreen" === t.layout || "fullscreen" === t.l) h = Math.max(t.mh, window.innerHeight); else { for (var i in t.gw = Array.isArray(t.gw) ? t.gw : [t.gw], t.rl) void 0 !== t.gw[i] && 0 !== t.gw[i] || (t.gw[i] = t.gw[i - 1]); for (var i in t.gh = void 0 === t.el || "" === t.el || Array.isArray(t.el) && 0 == t.el.length ? t.gh : t.el, t.gh = Array.isArray(t.gh) ? t.gh : [t.gh], t.rl) void 0 !== t.gh[i] && 0 !== t.gh[i] || (t.gh[i] = t.gh[i - 1]); var r, a = new Array(t.rl.length), n = 0; for (var i in t.tabw = t.tabhide >= e ? 0 : t.tabw, t.thumbw = t.thumbhide >= e ? 0 : t.thumbw, t.tabh = t.tabhide >= e ? 0 : t.tabh, t.thumbh = t.thumbhide >= e ? 0 : t.thumbh, t.rl) a[i] = t.rl[i] < window.innerWidth ? 0 : t.rl[i]; for (var i in r = a[0], a) r > a[i] && 0 < a[i] && (r = a[i], n = i); var d = e > t.gw[n] + t.tabw + t.thumbw ? 1 : (e - (t.tabw + t.thumbw)) / t.gw[n]; h = t.gh[n] * d + (t.tabh + t.thumbh) } void 0 === window.rs_init_css && (window.rs_init_css = document.head.appendChild(document.createElement("style"))), document.getElementById(t.c).height = h, window.rs_init_css.innerHTML += "#" + t.c + "_wrapper { height: " + h + "px }" } catch (t) { console.log("Failure at Presize of Slider:" + t) } };


function initTable() {
    table = $('#tableData').DataTable({
        aLengthMenu: [
            [-1, 10, 25, 50, 100, 200],
            ['Tất cả', 10, 25, 50, 100, 200]
        ],
        'order': [
            [1, 'desc']
        ],
        "oLanguage": {
            "sUrl": "/js/Vietnamese.json"
        },
        "initComplete": function (settings, json) {
            table.on('order.dt search.dt', function () {
                table.column(0, {
                    search: 'applied',
                    order: 'applied'
                }).nodes().each(function (cell, i) {
                    cell.innerHTML = i + 1;
                });
            }).draw();

            //$('#tableData tfoot th:not(:last-child):not(:nth-last-child(2))').each(function () {
            //    var title = $(this).text();
            //    $(this).html("<input type='text' class='tableFooterFilter' placeholder=' ' />");
            //});
            $('#tableData tfoot th').each(function () {
                var title = $(this).text();
                $(this).html("<input type='text' class='tableFooterFilter' placeholder=' ' />");
            });

            table.columns().every(function () {
                var that = this;
                $('input.tableFooterFilter', this.footer()).on('keyup change', function () {
                    if (that.search() !== this.value) {
                        that.search(this.value).draw();
                    }
                });
            });
        }
    });

    setTimeout(function () {
        table.page.len(10).draw();
    }, 1000);
}

function SortByPriceDesc() {
    $("#listSearch li").sort(function (a, b) {
        return $(b).data('price') - $(a).data('price');
    }).appendTo("#listSearch");
    console.log("Success Sort desc by Price!");
}

function SortByPriceESC() {
    $("#listSearch li").sort(function (a, b) {
        return $(a).data('price') - $(b).data('price');
    }).appendTo("#listSearch");
    console.log("Success Sort esc by Price!");
}

function SortByDateDesc() {
    //debugger;
    $("#listSearch li").sort(function (a, b) {
        return new Date(ConvertDateTimeLanguage($(b).data('createdtime'))) - new Date(ConvertDateTimeLanguage($(a).data('createdtime')));
    }).appendTo("#listSearch");
    console.log("Success Sort desc by Date!");
}

function SortByDateEsc() {
    //debugger;
    $("#listSearch li").sort(function (a, b) {
        return new Date(ConvertDateTimeLanguage($(a).data('createdtime'))) - new Date(ConvertDateTimeLanguage($(b).data('createdtime')));
    }).appendTo("#listSearch");
    console.log("Success Sort esc by Date!");
}


/* * * * * * * * * * * * * * * * *
     * Pagination
     * javascript page navigation
     * * * * * * * * * * * * * * * * */

var Pagination = {

    code: '',

    // --------------------
    // Utility
    // --------------------

    // converting initialize data
    Extend: function (data) {
        data = data || {};
        Pagination.size = data.size || 1;
        Pagination.page = data.page || 1;
        Pagination.step = data.step || 3;
    },

    // add pages by number (from [s] to [f])
    Add: function (s, f) {
        for (var i = s; i < f; i++) {
            Pagination.code += '<li id="nPaging-' + i + '"><a>' + i + '</a></li>';
        }
    },

    // add last page with separator
    Last: function () {
        Pagination.code += '<i>...</i><li><a>' + Pagination.size + '</a></li>';
    },

    // add first page with separator
    First: function () {
        Pagination.code += '<li><a>1</a><li><i>...</i>';
    },

    // --------------------
    // Handlers
    // --------------------

    // change page
    Click: function () {
        Pagination.page = +this.innerHTML;
        Pagination.Start();
    },

    // previous page
    Prev: function () {
        Pagination.page--;
        if (Pagination.page < 1) {
            Pagination.page = 1;
        }
        Pagination.Start();
    },

    // next page
    Next: function () {
        Pagination.page++;
        if (Pagination.page > Pagination.size) {
            Pagination.page = Pagination.size;
        }
        Pagination.Start();
    },

    // --------------------
    // Script
    // --------------------

    // binding pages
    Bind: function () {
        var a = Pagination.e.getElementsByTagName('a');
        for (var i = 0; i < a.length; i++) {
            if (+a[i].innerHTML === Pagination.page) a[i].className = 'current';
            a[i].addEventListener('click', Pagination.Click, false);
        }
    },

    // write pagination
    Finish: function () {
        Pagination.e.innerHTML = Pagination.code;
        Pagination.code = '';
        Pagination.Bind();
    },

    // find pagination type
    Start: function () {
        if (Pagination.size < Pagination.step * 2 + 6) {
            Pagination.Add(1, Pagination.size + 1);
        } else if (Pagination.page < Pagination.step * 2 + 1) {
            Pagination.Add(1, Pagination.step * 2 + 4);
            Pagination.Last();
        } else if (Pagination.page > Pagination.size - Pagination.step * 2) {
            Pagination.First();
            Pagination.Add(Pagination.size - Pagination.step * 2 - 2, Pagination.size + 1);
        } else {
            Pagination.First();
            Pagination.Add(Pagination.page - Pagination.step, Pagination.page + Pagination.step + 1);
            Pagination.Last();
        }
        Pagination.Finish();
    },

    // --------------------
    // Initialization
    // --------------------

    // binding buttons
    Buttons: function (e) {
        var nav = e.getElementsByTagName('a');
        nav[0].addEventListener('click', Pagination.Prev, false);
        nav[1].addEventListener('click', Pagination.Next, false);
    },

    // create skeleton
    Create: function (e) {

        var html = [
            '<li id="nPaging-Previous"><a>←</a></li>', // previous button
            '<span></span>', // pagination container
            '<li id="nPaging-Next"><a>→</a></li>' // next button
        ];
        if (e != null) {
            e.innerHTML = html.join('');
            Pagination.e = e.getElementsByTagName('span')[0];
            Pagination.Buttons(e);
        }

    },

    // init
    Init: function (e, data) {
        Pagination.Extend(data);
        Pagination.Create(e);
        Pagination.Start();
    }
};

/* * * * * * * * * * * * * * * * *
 * Initialization
 * * * * * * * * * * * * * * * * */

//var init = function () {
//    Pagination.Init(document.getElementById('nPagination'), {
//        size: 30, // pages size
//        page: 1, // selected page
//        step: 2 // pages before and after current
//    });
//};
//document.addEventListener('DOMContentLoaded', init, false);

function ShowPaging(size, page, step) {
    Pagination.Init(document.getElementById('nPagination'), {
        size: size,
        page: page,
        step: 1
    })
}

function validateEmail(email) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}

function ConvertDateTimeLanguage(datetime) {
    //debugger;
    if (datetime != null) {
        var arr = datetime.split(" ");
        var arrAM = ["SA", "Sa", "sA", "sa"];
        var arrPM = ["CH", "Ch", "cH", "ch"];
        arrAM.every(function (item, index) {
            if (arr.includes(item)) {
                datetime = datetime.replace(item, "AM");
                return datetime;
            }
        });
        arrPM.every(function (item, index) {
            if (arr.includes(item)) {
                datetime = datetime.replace(item, "PM");
                return datetime;
            }
        });
        return datetime;
    }
}


//$("#nPagination > span > li > a").click(function(){ alert($(this).text()); })

// kiem tra dang nhap
function CheckLoginBiddingRequest(currentUserId) {
    if (currentUserId == 0 || currentUserId == undefined) {
        //Swal.fire(
        //    'Bạn chưa đăng nhập!',
        //    'Đăng nhập để tham gia đấu giá',
        //    'info'
        //)
        $("#btnLogginTrigger").click();
        return false;
    }
}




//Migrated from _Library ends


//Migrated from _Homepage_ContactUs
function ValidateEmail(mail) {
    if (/^\w+([\.-]?\w+)*@@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(mail)) {
        return (true)
    }
    //alert("You have entered an invalid email address!")
    return (false)
};

function ValidatePhone(phone) {
    if (/^[0-9\-\+]{9,15}$/.test(phone)) {
        return (true)
    }
    //alert("You have entered an invalid email address!")
    return (false)
};
function contactSubmit() {
    //1 get inserted values
    var email = $("#email").val();
    var phone = $("#namePhone").val();


    //2 validate data
    if ($("#nameContact").val() == '') {
        Swal.fire({
            icon: 'warning',
            title: 'Vui lòng nhập thông tin của bạn',
            text: 'Bạn nhập hoặc chưa nhập đầy đủ các thông tin',
            //footer: '<a href>Why do I have this issue?</a>'
        })
        return;
    };
    if (phone == '') {
        Swal.fire({
            icon: 'info',
            title: 'Vui lòng nhập Số điện thoại của bạn!',
            //text: 'Hãy kiểm tra lại định dạng Số điện thoại',
            //footer: '<a href>Why do I have this issue?</a>'
        })
        return;
    };
    if (phone.length < 9) {
        Swal.fire({
            icon: 'info',
            title: 'Số điện thoại không hợp lệ!',
            //text: 'Hãy kiểm tra lại định dạng Số điện thoại',
            //footer: '<a href>Why do I have this issue?</a>'
        })
        return;
    };
    if (email == '') {
        Swal.fire({
            icon: 'info',
            title: 'Vui lòng nhập email của bạn',
            //text: 'Hãy kiểm tra lại định dạng mail!',
            //footer: '<a href>Why do I have this issue?</a>'
        })
        return;
    };
    if (ValidateEmail(email) == false) {
        Swal.fire({
            icon: 'info',
            title: 'Vui lòng kiểm tra lại định dạng email',
            //text: 'Hãy kiểm tra lại định dạng mail!',
            //footer: '<a href>Why do I have this issue?</a>'
        })
        return;
    };

    if ($("#messageContent").val() == '') {
        Swal.fire({
            icon: 'info',
            title: 'Vui lòng nhập tin nhắn của bạn',
            //text: 'hãy nhập tin nhắn trên 50 ký tự',
            //footer: '<a href>Why do I have this issue?</a>'
        })
        return;
    };
    //3 insert data
    //debugger;
    var dt = new Date();
    var obj = {
        "Id": 0,
        "Name": $("#nameContact").val(),
        "Phone": $("#namePhone").val(),
        "Active": 1,
        "Email": $("#email").val(),
        "Description": $("#messageContent").val(),
        "Message": $("#messageContent").val(),
        "Job": $("#nameJob").val(),
        "ContactStatusId": 1000002,
        "IsChecked": 0,
        "CreatedTime": dt,
    };
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "Contact/api/Add",
        data: JSON.stringify(obj),
        success: function (responseData) {
            //responseData = JSON.parse(responseData)
            if (responseData.status == 201 && responseData.message === "CREATED") {
                Swal.fire({
                    icon: 'success',
                    title: 'Thành công!',
                    text: 'Chúng tôi sẽ liện hệ lại bạn trong thời gian sớm nhất',

                }).then((result) => {
                    location.reload();
                    if (result.value) {

                    }
                });
            }
        },
        error: function (e) {
            //console.log(e.message);
            Swal.fire(
                'Thất bại',
                'Có lỗi xảy ra, Vui lòng kiểm tra lại thông tin của bạn!',
                'error'
            );
        }
    });
}


function emailSubmit() {
    //debugger;
    var email = $("#emailSub").val();
    //debugger;
    //2 validate data
    if (email == '') {
        Swal.fire({
            icon: 'warning',
            title: 'Vui lòng nhập email của bạn',
            text: 'Bạn hãy nhập email để đăng ký nhận thông báo'
        })
        return;
    };
    if (ValidateEmail(email) == false) {
        Swal.fire({
            icon: 'error',
            title: 'Vui lòng kiểm tra lại định dạng email',
        })
        return;
    };
    var dt = new Date();
    var obj = {
        "Id": 0,
        "Name": $("#emailSub").val(),
        "Active": 1,
        "IsChecked": 0,
        "Email": $("#emailSub").val(),
        "Description": "email",
    };
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "Subscribe/api/Add",
        data: JSON.stringify(obj),
        success: function (responseData) {
            if (responseData.status == 201 && responseData.message === "CREATED") {
                Swal.fire({
                    icon: 'success',
                    title: 'Thành công!',
                    text: 'Gửi liên hệ thành công!',
                    footer: 'Lạc Việt Online đã nhận được liên hệ của các bạn',
                });
                setTimeout(function () {
                    window.location.reload();
                }, 4000);
            }
            if (responseData.status == 202 && responseData.message == "EMAILEXIST") {
                Swal.fire({
                    icon: 'error',
                    title: 'Email đã tồn tại!',
                    text: 'Email này đã được đăng ký trước đó!',
                    footer: 'Cảm ơn bạn đã đăng ký email tại Lạc Việt Online',
                    timer: 4000
                });
                setTimeout(function () {
                    $("#emailSub").val("");
                }, 6000);
            }
        },
        error: function (e) {
            console.log(e.message);
            Swal.fire(
                'Thất bại',
                'Lỗi không xác định, vui lòng thử lại',
                'error'
            );
        }
    });
}



var currentServerTimestampValue = currentServerDatetimeValue.getTime();
var interval_id;

$(window).focus(function () {
    //debugger;
    if (!interval_id) {
        interval_id = setInterval(function () {
            console.log("focus this page");
            loadServerDateTimeValue();
            //if (isBiddingNow == true) {
            //    CountDownEndBidding();
            //}
            clearInterval(interval_id);
            interval_id = 0;
        }, 100)
    }
});
$(window).blur(function () {
    clearInterval(interval_id);
    interval_id = 0;
});
$(document).ready(function () {
    setTimeout(function () { loadServerDateTimeValue() }, 2000)
    setTimeout(function () { loadServerDateTimeValue() }, 5000)
    setTimeout(function () { loadServerDateTimeValue() }, 10000)
    var intervalServerTime = setInterval(async function () {
        loadServerDateTimeValue();
        //if (isBiddingNow == true) {
        //    CountDownEndBidding();
        //}
        //Harry 17/9/20 - tạm thời giảm loop time từ 180s xuống 10s - 180000 --> 6000
        //Hiếu 18/11/20 - up to 60000s
    }, 60000)

    var interval = setInterval(function () {
        var momentNow = moment(currentServerTimestampValue);
        currentServerTimestampValue += 100;
        $('#date-part').html(momentNow.locale('vi-VI').format('DD/MM/YYYY'));
        $('#time-part').html(momentNow.locale('vi-VI').format('HH:mm:ss') + ' '
            + momentNow.locale('vi-VI').format('dddd').toUpperCase() + ',');
    }, 100);
});


function forceLogout() {
    Swal.fire(
        'Đăng xuất tự động',
        'Bạn đã đăng nhập từ một thiết bị/ trình duyệt khác và bị tự động đăng xuất khỏi thiết bị/ trình duyệt này.',
        'info'
    ).then(function () {
        window.location = "/";
    });
}


function searchFooter() {
    //debugger;
    var keyword = $("#searchKeywordFooter").val();
    keyword = keyword.split("/").join(" ");
    if (keyword != "") {
        var url = window.location.href;
        var arr = url.split("/");
        var currentUrl = arr[0] + "//" + arr[2]
        var searchUrl = "";
        searchUrl = currentUrl + "/Search?keyword=" + keyword;


        //window.location.replace(searchUrl);
        window.location.href = searchUrl;
    }
    else {
        Swal.fire({
            title: 'Bạn chưa nhập từ khóa tìm kiếm',
            text: "Vui lòng nhập từ khóa",
            icon: 'warning',
            showCancelButton: false,
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Đồng ý',

        }).then((result) => {
            if (result.value) {
                //window.location.reload();
            }
        })
    }

}



//Activity log
(function (window) {
    {
        var unknown = '-';

        // screen
        var screenSize = '';
        if (screen.width) {
            width = (screen.width) ? screen.width : '';
            height = (screen.height) ? screen.height : '';
            screenSize += '' + width + " x " + height;
        }

        // browser
        var nVer = navigator.appVersion;
        var nAgt = navigator.userAgent;
        var browser = navigator.appName;
        var version = '' + parseFloat(navigator.appVersion);
        var majorVersion = parseInt(navigator.appVersion, 10);
        var nameOffset, verOffset, ix;

        // Opera
        if ((verOffset = nAgt.indexOf('Opera')) != -1) {
            browser = 'Opera';
            version = nAgt.substring(verOffset + 6);
            if ((verOffset = nAgt.indexOf('Version')) != -1) {
                version = nAgt.substring(verOffset + 8);
            }
        }
        // Opera Next
        if ((verOffset = nAgt.indexOf('OPR')) != -1) {
            browser = 'Opera';
            version = nAgt.substring(verOffset + 4);
        }
        // Edge
        else if ((verOffset = nAgt.indexOf('Edge')) != -1) {
            browser = 'Microsoft Edge';
            version = nAgt.substring(verOffset + 5);
        }
        // MSIE
        else if ((verOffset = nAgt.indexOf('MSIE')) != -1) {
            browser = 'Microsoft Internet Explorer';
            version = nAgt.substring(verOffset + 5);
        }
        // Chrome
        else if ((verOffset = nAgt.indexOf('Chrome')) != -1) {
            browser = 'Chrome';
            version = nAgt.substring(verOffset + 7);
        }
        // Safari
        else if ((verOffset = nAgt.indexOf('Safari')) != -1) {
            browser = 'Safari';
            version = nAgt.substring(verOffset + 7);
            if ((verOffset = nAgt.indexOf('Version')) != -1) {
                version = nAgt.substring(verOffset + 8);
            }
        }
        // Firefox
        else if ((verOffset = nAgt.indexOf('Firefox')) != -1) {
            browser = 'Firefox';
            version = nAgt.substring(verOffset + 8);
        }
        // MSIE 11+
        else if (nAgt.indexOf('Trident/') != -1) {
            browser = 'Microsoft Internet Explorer';
            version = nAgt.substring(nAgt.indexOf('rv:') + 3);
        }
        // Other browsers
        else if ((nameOffset = nAgt.lastIndexOf(' ') + 1) < (verOffset = nAgt.lastIndexOf('/'))) {
            browser = nAgt.substring(nameOffset, verOffset);
            version = nAgt.substring(verOffset + 1);
            if (browser.toLowerCase() == browser.toUpperCase()) {
                browser = navigator.appName;
            }
        }
        // trim the version string
        if ((ix = version.indexOf(';')) != -1) version = version.substring(0, ix);
        if ((ix = version.indexOf(' ')) != -1) version = version.substring(0, ix);
        if ((ix = version.indexOf(')')) != -1) version = version.substring(0, ix);

        majorVersion = parseInt('' + version, 10);
        if (isNaN(majorVersion)) {
            version = '' + parseFloat(navigator.appVersion);
            majorVersion = parseInt(navigator.appVersion, 10);
        }

        // mobile version
        var mobile = /Mobile|mini|Fennec|Android|iP(ad|od|hone)/.test(nVer);

        // cookie
        var cookieEnabled = (navigator.cookieEnabled) ? true : false;

        if (typeof navigator.cookieEnabled == 'undefined' && !cookieEnabled) {
            document.cookie = 'testcookie';
            cookieEnabled = (document.cookie.indexOf('testcookie') != -1) ? true : false;
        }

        // system
        var os = unknown;
        var clientStrings = [
            { s: 'Windows 10', r: /(Windows 10.0|Windows NT 10.0)/ },
            { s: 'Windows 8.1', r: /(Windows 8.1|Windows NT 6.3)/ },
            { s: 'Windows 8', r: /(Windows 8|Windows NT 6.2)/ },
            { s: 'Windows 7', r: /(Windows 7|Windows NT 6.1)/ },
            { s: 'Windows Vista', r: /Windows NT 6.0/ },
            { s: 'Windows Server 2003', r: /Windows NT 5.2/ },
            { s: 'Windows XP', r: /(Windows NT 5.1|Windows XP)/ },
            { s: 'Windows 2000', r: /(Windows NT 5.0|Windows 2000)/ },
            { s: 'Windows ME', r: /(Win 9x 4.90|Windows ME)/ },
            { s: 'Windows 98', r: /(Windows 98|Win98)/ },
            { s: 'Windows 95', r: /(Windows 95|Win95|Windows_95)/ },
            { s: 'Windows NT 4.0', r: /(Windows NT 4.0|WinNT4.0|WinNT|Windows NT)/ },
            { s: 'Windows CE', r: /Windows CE/ },
            { s: 'Windows 3.11', r: /Win16/ },
            { s: 'Android', r: /Android/ },
            { s: 'Open BSD', r: /OpenBSD/ },
            { s: 'Sun OS', r: /SunOS/ },
            { s: 'Chrome OS', r: /CrOS/ },
            { s: 'Linux', r: /(Linux|X11(?!.*CrOS))/ },
            { s: 'iOS', r: /(iPhone|iPad|iPod)/ },
            { s: 'Mac OS X', r: /Mac OS X/ },
            { s: 'Mac OS', r: /(MacPPC|MacIntel|Mac_PowerPC|Macintosh)/ },
            { s: 'QNX', r: /QNX/ },
            { s: 'UNIX', r: /UNIX/ },
            { s: 'BeOS', r: /BeOS/ },
            { s: 'OS/2', r: /OS\/2/ },
            { s: 'Search Bot', r: /(nuhk|Googlebot|Yammybot|Openbot|Slurp|MSNBot|Ask Jeeves\/Teoma|ia_archiver)/ }
        ];
        for (var id in clientStrings) {
            var cs = clientStrings[id];
            if (cs.r.test(nAgt)) {
                os = cs.s;
                break;
            }
        }

        var osVersion = unknown;

        if (/Windows/.test(os)) {
            osVersion = /Windows (.*)/.exec(os)[1];
            os = 'Windows';
        }

        switch (os) {
            case 'Mac OS X':
                osVersion = /Mac OS X (10[\.\_\d]+)/.exec(nAgt)[1];
                break;

            case 'Android':
                osVersion = /Android ([\.\_\d]+)/.exec(nAgt)[1];
                break;

            case 'iOS':
                osVersion = /OS (\d+)_(\d+)_?(\d+)?/.exec(nVer);
                osVersion = osVersion[1] + '.' + osVersion[2] + '.' + (osVersion[3] | 0);
                break;
        }

        // flash (you'll need to include swfobject)
        /* script src="//ajax.googleapis.com/ajax/libs/swfobject/2.2/swfobject.js" */
        var flashVersion = 'no check';
        if (typeof swfobject != 'undefined') {
            var fv = swfobject.getFlashPlayerVersion();
            if (fv.major > 0) {
                flashVersion = fv.major + '.' + fv.minor + ' r' + fv.release;
            }
            else {
                flashVersion = unknown;
            }
        }
    }

    window.jscd = {
        screen: screenSize,
        browser: browser,
        browserVersion: version,
        browserMajorVersion: majorVersion,
        mobile: mobile,
        os: os,
        osVersion: osVersion,
        cookies: cookieEnabled,
        flashVersion: flashVersion
    };


    var clientInfo = 'OS: ' + jscd.os + ' ' + jscd.osVersion + '\n' +
        'Browser: ' + jscd.browser + ' ' + jscd.browserMajorVersion +
        ' (' + jscd.browserVersion + ')\n' +
        'Mobile: ' + jscd.mobile + '\n' +
        'Flash: ' + jscd.flashVersion + '\n' +
        'Cookies: ' + jscd.cookies + '\n' +
        'Screen Size: ' + jscd.screen + '\n\n' +
        'Full User Agent: ' + navigator.userAgent;
    var url = window.location.href;
    var urlSource = document.referrer;
    var updatingObj = {
        "active": "1",
        "accountId": 0,
        "guId": "Log",
        "name": "Log",
        "entityCode": "BROWSING",
        "info": clientInfo,
        "objectOld": "",
        "objectNew": "",
        "url": url,
        "urlSource": urlSource,
        "ipAddress": "Log",
        "device": jscd.mobile ? "MOBILE" : "PC",
        "browser": jscd.browser + "-" + jscd.browserMajorVersion,
        "os": jscd.os + "-" + jscd.osVersion,
        "userAgent": navigator.userAgent,
        "description": "Log",
        "createdTime": "2018-12-10 00:00:00"
    };

    updatingObj.id = 1;
    updatingObj.active = 1;
    //updatingObj.createdTime = new Date();
    //console.log(updatingObj);
    $.ajax({
        url: systemURL + "activityLog/api/add",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(updatingObj),
        success: function (responseData) {
            // debugger;
            if (responseData.status == 201 && responseData.message === "CREATED") {
                console.log("data log success");
            }
        },
        error: function (e) {
            console.log(e.message);
            console.log("data log failed");
        }
    });

}(this));


//Migrated from _Homepage_ContactUs ends

//Migrated from _Javascript  

(function () {
    function addEventListener(element, event, handler) {
        if (element.addEventListener) {
            element.addEventListener(event, handler, false);
        } else if (element.attachEvent) {
            element.attachEvent('on' + event, handler);
        }
    } function maybePrefixUrlField() {
        if (this.value.trim() !== '' && this.value.indexOf('http') !== 0) {
            this.value = "http://" + this.value;
        }
    }

    var urlFields = document.querySelectorAll('.mc4wp-form input[type="url"]');
    if (urlFields && urlFields.length > 0) {
        for (var j = 0; j < urlFields.length; j++) {
            addEventListener(urlFields[j], 'blur', maybePrefixUrlField);
        }
    }/* test if browser supports date fields */
    var testInput = document.createElement('input');
    testInput.setAttribute('type', 'date');
    if (testInput.type !== 'date') {

        /* add placeholder & pattern to all date fields */
        var dateFields = document.querySelectorAll('.mc4wp-form input[type="date"]');
        for (var i = 0; i < dateFields.length; i++) {
            if (!dateFields[i].placeholder) {
                dateFields[i].placeholder = 'YYYY-MM-DD';
            }
            if (!dateFields[i].pattern) {
                dateFields[i].pattern = '[0-9]{4}-(0[1-9]|1[012])-(0[1-9]|1[0-9]|2[0-9]|3[01])';
            }
        }
    }

})();


if (typeof revslider_showDoubleJqueryError === "undefined") {
    function revslider_showDoubleJqueryError(sliderID) {
        var err = "<div class='rs_error_message_box'>";
        err += "<div class='rs_error_message_oops'>Oops...</div>";
        err += "<div class='rs_error_message_content'>";
        err += "You have some jquery.js library include that comes after the Slider Revolution files js inclusion.<br>";
        err += "To fix this, you can:<br>&nbsp;&nbsp;&nbsp; 1. Set 'Module General Options' -> 'Advanced' -> 'jQuery & OutPut Filters' -> 'Put JS to Body' to on";
        err += "<br>&nbsp;&nbsp;&nbsp; 2. Find the double jQuery.js inclusion and remove it";
        err += "</div>";
        err += "</div>";
        jQuery(sliderID).show().html(err);
    }
}

var yith_wcwl_l10n = { "ajax_url": "\/real-estate\/wp-admin\/admin-ajax.php", "redirect_to_cart": "no", "multi_wishlist": "", "hide_add_button": "1", "is_user_logged_in": "", "ajax_loader_url": "\//wp-content\/plugins\/yith-woocommerce-wishlist\/assets\/images\/ajax-loader.gif", "remove_from_wishlist_after_add_to_cart": "yes", "labels": { "cookie_disabled": "We are sorry, but this feature is available only if cookies are enabled on your browser.", "added_to_cart_message": "<div class=\"woocommerce-message\">Product correctly added to cart<\/div>" }, "actions": { "add_to_wishlist_action": "add_to_wishlist", "remove_from_wishlist_action": "remove_from_wishlist", "move_to_another_wishlist_action": "move_to_another_wishlsit", "reload_wishlist_and_adding_elem_action": "reload_wishlist_and_adding_elem" } };
    /* ]]> */

/* <![CDATA[ */
var lvca_settings = { "mobile_width": "780", "custom_css": "" };
/* ]]> */

/* <![CDATA[ */
var wpcf7 = { "apiSettings": { "root": "\/wp-json\/contact-form-7\/v1", "namespace": "contact-form-7\/v1" } };
    /* ]]>
     * 
     *   /* <![CDATA[ */
var woocommerce_params = { "ajax_url": "\/real-estate\/wp-admin\/admin-ajax.php", "wc_ajax_url": "\/real-estate\/?wc-ajax=%%endpoint%%" };
    /* ]]> */

/* <![CDATA[ */
var wc_cart_fragments_params = { "ajax_url": "\/real-estate\/wp-admin\/admin-ajax.php", "wc_ajax_url": "\/real-estate\/?wc-ajax=%%endpoint%%", "cart_hash_key": "wc_cart_hash_5ef5e66dc9c93e9015aefd59a759c5db", "fragment_name": "wc_fragments_5ef5e66dc9c93e9015aefd59a759c5db", "request_timeout": "5000" };
    /* ]]> *
    /* <![CDATA[ */
var yith_qv = { "ajaxurl": "\/real-estate\/wp-admin\/admin-ajax.php", "loader": "\//wp-content\/plugins\/yith-woocommerce-quick-view\/assets\/image\/qv-loader.gif", "is2_2": "", "lang": "" };
/* ]]> */

jQuery(document).ready(function (jQuery) { jQuery.datepicker.setDefaults({ "closeText": "Close", "currentText": "Today", "monthNames": ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"], "monthNamesShort": ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"], "nextText": "Next", "prevText": "Previous", "dayNames": ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"], "dayNamesShort": ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"], "dayNamesMin": ["S", "M", "T", "W", "T", "F", "S"], "dateFormat": "MM d, yy", "firstDay": 1, "isRTL": false }); });

/* <![CDATA[ */
var mwb_fmlcrt_messenger_script_obj = { "language": "en_US", "fb_app_id": "" };
    /* ]]> *
     * 
     * 
     /* <![CDATA[ */
var _wpUtilSettings = { "ajax": { "url": "\/real-estate\/wp-admin\/admin-ajax.php" } };
/* ]]> */

/* <![CDATA[ */
var wc_add_to_cart_variation_params = { "wc_ajax_url": "\/real-estate\/?wc-ajax=%%endpoint%%", "i18n_no_matching_variations_text": "Sorry, no products matched your selection. Please choose a different combination.", "i18n_make_a_selection_text": "Please select some product options before adding this product to your cart.", "i18n_unavailable_text": "Sorry, this product is unavailable. Please choose a different combination." };
/* ]]> */

/* <![CDATA[ */
var wc_single_product_params = { "i18n_required_rating_text": "Please select a rating", "review_rating_required": "yes", "flexslider": { "rtl": false, "animation": "slide", "smoothHeight": true, "directionNav": false, "controlNav": "thumbnails", "slideshow": false, "animationSpeed": 500, "animationLoop": false, "allowOneSlide": false }, "zoom_enabled": "1", "zoom_options": [], "photoswipe_enabled": "1", "photoswipe_options": { "shareEl": false, "closeOnScroll": false, "history": false, "hideAnimationDuration": 0, "showAnimationDuration": 0 }, "flexslider_enabled": "" };
/* ]]> */

/* <![CDATA[ */
var mc4wp_forms_config = [];
/* ]]> */

//Migrated from _Javascript ends



