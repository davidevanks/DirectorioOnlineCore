CREATE TABLE [bs].[CuponRedencionUsuario] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [IdUsuario]      NVARCHAR (500) NOT NULL,
    [IdCupon]        INT            NOT NULL,
    [FechaRedencion] DATETIME       NOT NULL,
    CONSTRAINT [PK_CuponRedencionUsuario] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CuponRedencionUsuario_CuponNegocio] FOREIGN KEY ([IdCupon]) REFERENCES [bs].[CuponNegocio] ([Id])
);

