CREATE TABLE [bs].[CatCategoria] (
    [Id]      INT           IDENTITY (1, 1) NOT NULL,
    [IdPadre] INT           NULL,
    [Nombre]  VARCHAR (300) NULL,
    [Activo]  BIT           NULL,
    CONSTRAINT [PK_CatCategorias] PRIMARY KEY CLUSTERED ([Id] ASC)
);

