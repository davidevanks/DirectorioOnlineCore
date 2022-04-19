CREATE TABLE [bs].[UserDetails] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [UserId]           NVARCHAR (450) NULL,
    [FullName]         VARCHAR (500)  NULL,
    [RegistrationDate] DATETIME       NOT NULL,
    [IdUserUpdate]     NVARCHAR (450) NULL,
    [UpdateDate]       DATETIME       NULL,
    CONSTRAINT [PK_UserDetails] PRIMARY KEY CLUSTERED ([Id] ASC)
);

