CREATE TABLE [dbo].[Well]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [PlatformId] INT NOT NULL, 
    [UniqueName] NVARCHAR(50) NULL, 
    [Latitude] FLOAT NULL, 
    [Longitude] FLOAT NULL, 
    [CreatedAt] DATETIME NULL, 
    [UpdatedAt] DATETIME NULL, 
    CONSTRAINT [FK_Well_Platform] FOREIGN KEY ([PlatformId]) REFERENCES [Platform]([Id])
)
