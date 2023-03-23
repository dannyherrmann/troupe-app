CREATE DATABASE [Troupe]
GO

USE [Troupe]
GO

CREATE TABLE [users] (
  [id] int PRIMARY KEY IDENTITY,
  [uid] varchar(255),
  [name] varchar(255),
  [email] varchar(255),
  [photo] varchar(255),
  [bio] varchar(500),
  [phone] varchar(255),
)
GO

CREATE TABLE [userTroupes] (
  [id] int PRIMARY KEY IDENTITY,
  [userId] int,
  [troupeId] int,
  [isLeader] bit,
  [userTypeId] int
)
GO

CREATE TABLE [userTypes] (
  [id] int PRIMARY KEY IDENTITY,
  [name] varchar(255)
)
GO

CREATE TABLE [troupe] (
  [id] int PRIMARY KEY IDENTITY,
  [name] varchar(255),
  [logo] varchar(255)
)
GO

CREATE TABLE [events] (
  [id] int PRIMARY KEY IDENTITY,
  [eventTypeId] int,
  [startDateTime] dateTime,
  [endDateTime] dateTime,
  [troupeId] int
)
GO

CREATE TABLE [eventTypes] (
  [id] int PRIMARY KEY IDENTITY,
  [name] varchar(255)
)
GO

CREATE TABLE [availability] (
  [id] int PRIMARY KEY IDENTITY,
  [userTroupeId] int,
  [eventId] int,
  [response] varchar(255)
)
GO

CREATE TABLE [eventCast] (
  [id] int PRIMARY KEY IDENTITY,
  [eventId] int,
  [userTroupeId] int
)
GO

ALTER TABLE [userTroupes] ADD FOREIGN KEY ([troupeId]) REFERENCES [troupe] ([id])
GO

ALTER TABLE [userTroupes] ADD FOREIGN KEY ([userId]) REFERENCES [users] ([id])
GO

ALTER TABLE [availability] ADD FOREIGN KEY ([userTroupeId]) REFERENCES [userTroupes] ([id])
GO

ALTER TABLE [availability] ADD FOREIGN KEY ([eventId]) REFERENCES [events] ([id])
GO

ALTER TABLE [eventCast] ADD FOREIGN KEY ([userTroupeId]) REFERENCES [userTroupes] ([id])
GO

ALTER TABLE [eventCast] ADD FOREIGN KEY ([eventId]) REFERENCES [events] ([id])
GO

ALTER TABLE [events] ADD FOREIGN KEY ([troupeId]) REFERENCES [troupe] ([id])
GO

ALTER TABLE [userTroupes] ADD FOREIGN KEY ([userTypeId]) REFERENCES [userTypes] ([id])
GO

ALTER TABLE [events] ADD FOREIGN KEY ([eventTypeId]) REFERENCES [eventTypes] ([id])
GO

