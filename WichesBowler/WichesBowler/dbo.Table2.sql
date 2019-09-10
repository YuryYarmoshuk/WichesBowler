CREATE TABLE [dbo].[Orders]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [fName] NVARCHAR(50) NULL, 
    [mName] NVARCHAR(50) NULL, 
    [sName] NVARCHAR(50) NULL, 
    [Date] DATE NULL, 
    [Status] NVARCHAR(50) NULL, 
    [idBucket] INT NULL
)
