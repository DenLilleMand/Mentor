function searchForThings() {
    //show the searches
    var inputSearchText = $("#inputSearch").val();
    $('.searchHeaders').children().remove();
    // alert('You pressed Seach button');

    if (inputSearchText === "") {
        $('.searchField').hide();
        $('.searchHeaders').children().remove();
    }
    else {
        var test = { 'input': inputSearchText };
        $.ajax({
            type: 'POST',
            url: '/User/GetSearchData',
            data: JSON.stringify(test),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {

                $('.searchField').show();


                for (var i = 0; i < data.length; i++) {
                    if (data[i].FirstName != null) {
                        $('.Persons').append('<li><a class="outputSearch" href="/User/index/'+data[i].Id+'"><img src="~/pictures/b.png" class="searchPicture"/>' + data[i].FirstName + '</a></li>');
                    } else {
                        $('.Interest').append('<li><a class="outputSearch" href=""><img src="~/pictures/b.png" class="searchPicture"/>' + data[i].Name + '</a></li>');
                        
                    }


                }


            }, error: function (data, succes, error) {
                alert('err');
            }
        });



    }

    $('#searchResult').text("Show more results for: " + inputSearchText);



}