const WishList = new function () {

    const insertItem = function (href, productId) {
        $.post(href, { idProduct: productId })
            .done(function (result) {

                if (result.authenticated === true && result.success === true) {
                    toastr.success(result.message);
                } else {
                    toastr.error(result.message);
                }

            }).fail(function () {
                toastr.error("Ocorreu um erro");
            });
    }

    const bind = function () {
        $(".add-to-wish-list").on("click", function (event) {
            event.preventDefault();

            const me = $(this);

            const href = me.attr("href");
            const productId = me.data("productid");

            insertItem(href, productId);
        });
    }

    this.init = function () {
        bind();
    }
}

WishList.init();
