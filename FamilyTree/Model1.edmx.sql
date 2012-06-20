
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 06/04/2012 20:28:54
-- Generated from EDMX file: D:\Library\IT Step\Entity Framework\FamilyTree\FamilyTree\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Genealogy];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Persons'
CREATE TABLE [dbo].[Persons] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Surname] nvarchar(max)  NULL,
    [Name] nvarchar(max)  NULL,
    [Patronymic] nvarchar(max)  NULL,
    [BirthDate] nvarchar(max)  NULL,
    [PlaceOfBirth] nvarchar(max)  NULL,
    [DeathDate] nvarchar(max)  NULL,
    [PlaceOfDeath] nvarchar(max)  NULL,
    [Nationality] nvarchar(max)  NULL,
    [Physique] nvarchar(max)  NULL,
    [CauseOfDeath] nvarchar(max)  NULL,
    [MentalHealth] nvarchar(max)  NULL,
    [Religion] nvarchar(max)  NULL,
    [PoliticalViews] nvarchar(max)  NULL,
    [Education] nvarchar(max)  NULL,
    [Rank] nvarchar(max)  NULL,
    [Rewards] nvarchar(max)  NULL,
    [FinancialPosition] nvarchar(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Persons'
ALTER TABLE [dbo].[Persons]
ADD CONSTRAINT [PK_Persons]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------