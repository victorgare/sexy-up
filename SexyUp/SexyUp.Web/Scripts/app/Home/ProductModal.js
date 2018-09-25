const ProductModal = new function () {

    const fulfillProductModal = function (product) {

        const productInfo = Cart.getProductInfo(product);

        $("#modal-product-image").attr("src", productInfo.productImage);
        $("#modal-product-name").html(productInfo.productName);
        $("#modal-product-price").html("R$ " + productInfo.productPrice);
        $("#modal-product-description").html(productInfo.productDescription);
        $("#modal-product-brand")[0].innerHTML = productInfo.productBrand;
        $("#modal-product-id").val(productInfo.productId);

        $(".product-description").val(productInfo.productDescription);
        $(".product-id").val(productInfo.productId);
        $(".add-to-wish-list").data("productid", productInfo.productId);
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