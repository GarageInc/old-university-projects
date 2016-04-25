<html>
<head>
	<meta http-equiv="Content-Type" content="text/html; charset=windows-1251">
	
	<title>Компания сладости</title>
</head>

<body>
<h2><font color="#ff0066">Компания "Сладости"</font></h2>

<form action="proces.php" method="post">
<table width="500" border="0">
	<tr bgcolor="#3300CC">
		<td width="250" style="color:#FFF">Наименование товара</td>
		<td width="30" style="color:#FFF">Количество</td>
	</tr>
	<tr>
		<td>Конфеты шоколадные</td>
		<td><input name="shoco" type="text" size="4" maxlength="4"></td>
	</tr>
	<tr>
		<td>Орехи в шоколаде</td>
		<td><input name="orehy" type="text" size="4" maxlength="4"></td>
	</tr>
	<tr>
		<td>Сухофрукты</td>
		<td><input name="fruct" type="text" size="4" maxlength="4"></td>
	</tr>
	<tr>
		<td>Как вы хотите получить товар?</td>
		<td>
			<select name="find">
			<option value="a" selected>По почте</option>
			<option value="b">Курьерской доставкой</option>
			<option value="с">Визит на фирму</option>
			</select>
		</td>
	</tr>
	<tr>
		<td colspan="2" align="center"><input name="" type="submit" value="Отправить заказ"></td>
	</tr>
</table>

</form>

</body></html>