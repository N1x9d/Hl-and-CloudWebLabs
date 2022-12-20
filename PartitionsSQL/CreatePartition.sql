CREATE TABLE dumps_table_test_y2019 PARTITION OF dumps_table_test
    FOR VALUES FROM ('2019-01-01') TO ('2020-01-01');