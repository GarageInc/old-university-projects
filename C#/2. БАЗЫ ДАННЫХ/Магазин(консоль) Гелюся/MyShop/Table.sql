CREATE TABLE Zakaz
(
	[idZakaz] INT NOT NULL PRIMARY KEY, 
    [idPokup] INT NULL FOREIGN KEY REFERENCES Pokupatel(idPokup), 
    [idTovar] INT NULL FOREIGN KEY REFERENCES Tovar(idTovar), 
    [amount] INT NULL, 
    [date] DATETIME NULL
)
