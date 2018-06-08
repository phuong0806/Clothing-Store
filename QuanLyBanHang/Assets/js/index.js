format_money = function (Number) {
    return Number.toLocaleString('it-IT', { style: 'currency', currency: 'VND' });
}

function toDateTime(secs) {
    var t = new Date(1970, 0, 1); // Epoch
    t.setSeconds(secs);
    return t;
}

function getFormattedDate(date) {
    var str = date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate() + " " + date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
    return str;
}

function js_yyyy_mm_dd_hh_mm_ss(date) {
    if (date == null) {
        return "-";
    }
    var now = new Date(date);
    year = "" + now.getFullYear();
    month = "" + (now.getMonth() + 1); if (month.length == 1) { month = "0" + month; }
    day = "" + now.getDate(); if (day.length == 1) { day = "0" + day; }
    hour = "" + now.getHours(); if (hour.length == 1) { hour = "0" + hour; }
    minute = "" + now.getMinutes(); if (minute.length == 1) { minute = "0" + minute; }
    second = "" + now.getSeconds(); if (second.length == 1) { second = "0" + second; }
    return day + "-" + month + "-" + year + "/" + hour + ":" + minute;
}

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
            url: "/Admin/Common/deleteImage",
            dataType: "json",
            data: { filename: filename },
            success: function (result) {
                reset();
                alert("Xóa thành công");
                var html = "";
                for (var i = 0; i < result.length; i++) {
                    html += "<div class='col-sm-2 a-book'>";
                    html += "<img src='" + result[i].path + "' class='hinh-anh' alt='Alternate Text' />";
                    html += "<span class='ten-hinh-anh'>" + result[i].name + "</span>";
                    html += "</div>";
                }
                $('.manage-image .list-img #result').html(html);
            },
            error: function (err) {
                alert(err.statusText);
            }
        });
    }
});


$(document).ready(function () {
    $(document).on('dblclick','.hinh-anh',function () {
        var url = $(this).attr('src');
        $('.image').removeClass('upload');
        $(`<div class="image upload"><img src="${url}" class="more-img" /><a href="#" class="remove-image">×</a></div>`).insertAfter('#result-images-wrapper');
        $('.manage-image').css({ 'display': 'none', 'visibility': 'hidden' });
        $('.background-black').css({ 'display': 'none', 'visibility': 'hidden' });
        
        setTimeout(function () {
            saveImage();
        }, 500);
    });

    $(document).on('dblclick', '.remove-image', function () {
        $(this).parent().remove();
        setTimeout(function () {
            saveImage();
        }, 500);
    })

    $(document).on('click', '.more-images', function () {
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
                var html = '<div id="result-images-wrapper"></div><div class="line-dashed"></div>';
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
    $(document).on('click', "#upload-link", function (e) {
        e.preventDefault();
        $("#upload:hidden").trigger('click');
    });
});

$(document).on('change', '#upload', function () {
    $.ajax({
        url: "/Admin/Common/saveImage",
        data: function () {
            var data = new FormData();
            data.append("file", $("#upload").get(0).files[0]);
            return data;
        }(),
        contentType: false,
        processData: false,
        dataType: "json",
        type: "POST",
        success: function (response) {
            var html = "";
            var url = response.data;
            html += "<div class='col-sm-2 a-book'>";
            html += "<img src='" + url + "' class='hinh-anh' alt='Alternate Text' />";
            html += "<span class='ten-hinh-anh'>" + url.slice(20) + "</span>";
            html += "</div>";
            $(html).insertBefore('#result-img-upload');
        }
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
    $('#result .a-book .hinh-anh').removeClass('active');
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

$(document).on('click', '.btn-chon-anh', function () {
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