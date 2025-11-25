	USE QL_BANHANG_ONLINE

--CRUD Nhà cung cấp
go
CREATE PROC P_AddSupplier
	@SupplierID char(10),
    @NameSupplier NVARCHAR(50),
	@CreatedUser NVARCHAR(50)
AS
BEGIN
    -- Thêm vào bảng
    INSERT INTO Supplier (SupplierID, Name, CreatedAt, CreatedBy)
    VALUES (@SupplierID, @NameSupplier, GETDATE(), @CreatedUser)
END;

GO
CREATE PROCEDURE P_UpdateSupplier
	@SupplierID char(10),
    @NameSupplier NVARCHAR(50),
	@UpdatedUser NVARCHAR(50)
AS
BEGIN
    -- Cập nhật bảng
    UPDATE Supplier
    Set Name = @NameSupplier, UpdatedAt = GETDATE(), UpdatedBy = @UpdatedUser
	WHERE SupplierID = @SupplierID
END;

GO
CREATE PROCEDURE P_DeleteSupplier
	@SupplierID char(10)
AS
BEGIN
    UPDATE Supplier
	SET IsDeleted = 1
	WHERE SupplierID = @SupplierID
END;
GO
EXEC P_AddSupplier N'Cty của thinh', 'thinh', 'thinh';
SELECT * FROM SUPPLIER
EXEC P_UpdateSupplier 'NCC007', N'Cty của thịnh nè', 'thinh';
EXEC P_DeleteSupplier 'NCC007';

GO

--CRUD PHIẾU NHẬP
CREATE PROC P_Add_GoodsReceiptNote
	@GoodsReceiptNoteID char(10),
	@SupplierID char(10),
	@ReceiptDate datetime,
	@CreatedUser NVARCHAR(50)
AS
BEGIN
	INSERT INTO GoodsReceiptNote (GoodsReceiptNoteID, SupplierID, ReceiptDate, CreatedAt, CreatedBy)
	VALUES (@GoodsReceiptNoteID, @SupplierID, @ReceiptDate, GETDATE(), @CreatedUser)
END
GO
CREATE PROC P_Update_GoodsReceiptNote
	@GoodsReceiptNoteID char(10),
	@SupplierID char(10),
	@ReceiptDate datetime,
	@UpdatedUser NVARCHAR(50)
AS
BEGIN
	UPDATE GoodsReceiptNote
	SET SupplierID = @SupplierID,
	ReceiptDate = @ReceiptDate,
	UpdatedAt = GETDATE(),
	UpdatedBy = @UpdatedUser
	WHERE GoodsReceiptNoteID = @GoodsReceiptNoteID
END

GO
CREATE PROC P_Delete_GoodsReceiptNote
	@GoodsReceiptNoteID char(10)
AS
BEGIN
	UPDATE GoodsReceiptNote
	SET IsDeleted = 1
	WHERE GoodsReceiptNoteID = @GoodsReceiptNoteID
END

go

--CRUD Brand
go
CREATE PROC P_Add_Brand
	@BrandID char(10),
	@BrandName nvarchar(50),
	@Image nvarchar(100),
	@CreatedUser NVARCHAR(50)
AS
BEGIN
	INSERT INTO Brand(BrandID, BrandName, Image, CreatedAt, CreatedBy)
	VALUES(@BrandID, @BrandName, @Image, GETDATE(), @CreatedUser)
END
go
CREATE PROC P_Update_Brand
	@BrandID char(10),
	@BrandName nvarchar(50),
	@Image nvarchar(100),
	@UpdatedUser NVARCHAR(50)
AS
BEGIN
	UPDATE Brand
	SET BrandName = @BrandName,
	Image = @Image,
	UpdatedAt = GETDATE(),
	UpdatedBy = @UpdatedUser
	WHERE BrandID = @BrandID
END
go
CREATE PROC P_Delete_Brand
	@BrandID char(10)
AS
BEGIN
	UPDATE Brand
	SET IsDeleted = 1
	WHERE BrandID = @BrandID
END
go

--CRUD ProductType
go
CREATE PROC P_Add_ProductType
	@ProductTypeID char(10),
	@ProductTypeName nvarchar(50)
AS
BEGIN
	INSERT INTO ProductType(ProductTypeID, ProductTypeName)
	VALUES(@ProductTypeID, @ProductTypeName)
