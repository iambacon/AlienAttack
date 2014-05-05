jQuery(document).ready(function() {

    $('.js-button').click(function() {
        var email = $('.js-email').val(),
            getMovesUri = 'api/moves',
            submitPositionUri = 'api/moves/position/submit',
            currentPosition = {
                x: 0,
                y: 1
            },
            params = {
            email: email,
            'currentPosition': currentPosition
        };

        // Get coordinates
        ajaxGet(getMovesUri, params, function(response) {
            var positions = response,
                finalPosition = response[response.length - 1];

            // Move probe on grid
            moveProbe(positions, function() {                
                params = {
                    email: email,
                    position: {
                        x: finalPosition.Coordinate.x,
                        y: finalPosition.Coordinate.y
                    }
                };

                // Send final position and display message.
                ajaxGet(submitPositionUri, params, function(response) {
                    $('.js-message').html(response);
                });
            });
        });

        /**
        function: moveProbe
        Iterates through positions and moves probe.
        */
        function moveProbe(positions, callback) {
            if (positions.length > 0) {
                var currentPos = positions.shift(),
                    x = currentPos.Coordinate.x,
                    y = currentPos.Coordinate.y,
                    orientation = currentPos.Orientation,
                    newPosition,
                    $item = $('[data-x=' + x + '][data-y=' + y + ']');

                setTimeout(function() {
                    newPosition = $item.position();

                    $('.probe').transform({
                        top: newPosition.top,
                        left: newPosition.left
                    }, {
                        direction: orientation
                    });

                    moveProbe(positions, callback);
                }, 1000);
            } else {
                callback();
            }
        }

        /**
        Function transform
        Set CSS3 transform rotate property.
        */
        $.fn.transform = function(properties, options) {
            properties['-webkit-transform'] = 'rotate( ' + options.direction + 'deg)';
            properties['-moz-transform'] = 'rotate( ' + options.direction + 'deg)';
            properties['-o-transform'] = 'rotate( ' + options.direction + 'deg)';
            properties['-ms-transform'] = 'rotate( ' + options.direction + 'deg)';
            properties['transform'] = 'rotate( ' + options.direction + 'deg)';
            $(this).css(properties);
        };

        /**
        Function ajaxGet
        Performs a http GET ajax request.
        */
        function ajaxGet(url, data, callback) {
            $.ajax({
                url: url,
                type: 'GET',
                data: data,
                contentType: "application/json;charset=utf-8"
            }).done(function(response) {
                callback(response);
            }).fail(function(response) {
                console.log('error: ' + response);
            });
        }
    });
});
