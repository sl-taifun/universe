CREATE TABLE [dbo].[Rel__Star_Planet] (
    [StarId] INT NOT NULL,
    [PlanetId] INT NOT NULL,

    CONSTRAINT [PK_Rel__Star_Planet] PRIMARY KEY CLUSTERED ([StarId], [PlanetId]),
    CONSTRAINT [FK_Rel__Star_Planet_Star] 
        FOREIGN KEY ([StarId]) 
        REFERENCES [dbo].[Star] ([Id]) ,

    CONSTRAINT [FK_Rel__Star_Planet_Planet] 
        FOREIGN KEY ([PlanetId]) 
        REFERENCES [dbo].[Planet] ([Id]) 
);  