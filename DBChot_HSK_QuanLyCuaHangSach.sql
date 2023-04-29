CREATE DATABASE QUANLICUAHANGSACH
USE QUANLICUAHANGSACH

CREATE TABLE tblQuyenTruyCap
(
	bMaQuyenTruyCap bit not null PRIMARY KEY,
	sTenQuyenTruyCap nvarchar(20)
)
CREATE TABLE tblNhanVien
(
	sMaNV VARCHAR(10) NOT NULL PRIMARY KEY,
	sTenNV NVARCHAR(50),
	sGioiTinh NVARCHAR(5),
	dNgaySinh DATE,
	sDiaChi NVARCHAR(50),
	sSoDT NVARCHAR(12),
	fLuongcoban FLOAT,
	fHSL FLOAT,
	bMaQuyenTruyCap bit not NULL,
	sPassWord VARCHAR(15)
) 
ALTER TABLE dbo.tblNhanVien ADD CONSTRAINT FK_MaQuyen FOREIGN KEY(bMaQuyenTruyCap) REFERENCES dbo.tblQuyenTruyCap(bMaQuyenTruyCap)
CREATE TABLE tblNhaXuatBan
(
	sMaNXB VARCHAR(10) NOT NULL  PRIMARY KEY,
	sTenNXB NVARCHAR(50) NOT NULL,
	sSoDT VARCHAR(15),
	sDiaChi nvarchar(50)
)

CREATE TABLE tblSach(
	sMaSach VARCHAR(10) NOT NULL UNIQUE ,
	sTenSach NVARCHAR(50),
	sMaNXB VARCHAR(10) NOT NULL,
	iSoLuong INT,
	fDongia FLOAT,
	sNamXB CHAR(4)
)
ALTER TABLE dbo.tblSach ADD CONSTRAINT PK_maSach PRIMARY KEY(sMaSach)
ALTER TABLE dbo.tblSach ADD CONSTRAINT FK_maNXB FOREIGN KEY(sMaNXB) REFERENCES dbo.tblNhaXuatBan(sMaNXB)

CREATE TABLE tblKhachHang(
sMaKH VARCHAR(10) NOT NULL ,
sTenKH NVARCHAR(50),
sGioiTinh NVARCHAR(5),
dNgaySinh DATE,
sSoDT CHAR(12),
sDiaChi NVARCHAR(50)
)
ALTER TABLE dbo.tblKhachHang ADD CONSTRAINT PK_maKhachHang PRIMARY KEY(sMaKH)

CREATE TABLE tblHoaDon
(
	sMaHD VARCHAR(10) NOT NULL UNIQUE,
	sMaNV VARCHAR(10) NOT NULL,
	sMaKH VARCHAR(10) NOT NULL ,
	dNgaylap DATE,
	dNgaythanhtoan DATE
)
ALTER TABLE dbo.tblHoaDon ADD CONSTRAINT PK_maHoaDon PRIMARY KEY(sMaHD)
ALTER TABLE dbo.tblHoaDon ADD CONSTRAINT FK_maKH FOREIGN KEY(sMaKH) REFERENCES dbo.tblKhachHang(sMaKH)
ALTER TABLE dbo.tblHoaDon ADD CONSTRAINT FK_maNV FOREIGN KEY(sMaNV) REFERENCES dbo.tblNhanVien(sMaNV)

CREATE TABLE tblChiTietHoaDon
(
	sMaHD VARCHAR(10) NOT NULL,
	sMaSach VARCHAR(10) NOT NULL,
	iSoluong INT,
	fGiaban FLOAT,
	fMucgiamgia FLOAT
)
ALTER TABLE dbo.tblChiTietHoaDon ADD CONSTRAINT PK_maHD_maSach PRIMARY KEY(sMaHD, sMaSach)
ALTER TABLE dbo.tblChiTietHoaDon ADD CONSTRAINT FK_maSach FOREIGN KEY(sMaSach) REFERENCES dbo.tblSach(sMaSach)
ALTER TABLE dbo.tblChiTietHoaDon ADD CONSTRAINT FK_maHD FOREIGN KEY(sMaHD) REFERENCES dbo.tblHoaDon(sMaHD)

