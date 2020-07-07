var FollowingsService = function () {

    var createFollow = function (userId, done, fail) {
        console.log("you now follow");
        $.post("/api/followings", { followedId: userId })
            .done(done)
            .fail(fail);
    }

    var deleteFollow = function (followedId, done, fail) {
        console.log("you now dont follow");
        $.ajax({
                url: "/api/followings/" + followedId,
                method: "DELETE"
            })
            .done(done)
            .fail(fail);
    };

    

    return {
        createFollow: createFollow,
        deleteFollow: deleteFollow

    }

}();