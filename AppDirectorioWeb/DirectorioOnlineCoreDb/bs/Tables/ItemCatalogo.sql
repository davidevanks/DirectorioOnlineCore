CREATE TABLE [bs].[ItemCatalogo] (
    [Id]                     INT             IDENTITY (1, 1) NOT NULL,
    [IdConfigCatalogo]       INT             NOT NULL,
    [IdCategoriaItem]        INT             NOT NULL,
    [NombreItem]             VARCHAR (MAX)   NOT NULL,
    [DescripcionItem]        VARCHAR (MAX)   NOT NULL,
    [PrecioUnitario]         DECIMAL (18, 2) NOT NULL,
    [TieneDescuento]         BIT             NOT NULL,
    [PorcentajeDescuento]    INT             NOT NULL,
    [ImagenItem]             VARCHAR (MAX)   NOT NULL,
    [Activo]                 BIT             NOT NULL,
    [FechaCreacion]          DATETIME        NOT NULL,
    [IdUsuarioCreacion]      NVARCHAR (500)  NOT NULL,
    [FechaActualizacion]     DATETIME        NULL,
    [IdUsuarioActualizacion] NVARCHAR (500)  NULL,
    CONSTRAINT [PK_ItemCatalogo] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ItemCatalogo_CatCategoria] FOREIGN KEY ([IdCategoriaItem]) REFERENCES [bs].[CatCategoria] ([Id]),
    CONSTRAINT [FK_ItemCatalogo_ConfigCatalogo] FOREIGN KEY ([IdConfigCatalogo]) REFERENCES [bs].[ConfigCatalogo] ([Id])
);