INSERT INTO dbo.tblQuyenTruyCap
(
    bMaQuyenTruyCap,
    sTenQuyenTruyCap
)
VALUES
(   NULL, -- bMaQuyenTruyCap - bit
    NULL  -- sTenQuyenTruyCap - nvarchar(255)
    )
select *from tblSach where sTenSach like 'si[^eê]u quay'
select *from tblSach where sMaSach = 18
-- ===========END CREATE TABLE=============
-- ===========STAR CREATE PROC=============
-- tạo proc thêm sách
CREATE PROC insert_SBook
@maSach varchar(10), @tenSach nvarchar(50), @maNXB VARCHAR(10), @soLuong int, @donGia float, @namXB CHAR(4)
AS
BEGIN
INSERT INTO tblSach(sMaSach,sTenSach,sMaNXB,iSoLuong,fDongia,sNamXB) VALUES(@maSach, @tenSach, @maNXB, @soLuong, @donGia, @namXB)
END 
-- tạo proc sửa sách
CREATE PROC edit_Book
@maSach varchar(10), @tenSach nvarchar(50), @maNXB VARCHAR(10), @soLuong int, @donGia float, @namXB CHAR(4)
AS 
BEGIN
UPDATE dbo.tblSach SET sTenSach = @tenSach, sMaNXB = @maNXB, iSoLuong = @soLuong, fDongia = @donGia, sNamXB = @namXB WHERE sMaSach = @maSach
END 
-- tạo proc xoá sách
CREATE PROC del_Book
@maSach varchar(10)
AS 
BEGIN
DELETE FROM dbo.tblSach WHERE sMaSach = @maSach
END 
EXEC del_Book 
-- tạo proc thêm NXB
CREATE proc insert_Publisher
@maNXB varchar(20), @tenNXB nvarchar(20), @diaChi nvarchar(30), @soDT CHAR(15)
as 
begin
insert into tblNhaXuatBan(sMaNXB, sTenNXB, sDiaChi, sSoDT) values(@maNXB, @tenNXB, @diaChi, @soDT)
end 
-- tạo proc sửa NXB
CREATE PROC edit_Publisher
@maNXB varchar(20), @tenNXB nvarchar(20), @diaChi nvarchar(30), @soDT CHAR(15)
AS 
BEGIN 
UPDATE dbo.tblNhaXuatBan SET sTenNXB = @tenNXB, sDiaChi = @diaChi, sSoDT = @soDT  WHERE sMaNXB = @maNXB
END 
SELECT *FROM tblNXB
-- tạo proc xóa NXB
CREATE PROC del_Publisher
@maNXB varchar(20)
AS 
BEGIN
DELETE FROM dbo.tblNhaXuatBan WHERE sMaNXB = @maNXB
END 
ALTER TABLE dbo.tblSach ADD NamXB INT
ALTER TABLE dbo.tblNhanVien DROP COLUMN sTaiKhoan
ALTER TABLE dbo.tblNhanVien ADD bMaQuyenTruyCap bit
CREATE TABLE tblQuyenTruyCap
( bMaQuyenTruyCap BIT NOT NULL PRIMARY KEY,
sTenQuyenTruyCap NVARCHAR(20)
)
ALTER TABLE dbo.tblNhanVien ADD CONSTRAINT FK_Quyen FOREIGN KEY(bMaQuyenTruyCap) REFERENCES dbo.tblQuyenTruyCap(bMaQuyenTruyCap)
ALTER PROCEDURE del_Publisher
@maNXB VARCHAR(10)
AS
BEGIN 
	Delete from tblNXB WHERE sMaNXB = @maNXB
