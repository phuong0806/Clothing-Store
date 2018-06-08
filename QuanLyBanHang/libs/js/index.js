format_money = function (Number) {
    return Number.toLocaleString('it-IT', { style: 'currency', currency: 'VND' });
}

var Store = (function () {
    var cart = [];
    function Item(id, size, color, price, amount) {
        this.ID = id;
        this.Amount = amount;
        this.Color = color;
        this.Size = size;
        this.Price = price;
    };

    function saveCart() {
        localStorage.setItem("cart", JSON.stringify(cart));
    }

    function loadCart() {
        cart = JSON.parse(localStorage.getItem("cart"));
    }

    //Public methods and properties
    var obj = {};

    //Thêm sản phẩm mới vào giỏ hàng
    obj.addItemToCart = function (id, size, color, price) { // add a item
        for (var i in cart) {
            if (cart[i].ID === id) {
                return;
            }
        }
        var item = new Item(id, size, color, price, 1);
        if (cart === null)
            cart = [];
        cart.push(item);
        saveCart();
    };

    //Tổng tiền tất cả sản phẩm
    obj.totalCost = function () {
        var total = 0;
        for (var i in cart) {
            total += cart[i].Amount * cart[i].Price;
        }
        $('.total-cost-cart').text(format_money(total));
    }

    //Màu của sản phẩm
    obj.setColor = function (SanPhamID, MauID) {
        for (var i in cart) {
            if (cart[i].ID == SanPhamID) {
                cart[i].Color = MauID;
                saveCart();
                return;
            }
        }
    };

    //kích cỡ của sản phẩm
    obj.setSize = function (SanPhamID, KichCoID) {
        for (var i in cart) {
            if (cart[i].ID == SanPhamID) {
                cart[i].Size = KichCoID;
                saveCart();
                return;
            }
        }
    };

    //số lượng của sản phẩm
    obj.setAmount = function (SanPhamID, Amount) {
        for (var i in cart) {
            if (cart[i].ID == SanPhamID) {
                cart[i].Amount = Amount;
                //Tổng tiền thay đổi theo số lượng sản phẩm thay đổi
                $('.total-cost-product.id' + SanPhamID).text(format_money(cart[i].Amount * cart[i].Price))
                obj.totalCost();
                saveCart();
                return;
            }
        }
    };

    //Load các chi tiết của sản phẩm
    obj.loadDetail = function () {
        for (var i in cart) {
            $('.mau-sel.id' + cart[i].ID).val(cart[i].Color);
            $('.kichco-sel.id' + cart[i].ID).val(cart[i].Size);
            $('.amount-sel.id' + cart[i].ID).val(cart[i].Amount);
            $('.total-cost-product.id' + cart[i].ID).text(format_money(cart[i].Amount * cart[i].Price))
        }
        obj.totalCost();
    };

    //Tổng số sản phẩm có trong giỏ hàng
    obj.AmountInCart = function () {
        if (cart == null || cart.length === 0) {
            $('.cart').addClass('hidden');
            cart = [];
            return;
        }
        $('.cart').removeClass('hidden');
        $('.number-product').text(cart.length);
    };

    //Xóa sản phẩm trong giỏ hàng
    obj.removeProduct = function (SanPhamID) {
        for (var i in cart) {
            if (cart[i].ID == SanPhamID) {
                cart.splice(i, 1);
                saveCart();
                return;
            }
        };
    }

    obj.returnTotalCostOfProduct = function (SanPhamID) {
        var total = 0;
        for (var i in cart) {
            total += cart[i].Amount * cart[i].Price;
        }
        return total;
    }

    //Xóa giỏ hàng
    obj.removeCart = function () {
        localStorage.cart =  null;
    };

    //Load cart
    loadCart();
    obj.AmountInCart();
    return obj;

})();

$('.btn-addcart').click(function (event) { // thêm sản phẩm vào giỏ hàng
    event.preventDefault();
    var id = $(this).data('id');
    var size = $(this).data('size');
    var color = $(this).data('color');
    var price = $(this).data('price');
    Store.addItemToCart(id, size, color, price);
    Store.AmountInCart();
});

