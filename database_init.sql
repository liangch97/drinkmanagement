-- 饮品管理系统数据库初始化脚本

-- 创建数据库
CREATE DATABASE IF NOT EXISTS drinkmanagement 
CHARACTER SET utf8mb4 
COLLATE utf8mb4_unicode_ci;

-- 创建专用数据库用户（推荐，安全性更高）
-- 注意：在生产环境中使用更强的密码
CREATE USER IF NOT EXISTS 'drinkuser'@'localhost' IDENTIFIED BY 'YourSecurePassword123!';
GRANT ALL PRIVILEGES ON drinkmanagement.* TO 'drinkuser'@'localhost';
FLUSH PRIVILEGES;

USE drinkmanagement;

-- 创建分类表
CREATE TABLE IF NOT EXISTS Categories (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(50) NOT NULL,
    Description VARCHAR(200),
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    INDEX idx_name (Name)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 创建饮品表
CREATE TABLE IF NOT EXISTS Drinks (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Description VARCHAR(500),
    Price DECIMAL(10, 2) NOT NULL,
    CategoryId INT NOT NULL,
    Stock INT NOT NULL DEFAULT 0,
    ImageUrl VARCHAR(500),
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    INDEX idx_name (Name),
    INDEX idx_category (CategoryId),
    FOREIGN KEY (CategoryId) REFERENCES Categories(Id) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 插入初始分类数据
INSERT INTO Categories (Id, Name, Description, CreatedAt) VALUES
(1, '茶饮', '各类茶饮料', NOW()),
(2, '咖啡', '咖啡类饮品', NOW()),
(3, '果汁', '鲜榨果汁', NOW()),
(4, '奶茶', '奶茶系列', NOW());

-- 插入初始饮品数据
INSERT INTO Drinks (Id, Name, Description, Price, CategoryId, Stock, CreatedAt, UpdatedAt) VALUES
(1, '绿茶', '清新绿茶', 8.00, 1, 100, NOW(), NOW()),
(2, '红茶', '经典红茶', 8.00, 1, 100, NOW(), NOW()),
(3, '美式咖啡', '浓郁美式', 15.00, 2, 80, NOW(), NOW()),
(4, '拿铁', '香醇拿铁', 18.00, 2, 80, NOW(), NOW()),
(5, '橙汁', '鲜榨橙汁', 12.00, 3, 60, NOW(), NOW()),
(6, '珍珠奶茶', '经典珍珠奶茶', 10.00, 4, 90, NOW(), NOW());

-- 查看创建的表
SHOW TABLES;

-- 查看分类数据
SELECT * FROM Categories;

-- 查看饮品数据
SELECT d.*, c.Name as CategoryName 
FROM Drinks d 
JOIN Categories c ON d.CategoryId = c.Id;
