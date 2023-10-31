-- Kill all user session
select pg_terminate_backend(pid) from pg_stat_activity where datname = 'Lazy';

-- Execute on postgre database logged in as postgre user
drop database if exists "Lazy";
drop user if exists lazy;
create database "Lazy";
create user lazy with password 'lazy';

-- Execute on created database logged in as postgre user
grant all on schema public to lazy;

create table Transaction_CommitRollback
(
	Id smallint,
    Content varchar(256),
    constraint Pk_Transaction_CommitRollback primary key (Id)
);

create table QueryValue_DataAdapterFill
(
	TestCode varchar(128),
	ColumnCharD char(1),
	ColumnCharB char(1),
	ColumnCharNull char(1),
	ColumnVarChar1 varchar(32),
    ColumnVarChar2 varchar(256),
    ColumnVarCharNull varchar(512),
    ColumnVarText1 text,
    ColumnVarText2 text,
    ColumnVarTextNull text,
    ColumnByteN smallint,
    ColumnByteP smallint,
    ColumnByteNull smallint,
    ColumnInt16N smallint,
    ColumnInt16P smallint,
    ColumnInt16Null smallint,
    ColumnInt32N integer,
    ColumnInt32P integer,
    ColumnInt32Null integer,
    ColumnInt64N bigint,
    ColumnInt64P bigint,
    ColumnInt64Null bigint,
    ColumnUByte1 smallint,
    ColumnUByte2 smallint,
    ColumnUByteNull smallint,
    ColumnFloatN real,
    ColumnFloatP real,
    ColumnFloatNull real,
    ColumnDoubleN double precision,
    ColumnDoubleP double precision,
    ColumnDoubleNull double precision,
    ColumnDecimalN numeric(38),
    ColumnDecimalP numeric(38),
    ColumnDecimalNull numeric(38),
    ColumnDateTime1 timestamp,
    ColumnDateTime2 timestamp,
    ColumnDateTimeNull timestamp,
    ColumnVarUByte1 bytea,
    ColumnVarUByte2 bytea,
    ColumnVarUByteNull bytea,
	constraint Pk_QueryValue_DataAdapterFill primary key (TestCode)
);

create table QueryFind_DataAdapterFill
(
	Id integer,
    Code varchar(8),
    Description varchar(256),
    Amount numeric(38),
	constraint Pk_QueryFind_DataAdapterFill primary key (Id)
);

create table QueryRecord_DataAdapterFill
(
	Id smallint,
    Name varchar(64),
    Birthdate timestamp,
    constraint Pk_QueryRecord_DataAdapterFill primary key (Id)
);

create table QueryTable_DataAdapterFill
(
	Code varchar(8),
    Elements bytea,
    Active char(1),
    constraint Pk_QueryTable_DataAdapterFill primary key (Code)
);

create table QueryPage_DataAdapterFill
(
	Id integer,
    Name varchar(32),
    Description varchar(256),
    constraint Pk_QueryPage_DataAdapterFill primary key (Id)
);

create table QueryLike_DataAdapterFill
(
	TestId integer,
    Content varchar(256),
    Notes text,
    constraint Pk_QueryLike_DataAdapterFill primary key (TestId)
);

create table QueryProc_ExecuteNonQuery
(
	Id integer,
    Name varchar(32),
    Description varchar(256),
    constraint Pk_QueryProc_ExecuteNonQuery primary key (Id)
);

create procedure ExecuteProcedure_ExecuteNonQuery (Id integer, Name varchar(32), Description varchar(256)) 
language sql
begin atomic
    insert into QueryProc_ExecuteNonQuery values (Id, Name, Description);
end;
