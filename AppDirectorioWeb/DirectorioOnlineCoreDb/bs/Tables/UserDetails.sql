CREATE TABLE [bs].[UserDetails] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [UserId]             NVARCHAR (450) NULL,
    [UserPicture]        VARCHAR (MAX)  NULL,
    [FullName]           VARCHAR (500)  NULL,
    [NotificationsPromo] BIT            NULL,
    [IdPlan]             INT            CONSTRAINT [DF_UserDetails_IdPlan] DEFAULT ((1)) NULL,
    [PlanExpirationDate] DATE           NULL,
    [IdUserCreate]       NVARCHAR (450) NULL,
    [RegistrationDate]   DATETIME       NOT NULL,
    [IdUserUpdate]       NVARCHAR (450) NULL,
    [UpdateDate]         DATETIME       NULL,
    CONSTRAINT [PK_UserDetails] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserDetails_CatPlan] FOREIGN KEY ([IdPlan]) REFERENCES [bs].[CatPlan] ([Id])
);







