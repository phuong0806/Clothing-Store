function ChangeToSlug() {
    var title, slug;

    //Lấy text từ thẻ input title
    title = document.getElementById("tensanpham").value;

    //Đổi chữ hoa thành chữ thường
    slug = title.toLowerCase();

    //Đổi ký tự có dấu thành không dấu
    slug = slug.replace(/á|à|ả|ạ|ã|ă|ắ|ằ|ẳ|ẵ|ặ|â|ấ|ầ|ẩ|ẫ|ậ/gi, 'a');
    slug = slug.replace(/é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ/gi, 'e');
    slug = slug.replace(/i|í|ì|ỉ|ĩ|ị/gi, 'i');
    slug = slug.replace(/ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ/gi, 'o');
    slug = slug.replace(/ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự/gi, 'u');
    slug = slug.replace(/ý|ỳ|ỷ|ỹ|ỵ/gi, 'y');
    slug = slug.replace(/đ/gi, 'd');
    //Xóa các ký tự đặt biệt
    slug = slug.replace(/\`|\~|\!|\@|\#|\||\$|\%|\^|\&|\*|\(|\)|\+|\=|\,|\.|\/|\?|\>|\<|\'|\"|\:|\;|_/gi, '');
    //Đổi khoảng trắng thành ký tự gạch ngang
    slug = slug.replace(/ /gi, "-");
    //Đổi nhiều ký tự gạch ngang liên tiếp thành 1 ký tự gạch ngang
    //Phòng trường hợp người nhập vào quá nhiều ký tự trắng
    slug = slug.replace(/\-\-\-\-\-/gi, '-');
    slug = slug.replace(/\-\-\-\-/gi, '-');
    slug = slug.replace(/\-\-\-/gi, '-');
    slug = slug.replace(/\-\-/gi, '-');
    //Xóa các ký tự gạch ngang ở đầu và cuối
    slug = '@' + slug + '@';
    slug = slug.replace(/\@\-|\-\@|\@/gi, '');
    //In slug ra textbox có id “slug”
    document.getElementById('urlsanpham').value = slug;
}

$(document).on('click', '#delete-link', function () {
    if (confirm('Bạn có chắc là xóa hình này?')) {
        var filename = $('#result .hinh-anh.active').attr('src');
        $.ajax({
            type: "POST",
            url: "/Admin/SanPham/deleteImage",
            dataType: "json",
            data: { filename: filename },
            success: function (result) {
                reset();
                console.log(result);
                alert("Xóa thành công");
                var html = "";
                for (var i = 0; i < result.length; i++) {
                    html += "<div class='col-sm-3 a-book'>";
                    html += "<img src=" + result[i].path + " class='hinh-anh' alt='Alternate Text' />";
                    html += "<span class='ten-hinh-anh'>" + result[i].name + "</span>";
                    html += "</div>";
                }
                $('#result').html(html);
            },
            error: function (err) {
                alert(err.statusText);
            }
        });
    }
});


$(document).ready(function () {

    $('.hinh-anh').dblclick(function () {
        var url = $(this).attr('src');
        $('#result-images').append(`<div class="image"><img src="${url}" class="more-img" /><a href="#" class="remove-image">×</a></div>`);
        $('.manage-image').css({ 'display': 'none', 'visibility': 'hidden' });
        $('.background-black').css({ 'display': 'none', 'visibility': 'hidden' });
        saveImage();
    });

    $(document).on('dblclick', '.remove-image', function () {
        $(this).parent().remove();
        saveImage();
    })

    $(document).on('click','.more-images',function () {
        var SanPhamID = $(this).data('id');
        $('#id-hidden').val(SanPhamID);
        loadMoreImages(SanPhamID);
    });

    function resetModalMoreImage() {
        $('#result-images').html(`<img src="/libs/Image/logo/add.png" id="btn-moreimages" class="more-img btn-chon-anh" />`);
    }

    function loadMoreImages(SanPhamID) {
        resetModalMoreImage();
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
    }

    function saveImage() {
        var images = [];
        var id = $('#id-hidden').val();
        $.each($('#result-images .image img'), function (i, item) {
            images.push($(item).attr('src'));
        })
        $.ajax({
            url: "/Admin/SanPham/SaveImages",
            type: "POST",
            data: {
                id: id,
                images: JSON.stringify(images)
            },
            dataType: "json",
            success: function () {

            }
        });
    }
});



/* MODAL IMAGE */
$(function () {
    $("#upload-link").on('click', function (e) {
        e.preventDefault();
        $("#upload:hidden").trigger('click');
    });
});

function readURL(input) {
    let url = URL.createObjectURL(input.files[0]);
    $('.img-selected')
             .attr('src', url);
    $('#fileImage')
             .val(input.files[0]);
    $('.image-selected').text(input.files[0].name);
    $('#img-book').val(input.files[0]);
}

function reset() {
    $('.image-selected').text('...');
    $('.img-selected').attr('src', '/libs/Image/logo/add.png');
    $('#result .hinh-anh').removeClass('active');
    $('#img-book').val('');
}

$(document).on('click', '#result .hinh-anh', function () {
    var urlImg = $(this).attr('src');
    var idImg = $(this).attr('id');
    $('#result .hinh-anh').removeClass('active');
    $(this).toggleClass('active');
    $('.img-selected').attr('src', urlImg)
    $('#img-book').val(urlImg);
    $('.image-selected').text(idImg);
})

$(document).on('click', '#reset-link', function () {
    reset();
});

$('.background-black').on('click', function () {
    $('.manage-image').css({ 'display': 'none', 'visibility': 'hidden' });
    $('.background-black').css({ 'display': 'none', 'visibility': 'hidden' });
});

$(document).on('click','.btn-chon-anh', function () {
    $('.manage-image').css({ 'display': 'block', 'visibility': 'visible' });
    $('.background-black').css({ 'display': 'block', 'visibility': 'visible' });
});

$.notify.addStyle('foo', {
    html:
      "<div>" +
        "<div class='clearfix'>" +
          "<div class='title' data-notify-html='title'/>" +
          "<div class='buttons'>" +
            "<button class='no'>Cancel</button>" +
            "<button class='yes' data-notify-text='button'></button>" +
          "</div>" +
        "</div>" +
      "</div>"
});
/* end MODAL IMAGE */