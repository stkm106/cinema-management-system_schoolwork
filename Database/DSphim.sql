USE CinemaManagementDB;
GO

CREATE TABLE tbPhim (
    MaPhim INT IDENTITY(1,1) PRIMARY KEY, -- Khóa chính tự động tăng
    TenPhim NVARCHAR(200) NOT NULL,       
    TheLoai NVARCHAR(100),                
    ThoiLuong INT,                        -- Tính bằng phút
    DaoDien NVARCHAR(100),
    NgayKhoiChieu DATE,                   -- format mặc định YYYY-MM-DD
    Poster VARCHAR(100)                   -- Lưu đường dẫn ảnh (xài png hết)
);
GO

-- Insert dữ liệu
INSERT INTO tbPhim (TenPhim, TheLoai, ThoiLuong, DaoDien, NgayKhoiChieu, Poster)
VALUES  (N'Madagascar', N'Hoạt hình', 86, N'Tom McGrath, Eric Darnell', '2026-04-08', 'p01.png'),
        (N'Bí Kíp Luyện Rồng', N'Phiêu lưu', 125, N'Dean DeBlois', '2026-04-09', 'p02.png'),
        (N'Kẻ Ăn Hồn', N'Kinh dị', 105, N'Trần Hữu Tấn', '2026-04-10', 'p03.png'),
        (N'The Godfather', N'Hành động', 175, N'Francis Ford Coppola', '2026-04-11', 'p04.png'),
        (N'Lalaland', N'Lãng mạn', 128, N'Damien Chazelle', '2026-04-11', 'p05.png'),
        (N'Suzume', N'Hoạt hình', 122, N'Shinkai Makoto', '2026-04-10', 'p06.png'),
        (N'Mean Girls', N'Hài hước', 97, N'Mark Waters', '2026-04-09', 'p07.png'),
        (N'Những Đứa Trẻ Trong Sương', N'Tài liệu', 92, N'Hà Lệ Diễm', '2026-04-08', 'p08.png'),
        (N'The Blair Witch Project', N'Kinh dị', 78, N'Daniel Myrick, Eduardo Sánchez', '2026-04-12', 'p09.png');
GO