END
go
CREATE PROC P_Update_ProductType
	@ProductTypeID char(10),
	@ProductTypeName nvarchar(50)
AS
BEGIN
	UPDATE ProductType
	SET ProductTypeName = @ProductTypeName
	WHERE ProductTypeID = @ProductTypeID
END
go
CREATE PROC P_Delete_ProductType
	@ProductTypeID char(10)
AS
BEGIN
	UPDATE ProductType
	SET IsDeleted = 1
	WHERE ProductTypeID = @ProductTypeID
END
go

-- CRUB cho Product
CREATE PROC P_Add_Product
	@ProductID char(10),
	@ProductTypeID char(10),
	@ProductName nvarchar(30),
	@BrandID char(10),
	@Price money,
	@Origin nvarchar(30),
	@Description nvarchar(1000),
	@Image nvarchar(100),
	@Capacity float, 
	@Quantity int,
	@ExpirationDate datetime,
	@CreatedUser nvarchar(30)
AS
BEGIN
	IF NOT EXISTS(SELECT * FROM Product WHERE ProductID = @ProductID)
		BEGIN
			INSERT INTO Product(ProductID, ProductTypeID, ProductName, BrandID, Price, Origin, Description, Image, Capacity, Quantity, ExpirationDate, CreatedAt, CreatedBy, IsAvailable)
	VALUES(@ProductID, @ProductTypeID, @ProductName, @BrandID, @Price, @Origin, @Description, @Image, @Capacity, @Quantity, @ExpirationDate, GETDATE(), @CreatedUser, 0)
		END
END
go
CREATE PROC P_Update_Product
	@ProductID char(10),
	@ProductTypeID char(10),
	@ProductName nvarchar(30),
	@BrandID char(10),
	@Price money,
	@Origin nvarchar(30),
	@Description nvarchar(1000),
	@Image nvarchar(100),
	@Capacity float, 
	@Quantity int,
	@ExpirationDate datetime,
	@UpdatedUser nvarchar(30)
AS
BEGIN
	UPDATE Product
	SET ProductTypeID = @ProductTypeID,
		ProductName = @ProductName,
		BrandID = @BrandID,
		Price = @Price,
		Origin = @Origin,
		Description = @Description,
		Image = @Image,
		Capacity = @Capacity,
		Quantity = @Quantity,
		ExpirationDate = @ExpirationDate,
		UpdatedAt = GETDATE(),
		UpdatedBy = @UpdatedUser
	WHERE ProductID = @ProductID
END
go
CREATE PROC P_Disable_Product
	@ProductID char(10)
AS
BEGIN
	UPDATE Product
	SET IsAvailable = 1
	WHERE ProductID = @ProductID
END
go
CREATE PROC P_Enable_Product
	@ProductID char(10)
AS
BEGIN
	UPDATE Product
	SET IsAvailable = 0
	WHERE ProductID = @ProductID
END

go
--CRUB Discount
CREATE PROC P_Add_Discount
	@DiscountID char(10),
	@ProductID char(10),
	@DiscountName nvarchar(50),
	@StartDate datetime,
	@EndDate datetime,
	@DiscountRate float,
	@CreatedUser nvarchar(30)
AS
BEGIN
	INSERT INTO Discount(DiscountID, ProductID, DiscountName, StartDate, EndDate, DiscountRate, CreatedAt, CreatedBy)
	VALUES(@DiscountID, @ProductID, @DiscountName, @StartDate, @EndDate, @DiscountRate, GETDATE(), @CreatedUser)
END
GO
CREATE PROC P_Update_Discount
	@DiscountID char(10),
	@ProductID char(10),
	@DiscountName nvarchar(50),
	@StartDate datetime,
	@EndDate datetime,
	@DiscountRate float,
	@UpdatedUser nvarchar(30)
AS
BEGIN
	UPDATE Discount
	SET ProductID = @ProductID,
		DiscountName = @DiscountName,
		StartDate = @StartDate,
		EndDate = @EndDate,
		DiscountRate = @DiscountRate,
		UpdatedAt = GETDATE(),
		UpdatedBy = @UpdatedUser
	WHERE DiscountID = @DiscountID
END
GO
CREATE PROC P_Delete_Discount
	@DiscountID char(10)
AS
BEGIN
	DELETE FROM Discount
	WHERE DiscountID = @DiscountID
