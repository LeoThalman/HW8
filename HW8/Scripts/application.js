$(document).ready(function () {
    console.log("Working!!!");
    $('.genre').click(function () {
        var genre = $(this).val();
        console.log("Hello!!!");
        console.log(genre);
        var query = "Art/Search?genre=" + genre;
        $.ajax({
            type: "GET",
            dataType: "json",
            url: query,
            success: loadArt,
            error: failed
        });
    });

    function loadArt(data) {
        $('.artTable').empty();
        console.log("Hello!!!");    
        var temp = JSON.parse(data);
        console.log(temp);
        $('.artTable').append('<tr> <th> Artist Name </th><th>Title of Art</th></tr>');
        for (var i = 0; i < temp.length; i += 1) {
            $('.artTable').append('<tr> <td>' + temp[i].Artist + '</td><td> ' + temp[i].Title + '</td></tr>');
        }

    }

    function failed() {

    }
});