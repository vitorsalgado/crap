CREATE TABLE [dbo].[Snapshot] (
    [EventSourceId] UNIQUEIDENTIFIER NOT NULL,
    [Data]        VARBINARY (MAX)  NOT NULL,
    [Version]     INT              NULL,
    [Timestamp]    DATETIME         NOT NULL, 
    [Type] VARCHAR(255) NOT NULL
);
