function searchForThings() {
    //show the searches
    mentorCount = 0;
    personCount = 0;
    var inputSearchText = $("#inputSearch").val();
    $('.searchHeaders').children().remove();
 
    // alert('You pressed Seach button');

    if (inputSearchText === "") {
        $('.searchField').hide();
       // $('.searchHeaders').children().remove();
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
                        $('.Persons').append('<li><a class="outputSearch" href="/User/index/' + data[i].Id + '"><img src="~/pictures/b.png" class="searchPicture"/>' + data[i].FirstName +' '+ data[i].LastName+ '</a></li>');
                        personCount++;
                        $('#noResultPerson').hide();
                    } else {
                        
                        $('.Interest').append('<li><a class="outputSearch" href="/Program/index'+data[i].Id+'"><img src="~/pictures/b.png" class="searchPicture"/>' + data[i].Name + '</a></li>');
                        mentorCount++;
                        $('#noResultMentorPrograms').hide();
                    }


                }


            }, error: function (data, succes, error) {
                alert('err');
            }
        });

        if (personCount === 0) {
            $('#noResultPerson').show();
        }
        if (mentorCount === 0) {
            $('#noResultMentorPrograms').show();
        }

    }

    $('#searchResult').text("Show more results for: " + inputSearchText);



}