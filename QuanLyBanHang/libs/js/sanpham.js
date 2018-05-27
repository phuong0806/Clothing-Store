$(document).ready(function () {
    function resetModal() {
        $('#tensanpham').val("");
        $('#hinhanh').val("/libs/Image/logo/add.png");
        $('#gia').val("");
        $('#maloai').val("selected");
        $('#thuonghieu').val("selected");
        $('#mau').val("");
        $('#kichco').val("");
        $('#id').val("0");
        CKEDITOR.instances['fullDescription'].setData("");
    }

    $('.btn-save').click(function () {
        var sanpham = {
            TenSanPham: $('#tensanpham').val(),
            HinhAnh: $('#hinhanh').attr('src'),
            Gia: $('#gia').val(),
            MaLoai: $('#maloai').val(),
            MaThuongHieu: $('#thuonghieu').val(),
            Mau: $('#mau').val(),
            KichCo: $('#kichco').val(),
            ID: $('#id').val(),
            Mota: CKEDITOR.instances['fullDescription'].getData()
        }

        $.ajax({
            url: "/Admin/SanPham/Save",
            data: function () {
                    var data = new FormData();
                    data.append("SanPham", JSON.stringify(sanpham));
                    data.append("file", $("#upload").get(0).files[0]);
                    return data;
                }(),
            contentType: false,
            processData: false,
            dataType: "json",
            type: "POST",
            success: function (response) {
                if(response.status) {
                    $.notify("Thành công", "success");
                    resetModal();
                }
                else {
                    $.notify("Thất bại", "error");
                    resetModal();
                }
            }
        })
    });

    $('.btn-delete').click(function () {
        var id = $(this).data('id');
        if (confirm("Bạn có chắc xóa không?")) {
            $.ajax({
                url: "/Admin/SanPham/Xoa",
                data: {
                    id: id
                },
                type: "POST",
                dataType: "json",
                success: function (response) {
                    if (response.status) {
                        $.notify("Thành công", "success");
                        setTimeout(function myfunction() {
                            window.location.reload();
                        }, 1000);

                    } else {
                        $.notify("Thất bại", "error");
                    }

                }
            });
        }
    });

    $('.btn-add').click(function () {
        resetModal();
    });

    $('.btn-edit').click(function () {
        var idSanPham = $(this).data('id');
        $.ajax({
            url: '/Admin/SanPham/layChiTietSanPham',
            data: {
                id: idSanPham
            },
            type: "GET",
            datatype: "json",
            success: function (result) {
                var data = JSON.parse(result.data);
                $('#tensanpham').val(data.TenSanPham);
                $('#hinhanh').attr('src',data.HinhAnh);
                $('#gia').val(data.Gia);
                $('#maloai').val(data.MaLoai);
                $('#thuonghieu').val(data.MaThuongHieu);
                $('#mau').val(data.Mau);
                $('#kichco').val(data.KichCo);
                $('#id').val(data.ID);
                CKEDITOR.instances['fullDescription'].setData(data.MoTa);
            }
        })
    });
});