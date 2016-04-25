<?php
//создаем короткие имена переменным
$shoco = $_POST['shoco'];
$orehy = $_POST['orehy'];
$fruct = $_POST['fruct'];
?>

<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1251">
<title>Компания Сладости. Результаты заказа</title>
</head>


<body>
<h2>Компания Сладости</h2>
<h3>Результаты заказа</h3>

<?php
	echo "<p>Заказ обработан в ";
	echo date('H:i, j F');
	echo "</p>";
	echo "<p>Список вашего заказа:</p>";
	echo "$shoco - Шоколадные конфеты <br>"; /* или, запись однозначна */
   /* echo $shoco. " - Шоколадные конфеты <br>";   */
	echo $orehy. " - Орехи в шоколаде<br>";
	echo $fruct. " - Сухофрукты <br>";
	
	
	$total = 0;
	$total = $shoco + $orehy + $fruct;
	echo 'Заказано товаров: '.$total. '<br><br>';
	
/* Добавление констант - цены */
	define ('SHOCO_PRICE', 50);
	define ('OREHY_PRICE', 70);
	define ('FRUCT_PRICE', 40);

	$totalamount = 0.00;
	$totalamount = $shoco*SHOCO_PRICE + $orehy*OREHY_PRICE + $fruct*FRUCT_PRICE;

//Вывод цены покупки
	echo 'Итого: '.number_format($totalamount,2). ' RUB<br><br>';

	$ndc = 0.18; //Налог НДС составляет 18% - Присваивание значения для НДС.
	$totalamount = $totalamount * (1 + $ndc);

	echo 'Всего, включая НДС: '.$totalamount. ' RUB<br><br>';

/* вывод значения констант на экран */
	echo SHOCO_PRICE. " руб. - стоимость 1 кг шоколадных конфет.<br>";
	echo OREHY_PRICE. " руб. - стоимость 1 кг орехов в шоколаде.<br>";
	echo FRUCT_PRICE. " руб. - стоимость 1 кг сухофруктов.";

?>
</body>
</html>

