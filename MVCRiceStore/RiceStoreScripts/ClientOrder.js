function populateStoreList(storeId) {
    $.ajax({
        url: 'api/ClientOrder/GetStoreList',
        type: 'POST',
        data: storeId
    }).done(function (listItems) {
        var items = JSON.parse(listItems);
        for(var i in items) {
            
        }
    });
}
