-- Write your own SQL object definition here, and it'll be included in your package.
CREATE TABLE [dbo].[Galaxy] (
    [Id] INT NOT NULL IDENTITY,
    [Name] NVARCHAR(50) NOT NULL,
    [Description] NVARCHAR(2000) NULL,

    CONSTRAINT [PK_Galaxy] PRIMARY KEY CLUSTERED ([Id]),
    CONSTRAINT [Uk_Galaxy] UNIQUE ([Name])
);
