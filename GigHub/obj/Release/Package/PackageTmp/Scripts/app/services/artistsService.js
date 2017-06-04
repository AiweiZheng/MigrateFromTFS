
var ArtistService = function () {

    var getMoreArtists = function(startIndex, done, fail) {
        $.ajax({
                url: "/artists/more/" + startIndex,
                method: "GET"
            }).done(done)
            .fail(fail);
    };

    return {
        getMoreArtists: getMoreArtists
    }
}()

