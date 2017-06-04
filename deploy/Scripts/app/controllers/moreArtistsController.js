var MoreArtistsController = function (artistService) {

    var startIndex = 0;
    var sizePerLoad;
    var loadMoreBtn;
    var loadingSpinner;
    var noMoreContent;
    var container;
    var cssHide;

    var done = function(result) {

        loadingSpinner.addClass(cssHide);
        loadMoreBtn.removeClass(cssHide);

        if (result === noMoreContent) {
            loadMoreBtn.text("No More Artists").fadeOut("slow");
            return;
        }

        container.append(result);
        startIndex += sizePerLoad;
    }

    var fail = function (error) {
        loadingSpinner.addClass(cssHide);
        loadMoreBtn.removeClass(cssHide);

        alertDialog(error.responseJSON);
    };


    var getArtists = function () {
    
        loadingSpinner.removeClass(cssHide);
        loadMoreBtn.addClass(cssHide);

        artistService.getMoreArtists(startIndex, done, fail);
    };

    var loadMoreEvent = function(e) {
        e.preventDefault();

        getArtists();
    }

    var init = function (paras)
    {
        container = $(paras.containerId);
        loadMoreBtn = $(paras.loadMoreId);
        loadingSpinner = $(paras.loadingSpinner);
        sizePerLoad = paras.sizePerLoad;
        noMoreContent = paras.noContent;
        cssHide = paras.cssHideClass;

        loadMoreBtn.click(loadMoreEvent);
        getArtists();
    }

    return {
        init:init
    }

}(ArtistService);


