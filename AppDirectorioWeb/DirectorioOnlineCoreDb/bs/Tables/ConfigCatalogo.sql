CREATE TABLE [bs].[ConfigCatalogo] (
    [Id]                        INT            NOT NULL,
    [IdNegocio]                 INT            NOT NULL,
    [NombreCatalogo]            VARCHAR (MAX)  NOT NULL,
    [IdTipoCatalogo]            INT            NULL,
    [IdMoneda]                  INT            NOT NULL,
    [IdTipoPago]                INT            NOT NULL,
    [DescuentoMasivo]           BIT            NULL,
    [PorcentajeDescuentoMasivo] INT            NULL,
    [Activo]                    BIT            NOT NULL,
    [FechaCreacion]             DATETIME       NOT NULL,
    [IdUsuarioCreacion]         NVARCHAR (500) NOT NULL,
    [FechaActualizacion]        DATETIME       NULL,
    [IdUsuarioActualizacion]    NVARCHAR (500) NULL,
    CONSTRAINT [PK_ConfigCatalogo] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'para saber si el catalogo es para prductos o servicios', @level0type = N'SCHEMA', @level0name = N'bs', @level1type = N'TABLE', @level1name = N'ConfigCatalogo';

