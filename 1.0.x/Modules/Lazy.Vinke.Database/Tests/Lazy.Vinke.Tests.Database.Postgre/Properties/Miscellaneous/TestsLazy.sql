-- Kill all user session
select pg_terminate_backend(pid) from pg_stat_activity where datname = 'Lazy';

-- Execute on postgre database logged in as postgre user
drop database if exists "Lazy";
drop user if exists lazy;
create database "Lazy";
create user lazy with password 'lazy';

-- Execute on created database logged in as postgre user
grant all on schema public to lazy;



-- drop table TestsTransaction
create table TestsTransaction
(
	Id smallint,
    Content varchar(256),
    constraint Pk_TestsTransaction primary key (Id)
);

-- drop table TestsQueryLike
create table TestsQueryLike
(
	TestId integer,
    Content varchar(256),
    Notes text,
    constraint Pk_TestsQueryLike primary key (TestId)
);

-- drop table TestsQueryFind
create table TestsQueryFind
(
	Id integer,
    Code varchar(8),
    Description varchar(256),
    Amount numeric(13,4),
    constraint Pk_TestsQueryFind primary key (Id)
);

-- drop table TestsQueryRecord
create table TestsQueryRecord
(
	Id smallint,
    Name varchar(64),
    Birthdate timestamp,
    constraint Pk_TestsQueryRecord primary key (Id)
);

-- drop table TestsQueryTable
create table TestsQueryTable
(
	Code varchar(8),
    Elements bytea,
    Active char(1),
    constraint Pk_TestsQueryTable primary key (Code)
);

-- drop table TestsQueryPage
create table TestsQueryPage
(
	Id integer,
    Name varchar(32),
    Description varchar(256),
    constraint Pk_TestsQueryPage primary key (Id)
);

-- drop table TestsSelectQueryTable
create table TestsSelectQueryTable
(
	Id integer,
    Name varchar(32),
    Amount numeric(13,4),
    constraint Pk_TestsSelectQueryTable primary key (Id)
);

-- drop table TestsSelectQueryPage
create table TestsSelectQueryPage
(
	IdMaster integer,
    IdChild integer,
    Name varchar(32),
    Amount numeric(13,4),
    constraint Pk_TestsSelectQueryPage primary key (IdMaster,IdChild)
);

-- drop table TestsQueryValue
create table TestsQueryValue
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
	constraint Pk_TestsQueryValue primary key (TestCode)
);

-- drop table TestsExecuteProcedure
create table TestsExecuteProcedure
(
	Id integer,
    Name varchar(32),
    Description varchar(256),
    constraint Pk_TestsExecuteProcedure primary key (Id)
);

-- drop procedure Sp_TestsExecuteProcedure
create procedure Sp_TestsExecuteProcedure (Id integer, Name varchar(32), Description varchar(256)) 
language sql
begin atomic
    insert into TestsExecuteProcedure values (Id, Name, Description);
end;





















