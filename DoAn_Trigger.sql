USE QL_BANHANG_ONLINE

CREATE TRIGGER TR_GoodsReceiptNoteDetail_UpdateQuantity_Insert
ON GoodsReceiptNoteDetail
AFTER INSERT
AS
BEGIN
    -- Ngăn chặn các trigger khác chạy nếu có lỗi
    SET NOCOUNT ON;

    -- Cập nhật Quantity (tồn kho) trong bảng Product
    UPDATE p
    SET p.Quantity = p.Quantity + i.Quantity
    FROM Product p, inserted i
    WHERE p.ProductID = i.ProductID;
END;
Go
SELECT Quantity FROM Product WHERE ProductID = 'SP001';
SELECT * FROM Supplier
SET DATEFORMAT DMY

EXEC P_ThemPN
    @GoodsReceiptNoteID = 'GRN001',
    @SupplierID = 'NCC001',
    @ReceiptDate = '22/11/2025',
    @CreatedUser = N'Tester',
    @ProductID = 'SP001',
    @Quantity = 50,
    @UnitPrice = 1200;

EXEC P_ThemPN
    @GoodsReceiptNoteID = 'GRN001',
    @SupplierID = 'NCC001',
    @ReceiptDate = '22/11/2025',
    @CreatedUser = N'Tester',
    @ProductID = 'SP002',
    @Quantity = 50,
    @UnitPrice = 1200;
SELECT Quantity FROM Product WHERE ProductID = 'SP002';
SELECT COUNT(*) FROM GoodsReceiptNoteDetail WHERE GoodsReceiptNoteID = 'GRN001'; 
