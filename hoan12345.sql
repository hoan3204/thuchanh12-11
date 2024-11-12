CREATE DATABASE NhaSach;
USE NhaSach;
CREATE TABLE LoaiSach (
    MaLoai INT PRIMARY KEY AUTO_INCREMENT,
    TenLoai VARCHAR(100),
    MoTa TEXT
);
CREATE TABLE Sach (
    MaSach INT PRIMARY KEY AUTO_INCREMENT,
    TenSach VARCHAR(100),
    TacGia VARCHAR(100),
    GiaBan DECIMAL(18, 2),
    SoLuong INT,
    MoTa TEXT,
    MaLoai INT,
    FOREIGN KEY (MaLoai) REFERENCES LoaiSach(MaLoai)
);
CREATE TABLE KhachHang (
    MaKhachHang INT PRIMARY KEY AUTO_INCREMENT,
    TenKhachHang VARCHAR(100),
    DiaChi TEXT,
    SoDienThoai VARCHAR(20),
    Email VARCHAR(100)
);
CREATE TABLE HoaDon (
    MaHoaDon INT PRIMARY KEY AUTO_INCREMENT,
    NgayLap DATE,
    MaKhachHang INT,
    FOREIGN KEY (MaKhachHang) REFERENCES KhachHang(MaKhachHang)
);
CREATE TABLE ChiTietHoaDon (
    MaChiTietHoaDon INT PRIMARY KEY AUTO_INCREMENT,
    MaHoaDon INT,
    MaSach INT,
    SoLuong INT,
    DonGia DECIMAL(18, 2),
    FOREIGN KEY (MaHoaDon) REFERENCES HoaDon(MaHoaDon),
    FOREIGN KEY (MaSach) REFERENCES Sach(MaSach)
);
CREATE TABLE TaiKhoan (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Username VARCHAR(50) UNIQUE,
    Password VARCHAR(50)
);
