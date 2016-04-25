<?php
//создаем короткие имена переменным
$supersetwheypro = $_POST['supersetwheypro'];
?>

<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1251">
<title>Результаты заказа</title>
</head>


<body>
<h2>Компания Сладости</h2>
<h3>Результаты заказа</h3>

<?php
	echo "<p>Заказ обработан в ";
	echo date('H:i, j F');
	echo "</p>";
	echo "<p>Список вашего заказа:</p>";
	echo "$supersetwheypro - SuperSet Whey Pro <br>"; /* или, запись однозначна */
	
	
	$total = 0;
	$total = $supersetwheypro;
	echo 'Заказано товаров: '.$total. '<br><br>';
	
/* Добавление констант - цены */
	define ('SUPERSETWHEYPRO_PRICE', 2630);
	
	$totalamount = 0.00;
	$totalamount = $supersetwheypro*SUPERSETWHEYPRO_PRICE;

//Вывод цены покупки
	echo 'Итого: '.number_format($totalamount,2). ' RUB<br><br>';

/* вывод значения констант на экран */
	echo SUPERSETWHEYPRO_PRICE. " руб. - стоимость 1 ящика протеина SuperSet Whey Pro.<br>";

?>
</body>
</html>

