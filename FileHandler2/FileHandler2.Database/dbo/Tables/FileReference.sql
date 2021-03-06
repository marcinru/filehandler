CREATE TABLE [dbo].[FileReference]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[FileUid] UNIQUEIDENTIFIER NOT NULL,
	[Name] nvarchar(128) NOT NULL,
	[Url] nvarchar(300) NOT NULL,
	[Tags] xml NULL,
	Actual smallint DEFAULT 1,
	FileSize int NOT NULL,
	[CDate] datetime NOT NULL DEFAULT GETDATE(),
	[CBy] varchar(50) NOT NULL DEFAULT suser_sname()
)
