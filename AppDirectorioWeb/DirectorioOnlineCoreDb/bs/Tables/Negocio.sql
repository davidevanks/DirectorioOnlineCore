CREATE TABLE [bs].[Negocio] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [NombreNegocio]      VARCHAR (MAX) NOT NULL,
    [DescripcionNegocio] VARCHAR (MAX) NOT NULL,
    [Tags]               VARCHAR (MAX) NULL,
    [IdCategoria]        INT           NOT NULL,
    [IdDepartamento]     INT           NOT NULL,
    [DireccionNegocio]   VARCHAR (MAX) NOT NULL,
    [TelefonoNegocio1]   VARCHAR (10)  NOT NULL,
    [TelefonoNegocio2]   VARCHAR (10)  NULL,
    [TelefonoWhatsApp]   VARCHAR (10)  NULL,
    [EmailNegocio]       VARCHAR (250) NOT NULL,
    [SitioWebNegocio]    VARCHAR (500) NULL,
    [LinkedInURL]        VARCHAR (200) NULL,
    [FacebookURL]        VARCHAR (200) NULL,
    [TwitterURL]         VARCHAR (200) NULL,
    [LogoNegocio]        BINARY (50)   NULL,
    [Status]             INT           NOT NULL,
    CONSTRAINT [PK_Negocio] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Negocio_CatCategoria] FOREIGN KEY ([IdCategoria]) REFERENCES [bs].[CatCategoria] ([Id]),
    CONSTRAINT [FK_Negocio_CatDepartamento] FOREIGN KEY ([IdDepartamento]) REFERENCES [bs].[CatDepartamento] ([Id])
);

