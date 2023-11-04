declare @UseLazy varchar(16) = N'use [Lazy]';

if exists (select 1 from sys.database_principals where name = N'lazy')
	execute sp_executesql @UseLazy;

if exists (select 1 from sys.database_principals where name = N'lazy')
	drop user lazy;

if exists (select 1 from syslogins where loginname = 'lazy')
	drop login lazy;

use master;

if exists (select 1 from sys.databases where name = 'Lazy')
	drop database Lazy;

create database Lazy;
create login lazy with password = 'lazy';
use Lazy;
create user lazy for login lazy;
grant control on database::Lazy to lazy with grant option;



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
    Amount Decimal(13,4),
    constraint Pk_TestsQueryFind primary key (Id)
);

-- drop table TestsQueryRecord
create table TestsQueryRecord
(
	Id smallint,
    Name varchar(64),
    Birthdate datetime,
    constraint Pk_TestsQueryRecord primary key (Id)
);

-- drop table TestsQueryTable
create table TestsQueryTable
(
	Code varchar(8),
    Elements image,
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
    Amount Decimal(13,4),
    constraint Pk_TestsSelectQueryTable primary key (Id)
);

-- drop table TestsSelectQueryPage
create table TestsSelectQueryPage
(
	IdMaster integer,
    IdChild integer,
    Name varchar(32),
    Amount Decimal(13,4),
    constraint Pk_TestsSelectQueryPage primary key (IdMaster,IdChild)
);

-- drop table TestsInsert
create table TestsInsert
(
	Id integer,
    ColumnVarChar varchar(32),
    ColumnDecimal Decimal(13,4),
	ColumnDateTime datetime,
	ColumnByte smallint,
	ColumnChar char(1),
    constraint Pk_TestsInsert primary key (Id)
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

go
-- drop procedure Sp_TestsExecuteProcedure
create procedure Sp_TestsExecuteProcedure (@Id integer, @Name varchar(32), @Description varchar(256)) as
begin
    insert into TestsExecuteProcedure values (@Id, @Name, @Description);
end;













































