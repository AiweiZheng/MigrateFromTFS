function MoreGigsController() {

    MoreGigsController.prototype.init = function(params) {

        this.container = $(params.containerId);
        this.loadMoreBtn = $(params.loadMoreId);
        this.artistId = params.artistId;
        this.startIndex = params.startIndex;
        this.sizePerLoad = params.sizePerLoad;
        this.noMoreContent = params.noContent;
        this.cssHide = params.cssHideClass;

        this.gigService = GigService;

        this.loadMoreBtn.click(this.loadMoreBtnEvent.bind(this));
    };

    MoreGigsController.prototype.done = function(result) {

        this.loadMoreBtn.removeClass(this.cssHide);

        if (result === this.noMoreContent) {
           
            this.loadMoreBtn.text("No More Gigs").fadeOut("slow");
            return;
        }

        this.container.append(result);
        this.startIndex += this.sizePerLoad;
    };

    MoreGigsController.prototype.fail = function (error) {

        this.loadMoreBtn.removeClass(this.cssHide);

        alertDialog(error.responseJSON);
    };

    MoreGigsController.prototype.loadMoreBtnEvent = function() {
  
        this.loadMoreBtn.addClass(this.cssHide);

        this.gigService.getMoreGigs(this.artistId,
            this.startIndex,
            this.done.bind(this),
            this.fail.bind(this));
    };
}

