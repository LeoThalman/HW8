CREATE TABLE dbo.Artist
(
	ID			INT IDENTITY (1,1) NOT NULL,
	Name		NVARCHAR(50) NOT NULL,
	BirthDate	Date NOT NULL,
	BirthCity	NVARCHAR(100) NOT NULL,
	CONSTRAINT [PK_dbo.Artist] PRIMARY KEY CLUSTERED (Name)
);

CREATE TABLE dbo.ArtWork
(
	ID			INT IDENTITY (1,1) NOT NULL,
	Title		NVARCHAR(100) NOT NULL,
	Artist		NVARCHAR(50) NOT NULL,
	CONSTRAINT [PK_dbo.ArtWork] PRIMARY KEY CLUSTERED (Title),
	CONSTRAINT [FK_dbo.Artist] FOREIGN KEY (Artist)
		REFERENCES dbo.Artist (Name)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);

CREATE TABLE dbo.Genre
(
	ID			INT IDENTITY (1,1) NOT NULL,
	Name		NVARCHAR(50) NOT NULL UNIQUE,
	CONSTRAINT [PK_dbo.Genre] PRIMARY KEY CLUSTERED (Name)
);

CREATE TABLE dbo.Classification
(
	ID			INT IDENTITY (1,1) NOT NULL,
	ArtWork		NVARCHAR(100) NOT NULL,
	Genre		NVARCHAR(50) NOT NULL,
	CONSTRAINT [PK_dbo.Classification] PRIMARY KEY CLUSTERED (ID ASC),
	CONSTRAINT [FK_dbo.ArtWork] FOREIGN KEY (ArtWork)
		REFERENCES dbo.ArtWork (Title)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	CONSTRAINT [FK_dbo.Genre] FOREIGN KEY (Genre)
		REFERENCES dbo.Genre (Name)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);
INSERT INTO dbo.Artist (Name, BirthDate, BirthCity) VALUES
			('M.C. Escher', '18980617' ,'Leeuwarden, Netherlands'),
			('Leonardo Da Vinci', '15190502' ,'Vinci, Italy'),
			('Hatip Mehmed Efendi','16801118','Unknown'),
			('Salvador Dali','19040511','Figueres, Spain');

INSERT INTO dbo.ArtWork (Title, Artist) VALUES
			('Circle Limit III', 'M.C. Escher'),
			('Twon Tree', 'M.C. Escher'),
			('Mona Lisa',  'Leonardo Da Vinci'),
			('The Vitruvian Man','Leonardo Da Vinci'),
			('Ebru', 'Hatip Mehmed Efendi'),
			('Honey Is Sweeter Than Blood','Salvador Dali');
INSERT INTO dbo.Genre (Name) VALUES
			('Tesselation'),
			('Surrealism'),
			('Portrait'),
			('Renaissance');

INSERT INTO dbo.Classification (ArtWork, Genre) VALUES
			('Circle Limit III','Tesselation'),
			('Twon Tree','Tesselation'),
			('Twon Tree','Surrealism'),
			('Mona Lisa','Portrait'),
			('Mona Lisa','Renaissance'),
			('The Vitruvian Man','Renaissance'),
			('Ebru','Tesselation'),
			('Honey Is Sweeter Than Blood','Surrealism');
GO