END
SELECT *FROM tblNXB
SELECT *FROM dbo.tblSach
-- proc thêm mã nhà xuất bản vào bảng nhà sách
CREATE PROC add_maNXB
ALTER  PROC pc_Gia
@Gia1 float, @Gia2 float
AS
BEGIN 
SELECT *FROM dbo.tblSach 
WHERE fDongia BETWEEN @Gia1 AND @Gia2 
END 
EXEC pc_Gia 10000, 50000
---=== tạo proc lọc ra sách có số lượng theo yêu cầu
CREATE PROC pc_SoLuong
@soluong int
AS 
BEGIN
SELECT *FROM dbo.tblSach WHERE iSoLuong>@soluong
END 
EXEC pc_SoLuong 50
--============PROC KHACH HANG============
CREATE PROC insert_Customer
@maKH VARCHAR(10), @tenKH NVARCHAR(50), @Gioitinh nvarchar(5), @NgaySinh date, @soDT CHAR(12), @DiaChi nvarchar(50)
AS 
BEGIN 
INSERT INTO tblKhachHang(sMaKH,sTenKH,sGioiTinh,dNgaySinh,sSoDT,sDiaChi) VALUES (@maKH, @tenKH, @Gioitinh, @NgaySinh, @soDT, @DiaChi)
END  
ALTER PROC edit_Customer
@maKH VARCHAR(10), @tenKH NVARCHAR(50), @Gioitinh nvarchar(5), @NgaySinh date, @soDT CHAR(12), @DiaChi nvarchar(50)
AS 
BEGIN 
UPDATE dbo.tblKhachHang SET sTenKH = @tenKH, dNgaySinh = @NgaySinh, sGioiTinh = @Gioitinh, sSoDT = @soDT, sDiaChi = @DiaChi WHERE sMaKH = @maKH
END
CREATE PROC del_Customer
@maKH VARCHAR(10)
AS 
BEGIN 
DELETE FROM dbo.tblKhachHang WHERE sMaKH = @maKH 
END
CREATE PROC search_Customer
@maKH VARCHAR(10)
AS 
BEGIN
SELECT *FROM dbo.tblKhachHang WHERE sMaKH = @maKH
END
SELECT *FROM dbo.tblKhachHang
--===========PROC NHANVIEN===========
CREATE PROC insert_Staff
@maNV VARCHAR(10), @tenNV nvarchar(50), @gioiTinh nvarchar(5), @ngaySinh date, @diaChi nvarchar(50), 
@soDT CHAR(12), @luongCB float, @hsluong float, @maQuyen bit, @passWord varchar(15)
AS 
BEGIN 
INSERT INTO tblNhanVien(sMaNV,sTenNV,sGioiTinh,dNgaySinh,sDiaChi,sSoDT,fLuongcoban,fHSL,bMaQuyenTruyCap,sPassWord)
VALUES(@maNV, @tenNV, @gioiTinh, @ngaySinh, @diaChi, @soDT, @luongCB, @hsluong, @maQuyen, @passWord)
END 
CREATE PROC edit_Staff
@maNV VARCHAR(10), @tenNV nvarchar(50), @gioiTinh nvarchar(5), @ngaySinh date, @diaChi nvarchar(50), @soDT CHAR(12), @luongCB float, @hsluong float, @maQuyen bit, @passWord varchar(15)
AS
BEGIN 
UPDATE dbo.tblNhanVien SET sTenNV = @tenNV, sGioiTinh = @gioiTinh, dNgaySinh = @ngaySinh, sDiaChi = @diaChi, sSoDT = @soDT, fLuongcoban = @luongCB, fHSL = @hsluong, bMaQuyenTruyCap =  @maQuyen, sPassWord = @passWord
WHERE sMaNV = @maNV
END 
CREATE PROC del_Staff
@maNV VARCHAR(10)
AS 
BEGIN 
DELETE FROM dbo.tblNhanVien WHERE sMaNV = @maNV
END 
ALTER PROC search_Staff
@maNV VARCHAR(10)
AS 
BEGIN
SELECT *FROM dbo.tblNhanVien WHERE sMaNV = @maNV 
END      
--====== proc HoaDon=======
CREATE PROC insert_Invoice
@maHD VARCHAR(10), @maNV VARCHAR(10), @maKH VARCHAR(10), @ngayLap date, @ngayThanhtoan date
AS 
BEGIN
INSERT INTO tblHoaDon VALUES(@maHD, @maNV, @maKH, @ngayLap, @ngayThanhtoan)
END 
CREATE PROC edit_Invoice
@maHD VARCHAR(10), @maNV VARCHAR(10), @maKH VARCHAR(10), @ngayLap date, @ngayThanhtoan date
AS 
BEGIN
UPDATE dbo.tblHoaDon SET  sMaNV = @maNV, sMaKH = @maKH, dNgaylap = @ngayLap, dNgaythanhtoan = @ngayThanhtoan WHERE sMaHD = @maHD
END 
CREATE PROC del_Invoice
@maHD VARCHAR(10)
AS 
BEGIN
DELETE FROM dbo.tblHoaDon WHERE sMaHD = @maHD 
END 
CREATE PROC search_Invoice
@maHD VARCHAR(10)
AS
BEGIN
SELECT *FROM dbo.tblHoaDon WHERE sMaHD = @maHD 
END 
SELECT *FROM dbo.tblChiTietHoaDon
SELECT sMaNV FROM dbo.tblNhanVien
--======== proc chitiethoadon=========
CREATE PROC insert_Invoicedetail
@maHD varchar(10), @maSach varchar(10), @soLuong int, @giaBan float, @mucGiamgia float
AS 
BEGIN 
INSERT INTO tblChiTietHoaDon VALUES(@maHD, @maSach, @soLuong, @giaBan, @mucGiamgia)
END 
--=====
CREATE PROC edit_InvoiceDetail
@maHD varchar(10), @maSach varchar(10), @soLuong int, @giaBan float, @mucGiamgia float
AS 
BEGIN
UPDATE dbo.tblChiTietHoaDon SET sMaSach = @maSach, iSoluong = @soLuong, fGiaban = @giaBan, fMucgiamgia = @mucGiamgia 
WHERE sMaSach = @maSach AND sMaHD = @maHD
END 
UPDATE dbo.tblChiTietHoaDon SET sMaSach = 1 WHERE sMaHD =1 AND sMaSach =2
SELECT *FROM dbo.tblChiTietHoaDon
--=====
ALTER PROC del_InvoiceDetail
@maHD varchar(10), @maSach varchar(10)
AS
BEGIN 
DELETE FROM dbo.tblChiTietHoaDon WHERE sMaHD = @maHD AND  sMaSach = @maSach
END
 
