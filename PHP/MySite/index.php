
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Type" content="text/html; charset="utf-8" />
<title>Спортивный магазин SuperSet.</title>
</head>

<?php
	session_start();
?>

<body>
	<table width=70% align="center" border=1 hight=50% bgcolor="grey">
		<tr hight=30%>
			<td width=15% bgcolor="grey">
      			<img src="images/suplogo.jpg"  width="200" height="180" alt="Наш логотип">
  			</td>
			<td width=57% bgcolor="grey">
				<img src="images/supersets-for-growth.jpg"  width="525" height="180" alt="Наш логотип">
   			</td>
			<td bgcolor="grey" align="center">
      				      				Контактная информация:<br>
					пишите нам 
					на<br>
					*9-b-rinat@rambler.ru<br>
					*vk.com/rrrinat<br>
   			</td>
		</tr>
	</table>	

	<table width=70% hight=30% align="center" bgcolor="#1E90FF">
		<tr>
			<td>
				<form action="testreg.php" method="post">
				
					<label>Ваш логин:</label>
					<input name="login" type="text" size="15" maxlength="15">
				 			
					<label>Ваш пароль:</label>
					<input name="password" type="password" size="15" maxlength="15">
								
					<input type="submit" name="submit" value="Войти">
					
										
				</form>
			</td>

			<td rowspan="2" width=20.5% align="center"><font><a href="reg.php">РЕГИСТРАЦИЯ</a></font></td>
		</tr>
		
		<tr>
			<td>
				<?php
					if (empty($_SESSION['login']) or empty($_SESSION['id']))
					{
						echo "Вы вошли на сайт, как гость!  <a href='#'>Эта ссылка  доступна только зарегистрированным пользователям</a>";
					}
					else
					{
						echo "Вы вошли на сайт, как ".$_SESSION['login']." <a href='indexForReg.php'>  Эта ссылка доступна только зарегистрированным пользователям</a>";
					}
				?>
			</td>
		</tr>
		
	</table>

	<table width=70% hight=30% align="center"  bgcolor="grey">
		<tr>
			<td>
				<img src="images/whey-pro4.jpg"  align="right" width="300" height="180" alt="Протеин SuperSet Whey Pro">
			
				Вырабатывается из сывороточных белков, имеющих исключительно высокую пищевую и биологическую ценность, а также наивысшую скорость расщепления среди цельных белков.

50% белка!!! <br>
Порций в упаковке: 216 <br>
50% белка!!!<br><br>
Порций в упаковке: 216 <br>
ПИТАТЕЛЬНЫЕ ВЕЩЕСТВА (Размер порции 33 г): <br>
Калорийность 123 ккал<br>
Белок 16,5 г<br>
Углеводы 12,7 г<br>
Жиры 0,7 г<br>		
<br>		
ВИТАМИНЫ И МИКРОЭЛЕМЕНТЫ<br>		
<br>		
Витамин A 824 МЕ<br>		
Витамин E 2,5 мг<br>		
Витамин B1 0,32 мг<br>		
Витамин B2 0,32 мг<br>		
Витамин B6 1,25 мг<br>		
Витамин C 12,5 мг<br>		
Никотинамид 3,88 мг<br>		
Фолиевая кислота 25 мкг<br>		
Рутин 6,25 мг<br>				
Пантотенат кальция 2,5 мг<br>		
Витамин B12 3,1 мкг<br>		
Липоевая кислота 0,5 мг<br>		
Железо 6,23 мг<br>		
Медь 0,74 мг<br>		
Кобальт 0,12 мг<br>		
Марганец 2,74 мг<br>		
Цинк 2,2 мг<br>				
Магний 29,4 мг<br>		
<br>		
Рекомендации по применению Whey Pro:<br>		
Для приготовления одной порции белкового напитка смешайте 1 мерную чашку (33 г) порошка Whey Pro с 230-300 мл воды, нежирного молока или сока.<br>		
<br>		
Ингредиенты Whey Pro:<br>		
<br>		
Концентрат сывороточных белков ультра-фильтрационный (КСБ-УФ), кристаллическая фруктоза, витаминно-минеральный комплекс 
(ретинола ацетат, тиамина бромид, пиридоксина гидрохлорид, рибофлавина мононуклеотид, цианокобаламин, аскорбиновая кислота, токоферола ацетат, никотинамид, рутин, 
кальция пантотенат, фолиевая кислота, липоевая кислота, железа сульфат, меди сульфат, калия фосфат,
кобальта сульфат, марганца сульфат, цинка сульфат, магния фосфат, магния гидрокарбонат, кальция стеарат), Guar Gum и натуральный или искусственный ароматизатор.			
			</td>
			<td>
				<img src="images/banner.jpg"  width="185" height="400" alt="Наш баннер">
				<img src="images/banner.jpg"  width="185" height="400" alt="Наш баннер">
			</td>
		</tr>
		<tr>
    			<td align="center" bgcolor="grey">
				<br>
					<form action="proces.php" method="post">
						<table width="500" border="1">
							<tr bgcolor="#3300CC">
								<td width="250" style="color:#FFF">Наименование товара</td>
								<td width="30" style="color:#FFF">Количество</td>
							</tr>
							<tr>
								<td>SuperSet Whey Pro 7120г</td>
								<td><input name="supersetwheypro" type="text" size="4" maxlength="4"></td>
							</tr>
							
							<tr>
								<td>Как вы хотите получить товар?</td>
								<td>
									<select name="find">
									<option value="a" selected>По почте</option>
									<option value="b">Курьерской доставкой</option>
									<option value="с">Визит на фирму</option>
									<option value="d">Самовывоз</option>
									</select>
								</td>
							</tr>
							<tr>
								<td colspan="2" align="center"><input name="" type="submit" value="Сделать быстрый заказ"></td>
							</tr>
						</table>						
					</form>
					
					<br><br>
					
					<text align="center">
						КОММЕНТАРИИ:
					</text>
					<br/>			
					<?php include("readBD.php"); ?>
					<br/>
						<form name="" action="addBD.php" method="post">
							<table>
								<tr> 
									<td> Ваше имя:</td>
									<td> <input name="data[0]" type="text" value=""></td>
								</tr>
								<tr>
									<td>Введите e-mail:</td>
									<td> <input name="data[1]" type="text" value=""> </td>
								</tr>
								<tr>
									<td> Текст Вашего сообщения:</td>
									<td> <textarea name="data[2]" rows=5 cols=20 wrap="off"></textarea> </td> 
								</tr>
							</table>
							<br>
								<input type="submit" name="add" value="Добавить комментарий">
							<br/>
						</form>
       			</td>
    			<td width=20%>
       				<img src="images/banner.jpg"  width="185" height="400" alt="Наш баннер">
				</td>
 		</tr>
	</table>
	<table width=70% hight=30% align="center">
		<tr>
    			<td colspan=3 align="center" bgcolor="#1E90FF">СОЗДАТЕЛЬ САЙТА  (с)<a href="#"><font color="#FFFFFF">РИНАТ ФИХТЕНГОЛЬЦ</font></a></td>
		</tr>
	</table>
	
	
	

</body>
</html>