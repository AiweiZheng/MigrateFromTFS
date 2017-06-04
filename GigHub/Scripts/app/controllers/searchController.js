var SearchController = function () {

    var searchConcept;
    var searchBy;
    var searchInput;
    var searchForm;

    var attachTypeaheadConfig = function (url, displayName,minLength) {
        var bloodhound = new Bloodhound({
            datumTokenizer: Bloodhound.tokenizers.obj.whitespace(displayName),
            queryTokenizer: Bloodhound.tokenizers.whitespace,

            remote: {
                url: url + "%QUERY",
                wildcard: "%QUERY"
            }
        });

        $(searchInput).typeahead({
            hint:true,
            minLength: minLength,
            highlight: true
        }, {
                display: displayName,
                source: bloodhound
            });
    }

    var setupTypeaheadConfig = function (searchType) {

        var propertyName;
        var url;
        var minLength;
        switch (searchType) {
            case "Artist":
                minLength = 3;
                propertyName = "name";
                url = "/api/accounts/artists?query=";
                break;
            case "Genre":
                minLength = 2;
                propertyName = "name";
                url = "/api/genres?query=";
                break;
            case "Venue":
                minLength = 3;
                propertyName = "name";
                url = "/api/venues?query=";
                break;
            case "Anything":
                minLength = 3;
                propertyName = "";
                url = "/api/search?query=";
                break;
            default:
                alertDialog("Undefined Search Type");
                return;
        }

        attachTypeaheadConfig(url, propertyName,minLength);
    }

    var sendSearchQuery = function (e) {
        e.preventDefault();
        var concept = $(this).text();
        $(searchConcept).text(concept);

        $(searchBy).val(concept);
        $(searchForm).submit();
    }
    var init = function (params) {
        searchConcept = params.searchConcept;
        searchBy = params.searchBy;
        searchInput = params.searchInput;
        searchForm = params.searchForm;

        var searchTerm = $(searchConcept).text();
        setupTypeaheadConfig(searchTerm);

        $(params.container).on("click", "a", sendSearchQuery);
    };

    return { init: init }
}()