--======
ALTER  PROC search_InvoiceDetail
@maHD varchar(10)
as  
BEGIN
SELECT *FROM dbo.tblChiTietHoaDon WHERE sMaHD = @maHD 
END 
EXEC search_InvoiceDetail 2
SELECT *FROM dbo.tblSach
SELECT *FROM dbo.tblNhaXuatBan
ALTER PROC search_book_baseonPublisher
@maNXB VARCHAR(10)
AS
 BEGIN
 --SELECT sMaSach, sTenSach, sTenNXB, iSoLuong, fDongia, sNamXB FROM dbo.tblSach INNER JOIN dbo.tblNhaXuatBan ON tblNhaXuatBan.sMaNXB = tblSach.sMaNXB
SELECT *FROM dbo.tblSach WHERE sMaNXB = @maNXB
 END 
 EXEC search_Publisher N'Trẻ'
 SELECT *FROM dbo.tblChiTietHoaDon
 SELECT *FROM dbo.tblHoaDon
 -- thống kê theo từng mã sách số lượng sách đã bán theo tháng 
 -- đã bán những sách nào và số lượng bao nhiêu
 -- input tháng, năm
 SELECT *FROM dbo.tblSach
 CREATE PROC reportBookasTime
 @year int, @month int
 AS
 BEGIN
