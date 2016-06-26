CREATE TABLE [dbo].[EventSource] (
    [Id]			UNIQUEIDENTIFIER	NOT NULL,
	[Name]			VARCHAR(255)		NOT NULL,
    [Type]			VARCHAR (255)		NOT NULL,
    [Version]		INT					NOT NULL, 
    CONSTRAINT [PK_EventSources] PRIMARY KEY ([Id])
);
