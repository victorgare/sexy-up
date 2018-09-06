(function () {

    // phone number
    $(".phone-number").mask("(00) 0000-00009", {
        onKeyPress: function (phoneNumber, e, field, options) {
            const masks = ["(00) 0000-00009", "(00) 00000-0000"];
            const mask = (phoneNumber.length === 15) ? masks[1] : masks[0];

            $(".phone-number").mask(mask, options);
        }
    });

    // cpf
    $(".cpf").mask("000.000.000-00");

    // cnpj
    $(".cnpj").mask("00.000.000/0000-00");
})();