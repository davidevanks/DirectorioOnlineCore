CREATE TABLE [bs].[Factura] (
    [Id]                 INT             IDENTITY (1, 1) NOT NULL,
    [UserId]             NVARCHAR (450)  NOT NULL,
    [IdPlan]             INT             NOT NULL,
    [NoAutorizacionPago] VARCHAR (300)   NULL,
    [MontoPagado]        DECIMAL (18, 2) NOT NULL,
    [FechaPago]          DATETIME        NULL,
    [FacturaPagada]      BIT             CONSTRAINT [DF_Factura_FacturaPagada] DEFAULT ((0)) NOT NULL,
    [FacturaEnviada]     BIT             CONSTRAINT [DF_Factura_FacturaEnviada] DEFAULT ((0)) NOT NULL,
    [FechaCreacion]      DATETIME        NOT NULL,
    [FechaActualizacion] DATETIME        NULL,
    [IdUserCreate]       NVARCHAR (450)  NOT NULL,
    [IdUserUpdate]       NVARCHAR (450)  NULL,
    CONSTRAINT [PK_Factura] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Factura_Factura] FOREIGN KEY ([Id]) REFERENCES [bs].[Factura] ([Id])
);

