use master;

if exists (select 1 from syslogins where loginname = 'lazy')
	drop login lazy;

if exists (select * from sys.database_principals where name = N'lazy')
	drop user lazy;

if exists (select 1 from sys.databases where name = 'Lazy')
	drop database Lazy;

create database Lazy;
use Lazy;
create login lazy with password = 'lazy';
create user lazy for login lazy;
grant control on database::Lazy to lazy with grant option;

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
    ColumnUByte1 tinyint,
    ColumnUByte2 tinyint,
    ColumnUByteNull tinyint,
    ColumnFloatN real,
    ColumnFloatP real,
    ColumnFloatNull real,
    ColumnDoubleN double precision,
    ColumnDoubleP double precision,
    ColumnDoubleNull double precision,
    ColumnDecimalN decimal(38),
    ColumnDecimalP decimal(38),
    ColumnDecimalNull decimal(38),
    ColumnDateTime1 datetime,
    ColumnDateTime2 datetime,
    ColumnDateTimeNull datetime,
    ColumnVarUByte1 image,
    ColumnVarUByte2 image,
    ColumnVarUByteNull image,
	constraint Pk_QueryValue_DataAdapterFill primary key (TestCode)
);

create table QueryFind_DataAdapterFill
(
	Id integer,
    Code varchar(8),
    Description varchar(256),
    Amount decimal(38),
	constraint Pk_QueryFind_DataAdapterFill primary key (Id)
);

create table QueryRecord_DataAdapterFill
(
	Id smallint,
    Name varchar(64),
    Birthdate datetime,
    constraint Pk_QueryRecord_DataAdapterFill primary key (Id)
);

create table QueryTable_DataAdapterFill
(
	Code varchar(8),
    Elements image,
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

go
create procedure ExecuteProcedure_ExecuteNonQuery (@Id integer, @Name varchar(32), @Description varchar(256)) as
begin
    insert into QueryProc_ExecuteNonQuery values (@Id, @Name, @Description);
end;
