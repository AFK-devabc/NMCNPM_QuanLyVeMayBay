﻿CREATE DATABASE BANVEMAYBAY
USE BANVEMAYBAY

create table USER_ACOUNT
(
	UserName varchar(50),
	UserPass varchar(50),
	CONSTRAINT PK_UA PRIMARY KEY (UserName)

)

insert into USER_ACOUNT values ('ADMIN' ,'202CB962AC59075B964B07152D234B70')


-- TẠO BẢNG
CREATE TABLE CHUYENBAY
(
	MaChuyenBay CHAR(6),	
	MaSanBayDi CHAR(6),
	MaSanBayDen CHAR(6),
	GiaVe MONEY,
	NgayBay SMALLDATETIME,
	ThoiGianBay INT,
	--ThoiGianDung INT,
	CONSTRAINT PK_CB PRIMARY KEY (MaChuyenBay)
)

CREATE TABLE TRUNGGIAN 
(
	MaChuyenBay CHAR(6),
	MaSanBay CHAR(6),
	ThoiGianDung INT,
	GhiChu TEXT,
	CONSTRAINT PK_TG PRIMARY KEY (MaChuyenBay, MaSanBay)
)

CREATE TABLE SANBAY
(
	MaSanBay CHAR(6),
	TenSanBay NVARCHAR(100),
	CONSTRAINT PK_SB PRIMARY KEY (MaSanBay)
)

CREATE TABLE VEMAYBAY
(
	MaVe CHAR(6),
	MaChuyenBay CHAR(6),
	CMND VARCHAR(15),
	MaHangVe CHAR(6),
	GiaTien MONEY,
	Loaive VARCHAR(10),
	NgayMua smalldatetime,
	CONSTRAINT PK_VE PRIMARY KEY (MaVe)
)

CREATE TABLE HANGVE
(
	MaHangVe CHAR(6),
	TenHangVe NVARCHAR(30),
	TiLe float,
	CONSTRAINT PK_HANGVE PRIMARY KEY (MaHangVe)
)

CREATE TABLE SOLUONGVE
(
	MaChuyenBay CHAR(6),
	MaHangVe CHAR(6),
	TongSoGhe INT,
	SoGheDaDat INT,
	CONSTRAINT PK_SLVE PRIMARY KEY (MaChuyenBay, MaHangVe)
)

CREATE TABLE HANHKHACH
(
	CMND VARCHAR(15),
	HoTen NVARCHAR(50),
	SoDienThoai VARCHAR(10),
	CONSTRAINT PK_HK PRIMARY KEY (CMND)
)

CREATE TABLE THAMSO
(
	SoSanBay INT not null,
	SoLuongSanBayTGToiDa INT not null,
	ThoiGianBayToiThieu INT not null,
	ThoiGianDungToiThieu INT not null,
	ThoiGianDungToiDa INT not null,
	SoHangVe INT not null,
	ThoiGianDatVe INT not null,
	ThoiGianHuyVe INT not null
)
-- RÀNG BUỘC
ALTER TABLE CHUYENBAY ADD CONSTRAINT FK_CB_SBDI FOREIGN KEY (MaSanBayDi) REFERENCES SANBAY(MaSanBay)
ALTER TABLE CHUYENBAY ADD CONSTRAINT FK_CB_SBDEN FOREIGN KEY (MaSanBayDen) REFERENCES SANBAY(MaSanBay)
ALTER TABLE CHUYENBAY ADD CONSTRAINT CK_CB_TGBAY CHECK (ThoiGianBay > 0)

ALTER TABLE TRUNGGIAN ADD CONSTRAINT FK_TG_CB FOREIGN KEY (MaChuyenBay) REFERENCES CHUYENBAY(MaChuyenBay)
ALTER TABLE TRUNGGIAN ADD CONSTRAINT FK_TG_SB FOREIGN KEY (MaSanBay) REFERENCES SANBAY(MaSanBay)
ALTER TABLE TRUNGGIAN ADD CONSTRAINT CK_TG_TGDUNG CHECK (ThoiGianDung > 0)

