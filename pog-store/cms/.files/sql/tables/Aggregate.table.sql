CREATE TABLE [dbo].[EventSources] (
    [AggregateId] UNIQUEIDENTIFIER NOT NULL,
    [Type]        VARCHAR (255)    NOT NULL,
    [Version]     INT              NOT NULL
);
