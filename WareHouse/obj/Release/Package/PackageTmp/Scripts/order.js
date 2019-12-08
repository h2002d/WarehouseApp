function calculateOrderSum() {
    //Calculates order sum 
    var quantity = $('#Quantity').val();//Getting quantity from input
    var productId = $('#ProductId').val();//Getting product from dropdown
    console.log(quantity);
    console.log(productId);
    $.ajax({
        url: '/Order/CalculateSum',
        type: 'post',
        data: { quantity: quantity, productId: productId },
        success: function (data) {
            $('#Sum').val(data);
        }
    });
}