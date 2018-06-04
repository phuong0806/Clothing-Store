$(document).ready(function () {
    $('#mau').chosen();
    $('#kichco').chosen();
    $.validator.setDefaults({ ignore: ":hidden:not(select)" })
    CKEDITOR.replace("fullDescription");
    function loadLoaiSanPhamTheoID(id) {
        $.ajax({
            url: "/Admin/SanPham/LayLoaiSanPham",
            type: "POST",
            data: { DanhMucID: id },
            dataType: "json",
            ContentType : "application/json",
            success: function (response) {
                var html = '<option disabled>--Chọn loại sản phẩm--</option>';
                var data = JSON.parse(response.data);

                $.each(data, function (i, item) {
                    html += '<option value="' + item.ID + '">' + item.TenLoai + '</option>'

                });
                $('#maloai').html(html);
            }
        });
    }

    $('#tensanpham').on('blur', function () {
        var url = $('#urlsanpham').val();
        var id = $('#id').val();
        if (url != "") {
            $.ajax({
                url: "/Admin/SanPham/kiemTraUrlTonTai",
                data: {
                    url: url,
                    id : id
                },
                type: "POST",
                dataType: "json",
                success: function (response) {
                    if (response.status) {
                        $('span.error').text("Url đã tồn tại");
                    } else {
                        $('span.error').text("");
                    }
                }
            });
        }
        else {
            $('span.error').text("");
        }
    });

    $('#danhmuc').off('change').on('change', function () {
        var id = $(this).val();
        if (id != '') {
            loadLoaiSanPhamTheoID(id);
        }
        else {
            $('#ddlDistrict').html('');
        }
    });

    // Kiểm tra đã nhập chưa
    $('#form-save').validate({
        rules: {
            TenSanPham: "required",
            gia: "required",
            maloai: "required",
            MaThuongHieu: "required",
            Mau: "required",
            KichCo: "required",
            MoTa: "required",
            SoLuong: "required"
        },
        messages: {
            TenSanPham: "Bạn phải nhập trường này",
            gia: "Bạn phải nhập trường này",
            maloai: "Bạn phải nhập trường này",
            MaThuongHieu: "Bạn phải nhập trường này",
            Mau: "Bạn phải nhập trường này",
            KichCo: "Bạn phải nhập trường này",
            MoTa: "Bạn phải nhập trường này",
            SoLuong: "Bạn phải nhập trường này"
        },
    });

    //reset modal
    function resetModal() {
        $('#tensanpham').val("");
        $('#hinhanh').attr('src',"/libs/Image/logo/add.png");
        $('#gia').val("");
        $('#danhmuc').val("selected");
        $('#thuonghieu').val("selected");
        $('#mau').val("");
        $('#urlsanpham').val("");
        $('#kichco').val("");
        $('#id').val("0");
        $('#SoLuong').val("");
        $("#mau").trigger("chosen:updated");
        $("#kichco").trigger("chosen:updated");
        $('#maloai').html('<option disabled selected>--Chọn loại sản phẩm--</option>');
    }

    // Thêm và Cập nhật sản phẩm
    $('.btn-save').click(function () {
        if ($('#form-save').valid()) {
            var sanpham = {
                TenSanPham: $('#tensanpham').val(),
                HinhAnh: $('#hinhanh').attr('src'),
                Gia: $('#gia').val(),
                MaLoai: $('#maloai').val(),
                MaThuongHieu: $('#thuonghieu').val(),
                Mau: $('#mau').val(),
                KichCo: $('#kichco').val(),
                SoLuong: $('#SoLuong').val(),
                Url: $('#urlsanpham').val(),
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
                    if (response.status) {
                        $.notify("Thành công", "success");
                        resetModal();
                    }
                    else {
                        $.notify("Thất bại", "error");
                        resetModal();
                    }
                }
            })
        }
    });

    //Xóa sản phẩm
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

    //Khi click nút thêm sản phẩm thì reset modal tránh trường hợp có dữ liệu
    $('.btn-add').click(function () {
        CKEDITOR.instances.fullDescription.setData("");
        resetModal();
    });

    // Khi click vào nút cập nhật sản phẩm đó thì load dữ liệu sản phẩm đó ra modal
    $('.btn-edit').click(function () {
        var idSanPham = $(this).data('id');
        $.ajax({
            url: '/Admin/SanPham/layChiTietSanPham',
            data: {
                id: idSanPham
            },
            async: false,
            type: "GET",
            datatype: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                resetModal();
                var SanPham = response.SanPham;
                var Mau = response.Mau;
                var KichCo = response.KichCo;
                console.log(SanPham);
                $('#tensanpham').val(SanPham.TenSanPham);
                $('#hinhanh').attr('src', SanPham.HinhAnh);
                $('#urlsanpham').val(SanPham.Url);
                $('#urlCu').val(SanPham.Url);
                $('#gia').val(SanPham.Gia);
                $('#danhmuc').val(SanPham.DanhMucID);
                $('#thuonghieu').val(SanPham.MaThuongHieu);
                $('#SoLuong').val(SanPham.SoLuong);
                $('#id').val(SanPham.ID);

                loadLoaiSanPhamTheoID(SanPham.DanhMucID);

                setTimeout(function () {
                    $('#maloai').val(SanPham.MaLoai);
                }, 1);

                CKEDITOR.instances.fullDescription.setData(SanPham.MoTa);

                $.each(Mau, function (i, item) {
                    $("#mau option[value='" + item.ID + "']").prop("selected", true);
                });

                $.each(KichCo, function (i, item) {
                    $("#kichco option[value='" + item.ID + "']").prop("selected", true);
                });

                $("#mau").trigger("chosen:updated");
                $("#kichco").trigger("chosen:updated");
            }
        })
    });
});