$(function () {
    $(".increment").click(function () {
        var countNow = parseInt($(".countNow").text());
        var total = parseInt($(".total").text());

       // eventThis = this;
        //console.log("this: " + this);
        //this: <div class="increment up"></div>


        if ($(this).hasClass("up")) {
            countNow = countNow + 1;

            $(".countNow").text(countNow);
        } else {
            countNow = countNow - 1;
            $(".countNow").text(countNow);
        }

        $(this).addClass("bump");

        setTimeout(function () {
            $(this).removeClass("bump");
        }, 400);
    });
});