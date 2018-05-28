$(document).ready(function () {
    $('#form-save').validate({
        rules: {
            tenthuonghieu: "required",
        },
        messages: {
            tenthuonghieu: "Bạn phải nhập tên danh mục"
        },
    });

    $('.btn-edit').click(function () {
        var id = $(this).data('id');
        $.ajax({
            url: '/Admin/ThuongHieu/layThuongHieu',
            data: {
                id: id
            },
            type: "GET",
            datatype: "json",
            success: function (response) {
                console.log("AA");
                var data = JSON.parse(response.data);
                $('#tenthuonghieu').val(data.TenThuongHieu);
                $('#id').val(data.ID);
            }
        })
    });

    $('.btn-save').click(function () {
        if ($('#form-save').valid()) {
            var id = $('#id').val();
            var tenthuonghieu = $('#tenthuonghieu').val();

            var thuonghieu = {
                ID: id,
                TenThuongHieu : tenthuonghieu
            }

            $.ajax({
                url: '/Admin/ThuongHieu/Save',
                data: {
                    thuonghieu: thuonghieu
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
        }
    });

    $('.btn-delete').click(function () {
        if (confirm("Bạn có chắc là xóa không?")) {
            var id = $(this).data('id');
            $.ajax({
                url: '/Admin/ThuongHieu/Delete',
                data: {
                    id: id
                },
                type: "POST",
                dataType: "json",
                success: function (response) {
                    if (response.status) {
                        $.notify("Thành công", "success");
                        setTimeout(function () {
                            window.location.reload();
                        }, 1000);
                    }
                    else {
                        $.notify("Thất bại", "error");
                    }
                }
            });
        }
    });
});