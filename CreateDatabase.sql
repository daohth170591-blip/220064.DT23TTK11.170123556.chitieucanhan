-- Tạo database
CREATE DATABASE ExpenseManagerDB;
GO

USE ExpenseManagerDB;
GO

-- Bảng Users
CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    PasswordHash NVARCHAR(MAX) NOT NULL,
    IsAdmin BIT NOT NULL,
    CreatedAt DATETIME2 NOT NULL
);

-- Bảng Categories
CREATE TABLE Categories (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Icon NVARCHAR(10) NOT NULL,
    Color NVARCHAR(20) NOT NULL
);

-- Bảng Transactions
CREATE TABLE Transactions (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Amount DECIMAL(18,2) NOT NULL,
    Type INT NOT NULL,
    CategoryId INT NOT NULL,
    UserId INT NOT NULL,
    Date DATETIME2 NOT NULL,
    Description NVARCHAR(500) NOT NULL,
    AttachmentPath NVARCHAR(MAX) NOT NULL,
    CreatedAt DATETIME2 NOT NULL,
    FOREIGN KEY (CategoryId) REFERENCES Categories(Id),
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

-- Bảng Budgets
CREATE TABLE Budgets (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    CategoryId INT NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    StartDate DATETIME2 NOT NULL,
    EndDate DATETIME2 NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
);

-- Bảng FinancialGoals
CREATE TABLE FinancialGoals (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    Name NVARCHAR(200) NOT NULL,
    TargetAmount DECIMAL(18,2) NOT NULL,
    CurrentAmount DECIMAL(18,2) NOT NULL,
    TargetDate DATETIME2 NOT NULL,
    ImagePath NVARCHAR(MAX) NOT NULL,
    CreatedAt DATETIME2 NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

-- Thêm dữ liệu Categories
INSERT INTO Categories (Name, Icon, Color) VALUES
(N'Lương', N'💰', '#4CAF50'),
(N'Ăn uống', N'🍔', '#FF9800'),
(N'Đi lại', N'🚗', '#2196F3'),
(N'Mua sắm', N'🛒', '#E91E63'),
(N'Giải trí', N'🎮', '#9C27B0'),
(N'Hóa đơn', N'📄', '#F44336'),
(N'Sức khỏe', N'🏥', '#00BCD4'),
(N'Giáo dục', N'📚', '#3F51B5');

GO

SELECT 'Database ExpenseManagerDB đã được tạo thành công!' AS Message;
