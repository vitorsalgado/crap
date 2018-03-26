CREATE TABLE [dbo].[Snapshot] (
    [AggregateId] UNIQUEIDENTIFIER NOT NULL,
    [SerializedData]        VARBINARY (MAX)  NOT NULL,
    [Version]     INT              NOT NULL,
    [DateTime]    DATETIME         NOT NULL
);
