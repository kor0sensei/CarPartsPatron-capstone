CREATE TABLE [UserProfile] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [FireBaseUserId] nvarchar(255) NOT NULL,
  [Displayname] nvarchar(255) NOT NULL,
  [FirstName] nvarchar(255) NOT NULL,
  [LastName] nvarchar(255) NOT NULL,
  [Email] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [Car] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [UserProfileId] int NOT NULL,
  [Year] int NOT NULL,
  [Manufacturer] nvarchar(255) NOT NULL,
  [Model] nvarchar(255) NOT NULL,
  [Submodel] nvarchar(255),
  [Engine] nvarchar(255),
  [Drivetrain] nvarchar(255),
  [Transmission] nvarchar(255),
  [Color] nvarchar(255),
  [PhotoUrl] nvarchar(255)

)
GO

CREATE TABLE [Part] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [CarId] int NOT NULL,
  [Brand] nvarchar(255),
  [PartType] nvarchar(255) NOT NULL,
  [Price] int,
  [PhotoUrl] nvarchar(255),
  [DateInstalled] datetime

  CONSTRAINT [FK_Part_Car] FOREIGN KEY ([CarId]) REFERENCES [Car] ([Id]) ON DELETE CASCADE
)
GO

CREATE TABLE [PartSetup] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [PartId] int NOT NULL,
  [SetupNote] nvarchar(255) NOT NULL,
  [CreateDateTime] datetime

  CONSTRAINT [FK_PartSetup_Part] FOREIGN KEY ([PartId]) REFERENCES [Part] ([Id]) ON DELETE CASCADE
)
GO
