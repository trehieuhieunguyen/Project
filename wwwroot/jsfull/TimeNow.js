function getDateTime() {
    var today = new Date(); //lay ngay gio hien tai

    // Lấy số thứ tự của ngày hiện tại
    var current_day = today.getDay();
    var day_name = '';
    // Lấy tên thứ của ngày hiện tại
    switch (current_day) {
        case 0:
            day_name = "CHỦ NHẬT";
            break;
        case 1:
            day_name = "THỨ HAI";
            break;
        case 2:
            day_name = "THỨ BA";
            break;
        case 3:
            day_name = "THỨ TƯ";
            break;
        case 4:
            day_name = "THỨ NĂM";
            break;
        case 5:
            day_name = "THỨ SÁU";
            break;
        case 6:
            day_name = "THỨ BẢY";
    }
    var DateTime = today.toLocaleTimeString("vi-Vn",
        {
            hour: "numeric",
            minute: "numeric",
            second: "numeric"
        });
    var DateY = ('0' + today.getDate()).slice(-2) + '/' + ('0' + (today.getMonth() + 1)).slice(-2) + '/' + today.getFullYear();
    //truy cap vao id time, dan text thongtin
    var dateTime = DateTime + "   " + day_name + "," + "   " + DateY;
    return dateTime;
}


setInterval(function () {
    currentTime = getDateTime();
    $("#time").html(currentTime);
}, 1000);
