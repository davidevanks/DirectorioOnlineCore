CREATE TABLE [bs].[CatTipoPagoXcatalogoConfig] (
    [Id]                  INT IDENTITY (1, 1) NOT NULL,
    [IdCatConfigProdServ] INT NULL,
    [IdTipoPago]          INT NULL,
    [Active]              BIT NULL,
    CONSTRAINT [PK_CatTipoPagoXcatalogoConfig] PRIMARY KEY CLUSTERED ([Id] ASC)
);

