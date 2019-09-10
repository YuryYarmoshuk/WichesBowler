CREATE TABLE [dbo].[Products]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] NVARCHAR(50) NULL, 
    [Category] NVARCHAR(50) NULL, 
    [Cost] INT NULL, 
    [Discription] NVARCHAR(50) NULL, 
    [Quantity] INT NULL
)
