CREATE TABLE [dbo].[Platform]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [UniqueName] NVARCHAR(50) NULL, 
    [Latitude] FLOAT NULL, 
    [Longitude] FLOAT NULL, 
    [CreatedAt] DATETIME NULL, 
    [UpdatedAt] DATETIME NULL
)
