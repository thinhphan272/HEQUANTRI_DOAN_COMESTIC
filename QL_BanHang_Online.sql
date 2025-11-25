drop database QL_BANHANG_ONLINE
CREATE DATABASE QL_BANHANG_ONLINE
USE QL_BANHANG_ONLINE

--DELETE FROM OrderDetail;
--DELETE FROM GoodsReceiptNoteDetail;
--DELETE FROM ShoppingCartItem;
--DELETE FROM Discount;
--DELETE FROM Rating;
--DELETE FROM Orders;
--DELETE FROM ShoppingCart;
--DELETE FROM GoodsReceiptNote;
--DELETE FROM Product;
--DELETE FROM ProductType;
--DELETE FROM Brand;
--DELETE FROM Supplier;
--DELETE FROM Users;

DROP TABLE OrderDetail;
DROP TABLE Orders;
DROP TABLE Rating;
DROP TABLE ShoppingCartItem;
DROP TABLE ShoppingCart;
DROP TABLE Discount;
DROP TABLE GoodsReceiptNoteDetail;
DROP TABLE GoodsReceiptNote;
DROP TABLE Product;
DROP TABLE ProductType;
DROP TABLE Brand;
DROP TABLE Supplier;
DROP TABLE Users;

CREATE TABLE Supplier
(
	SupplierID char(10),
	Name nvarchar(50),
	constraint PK_Supplier primary key(SupplierID),
	IsDeleted int, -- Xóa mềm cho nhà cung cấp
	CreatedAt datetime,
	CreatedBy nvarchar(30),
	UpdatedAt datetime,
	UpdatedBy nvarchar(30)
)

create table GoodsReceiptNote
(
	GoodsReceiptNoteID char(10),
	SupplierID char(10),
	ReceiptDate datetime,
	IsDeleted int, -- Xóa mềm cho phiếu nhập
	CreatedAt datetime,
	CreatedBy nvarchar(30),
	UpdatedAt datetime,
	UpdatedBy nvarchar(30),
	constraint PK_GoodsReceiptNote primary key(GoodsReceiptNoteID),
	constraint FK_GoodsReceiptNote_Supplier foreign key(SupplierID) references Supplier(SupplierID),
	constraint CK_GoodsReceiptNote_ReceiptDate Check(ReceiptDate <= GetDate())
)

CREATE TABLE Brand
(
	BrandID char(10),
	BrandName nvarchar(50),
	Image nvarchar(500),
	Icon nvarchar(500),
	IsDeleted int, -- Xóa mềm cho hãng
	CreatedAt datetime,
	CreatedBy nvarchar(30),
	UpdatedAt datetime,
	UpdatedBy nvarchar(30),
	constraint PK_Brand primary key(BrandID)
)

CREATE TABLE ProductType
(
	ProductTypeID char(10),
	ProductTypeName nvarchar(50),
	ProductTypeParentID char(10),
	IsDeleted int, -- Xóa mềm
	constraint PK_ProductType primary key(ProductTypeID)
)

CREATE TABLE Product
(
	ProductID char(10),
	ProductTypeID char(10),
	ProductName nvarchar(255),
	BrandID char(10),
	Price money,
	Origin nvarchar(30),
	Description nvarchar(1000),
	Image nvarchar(500),
	Capacity float, 
	Quantity int,
	ExpirationDate datetime,
	IsAvailable int,  -- Xóa mềm
	CreatedAt datetime,
	CreatedBy nvarchar(30),
	UpdatedAt datetime,
	UpdatedBy nvarchar(30),
	constraint PK_Product primary key(ProductID),
	constraint FK_Product_ProductType foreign key(ProductTypeID) references ProductType(ProductTypeID),
	constraint FK_Product_Brand foreign key(BrandID) references Brand(BrandID),
	constraint CK_Product_Price Check(Price >= 0),
	constraint CK_Product_Quantity Check(Quantity >= 0)
)

