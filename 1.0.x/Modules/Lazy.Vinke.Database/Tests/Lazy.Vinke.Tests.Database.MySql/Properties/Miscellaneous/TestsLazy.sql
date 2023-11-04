drop user if exists 'lazy'@'%';
drop database if exists Lazy;
create database Lazy;
create user 'lazy'@'%' identified by 'lazy';
grant all privileges on Lazy.* to 'lazy'@'%';
flush privileges;
use Lazy;



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
    Notes longtext,
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
    Elements blob,
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
delimiter ;;
create definer=current_user procedure Sp_TestsExecuteProcedure(in Id integer, in Name varchar(32), in Description varchar(256))
begin
	insert into TestsExecuteProcedure values (Id, Name, Description);
end;;
delimiter ;;





















