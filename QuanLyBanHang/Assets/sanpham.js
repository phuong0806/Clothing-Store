$(document).ready(function () {
    var sanphamController = {
        init: function () {
            sanphamController.loadMoreImages();
            sanphamController.registerEvents();
        },

        registerEvents: function () {

        },

        loadMoreImages: function () {
            $.ajax({
                url: "/Admin/SanPham/loadMoreImages",
                type: "GET",
                data: {
                    id: SanPhamID
                },
                dataType: "json",
                success: function (response) {
                    var data = response.data;
                    var html = '';
                    $.each(data, function (i, item) {
                        html += `<div class="image"><img src="${item}" class="more-img" /><a href="#" class="remove-image">×</a></div>`;
                    })
                    $('#result-images').append(html);
                }
            });
        },
    }
    sanphamController.init();
});