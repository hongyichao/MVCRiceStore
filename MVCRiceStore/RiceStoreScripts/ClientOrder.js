$("#StoreId").change(function () {
    var storeId = "";
    $("#StoreId option:selected").each(function () {
        storeId = $(this).val();
    });
    
    var orderId = $("#Id").val();
    populateRiceList(storeId, orderId);
});

$(document).ready(function () {
    if ($("#ViewName").val() === 'ClientOrderEditView' || $("#ViewName").val() === 'ClientOrderCreateView')
    {
        populateStoreList();
    }
});

function populateStoreList() {
    
    var orderId = $("#Id").val();

    if (!orderId) orderId = -1;

    $.ajax({
        url: '/ClientOrder/GetStoreList',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        type: 'GET',
        data: {clientOrderId : orderId}
    }).done(function (listItems) {
        $("#StoreId").empty();
        $("#StoreId").append("<option value=''></option>");
        for (var i = 0; i < listItems.length; i++) {
            var optionStr = "<option value='" + listItems[i].Value + "'";
            if (listItems[i].Selected) {
                optionStr += " selected='selected' ";

                if (orderId!==-1) {
                    populateRiceList(listItems[i].Value, orderId);
                }
            }
            optionStr +=  ">" + listItems[i].Text + "</option>";
            $("#StoreId").append(optionStr);
        }
    }).fail(function (e) {
        alert("failed to populate store list");
    }).error(function (request, status, error) {
        alert(request.responseText);
    });

}

function populateRiceList(sId, oId) {
    if (!oId) oId = -1;

    $.ajax({
        url: '/ClientOrder/GetStoreAvailableRice',
        contentType: "application/json",
        type: 'GET',
        data: {storeId: sId, orderId : oId}
    }).done(function (listItems) {
        $("#RiceId").empty();
        for (var i = 0; i < listItems.length; i++) {
            var optionStr = "<option value='" + listItems[i].Value + "'";
            if (listItems[i].Selected) {
                optionStr += " selected ";
            }
            optionStr += ">" + listItems[i].Text + "</option>";

            $("#RiceId").append(optionStr);
        }
    }).fail(function (e) {
        alert("failed to populate rice list");
    }).error(function (request, status, error) {
        alert(request.responseText);
    });

}
