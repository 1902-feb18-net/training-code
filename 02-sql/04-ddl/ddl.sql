-- DDL

-- Data Definition Language
-- DDL operates on whole tables at a time, can't see individual rows.
-- DDL also works with lots of other DB objects, like views, functions, procedures
-- triggers, constraints, etc.

-- to create, change, and delete these DB objects, we have:
-- CREATE, ALTER, DROP

-- GO is a special keyword to separate batches of commands
-- some commands demand to be in their own batch.

CREATE SCHEMA Movie;
GO

-- CREATE TABLE:

-- CREATE TABLE gets comma-separated list of columns
-- each column has name and data type.
CREATE TABLE Movie.Movie (
	MovieId INT
);

SELECT * FROM Movie.Movie;

-- we have ALTER TABLE to add or delete columns
--   (and do some other things too)
ALTER TABLE Movie.Movie ADD
	Title NVARCHAR(200);

-- delete an entire table
DROP TABLE Movie.Movie;

-- we can specify constraints on each column

-- constraints:
--  - NOT NULL (null not allowed)
--  - NULL (not really a constraint, just being explicit)
--       (the default behavior: null is allowed)
--  - PRIMARY KEY
--       (sets PK, enforces uniqueness, sets clustered index)
--       (implies NOT NULL, but we like to be explicit anyway)
--  - UNIQUE (can be set on multiple columns taken together)
--  - CHECK (an arbitrary condition that must be true for each row)
--  - DEFAULT (give a default value for that column when inserted
--         without an explicit value.) NULL is the default DEFAULT.
--  - FOREIGN KEY
--  - IDENTITY(start,increment) (not exactly a cosntraint, but similar)
--       IDENTITY(10,10) would count: 10, 20, 30, 40 etc
--       default values are 1,1. (counting: 1, 2, 3, 4, 5)
--     by default, we aren't allowed to give explicit values for IDENTITY columns
--       you'd need to turn on IDENTITY_INSERT option
DROP TABLE Movie.Movie;
CREATE TABLE Movie.Movie (
	MovieId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	Title NVARCHAR(200) NOT NULL,
	ReleaseDate DATETIME2 NOT NULL,
	DateModified DATETIME2 NOT NULL DEFAULT(GETDATE())
	CONSTRAINT U_Movie_Title_Date UNIQUE (Title, ReleaseDate),
	-- CONSTRAINT PK_blah PRIMARY KEY (col1, col2) -- (composite PK)
	CONSTRAINT CHK_DateNotTooEarly CHECK (ReleaseDate > '1900')
);

INSERT INTO Movie.Movie (Title, ReleaseDate) VALUES (
	'LOTR: THe Fellowship of the Ring', '2002'
);

--SELECT * FROM Movie.Movie;

--INSERT INTO Movie.Movie (Title, ReleaseDate) VALUES (
--	'bad movie date', '1800'
--);

--INSERT INTO Movie.Movie (MovieId, Title, ReleaseDate) VALUES (
--	10, 'cant insert to identity col', '1950'
--);

DROP TABLE Movie.Genre;
CREATE TABLE Movie.Genre (
	GenreId INT NOT NULL PRIMARY KEY IDENTITY,
	Name NVARCHAR(100) NOT NULL,
	DateModified DATETIME2 DEFAULT(GETDATE()),
	CHECK (Name != '')
);

ALTER TABLE Movie.Movie ADD
	GenreID INT NULL,
	CONSTRAINT FK_Movie_Genre FOREIGN KEY (GenreID) REFERENCES Movie.Genre (GenreId);

-- adding columns without some default (or allowing null as default) is not allowed.
-- workaround: allow NULL at first, fix up the existing rows, then add NOT NULL constraint.


INSERT INTO Movie.Genre (Name) VALUES ('Action/Adventure');

UPDATE Movie.Movie SET GenreID = 1;

SELECT * FROM Movie.Movie;

DELETE FROM Movie.Genre;

ALTER TABLE Movie.Movie
	ALTER COLUMN GenreID INT NOT NULL;

-- computed columns
ALTER TABLE Movie.Movie DROP COLUMN FullName;
ALTER TABLE Movie.Movie ADD
	FullName AS (Title + ' (' + CONVERT(NVARCHAR, YEAR(ReleaseDate)) + ')');