CREATE TABLE Discount
(
	DiscountID char(10),
	ProductID char(10),
	DiscountName nvarchar(50),
	StartDate datetime,
	EndDate datetime,
	DiscountRate float,
	constraint PK_Discount primary key(DiscountID),
	constraint FK_Discount_Product foreign key(ProductID) references Product(ProductID),
	constraint CK_Discount_StartDate_EndDate Check(StartDate <= EndDate),
	constraint CK_Discount_DiscountRate Check(DiscountRate >= 0),
	CreatedAt datetime,
	CreatedBy nvarchar(30),
	UpdatedAt datetime,
	UpdatedBy nvarchar(30),
)

CREATE TABLE GoodsReceiptNoteDetail
(
	ProductID char(10),
	GoodsReceiptNoteID char(10),
	UnitPrice money,
	Quantity int,
	constraint PK_GoodsReceiptNoteDetail primary key(ProductID, GoodsReceiptNoteID),
	constraint FK_GoodsReceiptNoteDetail_ProductID foreign key(ProductID) references Product(ProductID),
	constraint FK_GoodsReceiptNoteDetail_GoodsReceiptNoteID foreign key(GoodsReceiptNoteID) references GoodsReceiptNote(GoodsReceiptNoteID),
	constraint CK_GoodsReceiptNoteDetail_UnitPrice Check(UnitPrice >= 0),
	constraint CK_GoodsReceiptNoteDetail_Quantity Check(Quantity >= 0)
)

CREATE TABLE Users
(
	UserID char(10),
	Name nvarchar(50) NOT NULL,
	Email varchar(50),
	Password varchar(64) NOT NULL,
	Gender nvarchar(10),
	Address nvarchar(100),
	CreatedAt datetime,
	CreatedBy nvarchar(30),
	UpdatedAt datetime,
	UpdatedBy nvarchar(30),
	IsEnabled int, -- Xóa mềm
	constraint PK_User primary key(UserID),
	constraint CK_Users_Gender Check(Gender in (N'Nữ', N'Nam')),
	constraint UNI_Email UNIQUE(Email)
)

CREATE TABLE ShoppingCart
(
	ShoppingCartID char(10),
	UserID char(10),
	constraint PK_ShoppingCart primary key(ShoppingCartID),
	constraint FK_ShoppingCart_UserID foreign key(UserID) references Users(UserID)
)

CREATE TABLE ShoppingCartItem
(
	ShoppingCartID char(10),
	ProductID char(10),
	Quantity int,
	constraint PK_ShoppingCartItem primary key(ShoppingCartID, ProductID),
	constraint FK_ShoppingCartItem_ShoppingCartID foreign key(ShoppingCartID) references ShoppingCart(ShoppingCartID),
	constraint FK_ShoppingCartItem_ProductID foreign key(ProductID) references Product(ProductID),
	constraint CK_ShoppingCartItem_Quantity Check(Quantity >= 0)
)

CREATE TABLE Rating
(
	RatingID char(10),
	UserID char(10),
	ProductID char(10),
	Star int,
	Comment nvarchar(500),
	CreatedAt datetime,
	CreatedBy nvarchar(30),
	UpdatedAt datetime,
	UpdatedBy nvarchar(30),
	constraint PK_Rating primary key(RatingID),
	constraint FK_Rating_UserID foreign key(UserID) references Users(UserID),
	constraint FK_Rating_ProductID foreign key(ProductID) references Product(ProductID),
	constraint CK_Rating_Star Check(Star >= 0)
)

CREATE TABLE Orders
(
	OrderID char(10),
	UserID char(10),
	OrderDate datetime,
	Address nvarchar(100),
	Status nvarchar(20),
	UserPaymentMethod nvarchar(30),
	CreatedAt datetime,
	CreatedBy nvarchar(30),
	UpdatedAt datetime,
	UpdatedBy nvarchar(30),
	constraint PK_Order primary key(OrderID),
	constraint FK_Orders_UserID foreign key(UserID) references Users(UserID),
	constraint CK_Orders_OrderDate Check(OrderDate <= GetDate())
)