ALTER TABLE VEMAYBAY ADD CONSTRAINT FK_VE_CB FOREIGN KEY (MaChuyenBay) REFERENCES CHUYENBAY(MaChuyenBay)
ALTER TABLE VEMAYBAY ADD CONSTRAINT FK_VE_HK FOREIGN KEY (CMND) REFERENCES HANHKHACH(CMND)
ALTER TABLE VEMAYBAY ADD CONSTRAINT FK_VE_HANGVE FOREIGN KEY (MaHangVe) REFERENCES HANGVE(MaHangVe)
ALTER TABLE VEMAYBAY ADD CONSTRAINT CK_VE_LOAIVE CHECK (LoaiVe in ('Ve Mua', 'Ve Dat Cho'))

ALTER TABLE HANGVE ADD CONSTRAINT CK_TILE CHECK (TiLe >= 1.0)

ALTER TABLE SOLUONGVE ADD CONSTRAINT FK_SLVE_CB FOREIGN KEY (MachuyenBay) REFERENCES CHUYENBAY(MaChuyenBay)
ALTER TABLE SOLUONGVE ADD CONSTRAINT FK_SLVE_HANGVE FOREIGN KEY (MaHangVe) REFERENCES HANGVE(MaHangVe)
ALTER TABLE SOLUONGVE ADD CONSTRAINT CK_SLVE_TSGHE CHECK (TongSoGhe >= 0)
ALTER TABLE SOLUONGVE ADD CONSTRAINT CK_SLVE_GHEDADAT CHECK(SoGheDaDat >=0 and SoGheDaDat <= TongSoGhe)

ALTER TABLE THAMSO ADD CONSTRAINT CK_TS_SLSBMAX CHECK(SoLuongSanBayTGToiDa > 0)
ALTER TABLE THAMSO ADD CONSTRAINT CK_TS_TGBMIN CHECK(ThoiGianBayToiThieu >=0)
ALTER TABLE THAMSO ADD CONSTRAINT CK_TS_TGDMIN CHECK(ThoiGianDungToiThieu >=0)
ALTER TABLE THAMSO ADD CONSTRAINT CK_TS_TGDMAX CHECK(ThoiGianDungToiDa > 0 and ThoiGianDungToiDa > ThoiGianDungToiThieu)
ALTER TABLE THAMSO ADD CONSTRAINT CK_TS_TGDV CHECK(ThoiGianDatVe > 0 and ThoiGianDatVe > ThoiGianHuyVe)
ALTER TABLE THAMSO ADD CONSTRAINT CK_TS_TGHV CHECK(ThoiGianHuyVe >= 0)

-- TRIGGER
-- CHUYENBAY ThoiGianDung = Tổng ThoiGianDung tại các sân bay trung gian
-- triger nhập chuyến bay.
-- Kiểm tra thời gian bay tối thiểu

CREATE TRIGGER TRG_CHUYENBAY ON CHUYENBAY
FOR INSERT, UPDATE
AS 
BEGIN
	DECLARE @COUNT INT
	DECLARE @TGBAYMIN INT
	SELECT @TGBAYMIN = ThoiGianBayToiThieu FROM THAMSO
	SELECT @COUNT = COUNT(*) FROM inserted WHERE ThoiGianBay < @TGBAYMIN
	IF(@COUNT>0)
	BEGIN
		PRINT(N'ERROR: Thời gian bay < Thời gian bay tối thiểu')
		ROLLBACK TRANSACTION
	END