SELECT sMaSach, SUM(iSoluong) AS [Số lượng] FROM dbo.tblChiTietHoaDon, dbo.tblHoaDon 
 WHERE tblChiTietHoaDon.sMaHD = tblHoaDon.sMaHD AND YEAR(dNgaylap)=@year AND MONTH(dNgaylap) = @month GROUP BY sMaSach 
 END 
 --========PROC TÀI KHOẢN=====
 ALTER PROC Account
 @username varchar(10), @password CHAR(15)
 AS
 BEGIN
 SELECT sMaNV, sPassWord, bMaQuyenTruyCap FROM dbo.tblNhanVien WHERE sMaNV = @username AND sPassWord = @password
 END

 EXEC Account 'trapia2k41', 'mynee'
 --===== đổi mật khẩu
 ALTER PROC changePassword
 @username varchar(10), @pass CHAR(15) 
 AS
 BEGIN
 UPDATE  dbo.tblNhanVien SET sPassWord = @pass WHERE sMaNV = @username
 END 
 SELECT *FROM dbo.tblNhanVien
 EXEC changePassword 'anhnguyet', 5
 --====== tạo proc lập hoá đơn=====

 CREATE VIEW view_sp_hoadon 
 AS SELECT *FROM dbo.tblChiTietHoaDon
 ALTER VIEW view_sp_hoadon  ADD tongtien float 
 ALTER PROC sp_HoadonKH(
 @maKH VARCHAR(10), @ngaymua date 
 )
 AS 
 BEGIN
 SELECT sTenNV, dbo.tblKhachHang.*,  dbo.tblChiTietHoaDon.* FROM dbo.tblChiTietHoaDon 
 INNER JOIN dbo.tblHoaDon ON tblHoaDon.sMaHD = tblChiTietHoaDon.sMaHD
 INNER JOIN dbo.tblKhachHang ON tblKhachHang.sMaKH = tblHoaDon.sMaKH
 INNER JOIN dbo.tblNhanVien ON tblNhanVien.sMaNV = tblHoaDon.sMaNV
 WHERE tblHoaDon.sMaKH = @maKH AND dNgaylap = @ngaymua
 END
 SELECT *FROM dbo.tblChiTietHoaDon
 CREATE PROC tinhtongtien(
 @maKH VARCHAR(10), @ngaymua date 
 )
 AS 
 BEGIN
 SELECT SUM(iSoluong* fGiaban* fMucgiamgia) AS giagiam, SUM(fGiaban*iSoluong) AS tongtien  FROM dbo.tblChiTietHoaDon 
 INNER JOIN dbo.tblHoaDon ON tblHoaDon.sMaHD = tblChiTietHoaDon.sMaHD
 INNER JOIN dbo.tblKhachHang ON tblKhachHang.sMaKH = tblHoaDon.sMaKH
 WHERE tblHoaDon.sMaKH = @maKH AND dNgaylap = @ngaymua
 END
 SELECT SUM(fGiaban*iSoluong*fMucgiamgia), SUM(fGiaban*iSoluong) AS [soluong]  FROM dbo.tblChiTietHoaDon

 SELECT *FROM dbo.tblKhachHang
 SELECT *FROM dbo.tblChiTietHoaDon
 SELECT *FROM dbo.tblHoaDon
 SELECT *FROM dbo.tblNhanVien
 --======= Số lượng sách đã bán ra trong thời gian cụ thể CR
 SELECT sMaSach, COUNT(sMaSach) FROM dbo.tblChiTietHoaDon  INNER JOIN dbo.tblHoaDon ON tblHoaDon.sMaHD = tblChiTietHoaDon.sMaHD 
 WHERE MONTH(dNgaylap) = 12
 GROUP BY sMaSach
 --=== VIẾT PROC THỐNG KÊ NHÂN VIÊN ĐÃ BÁN ĐƯỢC BAO NHIÊU TIỀN, SỐ SẢN PHẨM LÀ BAO NHIÊU CR=====
 ALTER PROC ThongkeKPI
 @maNV varchar(10), @month int, @year int
 AS
 BEGIN
 SELECT sMaNV, sTenNV, GOM.SoLuong, GOM.tinhtien  FROM dbo.tblNhanVien ,
 (SELECT  SUM(iSoluong) AS [SoLuong], SUM(fMucgiamgia*fGiaban*iSoluong) AS [tinhtien] FROM dbo.tblNhanVien
 INNER JOIN dbo.tblHoaDon ON tblHoaDon.sMaNV = tblNhanVien.sMaNV
 INNER JOIN dbo.tblChiTietHoaDon ON tblChiTietHoaDon.sMaHD = tblHoaDon.sMaHD
 WHERE MONTH(dNgaylap) =@month  AND YEAR(dNgaylap) =@year AND tblNhanVien.sMaNV = @maNV
 ) AS [GOM] WHERE sMaNV = @maNV
 END
 EXEC ThongkeKPI 'DONGNHI', 12,2023
 SELECT sMaNV, sTenNV, GOM.SoLuong, GOM.tinhtien  FROM dbo.tblNhanVien ,
 (SELECT  SUM(iSoluong) AS [SoLuong], SUM(fMucgiamgia*fGiaban*iSoluong) AS [tinhtien] FROM dbo.tblNhanVien
 INNER JOIN dbo.tblHoaDon ON tblHoaDon.sMaNV = tblNhanVien.sMaNV
 INNER JOIN dbo.tblChiTietHoaDon ON tblChiTietHoaDon.sMaHD = tblHoaDon.sMaHD
 WHERE tblHoaDon.sMaNV = 'khanhduy'
 ) AS [GOM] WHERE sMaNV = 'khanhduy' 
  SELECT *FROM dbo.tblHoaDon
 SELECT *FROM dbo.tblChiTietHoaDon
 --===== liệt kê doanh thu của tháng CR=====
 SELECT tblChiTietHoaDon.sMaHD, SUM(iSoluong*fGiaban*fMucgiamgia) FROM dbo.tblChiTietHoaDon INNER JOIN dbo.tblHoaDon ON tblHoaDon.sMaHD = tblChiTietHoaDon.sMaHD
 WHERE MONTH(dNgaylap) = 12 AND YEAR(dNgaylap)=2023
 GROUP BY tblChiTietHoaDon.sMaHD
  -- thống kê theo từng mã sách số lượng sách đã bán theo tháng 
 -- đã bán những sách nào và số lượng bao nhiêu
 -- input tháng, năm
 SELECT *FROM dbo.tblSach
 CREATE PROC reportBookasTime
 @year int, @month int
 AS
 BEGIN
