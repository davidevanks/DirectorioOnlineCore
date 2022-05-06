CREATE TABLE [bs].[ImagenesNegocio] (
    [Id]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [IdNegocio]    INT            NULL,
    [Image]        VARCHAR (MAX)  NULL,
    [IdUserCreate] NVARCHAR (450) NOT NULL,
    [CreateDate]   DATETIME       NOT NULL,
    [IdUserUpdate] NVARCHAR (450) NULL,
    [UpdateDate]   DATETIME       NULL,
    CONSTRAINT [PK_ImagenesNegocio] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ImagenesNegocio_Negocio] FOREIGN KEY ([IdNegocio]) REFERENCES [bs].[Negocio] ([Id])
);





