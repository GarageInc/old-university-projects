
var Canvas = (function(Context){
		var canvas;
		var context;
				
		var isCanvas = false;
		var isStopped = true;
		
		var renderer; // цвет для canvas.
		var simulation;
		var ballArray = new Array();

		// переменные, регулирующие частоту кадров - тем самым, скорость
		var delay;// время отклика, чем меньше - тем выше скорость
		var frameRate;
		var frameTimer;

		// DeltaTime variables.
		var lastTime = Date.now(); // inistalise lastTime.
		var thisTime;
		var deltaTime;
				
		function initializeCanvas(){

			// найдем элемент canvas в разметке
			canvas = document.getElementById('canvas');
			// инициализируем симуляцию движения шариков - по ширине-
			simulation = new Simulation(canvas.width,canvas.height);
			renderer =  new Renderer('#CAE1FF');
			
			Canvas.prototype.width = canvas.width;
			Canvas.prototype.height = canvas.height;
			
			delay = 1000;
			frameRate = 25;
			frameTimer = delay/frameRate;
			
			/*########## Error checking to see if canvas is supported ############## */
			if (!canvas) {
				alert('Error: не найден элемент canvas!');
				return;
			}
			if (!canvas.getContext) {
				alert('Error: не найдено свойство canvas.getContent!');
				return;
			}
			
			context = canvas.getContext('2d');
			
			if (!context) {
				alert('Error: failed to getContent');
				return;
			}
			
			setCanvasBackground();
			isCanvas = true;
		}
		
		Canvas.prototype.startRandomCanvas = function () {
			
			if( !isCanvas || !isStopped ){
				return ;
			}
			
			createRandomBalls();
			start();
		}
		
		Canvas.prototype.stop = function(){
			isStopped = true;
		}
		
		Canvas.prototype.start = function(){
			start();
		}
		
		Canvas.prototype.setFrameRate = function( value ){
			frameRate = value;
			frameTimer = delay / frameRate;
		}
		
		Canvas.prototype.setDelay = function( value ){
			delay = value;
			frameTimer = delay / frameRate;
		}
		
		Canvas.prototype.addNewBall = function( ball ){
			
			ballArray.push( ball )
			renderer.draw(context, ballArray);
		}
		
		Canvas.prototype.clear = function( ){

			ballArray = new Array();
			isStopped = true;
			setCanvasBackground();
		}
		
		function start(){
			
			if( isStopped ){
				
				isStopped = false;
				mainLoop(); // запускаем цикл обработки				
			}
		}
		
		function createRandomBalls() {
			/* Ball takes X | Y | radius | Mass| vX | vY | colour */

			ballArray.push( new Ball(50, 100, 20, 20, 1, 0, '#8B6969'));
			ballArray.push( new Ball(500, 100, 15, 15, -2, 0, '#8B1A1A'));
			ballArray.push( new Ball(300, 10, 12, 12, 0, 5, '#FFFFFF'));
			ballArray.push( new Ball(400, 250, 17, 17, -2, -4, '#FFE303'));
			ballArray.push( new Ball(430, 20,18, 18, -2, 5, '#9ACD32'));
		}

		function setCanvasBackground(){
			
			context.beginPath();
			context.fillStyle = '#CAE1FF';
			context.fillRect(0, 0, canvas.width, canvas.height);
		}
		
		function mainLoop() {
			if(ballArray.length == 0 && !isStopped){
				
				alert("Нет добавленных мячиков-шариков!")
				isStopped = true;
				return;
			}
			
			thisTime = Date.now();
			deltaTime = thisTime - lastTime;

			setCanvasBackground();
			renderer.draw(context, ballArray);
			simulation.update(deltaTime, ballArray);

			lastTime = thisTime;
			
			if( !isStopped ){
				
				setTimeout( mainLoop, frameTimer );
			}
		}
		
		function Canvas() {	 
			initializeCanvas();
		}
		
		return Canvas;
})();
		