function getTimestampFromDBTime(databaseTime) {
    //input data chuẩn: 2020-05-15T15:26:50.842 hoặc  2020-05-15T15:26:50
    let inputLength = databaseTime.length;
    if (inputLength == 22) databaseTime = databaseTime + "0";
    if (inputLength == 21) databaseTime = databaseTime + "00";
    if (inputLength == 19) databaseTime = databaseTime + ".000";

    let dbYearValue = databaseTime.substring(0, 4);
    let dbMonthValue = databaseTime.substring(5, 7);
    let dbDateValue = databaseTime.substring(8, 10);
    let dbHourValue = databaseTime.substring(11, 13);
    let dbMinuteValue = databaseTime.substring(14, 16);
    let dbSecondValue = databaseTime.substring(17, 19);
    let dbMilisecondsValue = databaseTime.substring(20, databaseTime.length);

    let dateConvertedFromDb = new Date();
    dateConvertedFromDb.setYear(dbYearValue);
    dateConvertedFromDb.setMonth(dbMonthValue - 1);
    dateConvertedFromDb.setDate(dbDateValue);
    dateConvertedFromDb.setHours(dbHourValue);
    dateConvertedFromDb.setMinutes(dbMinuteValue);
    dateConvertedFromDb.setSeconds(dbSecondValue);
    dateConvertedFromDb.setMilliseconds(dbMilisecondsValue);

    return dateConvertedFromDb.getTime();
}

function formatDisplayTimeFromDBTime(databaseTime) {
    //input data chuẩn: 2020-05-15T15:26:50.842 hoặc  2020-05-15T15:26:50
    let inputLength = databaseTime.length;
    if (inputLength == 22) databaseTime = databaseTime + "0";
    if (inputLength == 21) databaseTime = databaseTime + "00";
    if (inputLength == 19) databaseTime = databaseTime + ".000";

    let dbYearValue = databaseTime.substring(0, 4);
    let dbMonthValue = databaseTime.substring(5, 7);
    let dbDateValue = databaseTime.substring(8, 10);
    let dbHourValue = databaseTime.substring(11, 13);
    let dbMinuteValue = databaseTime.substring(14, 16);
    let dbSecondValue = databaseTime.substring(17, 19);
    let dbMilisecondsValue = databaseTime.substring(20, inputLength > 23 ? 23 : databaseTime.length);

    return dbDateValue + "/" + dbMonthValue + "/" + dbYearValue + " " + dbHourValue + ":" + dbMinuteValue + ":" + dbSecondValue;
}

function formatDisplayTimeWithMilisecondsFromDBTime(databaseTime) {
    //input data chuẩn: 2020-05-15T15:26:50.842 hoặc  2020-05-15T15:26:50
    let inputLength = databaseTime.length;
    if (inputLength == 22) databaseTime = databaseTime + "0";
    if (inputLength == 21) databaseTime = databaseTime + "00";
    if (inputLength == 19) databaseTime = databaseTime + ".000";

    let dbYearValue = databaseTime.substring(0, 4);
    let dbMonthValue = databaseTime.substring(5, 7);
    let dbDateValue = databaseTime.substring(8, 10);
    let dbHourValue = databaseTime.substring(11, 13);
    let dbMinuteValue = databaseTime.substring(14, 16);
    let dbSecondValue = databaseTime.substring(17, 19);
    let dbMilisecondsValue = databaseTime.substring(20, inputLength > 23 ? 23 : databaseTime.length);

    return dbDateValue + "/" + dbMonthValue + "/" + dbYearValue + " " + dbHourValue + ":" + dbMinuteValue + ":" + dbSecondValue + "." + dbMilisecondsValue;
}

function formatDisplayTimeWithMicrosecondsFromDBTime(databaseTime) {
    //input data chuẩn: 2020-05-15T15:26:50.842 hoặc  2020-05-15T15:26:50
    let inputLength = databaseTime.length;
    if (inputLength == 22) databaseTime = databaseTime + "0";
    if (inputLength == 21) databaseTime = databaseTime + "00";
    if (inputLength == 19) databaseTime = databaseTime + ".000";

    let dbYearValue = databaseTime.substring(0, 4);
    let dbMonthValue = databaseTime.substring(5, 7);
    let dbDateValue = databaseTime.substring(8, 10);
    let dbHourValue = databaseTime.substring(11, 13);
    let dbMinuteValue = databaseTime.substring(14, 16);
    let dbSecondValue = databaseTime.substring(17, 19);
    //let dbMilisecondsValue = databaseTime.substring(20, inputLength > 23 ? 23 : databaseTime.length);//update allow microseconds
    let dbMicrosecondsValue = databaseTime.substring(20, databaseTime.length);

    return dbDateValue + "/" + dbMonthValue + "/" + dbYearValue + " " + dbHourValue + ":" + dbMinuteValue + ":" + dbSecondValue + "." + dbMicrosecondsValue;
}

