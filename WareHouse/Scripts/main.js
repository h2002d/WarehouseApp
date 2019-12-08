$(document).ready(function () {
    $('#qr_generate').click(function () {
        var productId = $('#ProductId').val();
        if (productId !== "") {
            window.location.href = '/Product/QrGenerator?productId=' + productId;
        }
        else {
            $('span[data-valmsg-for="ProductId"]').text('Please select product');
        }
    });
});