SELECT * FROM Movie.Movie;
-- computed columns have different options like PERSISTED

-- views
GO
CREATE VIEW Movie.NewReleases AS
	SELECT * FROM Movie.Movie
	WHERE ReleaseDate > '2019-01-01';
GO

GO
ALTER VIEW Movie.NewReleases AS
	SELECT * FROM Movie.Movie
	WHERE ReleaseDate >= '2019-02-01';
GO

SELECT * FROM Movie.Movie;
SELECT * FROM Movie.NewReleases;

-- 

INSERT INTO Movie.NewReleases (Title, ReleaseDate, GenreID) VALUES
	('LOTR: The Two Towers', '2019-02-01',
		(SELECT GenreId FROM Movie.Genre WHERE Name = 'Action/Adventure'));

-- views provide an abstraction over the actual table structure
-- by running a query behind the scenes to generated what pretends to be a table

-- we can do inserts/updates/deletes through it too, but
-- only on columns that directly map to real table columns.

-- variables in SQL Server
DECLARE @action AS INT;
SET @action = 1;

-- table-valued variables
DECLARE @my_table AS TABLE (
	col1 INT, col2 INT
);
--INSERT INTO @my_table
-- etc

-- user-defined functions
GO
CREATE FUNCTION Movie.MoviesReleasedInYear(@year INT)
RETURNS INT
AS
BEGIN
	DECLARE @result INT;

	SELECT @result = COUNT(*)
	FROM Movie.Movie
	WHERE YEAR(ReleaseDate) = @year;

	RETURN @result;
END
GO

SELECT Movie.MoviesReleasedInYear(2002);

-- functions do not allow writing any data - only reading.

-- exercise

-- 1. write a function to get the initials of a customer based on his ID (look up string functions)
GO
CREATE FUNCTION GetCustomerInitials(@id INT)
RETURNS NVARCHAR(3)
AS
BEGIN
	DECLARE @initials NVARCHAR(3);

	-- in SQL, string indexing is 1-based
	SELECT @initials = SUBSTRING(FirstName, 1, 1) + SUBSTRING(LastName, 1, 1)
	FROM Customer
	WHERE CustomerId = @id;

	RETURN @initials;
END

-- triggers

GO
CREATE TRIGGER Movie.MovieDateModified ON Movie.Movie
AFTER UPDATE
AS
BEGIN
	-- inside a trigger, you have access to two special table variables
	-- Inserted and Deleted
	UPDATE Movie.Movie
	SET DateModified = GETDATE()
	WHERE MovieId IN (SELECT MovieId FROM Inserted);
	-- in this case, Inserted has the new version of the updated rows
END

SELECT * FROM Movie.Movie;
UPDATE Movie.Movie
SET Title = 'LOTR: The Fellowship of the Ring'
WHERE MovieId = 1;

SELECT * FROM Movie.Movie;

-- we can do triggers on INSERT, UPDATE, or DELETE
-- they can be BEFORE, AFTER, or INSTEAD OF

-- procedures
-- procedures are like functions
-- but they allow any SQL command inside them including DB write
-- they don't have to return anything
-- you can only call them with EXECUTE, never inside a SELECT or anything else
GO
CREATE PROCEDURE Movie.RenameMovies(@newname NVARCHAR(50), @rowschanged INT OUTPUT)
AS
BEGIN
	-- we can use WHILE loops, IF ELSE, TRY CATCH
	BEGIN TRY
		IF (EXISTS (SELECT * FROM Movie.Movie))
		BEGIN
			SET @rowschanged = (SELECT COUNT(*) FROM Movie.Movie);

			UPDATE Movie.Movie
			SET Title = @newname;
		END
		ELSE
		BEGIN
			SET @rowschanged = 0;
			RAISERROR('no movies found!', 16, 1);
		END
	END TRY
	BEGIN CATCH
		PRINT ERROR_MESSAGE();
	END CATCH
END
GO

DECLARE @rowschanged INT;
EXECUTE Movie.RenameMovies 'Movie', @rowschanged;
SELECT @rowschanged;