END
GO

GO
--CRUD GoodsReceiptNoteDetail
CREATE PROC P_Add_GoodsReceiptNoteDetail
	@ProductID char(10),
	@GoodsReceiptNoteID char(10),
	@UnitPrice money,
	@Quantity int
AS
BEGIN
	INSERT INTO GoodsReceiptNoteDetail(ProductID, GoodsReceiptNoteID, UnitPrice, Quantity)
	VALUES(@ProductID, @GoodsReceiptNoteID, @UnitPrice, @Quantity)
END
GO

CREATE PROC P_Update_GoodsReceiptNoteDetail
	@ProductID char(10),
	@GoodsReceiptNoteID char(10),
	@UnitPrice money,
	@Quantity int
AS
BEGIN
	UPDATE GoodsReceiptNoteDetail
	SET UnitPrice = @UnitPrice,
		Quantity = @Quantity
	WHERE ProductID = @ProductID AND GoodsReceiptNoteID = @GoodsReceiptNoteID
END
GO

CREATE PROC P_Delete_GoodsReceiptNoteDetail
	@ProductID char(10),
	@GoodsReceiptNoteID char(10)
AS
BEGIN
	DELETE FROM GoodsReceiptNoteDetail
	WHERE ProductID = @ProductID AND GoodsReceiptNoteID = @GoodsReceiptNoteID
END
go


--CRUD User
go
CREATE PROC P_Add_User
	@UserID char(10),
	@Name nvarchar(50),
	@Email varchar(50),
	@Password varchar(64),
	@Gender nvarchar(10),
	@Address nvarchar(100),
	@CreatedUser nvarchar(30)
AS
BEGIN
	IF Not Exists(SELECT * FROM Users WHERE Email = @Email)
		BEGIN
			INSERT INTO Users(UserID,  Name, Email, Password, Gender, Address, IsEnabled, CreatedAt, CreatedBy)
			VALUES(@UserID, @Name, @Email, @Password, @Gender, @Address, 0, GETDATE(), @CreatedUser)
		END
END
GO
ALTER PROC P_Update_User
	@UserID char(10),
	@Name nvarchar(50),
	@Email varchar(50),
	@Password varchar(64),
	@Gender nvarchar(10),
	@Address nvarchar(100),
	@UpdatedUser nvarchar(30)
AS
BEGIN
	IF NOT EXISTS(SELECT * FROM Users WHERE Email = @Email)
		BEGIN
			UPDATE Users
			SET Name = @Name,
				Email = @Email,
				Password = @Password,
				Gender = @Gender,
				Address = @Address,
				UpdatedAt = GETDATE(),
				UpdatedBy = @UpdatedUser
			WHERE UserID = @UserID
		END
END
go

CREATE PROC P_Deactive_User
	@UserID char(10),
	@UpdatedUser nvarchar(30)
AS
BEGIN
	UPDATE Users
	SET IsEnabled = 1,
	UpdatedAt = GETDATE(),
	UpdatedBy = @UpdatedUser
	WHERE UserID = @UserID
END
go

CREATE PROC P_Restore_User
	@UserID char(10),
	@UpdatedUser nvarchar(30)
AS
BEGIN
	UPDATE Users
	SET IsEnabled = 0,
	UpdatedAt = GETDATE(),
	UpdatedBy = @UpdatedUser
	WHERE UserID = @UserID
END
go
ALTER PROCEDURE P_LOGIN @Email VARCHAR(50), @Password VARCHAR(64)
AS
BEGIN 
	IF EXISTS (SELECT *
				FROM Users 
				WHERE Email = @Email AND PASSWORD = @Password AND IsEnabled = 1)
		return 1
	ELSE
		return 0
END
go


--CRUD Shopping cart
go
CREATE PROC P_Add_ShoppingCart
	@ShoppingCartID char(10),
	@UserID char(10)
AS
BEGIN
	INSERT INTO ShoppingCart(ShoppingCartID, UserID)
	VALUES(@ShoppingCartID, @UserID)
END
go
CREATE PROC P_Delete_ShoppingCart
	@ShoppingCartID char(10)
AS
BEGIN
	DELETE FROM ShoppingCartItem
	WHERE ShoppingCartID = @ShoppingCartID

	DELETE FROM ShoppingCart
	WHERE ShoppingCartID = @ShoppingCartID