CREATE TABLE OrderDetail
(
	OrderID char(10),
	ProductID char(10),
	Quantity int,
	UnitPrice money,
	constraint PK_OrderDetail primary key(OrderID, ProductID),
	constraint FK_OrderDetail_OrderID foreign key(OrderID) references Orders(OrderID),
	constraint FK_OrderDetail_ProductID foreign key(ProductID) references Product(ProductID),
	constraint CK_OrderDetail_Quantity Check(Quantity >= 0),
	constraint CK_OrderDetail_UnitPrice Check(UnitPrice >= 0)
)

--Nhập liệu
INSERT INTO Supplier
VALUES
('NCC001', N'Cty xuất nhập khẩu Nam Việt',0, NULL, NULL, NULL, NULL),
('NCC002', N'Cty xuất nhập khẩu Nam Việt',0, NULL, NULL, NULL, NULL),
('NCC003', N'Cty xuất nhập khẩu Phố Hoàng',0, NULL, NULL, NULL, NULL),
('NCC004', N'Cty TNHH Việt Hưng',0, NULL, NULL, NULL, NULL),
('NCC005', N'Cty liên doanh Việt-Nhật',0, NULL, NULL, NULL, NULL);

SET DATEFORMAT DMY;
INSERT INTO GoodsReceiptNote VALUES
('PH001', 'NCC001', '12/08/2025',0, NULL, NULL, NULL, NULL),
('PH002', 'NCC003', '11/07/2025',0, NULL, NULL, NULL, NULL),
('PH003', 'NCC002', '12/10/2024',0, NULL, NULL, NULL, NULL),
('PH004', 'NCC002', '12/08/2024',0, NULL, NULL, NULL, NULL),
('PH005', 'NCC004', '30/12/2024',0, NULL, NULL, NULL, NULL);

INSERT INTO Brand VALUES
('TH001', 'Cocoon','brandCOCOON.jpg','1593168007the-coc.jpg',0,  NULL, NULL, NULL, NULL),
('TH002', 'Decumar', 'brandSunplay.jpg','Sunplay-Skin-Aqua1662624548.jpg',0, NULL, NULL, NULL, NULL),
('TH003', 'Simple', 'brandSimple.jpg','brandSkin10041678681869.jpg',0, NULL, NULL, NULL, NULL),
('TH004', 'Cerave', 'brandCerave.jpg','brandMastige-logo1714194346.jpg',0, NULL, NULL, NULL, NULL),
('TH005', 'Vaseline','brandVaseline.jpg','labelVASELINE.jpg',0, NULL, NULL, NULL, NULL);
INSERT INTO Brand VALUES
('TH006', 'Anessa','brandAnessa.jpg', 'labelAnessa.jpg', 0, NULL, NULL, NULL, NULL);
INSERT INTO Brand VALUES
('TH007', 'Bioderma','brandBioderma.jpg', 'labelBioderma.jpg', 0, NULL, NULL, NULL, NULL);
INSERT INTO Brand VALUES
('TH008', 'Klairs','brandKlairs.jpg', 'labelKlairs.jpg', 0, NULL, NULL, NULL, NULL);
INSERT INTO ProductType VALUES
('LSP001', N'Chăm Sóc Da Mặt', null, 0),
('LSP002', N'Sữa rửa mặt', 'LSP001', 0),
('LSP003', N'Tẩy Trang Mặt', 'LSP001', 0),
('LSP004', N'Tẩy Tế Bào Chết Da Mặt', 'LSP001', 0),

('LSP005', N'Trang Điểm', null, 0),
('LSP006', N'Phấn phủ', 'LSP005', 0),
('LSP007', N'Kem lót', 'LSP005', 0),
('LSP008', N'Má Hồng', 'LSP005', 0),

('LSP009', N'Chăm Sóc Tóc Và Da Đầu', null, 0),
('LSP010', N'Dầu gội', 'LSP009', 0),
('LSP011', N'Dầu xả', 'LSP009', 0)

