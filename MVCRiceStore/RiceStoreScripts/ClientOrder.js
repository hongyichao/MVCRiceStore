$("#store").change(function () {
    var storeId = "";
    $("#store option:selected").each(function () {
        storeId = $(this).val();
    });

    
    populateRiceList(storeId);
});

$(document).ready(function () {
    if ($("#ViewName").val() === 'ClientOrderEditView') {
        populateStoreList();
    }
});

function populateStoreList() {
    
    var orderId = $("#Id").val();

    $.ajax({
        url: '/ClientOrder/GetStoreList',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        type: 'GET',
        data: {clientOrderId : orderId}
    }).done(function (listItems) {
        $("store").empty();
        for (var i = 0; i < listItems.length; i++) {
            $("#store").append("<option value='" + listItems[i].Value + "'>" + listItems[i].Text + "</option>");
        }
    }).fail(function (e) {
        alert(e);
    }).error(function (request, status, error) {
        alert(request.responseText);
    });

}

function populateRiceList(sId) {
    

    $.ajax({
        url: '/ClientOrder/GetStoreAvailableRice',
        contentType: "application/json",
        type: 'GET',
        data: {storeId: sId}
    }).done(function (listItems) {
        $("#rice").empty();
        for (var i = 0; i < listItems.length; i++) {
            $("#rice").append("<option value='" + listItems[i].Value + "'>" + listItems[i].Text + "</option>");
        }
    }).fail(function (e) {
        alert(e);
    }).error(function (request, status, error) {
        alert(request.responseText);
    });

}
