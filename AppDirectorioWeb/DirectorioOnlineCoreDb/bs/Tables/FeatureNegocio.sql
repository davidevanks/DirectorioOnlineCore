CREATE TABLE [bs].[FeatureNegocio] (
    [Id]        BIGINT IDENTITY (1, 1) NOT NULL,
    [IdFeature] INT    NULL,
    [IdNegocio] INT    NULL,
    [Activo]    BIT    NULL,
    CONSTRAINT [PK_FeatureNegocio] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FeatureNegocio_CatCategoria] FOREIGN KEY ([IdFeature]) REFERENCES [bs].[CatCategoria] ([Id]),
    CONSTRAINT [FK_FeatureNegocio_Negocio] FOREIGN KEY ([IdNegocio]) REFERENCES [bs].[Negocio] ([Id])
);

