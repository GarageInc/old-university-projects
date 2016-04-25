<?php
//Подключаемся к БД.
$db=mysql_connect("localhost", "root", "") or die("Ошибка подключения");
mysql_select_db("Primer", $db) or die("Не могу выбрать БД");
@mysql_query("SET NAMES UTF-8");
$q = mysql_query("SELECT * FROM gbook");

while($r = mysql_fetch_array($q)) {

echo "<a href=\"mailto:$r[email]\">$r[name]</a><br/> $r[text]<br/><br/>";
}
?>
