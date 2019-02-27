-- joins

-- cross join (on the same table)

SELECT e1.*, e2.*
FROM Employee AS e1 CROSS JOIN Employee AS e2;

-- most common join, inner.
-- most common condition for inner join: foreign key = primary key.

SELECT *
FROM Track INNER JOIN Genre ON Track.GenreId = Genre.GenreId;

-- joining on 'true" is the same as a cross join.
SELECT *
FROM Track INNER JOIN Genre ON 1 = 1;

-- three kinds of outer join: left outer, right outer, and full outer.
SELECT t.Name, g.Name
FROM Track t RIGHT JOIN Genre g ON t.GenreId = g.GenreId;

-- all rock songs in the database, showing artist name and song name.
SELECT ar.Name + ' - ' + t.Name
FROM Track AS t
	INNER JOIN Album AS al ON t.AlbumId = al.AlbumId
	INNER JOIN Artist AS ar ON al.ArtistId = ar.ArtistId
	INNER JOIN Genre AS g ON t.GenreId = g.GenreId
WHERE g.Name = 'Rock';

-- every employee together with who he reports to (his manager) if any
SELECT
	emp.FirstName + ' ' + emp.LastName AS Employee,
	mgr.FirstName + ' ' + mgr.LastName AS Manager
FROM Employee AS emp
	LEFT OUTER JOIN Employee AS mgr ON emp.ReportsTo = mgr.EmployeeId;