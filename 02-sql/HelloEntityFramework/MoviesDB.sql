CREATE DATABASE Movies;

GO
CREATE SCHEMA Movie;
GO

CREATE TABLE Movie.Movie (
	MovieId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	Title NVARCHAR(200) NOT NULL,
	ReleaseDate DATETIME2 NOT NULL,
	GenreId INT NOT NULL,
	DateModified DATETIME2 NOT NULL DEFAULT(GETDATE())
	CONSTRAINT U_Movie_Title_Date UNIQUE (Title, ReleaseDate)
);

CREATE TABLE Movie.Genre (
	GenreId INT NOT NULL PRIMARY KEY IDENTITY,
	Name NVARCHAR(100) NOT NULL,
	DateModified DATETIME2 DEFAULT(GETDATE()),
	CHECK (Name != '')
);

ALTER TABLE Movie.Movie ADD
	CONSTRAINT FK_Movie_Genre FOREIGN KEY (GenreId) REFERENCES Movie.Genre (GenreId);

INSERT INTO Movie.Genre (Name) VALUES
	('Action/Adventure');

INSERT INTO Movie.Movie (Title, ReleaseDate, GenreId) VALUES
	( 'LOTR: The Fellowship of the Ring', '2001',
		(SELECT GenreId FROM Movie.Genre WHERE Name = 'Action/Adventure'));