END
SELECT * FROM ShoppingCart
go

--CRUD ShoppingcartItem
go
ALTER PROC P_Add_ShoppingCartItem
	@ShoppingCartID char(10),
	@ProductID char(10),
	@Quantity int
AS
BEGIN
DECLARE @StockQuantity INT
	SELECT @StockQuantity = Quantity
	FROM Product p
	WHERE p.ProductID = @ProductID
	if(@StockQuantity > 0)
		BEGIN
			INSERT INTO ShoppingCartItem(ShoppingCartID, ProductID, Quantity)
			VALUES(@ShoppingCartID, @ProductID, @Quantity)
		END
END

go
ALTER PROC P_Update_ShoppingCartItem
	@ShoppingCartID char(10),
	@ProductID char(10),
	@Quantity int
AS
BEGIN
DECLARE @StockQuantity INT
	SELECT @StockQuantity = Quantity
	FROM Product p
	WHERE p.ProductID = @ProductID
		if(@StockQuantity > 0)
		BEGIN
			UPDATE ShoppingCartItem
			SET Quantity = @Quantity
			WHERE ShoppingCartID = @ShoppingCartID AND ProductID = @ProductID
		END
END

go
CREATE PROC P_Delete_ShoppingCartItem
	@ShoppingCartID char(10),
	@ProductID char(10)
AS
BEGIN
	DELETE FROM ShoppingCartItem
	WHERE ShoppingCartID = @ShoppingCartID AND ProductID = @ProductID
END
go

--CRUD Rating
CREATE PROC P_Add_Rating
	@RatingID char(10),
	@UserID char(10),
	@ProductID char(10),
	@Star int,
	@Comment nvarchar(500),
	@CreatedUser nvarchar(30)
AS
BEGIN
	INSERT INTO Rating(RatingID, UserID, ProductID, Star, Comment, CreatedAt, CreatedBy)
	VALUES(@RatingID, @UserID, @ProductID, @Star, @Comment, GETDATE(), @CreatedUser)
END
go
CREATE PROC P_Update_Rating
	@RatingID char(10),
	@UserID char(10),
	@ProductID char(10),
	@Star int,
	@Comment nvarchar(500),
	@UpdatedUser nvarchar(30)
AS
BEGIN
	UPDATE Rating
	SET UserID = @UserID,
		ProductID = @ProductID,
		Star = @Star,
		Comment = @Comment,
		UpdatedAt = GETDATE(),
		UpdatedBy = @UpdatedUser
	WHERE RatingID = @RatingID
END
go
CREATE PROC P_Delete_Rating
	@RatingID char(10)
AS
BEGIN
	DELETE FROM Rating
	WHERE RatingID = @RatingID
END

go
--CRUD Order
go
CREATE PROC P_Add_Order
	@OrderID char(10),
	@UserID char(10),
	@OrderDate datetime,
	@Address nvarchar(100),
	@Status nvarchar(20),
	@UserPaymentMethod nvarchar(30),
	@CreatedUser nvarchar(30)
AS
BEGIN
	INSERT INTO Orders(OrderID, UserID, OrderDate, Address, Status, UserPaymentMethod, CreatedAt, CreatedBy)
	VALUES(@OrderID, @UserID, @OrderDate, @Address, @Status, @UserPaymentMethod, GETDATE(), @CreatedUser)
END
go
CREATE PROC P_Update_Order
	@OrderID char(10),
	@UserID char(10),
	@OrderDate datetime,
	@Address nvarchar(100),
	@Status nvarchar(20),
	@UserPaymentMethod nvarchar(30),
	@UpdatedUser nvarchar(30)
AS
BEGIN
	UPDATE Orders
	SET UserID = @UserID,
		OrderDate = @OrderDate,
		Address = @Address,
		Status = @Status,
		UserPaymentMethod = @UserPaymentMethod,
		UpdatedAt = GETDATE(),
		UpdatedBy = @UpdatedUser
	WHERE OrderID = @OrderID
END
go
CREATE PROC P_Cancel_Order
	@OrderID char(10)
AS
BEGIN
	UPDATE Orders
	SET Status = N'Đã huỷ'
	WHERE OrderID = @OrderID
END
go
CREATE PROC P_Delete_Order
	@OrderID char(10)
