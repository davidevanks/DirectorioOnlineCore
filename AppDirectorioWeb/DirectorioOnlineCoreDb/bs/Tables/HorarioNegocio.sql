CREATE TABLE [bs].[HorarioNegocio] (
    [Id]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [IdNegocio]    INT            NULL,
    [IdDia]        INT            NULL,
    [HoraDesde]    VARCHAR (50)   NULL,
    [HoraHasta]    VARCHAR (50)   NULL,
    [IdUserCreate] NVARCHAR (450) NOT NULL,
    [CreateDate]   DATETIME       NOT NULL,
    [IdUserUpdate] NVARCHAR (450) NULL,
    [UpdateDate]   DATETIME       NULL,
    CONSTRAINT [PK_HorarioNegocio] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HorarioNegocio_Negocio] FOREIGN KEY ([IdNegocio]) REFERENCES [bs].[Negocio] ([Id])
);



