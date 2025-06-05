-- Write your own SQL object definition here, and it'll be included in your package.
CREATE TABLE [dbo].[Star] (
    [Id] INT NOT NULL IDENTITY,
    [Name] NVARCHAR(50) NOT NULL,
    [IsDeath] BIT DEFAULT 0,
    [GalaxyId] INT NOT NULL,

    CONSTRAINT [PK_Star] PRIMARY KEY CLUSTERED ([Id]),
    CONSTRAINT [FK_Star__Galaxy] 
        FOREIGN KEY ([GalaxyId]) 
        REFERENCES [dbo].[Galaxy] ([Id]),
);  