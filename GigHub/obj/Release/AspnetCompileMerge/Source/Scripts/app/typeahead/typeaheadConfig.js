function TypeaheadConfig() {

    TypeaheadConfig.prototype.init = function (params) {
       
        this.Bloodhound = new Bloodhound({
            datumTokenizer: Bloodhound.tokenizers.obj.whitespace(params.propertyName),
            queryTokenizer: Bloodhound.tokenizers.whitespace,

            remote: {
                url: params.url +"%QUERY",
                wildcard: "%QUERY"
            }
        });
    }
}

//var artists = new Bloodhound({
//    datumTokenizer: Bloodhound.tokenizers.obj.whitespace('name'),
//    queryTokenizer: Bloodhound.tokenizers.whitespace,
//
//    remote: {
//        url: '/api/accounts/artists?query=%QUERY',
//        wildcard: '%QUERY'
//    }
//});

//$('#SearchTerm').typeahead({
//    minLength: 3,
//    highlight: true
//}, {
//        name: 'artists',
//        display: 'name',
//        source: artists
//    });