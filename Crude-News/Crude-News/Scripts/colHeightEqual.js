(function () {
    var heights = $(".col-md-6").map(function () {
        return $(this).height();
    }).get(),
    maxHeight = Math.max.apply(null, heights);
    console.log(maxHeight);
    $(".col-md-6").height(maxHeight);
})();
