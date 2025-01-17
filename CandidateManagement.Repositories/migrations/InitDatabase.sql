USE master;
GO

IF NOT EXISTS (
    SELECT name
    FROM sys.databases
    WHERE name = N'CandidateDB'
)

CREATE DATABASE [CandidateDB];
GO

IF SERVERPROPERTY('ProductVersion') > '12'
   ALTER DATABASE [CandidateDB] SET QUERY_STORE = ON;
GO

IF OBJECT_ID('dbo.Candidate', 'U') IS NOT NULL
   DROP TABLE dbo.Candidate;
GO

CREATE TABLE dbo.Candidate (
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [Forename] NVARCHAR(50) NOT NULL,
    [Surname] NVARCHAR(50) NOT NULL,
    [Email] NVARCHAR (255) NULL,
    [DateOfBirth] DATETIME2 (7) NOT NULL,
    [SwqrNumber] NVARCHAR (8) NOT NULL
);
GO

INSERT INTO dbo.Candidate (
    [Forename],
    [Surname],
    [Email],
    [DateOfBirth],
    [SwqrNumber]
)
VALUES
    (NEWID(), 'John', 'Smith', 'john.smith@example.com', '1990-03-15', '10012345'),
    ('Jane', 'Doe', 'jane.doe@example.com', '1985-07-22', '10012346'),
    ('Michael', 'Brown', 'michael.brown@example.com', '1992-11-08', '10012347'),
    ('Emily', 'Davis', 'emily.davis@example.com', '1995-01-30', '10012348'),
    ('Daniel', 'Wilson', 'daniel.wilson@example.com', '1988-05-14', '10012349'),
    ('Sophia', 'Johnson', 'sophia.johnson@example.com', '1993-09-27', '10012350'),
    ('Liam', 'Garcia', 'liam.garcia@example.com', '1987-02-11', '10012351'),
    ('Olivia', 'Martinez', 'olivia.martinez@example.com', '1991-06-19', '10012352'),
    ('Noah', 'Clark', 'noah.clark@example.com', '1994-12-03', '10012353'),
    ('Ava', 'Rodriguez', 'ava.rodriguez@example.com', '1989-08-07', '10012354');
GO