END
-- Trigger nhập trung gian
-- Kiểm tra số lượng sân bay trung gian tối đa (ứng với một chuyến bay)
-- kiểm tra ThoiGianDung có thỏa mãn điều kiện của tham số
CREATE TRIGGER TRG_TG ON TRUNGGIAN
FOR INSERT, UPDATE
AS
BEGIN
	DECLARE @COUNT1 INT
	DECLARE @SLTGMAX INT
	SELECT @SLTGMAX = SoLuongSanBayTGToiDa FROM THAMSO 
	SELECT @COUNT1 = COUNT(*)
	FROM (
	SELECT inserted.MaChuyenBay FROM inserted
	GROUP BY inserted.MaChuyenBay
	HAVING COUNT(*) > @SLTGMAX
	) AS K

	IF(@COUNT1>0)
	BEGIN
		PRINT(N'ERROR: Số lượng sân bay trung gian lớn hơn số lượng sân bay trung gian tối đa')
		ROLLBACK TRANSACTION
	END
	
	DECLARE @COUNT2 INT
	DECLARE @TGDMIN INT
	DECLARE @TGDMAX INT
	SELECT @TGDMIN = ThoiGianDungToiThieu, @TGDMAX = ThoiGianDungToiDa FROM THAMSO 
	SELECT @COUNT2 = COUNT(*) FROM inserted WHERE inserted.ThoiGianDung>@TGDMAX OR inserted.ThoiGianDung<@TGDMIN

	IF(@COUNT2>0)
	BEGIN
		PRINT(N'ERROR: Thời gian dừng không hợp lệ')
		ROLLBACK TRANSACTION
	END
END
-- Trigger nhập sân bay
-- Cập nhật lại số sân bay trong bảng quy định
go
CREATE TRIGGER TRG_SANBAY ON SANBAY
FOR INSERT,UPDATE,DELETE
AS
BEGIN
	DECLARE @COUNT1 INT
	DECLARE @COUNT2 INT
	SELECT @COUNT1=COUNT(*) FROM inserted 
	SELECT @COUNT2=COUNT(*) FROM deleted
	UPDATE THAMSO SET THAMSO.SOSANBAY=THAMSO.SOSANBAY+@COUNT1-@COUNT2
END

-- Trigger nhập hạng vé
-- cập nhật lại số hạng vé
CREATE TRIGGER TRG_HANGVE ON HANGVE
FOR INSERT,UPDATE,DELETE
AS
BEGIN
	DECLARE @COUNT1 INT
	DECLARE @COUNT2 INT
	SELECT @COUNT1=COUNT(*) FROM inserted 
	SELECT @COUNT2=COUNT(*) FROM deleted
	UPDATE THAMSO SET THAMSO.SoHangVe=THAMSO.SoHangVe+@COUNT1-@COUNT2
END

-- VEMAYBAY GiaTien = GiaVe(CHUYENBAY) * TiLe(HANGVE), SoGheDaDat ++
-- trigger nhập vé
-- Kiểm tra GiaTien
-- Kiểm tra còn ghê trống hay không? (ứng với từng hạng vé)
-- Kiểm tra ThoiGianDatVe đối với loại vé đặt chỗ
CREATE TRIGGER TRG_VEMAYBAY ON VEMAYBAY
FOR INSERT,UPDATE
AS
BEGIN
	DECLARE @COUNT1 INT
	SELECT @COUNT1=COUNT(*) FROM inserted, CHUYENBAY, HANGVE 
	WHERE inserted.MaChuyenBay=CHUYENBAY.MaChuyenBay AND inserted.MaHangVe=HANGVE.MaHangVe AND inserted.GiaTien != CHUYENBAY.GiaVe*HANGVE.TiLe
	IF(@COUNT1>0)
	BEGIN
		PRINT(N'ERROR: Giá tiền nhập chưa đúng!')
		ROLLBACK TRANSACTION
	END

	DECLARE @COUNT2 INT
	DECLARE @SLGHE INT
	SELECT @SLGHE = COUNT(*) FROM inserted
		GROUP BY inserted.MaHangVe, inserted.MaChuyenBay
	SELECT @COUNT2 = COUNT(*)
	FROM 
	(
		SELECT inserted.MaChuyenBay, inserted.MaHangVe, COUNT(*) AS SoLuongGhe FROM inserted
		GROUP BY inserted.MaHangVe, inserted.MaChuyenBay
	) AS K, SOLUONGVE
	WHERE K.MaChuyenBay = SOLUONGVE.MaChuyenBay AND K.MaHangVe = SOLUONGVE.MaHangVe AND K.SoLuongGhe > SOLUONGVE.TongSoGhe - SOLUONGVE.SoGheDaDat
	IF(@COUNT2>0)
	BEGIN
		PRINT(@SLGHE)
		PRINT(N'ERROR: Không đủ ghế')
		ROLLBACK TRANSACTION
	END

	DECLARE @COUNT3 INT
	SELECT @COUNT3=COUNT(*) FROM inserted,CHUYENBAY,THAMSO WHERE inserted.MaChuyenBay=CHUYENBAY.MaChuyenBay AND inserted.Loaive='Ve Dat Cho' AND (DATEDIFF(HOUR,CURRENT_TIMESTAMP,CHUYENBAY.NgayBay))<THAMSO.ThoiGianDatVe
	IF(@COUNT3 >0)
	BEGIN
		PRINT(N'ERROR: Đã quá thời gian đặt vé!')
		ROLLBACK TRANSACTION
	END

	UPDATE SOLUONGVE SET SoGheDaDat = SoGheDaDat + 1
	WHERE SOLUONGVE.MaChuyenBay IN (SELECT MaChuyenBay FROM inserted )
	AND SOLUONGVE.MaHangVe IN (SELECT MaHangVe FROM inserted WHERE inserted.MaChuyenBay = SOLUONGVE.MaChuyenBay)

