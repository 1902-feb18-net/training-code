-- SQL has many statements/commands
-- they are categorized as a number of "sub-languages"

-- Data Manipulation Language (DML) operates on individual rows.
-- there are five DML commands in SQL Server -

-- SELECT, INSERT, UPDATE, DELETE, TRUNCATE TABLE

-- SELECT is for read access to the rows.

SELECT * FROM Genre;
-- INSERT (simple)
INSERT INTO Genre VALUES (26, 'Misc');
-- really we should name the columns.
-- this is more readable / less error-prone
-- and it lets us skip columns that have default values we are OK with.
INSERT INTO Genre (GenreId, Name) VALUES (27, 'Misc 2');
-- can use more complex expressions with subqueries.
INSERT INTO Genre (GenreId, Name) VALUES
(
(SELECT MAX(GenreId) FROM Genre) + 1,
'Misc 3'
);
-- can insert multiple rows at once.
INSERT INTO Genre (GenreId, Name) VALUES
	(29, 'Misc 4'),
	(30, 'Misc 5');
-- can insert the result of a query
-- (this one duplicates every genre)
INSERT INTO Genre (GenreId, Name)
	(SELECT GenreId + 100, Name 
	 FROM Genre);

-- UPDATE statement
UPDATE Genre
SET Name = 'Misc 1' -- could change more than one column, comma separated
WHERE Name = 'Misc';
-- if we left out the WHERE, we would change every row.

-- take the high-ID copies of my Misc genres,
-- lower the IDs, and rename them to say Miscellaneous
UPDATE Genre
SET GenreId = GenreId - 50, Name = 'Miscellaneous ' + SUBSTRING(Name, 6, 1)
WHERE GenreId > 100 AND Name LIKE 'Misc%';

-- DELETE statement
DELETE FROM Genre
WHERE GenreId > 100; -- without WHERE, would delete every row.

-- TRUNCATE TABLE
-- TRUNCATE TABLE Genre;
-- -- deletes every row, no conditions.

-- DELETE FROM Genre deletes every row, one at a time
-- TRUNCATE TABLE Genre deletes every row all at once, faster
