CREATE TABLE [bs].[CuponNegocio] (
    [Id]                      INT             IDENTITY (1, 1) NOT NULL,
    [IdNegocio]               INT             NOT NULL,
    [NombrePromocion]         VARCHAR (500)   NULL,
    [DescripcionPromocion]    VARCHAR (MAX)   NOT NULL,
    [DescuentoPorcentaje]     BIT             NOT NULL,
    [DescuentoMonto]          BIT             NOT NULL,
    [MonedaMonto]             INT             NULL,
    [ValorCupon]              DECIMAL (18, 2) NOT NULL,
    [CantidadCuponDisponible] INT             NOT NULL,
    [CantidadCuponUsados]     INT             NOT NULL,
    [ImagenCupon]             VARCHAR (MAX)   NULL,
    [FechaExpiracionCupon]    DATETIME        NOT NULL,
    [Status]                  BIT             NOT NULL,
    [FechaCreacion]           DATETIME        NOT NULL,
    [IdUsuarioCreacion]       NVARCHAR (300)  NOT NULL,
    [FechaModificacion]       DATETIME        NULL,
    [IdUsuarioModificacion]   NVARCHAR (300)  NULL,
    CONSTRAINT [PK_CuponNegocio] PRIMARY KEY CLUSTERED ([Id] ASC)
);



