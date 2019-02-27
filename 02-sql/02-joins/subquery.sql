-- subqueries

-- another way (actually several ways) besides joins
-- to combine info from multiple tables

-- every track that has never been purchased.
SELECT *
FROM Track AS t
WHERE t.TrackId NOT IN (
	SELECT TrackId
	FROM InvoiceLine
);
-- here we have a subquery in the WHERE clause of another query.
-- we have operators IN, NOT IN


SELECT t.Name
FROM Track AS t
WHERE t.TrackId = (
	SELECT TOP(1) TrackId
	FROM InvoiceLine
	GROUP BY TrackId
	ORDER BY COUNT(*) DESC
);
-- we have TOP(n) to take just the first n results of a query
-- the inner query is: get the top trackid, when we group all the invoicelines
-- by trackid and count up how many in each group

-- we can also have subqueries in HAVING (no difference)
-- but also in FROM clause

--
SELECT *
FROM Track INNER JOIN (
		SELECT Artist.Name AS Artist, Album.Title AS Album, Album.AlbumId AS AlbumId
		FROM Artist JOIN Album ON Album.ArtistId = Artist.ArtistId
	) AS subq ON Track.AlbumId = subq.AlbumId
WHERE Track.Name < 'b';

-- similar to subquery in FROM is "common table expression" (CTE)
-- which uses a "WITH" clause

-- every track that has never been purchased (CTE version)
WITH purchased_tracks AS (
	SELECT DISTINCT TrackId
	FROM InvoiceLine
)
SELECT *
FROM Track AS t
WHERE t.TrackId NOT IN (
	SELECT TrackId
	FROM purchased_tracks
);

-- some other subquery operators:
-- EXISTS, NOT EXISTS
-- SOME/ANY, ALL

-- get the artist who made the album with the longest title.
SELECT *
FROM Artist
WHERE ArtistId = (
		SELECT ArtistId
		FROM Album
		WHERE LEN(Title) >= ALL (SELECT LEN(Title) FROM Album)
	);

-- does the same thing
SELECT *
FROM Artist
WHERE ArtistId = (
	SELECT TOP(1) ArtistId
	FROM Album
	ORDER BY LEN(Title) DESC
);

-- set operations
-- we have from mathematical sets the concepts of
-- UNION, INTERSECT, and (set difference) EXCEPT

-- all first names of customers and employees
SELECT FirstName FROM Customer
UNION
SELECT FirstName FROM Employee; -- 63
-- all rows of the first query, and also all rows of the second query.
-- (the number and types of the columns need to be compatible)

-- for each set op, we have a distinct version, and an ALL version.
-- the distinct version is the default.
--   so by default, all these operators make a pass to remove duplicate rows from the result.
SELECT FirstName FROM Customer
UNION ALL
SELECT FirstName FROM Employee; -- 67 results (has duplicates)

-- UNION gives you values that are in EITHER result.
-- INTERSECT gives you values that are in BOTH results.
-- EXCEPT gives you values that are in the FIRST but NOT the SECOND result.

