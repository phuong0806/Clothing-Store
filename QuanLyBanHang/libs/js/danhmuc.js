$(document).ready(function () {
    $('.btn-save').click(function () {
        var id = $('#id').val();
        var TenLoai = $('#tenloai').val();

        var loaisanpham = {
            id: id,
            tenloai: tenloai
        }

        $.ajax({
            url: '/Admin/DanhMuc/Save',
            data: {
                loaisanpham : loaisanpham
            },
            type: "POST",
            dataType: "json",
            success: function (response) {
                if (response.status) {
                    $.notify("Thành công", "success");
                }
                else {
                    $.notify("Thất bại", "error");
                }
            }
        });
    });
});