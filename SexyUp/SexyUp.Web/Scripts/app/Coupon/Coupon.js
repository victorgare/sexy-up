const Coupon = new function () {

    this.validateCoupon = function (couponName, doneCallback) {
        $.post("/Order/ValidateCoupon/", { couponName: couponName })
            .done(doneCallback)
            .fail(function () {
                toastr.error("Ocorreu um erro");
            });
    }

    const bind = function () {
        $(".btn-coupon").on("click", function (event) {
            event.preventDefault();

            const me = $(this);
            const couponInput = $("#coupon");

            Coupon.validateCoupon(couponInput.val(), function (result) {
                console.log(result);

                if (result.Valid) {
                    console.log("valid");
                    me.attr("disabled", true);
                    couponInput.attr("disabled", true);
                    Cart.applyDiscount(result.DisctountPercentage);

                    toastr.success(result.Message);

                } else {
                    toastr.warning(result.Message);
                }

            });
        });
    }

    this.init = function () {
        bind();
    }
}

Coupon.init();