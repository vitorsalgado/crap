CREATE TABLE [dbo].[EventLog] (
	[SequentialId]	INT IDENTITY(1,1),
	[Id]			UNIQUEIDENTIFIER	NOT NULL,
	[Name]			VARCHAR(255)		NOT NULL,
	[Type]			VARCHAR(255)		NOT NULL,
    [EventSourceId]	UNIQUEIDENTIFIER	NOT NULL,
	[Timestamp]		DATETIME			NOT NULL,
    [Data]			VARBINARY (MAX)		NOT NULL,
	[StoredOn]		DATETIME			NOT NULL DEFAULT (getdate()),
    [Version]		INT					NOT NULL,
    [SourceVersion] VARCHAR(255) NOT NULL, 
    CONSTRAINT [PK_Events] PRIMARY KEY ([SequentialId])
);
