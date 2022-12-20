INSERT INTO dump_table_test(dump_id, dump_date, dump_latitude, dump_longitude, dump_confidence)
SELECT
        random()::int4,
        now() - '2 years'::interval * random(),
        random()*50+10,
        random()*40+10,
        random()
FROM
        generate_series(1, 1000);
