-- exercises:

-- 1. list all customers (full names, customer ID, and country) who are
-- not in the US

-- 2. list all customers from brazil

-- 3. list all sales agents

-- 4. show a list of all countries in billing addresses on invoices.

-- 5. how many invoices were there in 2009, and what was the sales total
-- for that year? (extra challenge: find the invoice count sales total for
-- every year, using one query)
SELECT COUNT(InvoiceDate) AS Count, SUM(Total) AS TotalCost
FROM Invoice
--WHERE InvoiceDate >= '2009' AND InvoiceDate < '2010';
--WHERE InvoiceDate BETWEEN '2009' AND '2010';
WHERE DATEPART(YEAR, InvoiceDate) = 2009;
-- couple different ways to compare dates.
-- BETWEEN operator is inclusive of both ends

SELECT YEAR(InvoiceDate) AS Year, COUNT(InvoiceDate) AS Count, SUM(Total) AS TotalCost
FROM Invoice
GROUP BY YEAR(InvoiceDate);

-- 6. how many line items were there for invoice #37?

-- 7. how many invoices per country?

-- 8. show total sales per country, ordered by highest sales first.
