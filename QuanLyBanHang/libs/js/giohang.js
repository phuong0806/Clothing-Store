$(document).ready(function () {
    var gioHangController = {

        init: function () {
            gioHangController.loadData();
            gioHangController.registerEvents();
        },

        registerEvents: function () {
            //Khi click btn thì xóa sản phẩm
            $(document).on('click', '.del-sanpham', function () {
                var id = $(this).data('id');
                Store.removeProduct(id);
                gioHangController.loadData();
            });

            $(document).on('change', '.mau-sel', function () {
                var SanPhamID = $(this).data('id');
                var MauID = parseInt($(this).val());
                Store.setColor(SanPhamID, MauID);
            });

            $(document).on('change', '.kichco-sel', function () {
                var SanPhamID = $(this).data('id');
                var KichCoID = parseInt($(this).val());
                Store.setSize(SanPhamID, KichCoID);
            });

            $(document).on('change', '.amount-sel', function () {
                var SanPhamID = $(this).data('id');
                var amount = parseInt($(this).val());
                Store.setAmount(SanPhamID, amount);
            });
        },

        //function 
        //Lấy danh sách sản phẩm trong giỏ hàng khi load trang
        loadData: function () {
            $.ajax({
                url: '/GioHang/loadGioHang',
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
                        var arrayAmount = [];
                        for (var i = 1; i <= item.SoLuong; i++) {
                            arrayAmount.push({
                                value: i
                            });
                        }
                        html += Mustache.render(template, {
                            ID: item.ID,
                            TenSanPham: item.TenSanPham,
                            KichCo: item.KichCo,
                            Mau: item.Mau,
                            SoLuong: arrayAmount,
                            Gia: format_money(item.Gia),
                            HinhAnh: item.HinhAnh
                        });
                    });
                    $('#result-data').append(html);
                    Store.loadDetail();
                }
            });
        },
    }
    gioHangController.init();
});