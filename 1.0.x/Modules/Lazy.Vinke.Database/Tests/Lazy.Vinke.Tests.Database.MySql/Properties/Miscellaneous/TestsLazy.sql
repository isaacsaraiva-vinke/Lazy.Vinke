drop user if exists 'lazy'@'localhost';
drop database if exists Lazy;
create database Lazy;
create user 'lazy'@'localhost' identified by 'lazy';
grant all privileges on Lazy.* to 'lazy'@'localhost';
flush privileges;
use Lazy;

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
    ColumnVarText1 longtext,
    ColumnVarText2 longtext,
    ColumnVarTextNull longtext,
    ColumnByteN tinyint,
    ColumnByteP tinyint,
    ColumnByteNull tinyint,
    ColumnInt16N smallint,
    ColumnInt16P smallint,
    ColumnInt16Null smallint,
    ColumnInt32N integer,
    ColumnInt32P integer,
    ColumnInt32Null integer,
    ColumnInt64N bigint,
    ColumnInt64P bigint,
    ColumnInt64Null bigint,
    ColumnUByte1 tinyint unsigned,
    ColumnUByte2 tinyint unsigned,
    ColumnUByteNull tinyint unsigned,
    ColumnFloatN float,
    ColumnFloatP float,
    ColumnFloatNull float,
    ColumnDoubleN double,
    ColumnDoubleP double,
    ColumnDoubleNull double,
    ColumnDecimalN decimal(38),
    ColumnDecimalP decimal(38),
    ColumnDecimalNull decimal(38),
    ColumnDateTime1 datetime,
    ColumnDateTime2 datetime,
    ColumnDateTimeNull datetime,
    ColumnVarUByte1 blob,
    ColumnVarUByte2 blob,
    ColumnVarUByteNull blob,
	constraint Pk_QueryValue_DataAdapterFill primary key (TestCode)
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
    Elements blob,
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

delimiter ;;
create definer=current_user procedure ExecuteProcedure_ExecuteNonQuery(in Id integer, in Name varchar(32), in Description varchar(256))
begin
	insert into QueryProc_ExecuteNonQuery values (Id, Name, Description);
end;;
delimiter ;;






















-- drop table TestsQueryFind
create table TestsQueryFind
(
	Id integer,
    Code varchar(8),
    Description varchar(256),
    Amount Decimal(13,4),
    constraint Pk_TestsQueryFind primary key (Id)
);

-- drop table TestsSelectQueryTable
create table TestsSelectQueryTable
(
	Id integer,
    Name varchar(32),
    Amount Decimal(13,4),
    constraint Pk_TestsSelectQueryTable primary key (Id)
);






















