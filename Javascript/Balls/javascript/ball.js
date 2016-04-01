/*############################################################
 # Класс Шарика со своими свойствами 						 #
 #############################################################*/

var Ball = (function (context) {

    var position;
    var lastGoodPosition
    var velocity;
    var radius;
    var mass;
    var colour;
    var x;
    var y;

	// Параметры шара: позициии, радиус, масса, векторы первоначального направления(скорости) и цвет
    function Ball(inX, inY, inRadius,inMass, inVelX, inVelY, inColour) { // constructor
        this.position = new vector();
        this.position.setX(inX);        
		this.position.setY(inY);

        this.velocity = new vector();
        this.velocity.setX(inVelX);     
		this.velocity.setY(inVelY);

        this.setRadius(inRadius);
        this.setMass(inMass);
        this.setColour(inColour);
    }

    /* #######################
       # Getters and Setters для получнеия/назначения свойств объекта#
       ####################### */

    Ball.prototype.setX = function (inX) { this.position.setX(inX);}
    Ball.prototype.setY = function (inY) { this.position.setY(inY);}

    Ball.prototype.getX = function () {return this.position.getX();}
    Ball.prototype.getY = function () {return this.position.getY();}

    Ball.prototype.setRadius = function (inRadius) { this.radius = inRadius;}
    Ball.prototype.getRadius = function () { return this.radius;}

    Ball.prototype.setMass = function (inMass) { this.mass = inMass;}
    Ball.prototype.getMass = function () { return this.mass;}
    Ball.prototype.setColour = function (inColour) { this.colour = inColour;}
    Ball.prototype.getColour = function () { return this.colour;}
	
    return Ball;
})();