CREATE TABLE [dbo].[CatTipoPagoXcatalogoConfig] (
    [Id]                  INT NOT NULL,
    [IdCatConfigProdServ] INT NULL,
    [IdTipoPago]          INT NULL,
    [Active]              BIT NULL,
    CONSTRAINT [PK_CatTipoPagoXcatalogoConfig] PRIMARY KEY CLUSTERED ([Id] ASC)
);

