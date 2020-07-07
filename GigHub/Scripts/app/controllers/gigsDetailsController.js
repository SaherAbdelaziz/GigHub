var GigsDetailsController = function (followingsService) {
    var followButton, followedId;

    var toggleFollow = function (e) {

        followButton = $(e.target);
        followedId = followButton.attr("data-user-id");

        if (followButton.hasClass("btn-default"))
            followingsService.createFollow(followedId, done, fail);

        else
            followingsService.deleteFollow(followedId, done, fail);
        
    }

    var fail = function () {
        alert("Something failed! in deleting or add follow");
    };

    var done = function () {
        var text = (followButton.text() == "Follow") ? "Following" : "Follow";
        followButton.toggleClass("btn-default").toggleClass("btn-info").text(text);
    };

    var init = function (container) {
        console.log("start");
        $(container).on("click", ".js-toggle-follow", toggleFollow);
    };

    return {
        init: init
    }

}(FollowingsService);