INSERT INTO ProductType VALUES
('LSP012', N'Chống nắng', null, 0),
('LSP013', N'Chống nắng da mặt', 'LSP012', 0)

SELECT * FROM PRODUCT
INSERT INTO Product VALUES
('SP001', 'LSP002', N'Combo 2 Nước Tẩy Trang Bí Đao Cocoon Làm Sạch & Giảm Dầu 500ml', 'TH001', 100000, N'Việt Nam',N'Nước Tẩy Trang Bí Đao Cocoon Winter Melon Micellar Water mới từ thương hiệu mỹ phẩm thuần chay Cocoon là sản phẩm tẩy trang được thiết kế chuyên biệt dành cho da dầu và da mụn nhạy cảm. Với công nghệ Micellar, nước tẩy trang bí đao giúp làm sạch hiệu quả lớp trang điểm, bụi bẩn và dầu thừa, mang lại làn da sạch hoàn toàn và dịu nhẹ.', 'img_SP001.jpg', 125.0, 300, '10/09/2026',0, NULL, NULL, NULL, NULL),

('SP002', 'LSP010', N'Dầu Gội Bưởi Cocoon Không Sulfate Và Giảm Gãy Rụng 500ml', 'TH001', 150000, N'Việt Nam', N'Dầu Gội Bưởi Và Dầu Xả Bưởi Cocoon Không Sulfate Và Giảm Gãy Rụng là sản phẩm dầu gội, dầu xả đến từ thương hiệu mỹ phẩm thuần chay Cocoon của Việt Nam, với công thức làm sạch dịu nhẹ không chứa sulfate, phù hợp với mọi loại da đầu, đặc biệt là da đầu nhạy cảm. Thay vì dùng sulfate để làm sạch, Cocoon dùng các thành phần tự nhiên khác như dầu cọ, dầu dừa và bắp để làm sạch hiệu quả mà vẫn lành tính. Ngoài ra, sản phẩm còn có các thành phần khác như tinh dầu từ vỏ bưởi, Vitamin B5, hoạt chất dưỡng ẩm Xylishine giúp giảm gãy rụng và kích thích sự phát triển của tóc.', 'img_SP002.jpg', 500.0, 100, '20/07/2026',0, NULL, NULL, NULL, NULL),

('SP003', 'LSP011', N'Sữa Rửa Mặt CeraVe Sạch Sâu Cho Da Thường Đến Da Dầu 473ml', 'TH004', 300000, N'Mỹ', N'Sữa Rửa Mặt Cerave Sạch Sâu là sản phẩm sữa rửa mặt đến từ thương hiệu mỹ phẩm Cerave của Mỹ, với sự kết hợp của ba Ceramides thiết yếu, Hyaluronic Acid sản phẩm giúp làm sạch và giữ ẩm cho làn da mà không ảnh hưởng đến hàng rào bảo vệ da mặt và cơ thể.', 'img_SP003.jpg', 473.0, 200, '20/05/2027',0, NULL, NULL, NULL, NULL),

('SP004', 'LSP011', N'Dầu Xả Bưởi Cocoon Cung Cấp Dưỡng Chất & Độ Ẩm 310ml', 'TH001', 200000, N'Việt Nam', N'Dầu Gội Và Xả OGX Biotin & Collagen Làm Dày Tóc là sản phẩm dầu gội và dầu xả đến từ thương hiệu chăm sóc tóc OGX (thuộc tập đoàn Johnson & Johnson) rất được ưa chuộng tại Mỹ, nay đã chính thức có mặt tại Việt Nam. Dầu gội có công thức kết hợp giữa Biotin (Vitamin B7) và Collagen - hai dưỡng chất tuyệt vời dành cho mái tóc yếu, tổn thương, dễ gãy, rụng; có khả năng thẩm thấu vào từng sợi tóc giúp mái tóc dày hơn, bồng bềnh và luôn chắc khỏe.', 'img_SP004.jpg', 310.0, 120, '15/10/2026',0, NULL, NULL, NULL, NULL),

