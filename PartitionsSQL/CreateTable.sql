CREATE TABLE "dump_table_test" (
	"dump_id" int4 NOT NULL,
	"dump_date" timestamp NOT NULL,
	"dump_latitude" float8 NOT NULL,
	"dump_longitude" float8 NOT NULL,
	"dump_confidence" float8 NOT NULL
) PARTITION BY RANGE ("dump_date" );
