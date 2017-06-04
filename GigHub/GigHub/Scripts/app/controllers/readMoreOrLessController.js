var ReadMoreOrLessController = function () {

    var readMoreOrLess = function (container, panel, dirty) {
        var readLessLength = 200;

        var readMoreHtml = $(panel).html();

        var lessText = readMoreHtml.replace(/^([\s\S]{200}\S*)[\s\S]*/, "$1");

        if (readMoreHtml.length > readLessLength) {
            $(panel).html(lessText)
                .append("<a href='' class='loadMore read-more-link'> >>more</a>");
        } else {
            $(panel).html(readMoreHtml);
        }

        var showMore = function (event) {
            event.preventDefault();

            $(this).parent(panel).html(readMoreHtml)
                .append("<a href='' class='loadMore show-less-link'> >>less</a>");
        }

        var readLess = function (event) {
            event.preventDefault();

            $(this).parent(panel).html(lessText)
                .append("<a href='' class='loadMore read-more-link tt'> >>more</a>");
        }

        $(container).attr(dirty, "true");
        $(container).on("click", ".read-more-link", showMore);
        $(container).on("click", ".show-less-link", readLess);
    }

    var readMoreGigs = function (params) {
        new MoreGigsController().init(params);
    };

    return {
        readMoreOrLess: readMoreOrLess,
        readMoreGigs: readMoreGigs
    };

}();

