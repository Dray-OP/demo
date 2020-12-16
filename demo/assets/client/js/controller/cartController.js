var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        $('#btnContinue').off('click').on('click', function () {
            //trở về trang chủ để mua tiếp
            window.location.href = "/";
        });
        $('#btnPayment').off('click').on('click', function () {
            //trở về trang chủ để mua tiếp
            window.location.href = "/thanh-toan";
        });

        $('#btnUpdate').off('click').on('click', function () {
            var listProduct = $('.txtQuantity');
            var cartList = [];

            $.each(listProduct, function (i, item) {
                cartList.push({
                    // hoặc dùng this lấy value
                    Quantity: $(item).val(),
                    Product: {
                        ID: $(item).data('id')
                    }
                });
            });

            $.ajax({
                url: '/Cart/Update',
                data: {
                    // chuyển json cartList sang 1 chuỗi 
                    cartModel: JSON.stringify(cartList)
                },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            })


        });

        $('#btnDelete').off('click').on('click', function () {
            
            $.ajax({
                url: '/Cart/DeleteAll',
                data: {
                },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            })


        });

        $('.btn-delete').off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                url: '/Cart/Delete',
                data: {
                    //lấy id của item click vào trong data-id
                    id: $(this).data('id')
                },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            })


        });

    }

}
cart.init();