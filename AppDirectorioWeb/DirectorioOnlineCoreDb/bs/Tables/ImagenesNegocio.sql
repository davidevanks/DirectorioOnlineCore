CREATE TABLE [bs].[ImagenesNegocio] (
    [Id]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [IdNegocio] INT            NULL,
    [Image]     VARBINARY (50) NULL,
    CONSTRAINT [PK_ImagenesNegocio] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ImagenesNegocio_Negocio] FOREIGN KEY ([IdNegocio]) REFERENCES [bs].[Negocio] ([Id])
);

