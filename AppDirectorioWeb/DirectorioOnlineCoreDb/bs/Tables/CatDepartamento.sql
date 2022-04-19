CREATE TABLE [bs].[CatDepartamento] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [IdPais]       INT            NULL,
    [Nombre]       VARCHAR (300)  NULL,
    [Activo]       BIT            NULL,
    [IdUserCreate] NVARCHAR (450) NOT NULL,
    [CreateDate]   DATETIME       NOT NULL,
    [IdUserUpdate] NVARCHAR (450) NULL,
    [UpdateDate]   DATETIME       NULL,
    CONSTRAINT [PK_CatDepartamento] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CatDepartamento_CatPais] FOREIGN KEY ([IdPais]) REFERENCES [bs].[CatPais] ([Id])
);