SELECT sMaSach, SUM(iSoluong) AS [Số lượng] FROM dbo.tblChiTietHoaDon, dbo.tblHoaDon 
 WHERE tblChiTietHoaDon.sMaHD = tblHoaDon.sMaHD AND YEAR(dNgaylap)=@year AND MONTH(dNgaylap) = @month GROUP BY sMaSach 
 END 
 --========PROC TÀI KHOẢN=====
 ALTER PROC Account
 @username varchar(10), @password CHAR(15)
 AS
 BEGIN
 SELECT sMaNV, sPassWord, bMaQuyenTruyCap FROM dbo.tblNhanVien WHERE sMaNV = @username AND sPassWord = @password
 END

 EXEC Account 'trapia2k41', 'mynee'
 --===== đổi mật khẩu
 ALTER PROC changePassword
 @username varchar(10), @pass CHAR(15) 
 AS
 BEGIN
 UPDATE  dbo.tblNhanVien SET sPassWord = @pass WHERE sMaNV = @username
 END 
 SELECT *FROM dbo.tblNhanVien
 EXEC changePassword 'anhnguyet', 5
 --====== tạo proc lập hoá đơn=====
 CREATE PROC sp_HoadonKH(
 @maKH VARCHAR(10)
 )
 AS 
 BEGIN
 SELECT tblHoaDon.sMaKH, dbo.tblChiTietHoaDon.* FROM dbo.tblChiTietHoaDon 
 INNER JOIN dbo.tblHoaDon ON tblHoaDon.sMaHD = tblChiTietHoaDon.sMaHD
 INNER JOIN dbo.tblKhachHang ON tblKhachHang.sMaKH = tblHoaDon.sMaKH
 WHERE tblHoaDon.sMaKH = @maKH
 END
--==============Tạo trigger================
-- 1. Tạo trigger khi thêm 1 mã NXB thì tự động thêm vào 
CREATE TRIGGER add_Publisher
ON tblNXB
FOR INSERT
AS 
BEGIN
DECLARE @maNXB VARCHAR(10) = SELECT sMaNXB FROM INSERTED
INSERT INTO 
END -

SELECT *FROM dbo.tblSach
SELECT *FROM dbo.tblChiTietHoaDon
