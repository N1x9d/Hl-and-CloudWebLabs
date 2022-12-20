SET constraint_exclusion = ON;
SELECT
	count(*)
FROM
	dump_table_test
WHERE
	dump_date >= DATE '2021-05-02';
