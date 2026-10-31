USE WB43;
GO
DROP TABLE IF EXISTS [Transaction];
DROP TABLE IF EXISTS Account;
GO
CREATE TABLE [dbo].[Account]
(
    [number]  INT,
    [name]    NVARCHAR(50) NOT NULL,
    [balance] INT NOT NULL,
    [nowdate] DATETIME NULL,
    CONSTRAINT [PK_Account_Number] PRIMARY KEY ([number])
)
GO
CREATE TABLE [dbo].[Transaction]
(
    [Id]      INT IDENTITY PRIMARY KEY,
    [number]  INT NOT NULL,
    [input]   INT NOT NULL DEFAULT 0,
    [output]  INT NOT NULL DEFAULT 0,
    [balance] INT NOT NULL,
    [nowdate] DATETIME NULL,
    CONSTRAINT [FK_Transaction_Account] 
        FOREIGN KEY ([number]) REFERENCES [Account]([number])
)

USE WB43;
INSERT INTO Account VALUES(111, '홍길동', 1000, GetDate());
SELECT * FROM Account;

USE WB43;
DELETE FROM [Transaction];
DELETE FROM Account;

USE WB43;
GO

-- 계좌 개설 프로시저
CREATE PROCEDURE proc_Insert_Account
    @number INT,
    @name NVARCHAR(50),
    @money INT
AS
BEGIN
    INSERT INTO Account VALUES(@number, @name, @money, GetDate())
    INSERT INTO [Transaction] VALUES(@number, @money, 0, @money, GetDate())
END
GO

-- 입금 프로시저
CREATE PROCEDURE proc_Input_Account
    @number INT,
    @money INT
AS
BEGIN
    DECLARE @balance INT
    SET @balance = (SELECT balance FROM Account WHERE number = @number) + @money
    UPDATE Account SET balance = balance + @money WHERE number = @number
    INSERT INTO [Transaction] VALUES(@number, @money, 0, @balance, GetDate())
END
GO

-- 출금 프로시저
CREATE PROCEDURE proc_Output_Account
    @number INT,
    @money INT
AS
BEGIN
    DECLARE @balance INT
    SET @balance = (SELECT balance FROM Account WHERE number = @number) - @money
    UPDATE Account SET balance = balance - @money WHERE number = @number
    INSERT INTO [Transaction] VALUES(@number, 0, @money, @balance, GetDate())
END
GO

-- 계좌 삭제 프로시저
CREATE PROCEDURE proc_Delete_Account
    @number INT
AS
BEGIN
    DELETE [Transaction] WHERE number = @number
    DELETE Account WHERE number = @number
END
GO