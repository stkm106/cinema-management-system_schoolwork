USE CinemaManagementDB;
GO

CREATE TABLE tbPhongChieu (
    MaPhong INT PRIMARY KEY,       
    LoaiPhong NVARCHAR(50),        -- 2D, 3D, IMAX
    SoGhe INT NOT NULL         
);
GO

--insert dữ liệu
INSERT INTO tbPhongChieu (MaPhong, LoaiPhong, SoGhe)
VALUES  (1, N'2D', 50),
        (2, N'3D', 40),
        (3, N'IMAX', 100);
GO