AS
BEGIN
	DELETE FROM OrderDetail
	WHERE OrderID = @OrderID

	DELETE FROM Orders
	WHERE OrderID = @OrderID
END
go
--CRUD OrderDetail
CREATE PROC P_Add_OrderDetail
	@OrderID char(10),
	@ProductID char(10),
	@Quantity int,
	@UnitPrice money
AS
BEGIN
	INSERT INTO OrderDetail(OrderID, ProductID, Quantity, UnitPrice)
	VALUES(@OrderID, @ProductID, @Quantity, @UnitPrice)
END
go
CREATE PROC P_Update_OrderDetail
	@OrderID char(10),
	@ProductID char(10),
	@Quantity int,
	@UnitPrice money
AS
BEGIN
	UPDATE OrderDetail
	SET Quantity = @Quantity,
		UnitPrice = @UnitPrice
	WHERE OrderID = @OrderID AND ProductID = @ProductID
END
GO

--CRUD OrderDetail
go
CREATE PROC P_Delete_OrderDetail
	@OrderID char(10),
	@ProductID char(10)
AS
BEGIN
	DELETE FROM OrderDetail
	WHERE OrderID = @OrderID AND ProductID = @ProductID
END

--Procedure đặt hàng
GO
CREATE PROC P_DATHANG
	@OrderID char(10),
	@UserID char(10),
	@OrderDate datetime,
	@Address nvarchar(100),
	@Status nvarchar(20),
	@UserPaymentMethod nvarchar(30),
	@CreatedUser nvarchar(30)
AS
BEGIN
	BEGIN TRANSACTION 
	--Lấy ShoppingCartID từ UserID
		DECLARE @ShoppingCartID CHAR(10) 
		SELECT @ShoppingCartID = (SELECT ShoppingCartID 
		FROM ShoppingCart 
		WHERE UserID = @UserID)

		IF @ShoppingCartID IS NULL
		BEGIN
			PRINT N'Người dùng/giỏ hàng không tồn tại'
			ROLLBACK TRAN
			RETURN
		END

		--Kiểm tra giỏ hàng có tồn tại sản phẩm không
		IF NOT EXISTS(SELECT * FROM ShoppingCartItem spci, ShoppingCart spc WHERE spci.ShoppingCartID = spc.ShoppingCartID AND spc.UserID = @UserID)
		BEGIN
			PRINT N'Không có sản phẩm trong giỏ hàng!'
			ROLLBACK TRAN
			RETURN
		END
		--Đặt hàng
		EXEC P_Add_Order @OrderID, @UserID, @OrderDate, @Address, @Status, @UserPaymentMethod, @CreatedUser
		--tạo cursor
		DECLARE CUR_DATHANG CURSOR
		FOR
			SELECT p.ProductID, spci.Quantity, p.Price
			FROM ShoppingCart spc, ShoppingCartItem spci, Product p
			WHERE spc.ShoppingCartID = spci.ShoppingCartID 
			AND p.ProductID = spci.ProductID 
			AND spc.UserID = @UserID

		DECLARE @ProductID char(10), @Quantity int, @Price money
		
		OPEN CUR_DATHANG
		FETCH NEXT FROM CUR_DATHANG INTO @ProductID, @Quantity, @Price
		
		WHILE @@FETCH_STATUS = 0
		BEGIN
		DECLARE @CurrentQuantity int = 
		(SELECT p.Quantity FROM Product p
		WITH (UPDLOCK) WHERE p.ProductID = @ProductID)
			IF @CurrentQuantity < @Quantity
			BEGIN
				PRINT N'Không đủ hàng tồn kho!'
				ROLLBACK TRAN
				RETURN
			END
			
			--Thực thi việc đặt hàng
			EXEC P_Add_OrderDetail @OrderID, @ProductID, @Quantity, @Price

			--Cập nhật số lượng tồn kho
			UPDATE Product
			SET Quantity = Quantity - @Quantity
			WHERE ProductID = @ProductID

			--Xóa sản phẩm khỏi giỏ hàng
			EXEC P_Delete_ShoppingCartItem @ShoppingCartID, @ProductID
			FETCH NEXT FROM CUR_DATHANG INTO @ProductID, @Quantity, @Price
		END
		CLOSE CUR_DATHANG
		DEALLOCATE CUR_DATHANG
		COMMIT TRAN
		PRINT N'Đặt hàng thành công'
