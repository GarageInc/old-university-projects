/*###################################################################
 # Класс, отвечающий за перерендерирование(перерисование) шариков   #
 ####################################################################*/
var Renderer = (function (Context) {
	
    function Renderer() {
    };

    Renderer.prototype.draw = function(context, ballArray) {
        // рисование шариков.
        drawBalls(context, ballArray);
    }
	
    function drawBalls(context,ballArray) {
		
        for (var i = 0; i < ballArray.length; i++) {
			
            context.beginPath();
            // рисование мячиков-шариков по переданному массиву мячей-шариков.
            context.arc(ballArray[i].getX(), ballArray[i].getY(),ballArray[i].getRadius(), 0, Math.PI * 2, false);
            context.strokeStyle = "000000";
            context.stroke();
            context.fillStyle = ballArray[i].getColour(); 
            context.fill();
            context.closePath();
        }
    }

    return Renderer;
})();