-- exercises

-- 1. show all invoices of customers from brazil (mailing address not billing)
SELECT c.FirstName, c.LastName, i.*
FROM Invoice AS i
	JOIN Customer AS c ON i.CustomerId = c.CustomerId
WHERE c.Country = 'Brazil';

-- 2. show all invoices together with the name of the sales agent of each one

-- 3. show all playlists ordered by the total number of tracks they have

-- 4. which sales agent made the most in sales in 2009?

-- 5. how many customers are assigned to each sales agent?

-- 6. which track was purchased the most since 2010?

-- 7. show the top three best-selling artists.

-- 8. which customers have the same initials as at least one other customer?
