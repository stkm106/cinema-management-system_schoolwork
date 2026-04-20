USE CinemaManagementDB;
GO

CREATE TABLE tbVeXemPhim (
    MaVe INT IDENTITY(1,1) PRIMARY KEY, -- Khóa chính tự động tăng
    MaLich INT,                    
    ViTriGhe NVARCHAR(10),         
    NgayBan DATETIME DEFAULT GETDATE(),
    TongTien DECIMAL(18, 2),        
    
    FOREIGN KEY (MaLich) REFERENCES tbLichChieu(MaLich)    -- Liên kết với bảng Phim
);