CREATE TABLE [bs].[LogError] (
    [Id]           BIGINT        NOT NULL,
    [MessageError] VARCHAR (MAX) NULL,
    [Observation]  VARCHAR (MAX) NULL,
    [Date]         DATETIME      NULL,
    [Status]       BIT           NULL,
    CONSTRAINT [PK_LogError] PRIMARY KEY CLUSTERED ([Id] ASC)
);