('SP005', 'LSP002', N'Sữa Rửa Mặt Decumar Sạch Sâu Dịu Nhẹ, Sáng Da Ngừa Mụn 50g', 'TH002', 100000, N'Việt Nam', N'Sữa Rửa Mặt Decuma Sạch Sâu Dịu Nhẹ, Sáng Da Ngừa Mụn 50g là sản phẩm sữa rửa mặt đến từ thương hiệu Decumar - Việt Nam. Sản phẩm với công thức kết hợp giữa công nghệ Nano THC từ nghệ và các dưỡng chất tự nhiên, giúp làm sạch sâu da mặt, cân bằng độ pH, ngăn ngừa mụn và mang lại làn da sáng mịn.', 'img_SP005.jpg', 50, 300, '15/12/2026',0, NULL, NULL, NULL, NULL);

INSERT INTO Product VALUES
('SP006', 'LSP013', N'Sữa Chống Nắng Anessa Dưỡng Da Kiềm Dầu 60ml', 'TH006', 700000, N'Nhật Bản',NULL, 'img_SP006.jpg', 60.0, 300, '10/09/2026',0, NULL, NULL, NULL, NULL),
('SP007', 'LSP013', N'Kem Chống Nắng Bioderma Dành Cho Da Dầu, Mụn SPF30 40ml', 'TH007', 500000, N'Pháp',NULL, 'img_SP007.jpg', 40.0, 300, '10/09/2026',0, NULL, NULL, NULL, NULL),
('SP008', 'LSP013', N'Sữa Chống Nắng La Roche-Posay Cho Da Dầu Mụn 50ml', 'TH007', 500000, N'Pháp',NULL, 'img_SP008.jpg', 40.0, 300, '10/09/2026',0, NULL, NULL, NULL, NULL),
('SP009', 'LSP004', N'Tẩy Da Chết Mặt Cocoon Cà Phê Đắk Lắk 150ml', 'TH001', 120000, N'Việt Nam',NULL, 'img_SP009.jpg', 150.0, 300, '10/09/2026',0, NULL, NULL, NULL, NULL),
('SP010', 'LSP002', N'Sữa Rửa Mặt Klairs Dưỡng Ẩm, Dịu Nhẹ, Sạch Sâu 140ml', 'TH008', 520000, N'Hàn Quốc',NULL, 'img_SP010.jpg', 140.0, 300, '10/09/2026',0, NULL, NULL, NULL, NULL)

INSERT INTO Discount VALUES
('GG001', 'SP001', N'Giảm 20% khi mua trên 500k', '10/11/2025', '10/02/2026', 20, NULL, NULL, NULL, NULL),
('GG002', 'SP002', N'Giảm 18% khi mua trên 2 sản phẩm', '10/10/2025', '10/01/2026', 18, NULL, NULL, NULL, NULL),
('GG003', 'SP003', N'Giảm 12% khi mua trên 2 sản phẩm', '12/12/2025', '12/01/2026', 12, NULL, NULL, NULL, NULL),
('GG004', 'SP004', N'Giảm 10% khi mua trên 2 sản phẩm', '10/09/2025', '10/12/2025', 10, NULL, NULL, NULL, NULL),
('GG005', 'SP005', N'Giảm 30% khi mua trên 3 sản phẩm', '10/11/2025', '10/2/2026', 30, NULL, NULL, NULL, NULL);

INSERT INTO GoodsReceiptNoteDetail VALUES
('SP001', 'PH001', 100000, 300 ),
('SP002', 'PH002', 150000, 100 ),
('SP003', 'PH003', 300000, 200 ),
('SP004', 'PH004', 100000, 120 ),
('SP005', 'PH005', 100000, 300 )

