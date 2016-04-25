
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

	<table width=70% hight=30%  align="center" bgcolor="#1E90FF">
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

	<table width=70% hight=30% align="center" border=1 bgcolor="grey">
		<tr align="center">
			<td>
    			<form action="save_user.php" method="post">
						<a align="center" font color="black" > РЕГИСТРАЦИЯ: </a>
						
						<p>
							<label>Имя:<br></label>
								<input name="name1" size="15" maxlength="15">
						</p>
						<p>
							<label>Фамилия:<br></label>
								<input name="surname" size="15" maxlength="15">
						</p>
						<p>
							<label>День:<br></label>
								<SELECT NAME="Day"> 
								<OPTION value="1"> 1
								<OPTION value="2"> 2
								<OPTION value="3"> 3
								<OPTION value="4"> 4
								<OPTION value="5"> 5
								</SELECT>
						</p>
						<p>
							<label>Месяц:<br></label>
								<SELECT NAME="Month">
								<OPTION value="January" > Январь
								<OPTION value="February" > Февраль
								<OPTION value="March" > Март
								<OPTION value="April" > Апрель
								<OPTION value="May" > Май
								</SELECT>
						</p>
						<p>
							<label>Укажите пол:<br></label>
								<INPUT TYPE="radio" NAME="m" VALUE="men"> Мужской<BR>
								<INPUT TYPE="radio" NAME="f" VALUE="female"> Женский<BR>
						</p>
						<p>
							<label>Код:<br></label>
								<INPUT NAME="cod"> <BR>
						</p>
						<p>
							<label>Телефон:<br></label>
								<INPUT NAME="phone" SIZE="12" MAXLENGTH="6" ><BR>
						</p>
						<p>
							<label>Адрес:<br></label>
								<TEXTAREA NAME="adress" COLS=32 ROWS=4>
								Украина
								Мариуполь
								пр.Нахимова, 99
								</TEXTAREA>
						</p>
						<p>
							<label>Код:<br></label>
								<INPUT NAME="cod"> <BR>
						</p>
					
						<!-- save_user.php - это адрес обработчика. То есть, после нажатия на кнопку "Зарегистрироваться", данные из полей  отправятся на страничку save_user.php методом "post" -->
						<p>
							<label>Ваш логин:<br></label>
								<input name="login" type="text" size="15" maxlength="15">
						</p>
						<!-- В текстовое поле (name="login" type="text") пользователь вводит свой логин  -->
						<p>
							<label>Ваш пароль:<br></label>
								<input name="password" type="password" size="15" maxlength="15">
						</p>
						<!-- В поле для паролей (name="password" type="password") пользователь вводит свой пароль --> 
						<p>
							<input type="submit" name="submit" value="Зарегистрироваться">
						<!--Кнопка (type="submit") отправляет данные на страничку save_user.php--> 
						</p>
					</form>
				</td>
    			<td width=20%>
       				<img src="images/banner.jpg"  width="185" height="400" alt="Наш баннер">
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