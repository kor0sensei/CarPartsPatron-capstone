CREATE TABLE [UserProfile] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [FireBaseUserId] nvarchar(255) NOT NULL,
  [Displayname] nvarchar(255) NOT NULL,
  [FirstName] nvarchar(255) NOT NULL,
  [LastName] nvarchar(255) NOT NULL,
  [Email] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [Cars] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [UserProfileId] int NOT NULL,
  [Manufacturer] nvarchar(255) NOT NULL,
  [Model] nvarchar(255) NOT NULL,
  [Submodel] nvarchar(255),
  [Engine] nvarchar(255),
  [Drivetrain] nvarchar(255),
  [Transmission] nvarchar(255),
  [Color] nvarchar(255),
  [PhotoURL] nvarchar(255) NOT NULL

)
GO

CREATE TABLE [Parts] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [CarsId] int NOT NULL,
  [Brand] nvarchar(255),
  [PartType] nvarchar(255) NOT NULL,
  [Price] int NOT NULL,
  [PhotoURL] nvarchar(255),
  [DateInstalled] nvarchar(255)

  CONSTRAINT [FK_Parts_Cars] FOREIGN KEY ([CarsId]) REFERENCES [Cars] ([Id]) ON DELETE CASCADE
)
GO

CREATE TABLE [PartsSetUp] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [PartsId] int NOT NULL,
  [CreateDateTime] nvarchar(255) NOT NULL,
  [SetUpNotes] nvarchar(255) NOT NULL

  CONSTRAINT [FK_PartsSetup_Parts] FOREIGN KEY ([PartsId]) REFERENCES [Parts] ([Id]) ON DELETE CASCADE
)
GO
