IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Categories] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [CreatedBy] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedBy] nvarchar(max) NOT NULL,
    [UpdateAt] datetime2 NULL,
    [DeletableBy] nvarchar(max) NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Courses] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(50) NOT NULL,
    [ShortDescription] nvarchar(280) NOT NULL,
    [LongDescription] nvarchar(600) NOT NULL,
    [TargetAudience] nvarchar(20) NOT NULL,
    [Objectives] nvarchar(100) NOT NULL,
    [Requirements] nvarchar(100) NOT NULL,
    [Level] int NOT NULL,
    [CreatedBy] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedBy] nvarchar(max) NOT NULL,
    [UpdateAt] datetime2 NULL,
    [DeletableBy] nvarchar(max) NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Courses] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(50) NOT NULL,
    [LastName] nvarchar(100) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [CreatedBy] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedBy] nvarchar(max) NOT NULL,
    [UpdateAt] datetime2 NULL,
    [DeletableBy] nvarchar(max) NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [CategoryCourse] (
    [CategoriesId] int NOT NULL,
    [CoursesId] int NOT NULL,
    CONSTRAINT [PK_CategoryCourse] PRIMARY KEY ([CategoriesId], [CoursesId]),
    CONSTRAINT [FK_CategoryCourse_Categories_CategoriesId] FOREIGN KEY ([CategoriesId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CategoryCourse_Courses_CoursesId] FOREIGN KEY ([CoursesId]) REFERENCES [Courses] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Chapters] (
    [Id] int NOT NULL IDENTITY,
    [CourseID] int NOT NULL,
    [CreatedBy] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedBy] nvarchar(max) NOT NULL,
    [UpdateAt] datetime2 NULL,
    [DeletableBy] nvarchar(max) NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Chapters] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Chapters_Courses_CourseID] FOREIGN KEY ([CourseID]) REFERENCES [Courses] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Students] (
    [Id] int NOT NULL IDENTITY,
    [FirstName] nvarchar(100) NOT NULL,
    [LastName] nvarchar(100) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [UserId] int NULL,
    [CreatedBy] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedBy] nvarchar(max) NOT NULL,
    [UpdateAt] datetime2 NULL,
    [DeletableBy] nvarchar(max) NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Students] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Students_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
);
GO

CREATE TABLE [CourseStudent] (
    [CoursesId] int NOT NULL,
    [StudentsId] int NOT NULL,
    CONSTRAINT [PK_CourseStudent] PRIMARY KEY ([CoursesId], [StudentsId]),
    CONSTRAINT [FK_CourseStudent_Courses_CoursesId] FOREIGN KEY ([CoursesId]) REFERENCES [Courses] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CourseStudent_Students_StudentsId] FOREIGN KEY ([StudentsId]) REFERENCES [Students] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_CategoryCourse_CoursesId] ON [CategoryCourse] ([CoursesId]);
GO

CREATE UNIQUE INDEX [IX_Chapters_CourseID] ON [Chapters] ([CourseID]);
GO

CREATE INDEX [IX_CourseStudent_StudentsId] ON [CourseStudent] ([StudentsId]);
GO

CREATE INDEX [IX_Students_UserId] ON [Students] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230718154339_UniversityDB', N'6.0.20');
GO

COMMIT;
GO

