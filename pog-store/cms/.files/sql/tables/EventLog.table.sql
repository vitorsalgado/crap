CREATE TABLE [dbo].[Events] (
    [AggregateId] UNIQUEIDENTIFIER NOT NULL,
    [Data]        VARBINARY (MAX)  NOT NULL,
    [Version]     INT              NOT NULL,
    [DateTime]    DATETIME         NOT NULL
);
