toastr = window.toastr;

const Cart = new function () {
    const localstorage = window.localStorage;

    const getProducts = function () {
        const products = localStorage.getItem("cart-products");

        return JSON.parse(products);
    }

    const updateProductQuantity = function () {
        const products = getProducts();

        let productsQuantity = 0;
        if (products) {
            productsQuantity = Object.keys(products).length;
        }

        $(".cart-no")[0].innerHTML = productsQuantity;
    }

    const cleanProducts = function () {
        localStorage.removeItem("cart-products");

        updateProductQuantity();
    }

    const getProduct = function (productId) {
        const products = getProducts();

        return products[productId];
    }

    const deleteProcuct = function (productId) {
        const products = getProducts();

        delete products[productId];

        localStorage.setItem("cart-products", JSON.stringify(products));

        updateProductQuantity();
    }

    const saveProduct = function (productInfo) {
        if (localstorage) {

            let products = localStorage.getItem("cart-products");

            if (products) {
                products = JSON.parse(products);
            } else {
                products = {};
            }

            products[productInfo.productId] = productInfo;

            localStorage.setItem("cart-products", JSON.stringify(products));

            updateProductQuantity();
        }
    }

    const getProductInfo = function (product) {
        const productId = product.find(".product-id").val();
        const productImage = product.find(".img-product").attr("src");

        let productName = "";
        if (product.find(".product-name")[0]) {
            productName = product.find(".product-name")[0].innerHTML;
        }

        let productPrice = "";
        if (product.find(".product-price")[0]) {
            productPrice = product.find(".product-price")[0].innerHTML;
        }

        let productBrand = "";
        if (product.find(".product-brand")[0]) {
            productBrand = product.find(".product-brand")[0].innerHTML;
        }

        const productDescription = product.find(".product-description").val();
        let quantity = product.find(".product-quantity").val();

        if (isNaN(quantity)) {
            quantity = 1;
        }

        return {
            productId: productId,
            productName: productName,
            productDescription: productDescription,
            productBrand: productBrand,
            productPrice: productPrice.replace("R$", "").trim(),
            productImage: productImage,
            quantity: quantity
        };
    }

    const getProductsArray = function () {
        const products = getProducts();

        const dados = [];
        for (let property in products) {
            if (products.hasOwnProperty(property)) {

                dados.push(products[property]);
            }
        }

        return dados;
    }

    const redirectPost = function (href) {
        const products = getProductsArray();

        if (products && products.length > 0) {
            $.redirect(href, { cartItens: products });

        } else {
            toastr.warning("Não há itens no carrinho");
        }
    }

    const updateTotal = function (item, productInfo) {

        const totalPrice = productInfo.quantity * productInfo.productPrice;

        item.find(".total-price")[0].innerHTML = "R$ " + totalPrice.toFixed(2);
    }


    this.updateOrderSumary = function () {
        const productsArray = getProductsArray();

        let subTotal = 0;
        productsArray.map(function (item) {
            subTotal += item.productPrice * item.quantity;
        });

        $("#subtotal")[0].innerHTML = "R$ " + subTotal.toFixed(2);
        $("#price-total")[0].innerHTML = "R$ " + (subTotal + 20.0).toFixed(2);
    }
    const bind = function () {
        $(".add-to-cart").on("click", function (event) {
            event.preventDefault();

            toastr.success("Adicionado ao carrinho");

            const me = $(this).closest(".product");

            const product = getProductInfo(me);

            saveProduct(product);
        });

        $("#cartdetails").on("click", function (event) {
            event.preventDefault();

            const href = $(this).attr("href");

            redirectPost(href);
        });

        $("i.delete").on("click", function () {
            const productId = $(this).attr("value");

            deleteProcuct(productId);
        });

        $(".dec-btn").click(function () {
            const item = $(this).closest(".item");

            let productInfo = getProductInfo(item);

            productInfo = getProduct(productInfo.productId);

            const quantity = productInfo.quantity;
            if (quantity > 1) {
                productInfo.quantity -= 1;
            }

            saveProduct(productInfo);
            updateTotal(item, productInfo);
            Cart.updateOrderSumary();
        });

        $(".inc-btn").click(function () {
            const item = $(this).closest(".item");

            let productInfo = getProductInfo(item);

            productInfo = getProduct(productInfo.productId);
            productInfo.quantity += 1;

            saveProduct(productInfo);
            updateTotal(item, productInfo);
            Cart.updateOrderSumary();
        });

        $(".quantity-no").on("blur", function () {
            const item = $(this).closest(".item");
            const quantity = $(this).val();

            let productInfo = getProductInfo(item);

            productInfo = getProduct(productInfo.productId);
            productInfo.quantity = parseInt(quantity, 10);

            saveProduct(productInfo);
            updateTotal(item, productInfo);
            Cart.updateOrderSumary();
        });

        updateProductQuantity();
    }

    this.init = function () {
        bind();
    }

    this.teste = function () {
        return getProducts();
    }
}

Cart.init();