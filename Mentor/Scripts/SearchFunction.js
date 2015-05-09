function searchUsers(dataUsers) {
    if (dataUsers.length !== 0) {
        for (var i = 0; i < dataUsers.length; i++) {

            $('.Persons').append('<li><a class="outputSearch" href="/User/index/' + dataUsers[i].Id + '"><img src="~/pictures/b.png" class="searchPicture"/>' + dataUsers[i].FirstName + ' ' + dataUsers[i].LastName + '</a></li>');

            $('#noResultPerson').hide();

        }
        $('.Persons').append('<li><p class="outputSearch">Number of Searches: ' +dataUsers.length + '</p></li>');
    } else {

        $('#noResultPerson').show();

    }

}
function searchMentorPrograms(dataPrograms) {
    if (dataPrograms.length !== 0) {
        for (var i = 0; i < dataPrograms.length; i++) {
            $('.Interest').append('<li><a class="outputSearch" href="/Program/index' + dataPrograms[i].Id + '"><img src="~/pictures/b.png" class="searchPicture"/>' + dataPrograms[i].Name + '</a></li>');

            $('#noResultMentorPrograms').hide();
        }
    } else {

        $('#noResultMentorPrograms').show();

    }

}

function searchForThings() {
    //show the searches

    var inputSearchText = $("#inputSearch").val();
    $('.searchHeaders').children().remove();


    if (inputSearchText === "") {
        $('.searchField').hide()
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
                searchUsers(data.Users);
                searchMentorPrograms(data.Programs);

            }, error: function (data, succes, error) {
                alert('err');
            }
        });



    }
    $('#searchResult').text("Show more results for: " + inputSearchText);
}