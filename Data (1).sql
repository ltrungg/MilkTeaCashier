/*
use master
drop database MilkTeaCashier
go 
*/

CREATE DATABASE MilkTeaCashier
GO

USE MilkTeaCashier
GO

--Beverage
--BeverageCategory
--Account
--Bill
--BillInfo


CREATE TABLE Account 
(
	UserName NVARCHAR(100)PRIMARY KEY,
	DisplayedName NVARCHAR(100) NOT NULL DEFAULT N'Cafe',
	PassWord NVARCHAR(1000) NOT NULL DEFAULT 0,
	Type INT NOT NULL DEFAULT 0 --1: admin && 0: staff
)
GO

CREATE TABLE BeverageCategory
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên',
)
GO

CREATE TABLE Beverage
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên',
	idCategory INT NOT NULL,
	price FLOAT NOT NULL DEFAULT 0

	FOREIGN KEY (idCategory) REFERENCES dbo.BeverageCategory(id)  
)
GO

CREATE TABLE Bill
(
	id INT IDENTITY PRIMARY KEY,
	DateCheckIn DATE NOT NULL DEFAULT GETDATE(),
	EntryTime TIME NOT NULL DEFAULT CONVERT(TIME, GETDATE()),
	TotalPrice FLOAT
)
GO

CREATE TABLE BillInfo
(
	id INT IDENTITY PRIMARY KEY,
	idBill INT NOT NULL,
	idBeverage INT NOT NULL,
	count INT NOT NULL DEFAULT 0

	FOREIGN KEY (idBill) REFERENCES dbo.Bill(id),
	FOREIGN KEY (idBeverage) REFERENCES dbo.Beverage(id)  
)
GO

INSERT INTO Account (UserName, DisplayedName,Password,Type)
		VALUES 	(N'K18', N'FPT', N'1', 1)

INSERT INTO Account (UserName, DisplayedName,Password,Type)
		VALUES 	(N'staff', N'staff', N'1', 0)

GO
-- THÊM CATEGORY --
INSERT INTO BeverageCategory(name)
VALUES 
    (N'Tea'),
    (N'MilkTea'),
    (N'Topping'),
    (N'Juice'),
    (N'Cake');
GO
--THÊM ĐỒ UỐNG--
-- Chèn dữ liệu cho loại đồ uống 'Tea'
INSERT INTO Beverage(name, idCategory, price)
VALUES 
    (N'Black Tea', (SELECT id FROM BeverageCategory WHERE name = N'Tea'), 3.5),
    (N'Green Tea', (SELECT id FROM BeverageCategory WHERE name = N'Tea'), 3.0),
    (N'Oolong Tea', (SELECT id FROM BeverageCategory WHERE name = N'Tea'), 3.2),
    (N'Jasmine Tea', (SELECT id FROM BeverageCategory WHERE name = N'Tea'), 3.3);

-- Chèn dữ liệu cho loại đồ uống 'MilkTea'
INSERT INTO Beverage(name, idCategory, price)
VALUES 
    (N'Taro Milk Tea', (SELECT id FROM BeverageCategory WHERE name = N'MilkTea'), 4.5),
    (N'Matcha Milk Tea', (SELECT id FROM BeverageCategory WHERE name = N'MilkTea'), 4.2),
    (N'Thai Milk Tea', (SELECT id FROM BeverageCategory WHERE name = N'MilkTea'), 4.0),
    (N'Honey Milk Tea', (SELECT id FROM BeverageCategory WHERE name = N'MilkTea'), 4.3);

-- Chèn dữ liệu cho loại đồ uống 'Topping'
INSERT INTO Beverage(name, idCategory, price)
VALUES 
    (N'Boba Topping', (SELECT id FROM BeverageCategory WHERE name = N'Topping'), 1.0),
    (N'Pudding Topping', (SELECT id FROM BeverageCategory WHERE name = N'Topping'), 1.2),
    (N'Grass Jelly Topping', (SELECT id FROM BeverageCategory WHERE name = N'Topping'), 1.3),
    (N'Red Bean Topping', (SELECT id FROM BeverageCategory WHERE name = N'Topping'), 1.5);

-- Chèn dữ liệu cho loại đồ uống 'Juice'
INSERT INTO Beverage(name, idCategory, price)
VALUES 
    (N'Orange Juice', (SELECT id FROM BeverageCategory WHERE name = N'Juice'), 3.0),
    (N'Apple Juice', (SELECT id FROM BeverageCategory WHERE name = N'Juice'), 3.2),
    (N'Watermelon Juice', (SELECT id FROM BeverageCategory WHERE name = N'Juice'), 3.5),
    (N'Mango Juice', (SELECT id FROM BeverageCategory WHERE name = N'Juice'), 3.3);

-- Chèn dữ liệu cho loại đồ uống 'Cake'
INSERT INTO Beverage(name, idCategory, price)
VALUES 
    (N'Chocolate Cake', (SELECT id FROM BeverageCategory WHERE name = N'Cake'), 5.0),
    (N'Cheesecake', (SELECT id FROM BeverageCategory WHERE name = N'Cake'), 4.5),
    (N'Red Velvet Cake', (SELECT id FROM BeverageCategory WHERE name = N'Cake'), 4.8),
    (N'Tiramisu Cake', (SELECT id FROM BeverageCategory WHERE name = N'Cake'), 4.7);


-- THÊM BILL --
INSERT INTO Bill (DateCheckIn, EntryTime)
		VALUES  (GETDATE(), CONVERT(TIME, GETDATE()))
INSERT INTO Bill (DateCheckIn, EntryTime)
		VALUES  (GETDATE(), CONVERT(TIME, GETDATE()))
INSERT INTO Bill (DateCheckIn, EntryTime)
		VALUES  (GETDATE(), CONVERT(TIME, GETDATE()))
INSERT INTO Bill (DateCheckIn, EntryTime)
		VALUES  (GETDATE(), CONVERT(TIME, GETDATE()))

-- THÊM BILLINFO --

-- BILL 1 --
INSERT INTO BillInfo (idBill, idBeverage, count)
		VALUES(1, 1, 2)
INSERT INTO BillInfo (idBill, idBeverage, count)
		VALUES(1, 3, 4)
INSERT INTO BillInfo (idBill, idBeverage, count)
		VALUES(1, 5, 1)

-- BILL 2 --
INSERT INTO BillInfo (idBill, idBeverage, count)
		VALUES(2, 1, 2)
INSERT INTO BillInfo (idBill, idBeverage, count)
		VALUES(2, 6, 2)

-- BILL 3 --
INSERT INTO BillInfo (idBill, idBeverage, count)
		VALUES(3, 5, 2)


select * from Beverage
SELECT * from BillInfo
select * from Bill
select * from BillInfo