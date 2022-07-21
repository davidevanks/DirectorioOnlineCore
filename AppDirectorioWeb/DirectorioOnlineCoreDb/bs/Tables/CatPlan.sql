CREATE TABLE [bs].[CatPlan] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [PlanName] VARCHAR (250) NULL,
    [Active]   BIT           NULL,
    CONSTRAINT [PK_CatPlan] PRIMARY KEY CLUSTERED ([Id] ASC)
);

