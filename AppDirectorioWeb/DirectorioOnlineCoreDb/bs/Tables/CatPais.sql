CREATE TABLE [bs].[CatPais] (
    [Id]     INT           IDENTITY (1, 1) NOT NULL,
    [Nombre] VARCHAR (300) NULL,
    [Activo] BIT           NULL,
    CONSTRAINT [PK_CatPais] PRIMARY KEY CLUSTERED ([Id] ASC)
);

