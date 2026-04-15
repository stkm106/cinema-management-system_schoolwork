CREATE DATABASE CinemaManagementDB;
GO
USE CinemaManagementDB;
GO

CREATE TABLE tbNguoiDung (
    MaND INT IDENTITY(1,1) PRIMARY KEY, -- Khóa chính tự động tăng
    HoTen NVARCHAR(100) NOT NULL,
    SDT VARCHAR(15),
    Email VARCHAR(100),
    Username VARCHAR(50) UNIQUE NOT NULL,
    Passwd VARCHAR(50) NOT NULL,
    Roles NVARCHAR(20) NOT NULL
);
GO

--Insert dữ liệu
INSERT INTO tbNguoiDung (HoTen, SDT, Email, Username, Passwd, Roles) 
VALUES (N'Quản Trị Viên', '0123459876', 'admin@cinema.com', 'admin', 'admin123', 'Admin'),
       (N'Nguyễn Văn A', '0987654321', 'staff01@cinema.com', 'nvql01', 'staff123', 'NhanVien'),
       (N'Trần Ngọc B', '0912345678', 'staff02@cinema.com', 'nvtk01', 'staff456', 'QuanLy'),
       (N'Khách Hàng Thân Thiết', '0123456789', 'guest006@gmail.com', 'kh01', 'khach1@@', 'KhachHang'),
       (N'Khách Hàng Vui Vẻ', '0246813579', 'guest007@gmail.com', 'kh02', 'khach2##', 'KhachHang');
GO

--SELECT * FROM tbNguoiDung;