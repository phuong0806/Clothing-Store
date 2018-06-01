$(document).ready(function () {
    $('#form-save').validate({
        rules: {
            tenmau: "required",
            code: "required",
        },
        messages: {
            tenmau: "Bạn phải nhập trường này",
            code: "Bạn phải nhập trường này"
        },
    });


    $('.btn-edit').click(function () {
        var id = $(this).data('id');
        $.ajax({
            url: '/Admin/Mau/layMau',
            data: {
                id: id
            },
            type: "GET",
            datatype: "json",
            success: function (response) {
                var data = JSON.parse(response.data);
                $('#id').val(data.ID);
                $('#tenmau').val(data.Name);
                $('#code').val(data.Code);
            }
        })
    });


    $('.btn-delete').click(function () {
        if (confirm("Bạn có chắc là xóa không?")) {
            var id = $(this).data('id');
            $.ajax({
                url: '/Admin/Mau/Delete',
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

    $('.btn-save').click(function () {
        if ($('#form-save').valid()) {
            var id = $('#id').val();
            var name = $('#tenmau').val();
            var code = $('#code').val();
            var mau = {
                ID: id,
                Name: name,
                Code: code
            }

            $.ajax({
                url: '/Admin/Mau/Save',
                data: {
                    mau: mau
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
});