INSERT INTO Users VALUES
('US001',  N'Trần Văn Đại', 'daitran001@gmail.com', 'vandai123!', N'Nam', N'TP. Hồ Chí Minh', NULL, NULL, NULL, NULL, 0),
('US002', N'Nguyễn Thảo Linh', 'nguyenlinh002@gmail.com', 'thaolinh123!', N'Nữ', N'Tây Ninh', NULL, NULL, NULL, NULL, 0),
('US003', N'Nguyễn Minh Anh', 'anhnguyen003@gmail.com', 'minhanh123!', N'Nữ', N'TP. Hồ Chí Minh', NULL, NULL, NULL, NULL, 0),
('US004', N'Lý Quốc Phong', 'phongly004@gmail.com', 'quocphong123!', N'Nam', N'An Giang', NULL, NULL, NULL, NULL, 0),
('US005', N'Nguyễn Minh Tuấn', 'tuannguyen005@gmail.com', 'minhtuan123!', N'Nam', N'TP. Hồ Chí Minh', NULL, NULL, NULL, NULL, 0)

INSERT INTO ShoppingCart VALUES
('SC001', 'US002'),
('SC002', 'US003'),
('SC003', 'US004')

INSERT INTO ShoppingCartItem VALUES
('SC001', 'SP003', 2),
('SC001', 'SP002', 1),
('SC002', 'SP003', 1),
('SC002', 'SP001', 3),
('SC003', 'SP005', 2),
('SC003', 'SP004', 1),
('SC003', 'SP003', 5)

INSERT INTO Rating VALUES
('RT001', 'US002', 'SP003', 5, N'Sữa rửa mặt nhà CeraVe mãi đỉnh <3', NULL, NULL, NULL, NULL),
('RT002', 'US002', 'SP002', 4, N'Dầu gội hãng Cocoon rất hợp với tóc mình, sản phẩm rất tốt. Có điều giao hàng hơi trễ!', NULL, NULL, NULL, NULL),
('RT003', 'US003', 'SP001', 3, N'Sửa rửa mặt hãng Cocoon sài tạm ổn, bị kích ứng nhẹ', NULL, NULL, NULL, NULL),
('RT004', 'US003', 'SP003', 5, N'Dòng sữa rửa mặt hãng CeraVe luôn là sự lựa chọn hàng đầu của mình', NULL, NULL, NULL, NULL),
('RT005', 'US004', 'SP004', 5, N'Dầu xả nhà Cocoon thơm lắm nha mọi người. Recomment nên sài nhaaa', NULL, NULL, NULL, NULL),
('RT006', 'US004', 'SP005', 4, N'Sửa rửa mặt bên decumar sài cũng ổn, mờ thâm nhẹ, hiệu quả thì chắc phải kiên trì sài mới thấy', NULL, NULL, NULL, NULL)

INSERT INTO Orders VALUES
('OD001', 'US002', '09/09/2025', N'Tây Ninh', N'Chờ giao hàng', N'Trả tiền khi nhận hàng (COD)', NULL, NULL, NULL, NULL ),
('OD002', 'US003', '10/09/2025', N'TP. Hồ Chí Minh', N'Chờ giao hàng', N'Trả tiền khi nhận hàng (COD)', NULL, NULL, NULL, NULL ),
('OD003', 'US002', '12/09/2025', N'Tây Ninh', N'Chờ lấy hàng', N'Trả qua ví Momo', NULL, NULL, NULL, NULL ),
('OD004', 'US004', '13/09/2025', N'An Giang', N'Chờ lấy hàng', N'Trả tiền khi nhận hàng (COD)', NULL, NULL, NULL, NULL ),
('OD005', 'US004', '14/09/2025', N'An Giang', N'Chờ xác nhận', N'Trả tiền khi nhận hàng (COD)', NULL, NULL, NULL, NULL )

INSERT INTO OrderDetail VALUES
('OD001', 'SP003', 2, 600000),
('OD002', 'SP001', 3, 300000),
('OD002', 'SP003', 1, 300000),
('OD003', 'SP002', 1, 150000),
('OD004', 'SP005', 2, 200000),
('OD005', 'SP004', 1, 100000)

SELECT * FROM Product