/* CONVERT NUMBER TO TEXT */
var mangso = ['không', 'một', 'hai', 'ba', 'bốn', 'năm', 'sáu', 'bảy', 'tám', 'chín'];
function dochangchuc(so, daydu) {
    var chuoi = "";
    chuc = Math.floor(so / 10);
    donvi = so % 10;
    if (chuc > 1) {
        chuoi = " " + mangso[chuc] + " mươi";
        if (donvi == 1) {
            chuoi += " mốt";
        }
    } else if (chuc == 1) {
        chuoi = " mười";
        if (donvi == 1) {
            chuoi += " một";
        }
    } else if (daydu && donvi > 0) {
        chuoi = " lẻ";
    }
    if (donvi == 5 && chuc > 0) {
        chuoi += " lăm";
    } else if (donvi > 1 || (donvi == 1 && chuc == 0)) {
        chuoi += " " + mangso[donvi];
    }
    return chuoi;
}
function dochangtram(so, daydu) {
    var chuoi = "";
    tram = Math.floor(so / 100);
    so = so % 100;
    if (daydu || tram > 0) {
        chuoi = " " + mangso[tram] + " trăm";
        chuoi += dochangchuc(so, true)
    } else {
        chuoi = dochangchuc(so, false)
    }
    return chuoi;
}
function dochangnghin(so, daydu) {
    var chuoi = "";
    nghin = Math.floor(so / 1000);
    so = so % 1000;
    if (nghin > 0) {
        chuoi += dochangtram(nghin, daydu) + " nghìn";
        daydu = true;
    }
    if (so > 0) {
        chuoi += dochangtram(so, daydu);
    }
    return chuoi;
}
function dochangtrieu(so, daydu) {
    var chuoi = "";
    trieu = Math.floor(so / 1000000);
    so = so % 1000000;
    if (trieu > 0) {
        chuoi += dochangnghin(trieu, daydu) + " triệu";
        daydu = true;
    }
    if (so > 0) {
        chuoi += dochangnghin(so, daydu);
    }
    return chuoi;
}
function convertMoneyNumberToText(so) {
    so = so.replace(/[^0-9]/g, '');
    if (so == 0) return mangso[0];
    var chuoi = "";
    var value = "";
    do {
        ty = so % 1000000000;
        so = Math.floor(so / 1000000000);
        if (so > 0) {
            chuoi = dochangtrieu(ty, true) + value + chuoi;

        } else {
            chuoi = dochangtrieu(ty, false) + value + chuoi;
        }
        var value = " tỷ";
    } while (so > 0) {
        chuoi = chuoi.trim();
        chuoi = chuoi.charAt(0).toUpperCase() + chuoi.slice(1);
        return chuoi + " đồng";
    }

}
/* CONVERT NUMBER TO TEXT */

function generateRandomString(length) {
    var result = '';
    var characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789';
    var charactersLength = characters.length;
    for (var i = 0; i < length; i++) {
        result += characters.charAt(Math.floor(Math.random() * charactersLength));
    }
    return result;
}

function formatNumber() {
    $('.column-price , .column-openingPrice , .column-value , .novaticPrice , .column-amount, .column-available, .column-balance1 , .column-biddingValue, .column-stepPrice, #realTimeHighestPrice, .realTimeHighestPrice, #openingPrice-value, #stepPrice-value, #highestPrice').each(function (event) {
        // format number
        $(this).text(function (index, value) {
            return value
                .replace(/\D/g, '')
                .replace(/\B(?=(\d{3})+(?!\d))/g, '.');
        });
    });

    $('.input-price,.input-depositPrice,.input-openingPrice,.input-buyPrice,.input-stepPrice , .input-value, .input-novaticPrice , .input-balance, .input-available, .input-auctionPropertyRegisterFee, .input-registerFee').each(function (event) {
        $(this).attr('type', 'text');

        // format number
        $(this).val(function (index, value) {
            return value
                .replace(/\D/g, '')
                .replace(/\B(?=(\d{3})+(?!\d))/g, '.');
        });
    });

    $('.column-createdTime , .column-publishedTime , .column-openTime , .column-closedTime, .column-OpenTime, .column-ClosedTime, .column-biddingCreatedTime,#closedTime-value').each(function (event) {
        //debugger;
        var currentValue = $(this).text();
        if (!currentValue.includes("T")) return;
        var date = new Date(currentValue);
        var newValue = date.toLocaleDateString() + " " + date.toLocaleTimeString();
        //$(this).text(newValue);
        $(this).text(formatDisplayTimeFromDBTime(currentValue));
    });

    // $('.column-createdTime').each(function (event) {
    //    //debugger;
    //    var currentValue = $(this).text();
    //    $(this).text(datetimeFormatVietNam(currentValue));
    //});


    $('.input-createdTime , .input-publishedTime').each(function (event) {
        //debugger;
        var inputObj = $(this).find("input");

        var currentValue = inputObj.val();
        if (!currentValue.includes("T")) return;
        var date = new Date(currentValue);
        var newValue = date.toLocaleDateString() + " " + date.toLocaleTimeString();
        inputObj.val(newValue);
    });

}

//$(document).ready(function () {
//    //Tạm thời cập nhật giao diện:

//    var currentURL = window.location.href;
//    //Trang Phiên đấu giá -> Ẩn tài liệu:
//    if (currentURL.includes("/auction/admin/list")) {
//        $(".mediaPanelContainer").hide();
//    }

//    //Trang Tài sản đấu giá -> Ẩn tài liệu:
//    if (currentURL.includes("/auctionProperty/admin/list")) {
//        $(".mediaPanelContainer").hide();
//    }


//});