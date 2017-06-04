function MoreGigsController() {

    MoreGigsController.prototype.init = function(params) {

        this.container = $(params.containerId);
        this.loadMoreBtn = $(params.loadMoreId);
        this.spinner = $(params.loadingSpinner);
        this.artistId = params.artistId;
        this.startIndex = params.startIndex;
        this.sizePerLoad = params.sizePerLoad;
        this.query = params.query;
        this.noMoreContent = params.noContent;
        this.cssHide = params.cssHideClass;

        this.getMoreGigsAction = params.getMoreGigs;

        this.loadMoreBtn.click(this.loadMore.bind(this));
    };

    MoreGigsController.prototype.done = function(result) {

        this.loadMoreBtn.removeClass(this.cssHide);
        this.spinner.addClass(this.cssHide);

        if (result === this.noMoreContent) {
           
            this.loadMoreBtn.text("No More Gigs").fadeOut("slow");
            return;
        }

        this.container.append(result);
        this.startIndex += this.sizePerLoad;
    };

    MoreGigsController.prototype.fail = function (error) {

        this.loadMoreBtn.removeClass(this.cssHide);
        this.spinner.addClass(this.cssHide);

        alertDialog(error.responseJSON);
    };

    MoreGigsController.prototype.loadMore = function() {
  
        this.loadMoreBtn.addClass(this.cssHide);
        this.spinner.removeClass(this.cssHide);

        if (this.getMoreGigsAction === GigService.getMoreGigsByArtist)
            this.getMoreGigsAction(this.artistId,
                this.startIndex,
                this.done.bind(this),
                this.fail.bind(this));

        else if (this.getMoreGigsAction === GigService.getMoreGigs) 
            this.getMoreGigsAction(
                this.startIndex,
                this.query,
                this.done.bind(this),
                this.fail.bind(this));

        else if (this.getMoreGigsAction === GigService.getMoreMyAttendingGigs)
            this.getMoreGigsAction(
                this.startIndex,
                this.query,
                this.done.bind(this),
                this.fail.bind(this));
    };
}

