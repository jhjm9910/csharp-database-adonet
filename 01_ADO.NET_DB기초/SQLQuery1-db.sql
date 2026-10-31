USE WB43;
GO

-- Product 테이블
CREATE TABLE Product
(
    [pid] INT IDENTITY(1000,1) PRIMARY KEY,
    [pname] VARCHAR(50) NOT NULL,
    [price] INT NOT NULL,
    [description] VARCHAR(100) NULL
);

-- Custom 테이블
CREATE TABLE Custom
(
    [cid] INT IDENTITY PRIMARY KEY,
    [cname] VARCHAR(50) NOT NULL,
    [phone] VARCHAR(50) NOT NULL
);

-- Sale 테이블
CREATE TABLE Sale
(
    [sid] INT IDENTITY PRIMARY KEY,
    [cid] INT NOT NULL,
    [pid] INT NOT NULL,
    [cnt] INT NOT NULL,
    [sdate] DATETIME NOT NULL
);

-- 테스트 데이터 넣기
INSERT INTO Product (pname, price, description) VALUES
    ('C#', 900, 'C# 교재입니다'),
    ('Java', 10000, 'Java 교재입니다'),
    ('ADO.NET', 15000, 'ADO.NET 교재입니다');

INSERT INTO Custom VALUES
    ('본인이름', '010-0000-0000'),
    ('홍길동', '010-1111-1111'),
    ('김길동', '010-2222-2222'),
    ('이길동', '010-3333-3333');

INSERT INTO Sale VALUES
    (1, 1000, 2, GETDATE()),
    (2, 1001, 1, GETDATE()),
    (3, 1002, 3, GETDATE());