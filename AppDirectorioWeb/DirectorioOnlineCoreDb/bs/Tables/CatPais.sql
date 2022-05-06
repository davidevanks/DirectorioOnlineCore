﻿CREATE TABLE [bs].[CatPais] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Nombre]       VARCHAR (300)  NULL,
    [Activo]       BIT            NULL,
    [IdUserCreate] NVARCHAR (450) NOT NULL,
    [CreateDate]   DATETIME       NOT NULL,
    [IdUserUpdate] NVARCHAR (450) NULL,
    [UpdateDate]   DATETIME       NULL,
    CONSTRAINT [PK_CatPais] PRIMARY KEY CLUSTERED ([Id] ASC)
);



