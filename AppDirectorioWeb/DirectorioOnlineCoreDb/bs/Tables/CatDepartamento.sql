CREATE TABLE [bs].[CatDepartamento] (
    [Id]     INT           IDENTITY (1, 1) NOT NULL,
    [IdPais] INT           NULL,
    [Nombre] VARCHAR (300) NULL,
    [Activo] BIT           NULL,
    CONSTRAINT [PK_CatDepartamento] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CatDepartamento_CatPais] FOREIGN KEY ([IdPais]) REFERENCES [bs].[CatPais] ([Id])
);

