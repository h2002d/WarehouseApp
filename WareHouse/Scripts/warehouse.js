function filterWarehouse() {
    var productId = $('#productId').val();
    $.ajax({
        url: '/Report/_StockReport/',
        type: 'get',
        data: { id: productId},
        success: function (data) {
            $('.warehouse-container').html(data);
        }
    });
}