CREATE TABLE [bs].[CatTipoPagoXcatalogoConfig] (
    [Id]                  INT IDENTITY (1, 1) NOT NULL,
    [IdCatConfigProdServ] INT NOT NULL,
    [IdTipoPago]          INT NOT NULL,
    [Active]              BIT NOT NULL,
    CONSTRAINT [PK_CatTipoPagoXcatalogoConfig] PRIMARY KEY CLUSTERED ([Id] ASC)
);



