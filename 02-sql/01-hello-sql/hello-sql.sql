SELECT 'Hello world!';

SELECT * FROM SalesLT.Customer;

-- commenting in SQL with --

-- in a SQL server... you have many databases.
-- within each database, we have many "schemas"
-- a schema is a namespace/scope for database objects

-- in AdventureWorksLT database, we have schema SalesLT
-- inside schemas, we have many db objects, incl. tables

-- this WOULD switch databases (from master),
-- but Azure SQL DB doesn't support it yet.
USE AdventureWorksLT;
-- have to use dropdown instead.

-- whitespace doesn't matter
SelecT                *
from

salesLt.Customer
-- semicolons aren't necessary
-- SQL syntax is case-insensitive

-- SQL's string comparison by default is also case-insensitive
-- but really that depends on the "collation" i.e. the
-- settings for datetime format, number format

-- the SELECT statement ...
-- doesn't even need to access the DB
SELECT 1;
SELECT CURRENT_TIMESTAMP;

-- we get all columns and all rows from a table with
-- SELECT *, and FROM.
SELECT *
FROM SalesLT.Customer;

-- we can select exactly which columns we want by replacing the *
SELECT Title, FirstName, MiddleName, LastName
FROM SalesLT.Customer;

-- we can do "aliases" with AS
-- the standard SQL way to spell an identifier with spaces is ""
-- the SQL Server way is []
SELECT FirstName AS [First Name], LastName AS "Last Name"
FROM SalesLT.Customer;

-- we can compute new values from the column's values.
SELECT FirstName + ' ' + LastName AS [Full Name]
FROM SalesLT.Customer;

-- we can process the returned rows with DISTINCT
-- any rows of the result set that have all the same column values
--  will be de-duplicated

-- get all first names that customers have.
SELECT DISTINCT FirstName
FROM SalesLT.Customer;

-- we can filter rows on conditions as well.
-- with WHERE clause.
SELECT *
FROM SalesLT.Customer
WHERE FirstName = 'Alice';

SELECT *
FROM SalesLT.Customer
WHERE FirstName = 'Alice' AND LastName != 'Steiner';
-- so we have boolean operators AND and OR and NOT, and
-- not-equals != (or... traditional SQL <>)

-- we have ordered comparison of numbers, dates, times, and strings
-- strings are ordered by "dictionary order" "lexicographic order"
-- but this is affected by collation.
-- with operators < <= > >=

-- all customers whose first name starts with C
SELECT *
FROM SalesLT.Customer
WHERE FirstName >= 'c' AND FirstName < 'd';
-- everything that starts with c and has more letters too
-- sorts after 'c'

-- we can sort the results (they are in undefined order by default)
-- ORDER BY clause accomplishes this.

-- all customers first name starting with C
-- ordered by first name, with last name (descending order) as tiebreaker
SELECT *
FROM SalesLT.Customer
WHERE FirstName >= 'c' AND FirstName < 'd'
ORDER BY FirstName, LastName DESC;
-- ordering is "ascending" (ASC) by default, but can be descending (DESC)

-- we can do regex-lite pattern matching on strings with LIKE operator.

-- all customers with first name starting with c, then a vowel.
SELECT *
FROM SalesLT.Customer
WHERE FirstName LIKE 'C[aeoiu]%';
-- [abc] for one character, either a, b, or c
-- % for any number of any characters
-- _ for one of any character