CREATE TABLE [bs].[ItemCatalogo] (
    [Id]                     INT             IDENTITY (1, 1) NOT NULL,
    [IdConfigCatalogo]       INT             NULL,
    [CodigoItemRef]          VARCHAR (MAX)   NOT NULL,
    [NombreItem]             VARCHAR (MAX)   NOT NULL,
    [DescripcionItem]        VARCHAR (MAX)   NOT NULL,
    [PrecioUnitario]         DECIMAL (18, 2) NOT NULL,
    [TieneDescuento]         BIT             NULL,
    [PorcentajeDescuento]    INT             NULL,
    [ImagenItem]             VARCHAR (MAX)   NOT NULL,
    [Activo]                 BIT             NULL,
    [FechaCreacion]          DATETIME        NOT NULL,
    [IdUsuarioCreacion]      NVARCHAR (500)  NOT NULL,
    [FechaActualizacion]     DATETIME        NULL,
    [IdUsuarioActualizacion] NVARCHAR (500)  NULL,
    CONSTRAINT [PK_ItemCatalogo] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ItemCatalogo_ConfigCatalogo] FOREIGN KEY ([IdConfigCatalogo]) REFERENCES [bs].[ConfigCatalogo] ([Id])
);



