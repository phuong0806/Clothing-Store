$(document).ready(function () {
    var donHangController = {

        init: function () {
            donHangController.loadData();
            donHangController.registerEvents();
        },

        registerEvents: function () {

        },

        loadData: function () {
            $.ajax({
                url: "/Admin/DonHang/loadDonHang",
                type: "GET",
                dataType: "json",
                success: function (response) {
                    var html = '';
                    var data = response.data;
                    var template = $('#data-template').html();
                    //Load ra từng sản phẩm
                    $.each(data, function (i, item) {
                        var tinhTrang = '';
                        if (item.TinhTrang == 1 && item.XacNhan == true) {
                            tinhTrang = "Đơn hàng mới";
                        } else if (item.TinhTrang == 2 && item.XacNhan == true) {
                            tinhTrang = "Đang xử lý";
                        } else if (item.TinhTrang == 3 && item.XacNhan == true) {
                            tinhTrang = "Đang vận chuyển";
                        } else if (item.TinhTrang == 4 && item.XacNhan == true) {
                            tinhTrang = "Hoàn tất";
                        } else {
                            tinhTrang = "Chưa xác thực";
                        }

                        html += Mustache.render(template, {
                            DonHang: item.ID,
                            NgayTao: (item.NgayTao),
                            NgayDuyet: toDateTime(item.NgayDuyet),
                            NgayHoanThanh: js_yyyy_mm_dd_hh_mm_ss(item.NgayHoanThanh),
                            XacNhan: item.XacNhan == true ? "Chưa xác thực" : "Đã xác thực",
                            TinhTrang: tinhTrang,
                            TongTien: format_money(item.TongTien)
                        });
                    });
                    $('#result-data').html(html);
                },
                error: function () {
                    alert("error load");
                }
            })
        },
    }
    donHangController.init();
});