END

CREATE TRIGGER TRG_VEMAYBAY_D ON VEMAYBAY
FOR DELETE
AS
BEGIN
	UPDATE SOLUONGVE SET SoGheDaDat = SoGheDaDat - 1
	WHERE SOLUONGVE.MaChuyenBay IN (SELECT MaChuyenBay FROM deleted )
	AND SOLUONGVE.MaHangVe IN (SELECT MaHangVe FROM deleted WHERE deleted.MaChuyenBay = SOLUONGVE.MaChuyenBay)

END



insert into THAMSO values(0,2,30,10,20,0,24,23);
insert into SANBAY values('AP0001',N'Nội Bài');
insert into SANBAY values('AP0002',N'Tân Sơn Nhất');
insert into SANBAY values('AP0003',N'Cát Bi');
insert into SANBAY values('AP0004',N'Vinh');
insert into SANBAY values('AP0005',N'Phú Bài');
insert into SANBAY values('AP0006',N'Đà Nẵng');
insert into SANBAY values('AP0007',N'Cam Ranh');
insert into SANBAY values('AP0008',N'Cần Thơ');
insert into SANBAY values('AP0009',N'Phú Quốc');
insert into SANBAY values('AP0010',N'Vân Đồn');

insert into HANGVE values('HV0001',N'Thường',1.1);
insert into HANGVE values('HV0002',N'Vip',1.5);

insert into CHUYENBAY values('MH0370','AP0002','AP0001',100000,'2022-5-7 20:30:00',30)
insert into CHUYENBAY values('MH0666','AP0010','AP0009',100000,'2022-6-6 20:34:00',31)
insert into CHUYENBAY values('MH0656','AP0001','AP0008',1000000,'2022-5-12 8:30:00',45) 
insert into CHUYENBAY values('MH0676','AP0005','AP0006',200000,'2022-5-17 20:34:00',55)


insert into SOLUONGVE values('MH0370','HV0001',9,0)
insert into SOLUONGVE values('MH0370','HV0002',9,0)

insert into SOLUONGVE values('MH0666','HV0001',4,0)
insert into SOLUONGVE values('MH0666','HV0002',7,0)

insert into SOLUONGVE values('MH0656','HV0001',10,0)
insert into SOLUONGVE values('MH0656','HV0002',1,0)


insert into SOLUONGVE values('MH0676','HV0001',8,0)
insert into SOLUONGVE values('MH0676','HV0002',8,0)

insert into HANHKHACH values('62202100001',N'Nguyễn Văn A','096000001');
insert into HANHKHACH values('233322333',N'Nguyễn Văn B','0379875985');
insert into HANHKHACH values('1',N'Trần Văn Thành','0323000323');
insert into HANHKHACH values('2',N'Nguyễn Thị C','0323000324');
insert into HANHKHACH values('20524032',N'Nguyễn VX X','656000001');


insert into VEMAYBAY values('FF0001','MH0370','1','HV0001',110000,'Ve Mua')

insert into VEMAYBAY values('FF0002','MH0666','1','HV0001',110000,'Ve Mua')