END
go

EXEC P_DATHANG 'OD006', 'US002', '25/10/2025', 'TP.HCM', N'Đặt hàng', N'Chuyển khoản', 'SP006', 5, 100000, N'thinh'

SET DATEFORMAT DMY
EXEC P_Add_Product
'SP006', 'LSP001', N'Sữa rửa mặt Decumar', 'TH002', 100000, N'Việt Nam', NULL, NULL, 50, 10, '15/12/2026', N'thinh'
select * from Product
select * from Orders
select * from OrderDetail
select * from Users

go


EXEC P_Restore_User 'US001', N'thinh'
EXEC P_LOGIN 'daitran001@gmail.com', 'vandai123!'

--Thêm phiếu nhập (Transaction)
go
ALTER PROC P_ThemPN 
	@GoodsReceiptNoteID char(10),
	@SupplierID char(10),
	@ReceiptDate datetime,
	@CreatedUser NVARCHAR(50),
	@ProductID char(10),
	@Quantity int,
	@UnitPrice money
AS
BEGIN
BEGIN TRAN
	IF NOT EXISTS (SELECT ProductID FROM Product WHERE ProductID = @ProductID)
	BEGIN
		PRINT N'Lỗi: Không tồn tại sản phẩm này!'
		ROLLBACK TRAN
		RETURN
	END
	IF NOT EXISTS (SELECT * FROM Supplier WHERE SupplierID = @SupplierID)
	BEGIN
		PRINT N'Không tồn tại nhà cung cấp này!'
		ROLLBACK TRAN
		RETURN 
	END
	IF NOT EXISTS (SELECT grn.GoodsReceiptNoteID 
	FROM GoodsReceiptNote grn 
	WHERE grn.GoodsReceiptNoteID = @GoodsReceiptNoteID)
	BEGIN
		EXEC P_Add_GoodsReceiptNote @GoodsReceiptNoteID, @SupplierID, @ReceiptDate, @CreatedUser
		EXEC P_Add_GoodsReceiptNoteDetail @ProductID, @GoodsReceiptNoteID, @UnitPrice, @Quantity
	END
	ELSE
	BEGIN
		EXEC P_Add_GoodsReceiptNoteDetail @ProductID, @GoodsReceiptNoteID, @UnitPrice, @Quantity
	END
	COMMIT TRAN
END
go
CREATE PROCEDURE CreateLogin
    @LoginName NVARCHAR(50),
    @Password NVARCHAR(50),
    @UserName NVARCHAR(50),
    @Role NVARCHAR(20)  -- 'Admin' hoặc 'NhanVien'
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = @LoginName)
    BEGIN
        DECLARE @sqlLogin NVARCHAR(MAX);
        SET @sqlLogin = 'CREATE LOGIN [' + @LoginName + '] WITH PASSWORD = N''' + @Password + ''', CHECK_POLICY = OFF;';
        EXEC (@sqlLogin);
        PRINT 'Login created successfully.';
    END
    ELSE
        PRINT 'Login already exists.';

    IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = @UserName)
    BEGIN
        DECLARE @sqlUser NVARCHAR(MAX);
        SET @sqlUser = 'CREATE USER [' + @UserName + '] FOR LOGIN [' + @LoginName + '];';
        EXEC (@sqlUser);
        PRINT 'Database user created successfully (User = ' + @UserName + ').';
    END
    ELSE
        PRINT 'Database user already exists.';

    IF @Role = 'Admin'
    BEGIN
        EXEC sp_addrolemember 'db_owner', @UserName;
        PRINT 'User added to db_owner (Admin).';
    END
    ELSE IF @Role = 'NhanVien'
    BEGIN
        IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'NhanVien')
        BEGIN
            CREATE ROLE NhanVien;
        END

        EXEC sp_addrolemember 'NhanVien', @UserName;

        GRANT SELECT, INSERT, UPDATE ON DATABASE::QL_BANHANG_ONLINE TO NhanVien;
        PRINT 'User added to NhanVien role with limited permissions.';
    END
    ELSE
        PRINT 'Invalid role. Use "Admin" or "NhanVien".';
END
GO

--4 vấn đề truy xuất đồng thời
--Nhiều người dùng đặt hàng cùng lúc 

--Nhiều người cập nhật sản phẩm cùng lúc

