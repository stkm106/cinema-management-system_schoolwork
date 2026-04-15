USE CinemaManagementDB;
GO

CREATE TABLE tbLichChieu (
    MaLich INT IDENTITY(1,1) PRIMARY KEY,   -- Khóa chính tự động tăng
    MaPhim INT,
    MaPhong INT,                    
    NgayChieu DATE,                 
    GioChieu TIME,                  
    GiaVe DECIMAL(18, 2),           
    
    FOREIGN KEY (MaPhim) REFERENCES tbPhim(MaPhim),         -- Liên kết với bảng Phim
    FOREIGN KEY (MaPhong) REFERENCES tbPhongChieu(MaPhong)  -- Liên kết với bảng Phòng
);
GO