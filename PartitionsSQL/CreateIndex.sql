CREATE TABLE dump_table_test_y2019 PARTITION OF dump_table_test
    FOR VALUES FROM ('2019-01-01') TO ('2020-01-01');
CREATE TABLE dump_table_test_y2020 PARTITION OF dump_table_test
    FOR VALUES FROM ('2020-01-01') TO ('2021-01-01');
CREATE TABLE dump_table_test_y2021 PARTITION OF dump_table_test
    FOR VALUES FROM ('2021-01-01') TO ('2022-01-01');
