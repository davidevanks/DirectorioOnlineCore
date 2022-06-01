CREATE TABLE [bs].[Reviews] (
    [Id]         BIGINT         IDENTITY (1, 1) NOT NULL,
    [IdBusiness] INT            NOT NULL,
    [IdUser]     NVARCHAR (450) NOT NULL,
    [FullName]   VARCHAR (500)  NULL,
    [Comments]   VARCHAR (MAX)  NULL,
    [Stars]      INT            NOT NULL,
    [Active]     BIT            NOT NULL,
    CONSTRAINT [PK_Reviews] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Reviews_Negocio] FOREIGN KEY ([IdBusiness]) REFERENCES [bs].[Negocio] ([Id])
);



