/*
Added by : KhoaHT
Description: this's common function for paging
*/
function buildPagingUri(pageIndex, pageSize) {
    var uri = '?$top=' + pageSize + '&$skip=' + (pageIndex * pageSize);
    return uri;
}

/*
Added by : KhoaHT
Description: this's common function for paging
*/

function getPagedResource(baseResource, pageIndex, pageSize) {
    var resource = baseResource;
    resource += (arguments.length == 3) ? buildPagingUri(pageIndex, pageSize) : '';
    return $http.get(resource).then(function (response) {
        var data = response.data;
        return {
            totalRecords: parseInt(response.headers('X-InlineCount')),
            results: data
        };
    });
}