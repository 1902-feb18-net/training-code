-- exercises

-- 1. insert two new records into the employee table.

-- 2. insert two new records into the tracks table.

-- 3. update customer Aaron Mitchell's name to Robert Walter
UPDATE Customer
SET FirstName = 'Robert', LastName = 'Walter'
WHERE FirstName = 'Aaron' AND LastName = 'Mitchell';

-- 4. delete one of the employees you inserted.

-- 5. delete customer Robert Walter. (more complex than it seems!)
DELETE FROM Customer
WHERE FirstName = 'Robert' AND LastName = 'Walter';
-- to preserve referential integrity, the DB throws an error.
-- -- we could set those foreign key values to NULL, or, we could delete all of them.

-- so, we need to remove all invoices that reference Robert, and, all invoice lines
-- that reference those invoices. but in the reverse order - deleting robert is the last
-- step.
