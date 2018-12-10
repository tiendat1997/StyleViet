IF EXISTS 
   (
     SELECT name FROM master.dbo.sysdatabases 
    WHERE name = N'StyleVietDB'
    )
BEGIN
    SELECT 'Database Name already Exist' AS Message
END
ELSE
BEGIN
    CREATE DATABASE [StyleVietDB]
    SELECT 'New Database is Created'
END
GO
IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Account] (
    [Id] int NOT NULL IDENTITY,
    [Username] nvarchar(max) NULL,
    [Password] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [RoleId] int NOT NULL,
    [Expired] bit NOT NULL,
    CONSTRAINT [PK_Account] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Category] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Role] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Salon] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [Phone] nvarchar(max) NULL,
    [Address] nvarchar(max) NULL,
    CONSTRAINT [PK_Salon] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Admin] (
    [Id] int NOT NULL IDENTITY,
    [Fullname] nvarchar(max) NULL,
    [Phone] nvarchar(max) NULL,
    [AccountId] int NOT NULL,
    CONSTRAINT [PK_Admin] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Admin_Account_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Account] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [User] (
    [Id] int NOT NULL IDENTITY,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [Phone] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [AccountId] int NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_User_Account_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Account] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Service] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [CategoryId] int NOT NULL,
    CONSTRAINT [PK_Service] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Service_Category_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AccountRole] (
    [AccountId] int NOT NULL,
    [RoleId] int NOT NULL,
    CONSTRAINT [PK_AccountRole] PRIMARY KEY ([AccountId], [RoleId]),
    CONSTRAINT [FK_AccountRole_Account_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Account] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AccountRole_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Role] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Gallery] (
    [Id] int NOT NULL IDENTITY,
    [Url] nvarchar(max) NULL,
    [SalonId] int NOT NULL,
    CONSTRAINT [PK_Gallery] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Gallery_Salon_SalonId] FOREIGN KEY ([SalonId]) REFERENCES [Salon] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [SalonService] (
    [SalonId] int NOT NULL,
    [ServiceId] int NOT NULL,
    [Description] nvarchar(max) NULL,
    [Price] float NOT NULL,
    [Disabled] bit NOT NULL,
    CONSTRAINT [PK_SalonService] PRIMARY KEY ([SalonId], [ServiceId]),
    CONSTRAINT [FK_SalonService_Salon_SalonId] FOREIGN KEY ([SalonId]) REFERENCES [Salon] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_SalonService_Service_ServiceId] FOREIGN KEY ([ServiceId]) REFERENCES [Service] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_AccountRole_RoleId] ON [AccountRole] ([RoleId]);

GO

CREATE INDEX [IX_Admin_AccountId] ON [Admin] ([AccountId]);

GO

CREATE INDEX [IX_Gallery_SalonId] ON [Gallery] ([SalonId]);

GO

CREATE INDEX [IX_SalonService_ServiceId] ON [SalonService] ([ServiceId]);

GO

CREATE INDEX [IX_Service_CategoryId] ON [Service] ([CategoryId]);

GO

CREATE INDEX [IX_User_AccountId] ON [User] ([AccountId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20181210101147_InitialDB', N'2.1.4-rtm-31024');

GO

ALTER TABLE [Salon] ADD [AccountId] int NOT NULL DEFAULT 0;

GO

CREATE INDEX [IX_Salon_AccountId] ON [Salon] ([AccountId]);

GO

ALTER TABLE [Salon] ADD CONSTRAINT [FK_Salon_Account_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Account] ([Id]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20181210101900_AddLinkAccountToSalon', N'2.1.4-rtm-31024');

GO

