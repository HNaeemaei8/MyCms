IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Roles] (
    [RoleId] int NOT NULL IDENTITY,
    [RoleTitle] nvarchar(max) NULL,
    [RoleName] nvarchar(max) NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([RoleId])
);

GO

CREATE TABLE [Users] (
    [UserId] int NOT NULL IDENTITY,
    [RoleId] int NOT NULL,
    [UserName] nvarchar(200) NOT NULL,
    [Email] nvarchar(200) NOT NULL,
    [Password] nvarchar(200) NOT NULL,
    [IsActive] bit NOT NULL,
    [ActiveCode] nvarchar(50) NULL,
    [CreateDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([UserId]),
    CONSTRAINT [FK_Users_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([RoleId]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Users_RoleId] ON [Users] ([RoleId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191022141701_initialDataBase', N'2.1.14-servicing-32113');

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210102094219_InitPage', N'2.1.14-servicing-32113');

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210106195000_inipage', N'2.1.14-servicing-32113');

GO

