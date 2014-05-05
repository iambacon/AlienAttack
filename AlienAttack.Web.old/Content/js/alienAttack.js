jQuery(document).ready(function($) {
    var uri = 'api/moves',
        getDataUri = 'http://goserver.cloudapp.net:3000/api/spaceprobe/getdata/',
        submitDataUri = 'http://goserver.cloudapp.net:3000/api/spaceprobe/submitdata/',
        email = 'alienAttack@iambacon.co.uk',
        json = '{"Directions":["FORWARD", "FORWARD", "RIGHT", "FORWARD", "FORWARD", "FORWARD", "FORWARD", "RIGHT", "FORWARD", "FORWARD", "LEFT", "FORWARD", "RIGHT", "LEFT", "LEFT", "FORWARD", "FORWARD", "FORWARD", "LEFT", "RIGHT"]}';

    $('.submitBtn').on('click', function() {
        var directions = $.parseJSON(json).Directions;
        var position = '{Coordinate:{x: 1, y: 1}}';
        var getData = {
            x: 1,
            y: 1,
            id: 1
        };

        $.ajax({
            type: 'GET',
            url: uri,
            data: getData
        }).done(function(data) {
            moveProbe(data);
        });

        // loop through directions, get new position and update UI.
        // $.each(directions, function(key, value) {
        //     var moveUri = uri + '/' + value;
        //     $.getJSON(moveUri)
        //         .done(function(data) {
        //             moveProbe(data);
        //         });
        // });
    });

    //$.getJSON(getDataUri)
    //    .done(function (data) {

    //    });

    /**
    Functions
    */

    function moveProbe(direction) {
        console.log('x value: ' + direction.x + ' y value: ' + direction.y);
    }
});
