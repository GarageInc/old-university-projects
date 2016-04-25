<?php
$name=strip_tags(stripslashes(substr($_POST['data']['0'],0,20)));
$mail=strip_tags(stripslashes(substr($_POST['data']['1'],0,30)));
$text=strip_tags(stripslashes(substr($_POST['data']['2'],0,300)));

if($_POST['add'] != '') {
    if($name != '') {
        if($mail !='') {
            if($text != '') {

//Подключаемся к БД.
$db=@mysql_connect("localhost", "root", "") or die("Ошибка подключения");
@mysql_select_db("Primer", $db) or die("Не могу выбрать БД");
@mysql_query("SET NAMES UTF-8");
$query = "INSERT INTO gbook VALUES (NULL,
'".$name."',
'".$mail."',
'".$text."')";
if(mysql_query($query)) { echo "Запись <br/><a href=\"index.php\">добавлена</a>";}
 else {echo "Ошибка записи";};
 }
 else {echo "Введите комментарий";}
 }
 else {echo "Введите e-mail";}
 }
 else {
 echo "Введите имя";
 }
 }
 else {echo "<a href=\"index.php\">назад</a>";  };
?>
