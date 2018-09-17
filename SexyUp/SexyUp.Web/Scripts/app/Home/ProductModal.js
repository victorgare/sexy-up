const ProductModal = new function () {

    const fulfillProductModal = function (product) {

        const productImage = product.find(".img-product").attr("src");
        const productName = product.find(".product-name")[0].innerHTML;
        const productPrice = product.find(".product-price")[0].innerHTML;
        const productDescription = product.find(".product-description").val();

        $("#modal-product-image").attr("src", productImage);
        $("#modal-product-name").html(productName);
        $("#modal-product-price").html(productPrice);
        $("#modal-product-description").html(productDescription);
    }

    const bind = new function () {
        $(".quick-view").on("click", function (event) {
            event.preventDefault();

            const me = $(this).closest(".product");

            fulfillProductModal(me);
        });
    }

    this.init = function () {
        bind();
    }
}