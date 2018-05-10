CREATE TABLE [dbo].[FileReference]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[FileUid] UNIQUEIDENTIFIER NOT NULL,
	[Name] nvarchar(128) NOT NULL,
	[Url] nvarchar(300) NOT NULL,
	[Tags] xml NULL,
	[CDate] datetime NOT NULL DEFAULT GETDATE(),
	[CBy] varchar(50) NOT NULL DEFAULT suser_sname()
)
