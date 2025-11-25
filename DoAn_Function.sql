USE QL_BANHANG_ONLINE

go
CREATE FUNCTION F_LaySanPhamGiamSauNhat(@SL int)
RETURNS TABLE
AS
RETURN(
	SELECT TOP (@SL)
	p.ProductID, p.ProductName,
	p.Image,
	p.Price,
	d.DiscountRate, 
	(p.Price * (1 - d.DiscountRate/100)) AS GiaDaGiam
	FROM Product p, Discount d
	WHERE p.ProductID = d.ProductID AND d.StartDate <= GETDATE() AND d.EndDate > GETDATE()
	ORDER BY d.DiscountRate DESC
)

CREATE FUNCTION F_TimKiemSanPhamTheoTen(@TenSP nvarchar(100)) 
RETURNS TABLE
AS
RETURN(
    SELECT *
    FROM product
    WHERE LOWER(ProductName) LIKE '%' + LOWER(@TenSP) + '%')

SELECT * FROM dbo.F_TimKiemSanPhamTheoTen('cocoon')

CREATE FUNCTION F_LocSanPham()
RETURNS TABLE
AS
RETURN(
	SELECT
	p.ProductID, p.ProductName,
	p.Image,
	p.Price,
	p.BrandID,
	p.ProductTypeID,
	CASE
		WHEN d.DiscountRate IS NULL THEN 0
		ELSE d.DiscountRate
	END AS DiscountRate,
	p.CreatedAt,
	CASE
		WHEN d.DiscountRate IS NULL THEN p.Price
		ELSE (p.Price * (1 - d.DiscountRate/100)) 
	END AS GiaDaGiam,
	SUM(od.Quantity) AS TotalSold
	FROM Product p
	JOIN OrderDetail od 
        ON p.ProductID = od.ProductID
	LEFT JOIN Discount d 
		on p.ProductID = d.ProductID 
		AND d.StartDate <= GETDATE()
		AND d.EndDate > GETDATE()
	GROUP BY
        p.ProductID,
        p.ProductName,
        p.Image,
        p.Price,
        d.DiscountRate,
		p.CreatedAt,
		p.BrandID,
		ProductTypeID
)

SELECT * FROM dbo.F_LocSanPham() ORDER BY CreatedAt Desc;

SELECT * FROM dbo.F_LocSanPham() ORDER BY Price ASC;

SELECT * FROM dbo.F_LocSanPham() ORDER BY TotalSold DESC;

CREATE FUNCTION F_TinhGiamGia(@ProductID char(10))
RETURNS FLOAT
AS
BEGIN
DECLARE @SOTIENGIAMGIA FLOAT
	SELECT @SOTIENGIAMGIA = 
		CASE WHEN d.ProductID IS NULL THEN 0
		ELSE p.Price * d.DiscountRate
		END
	FROM Product p
	LEFT JOIN Discount d on p.ProductID = d.ProductID
	WHERE p.ProductID = @ProductID
	RETURN @SOTIENGIAMGIA
END