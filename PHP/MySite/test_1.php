
<?php
$link = mysql_connect("localhost", "root")
or die("Не могу подключиться");
print ("Соединение выполнено");
mysql_close($link);
?>

<?php
$link = mysql_connect("localhost", "root")//Соединение вместо Root -> root выполняется!
or die("Не могу подключиться к базе" );
// сделать Primer текущей базой данных
mysql_select_db('Primer', $link) or die ('Не могу выбрать БД');
?>