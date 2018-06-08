$(document).ready(function () {
    var thanhToanController = {

        init: function () {
            thanhToanController.loadData();
            thanhToanController.registerEvents();
        },

        registerEvents: function () {
            $(document).on('click', '.btn-xacnhan', function () {
                thanhToanController.order();
            });
        },

        //function 
        //Đặt hàng
        order: function () {
            var listProduct = JSON.stringify(JSON.parse(localStorage.getItem("cart")));
            var orderDetail = {
                TenKH: $('#hoten').val(),
                SDT: $('#dienthoai').val(),
                DiaChiGiaoHang: $('#diachi').val(),
                Email: $('#email').val(),
                TongTien: Store.returnTotalCostOfProduct()
            };

            $.ajax({
                url: "/ThanhToan/DatHang",
                data: {
                    listProduct: listProduct,
                    orderDetail: JSON.stringify(orderDetail)
                },
                type: "POST",
                dataType: "json",
                success: function (response) {
                    if (response.status == true) {
                        alert("Đặt hàng thành công, chúng tôi sẽ liên hệ bạn trong thời gian sớm nhất");
                        //Xóa sản phẩm trong giỏ hàng
                        Store.removeCart();
                        window.location.href = "/";
                    } else {
                        alert("Hệ thống lỗi vui lòng thử lại sau!");
                    }
                }
            });
        },

        //Lấy danh sách sản phẩm trong giỏ hàng khi load trang
        loadData: function () {
            $.ajax({
                url: '/ThanhToan/loadDonHang',
                data: {
                    listCartString: JSON.stringify(JSON.parse(localStorage.getItem("cart")))
                },
                type: "POST",
                dataType: "json",
                success: function (response) {
                    var html = '';
                    var data = JSON.parse(response.data);
                    var template = $('#data-template').html();
                    //Load ra từng sản phẩm
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            ID: item.ID,
                            TenSanPham: item.TenSanPham,
                            KichCo: item.SizeString,
                            Mau: item.MauString,
                            SoLuong: item.Amount,
                            Gia: format_money(item.Gia),
                            HinhAnh: item.HinhAnh,
                            TongTien: format_money(item.TongTien),
                        });
                    });
                    $('#result-data').append(html);
                    Store.loadDetail();
                    Store.totalCost();
                }
            });
        },
    }
    thanhToanController.init();
});