-- alter session set "_oracle_script" = true;
-- drop user lazy cascade;
-- create user lazy identified by lazy;
-- grant all privileges to lazy;



-- drop table TestsTransaction
create table TestsTransaction
(
	Id smallint,
    Content varchar2(256),
    constraint Pk_TestsTransaction primary key (Id)
);

-- drop table TestsQueryLike
create table TestsQueryLike
(
	TestId integer,
    Content varchar2(256),
    Notes clob,
    constraint Pk_TestsQueryLike primary key (TestId)
);

-- drop table TestsQueryFind
create table TestsQueryFind
(
	Id integer,
    Code varchar2(8),
    Description varchar2(256),
    Amount number(13,4),
    constraint Pk_TestsQueryFind primary key (Id)
);

-- drop table TestsQueryRecord
create table TestsQueryRecord
(
	Id smallint,
    Name varchar2(64),
    Birthdate date,
    constraint Pk_TestsQueryRecord primary key (Id)
);

-- drop table TestsQueryTable
create table TestsQueryTable
(
	Code varchar2(8),
    Elements blob,
    Active char(1),
    constraint Pk_TestsQueryTable primary key (Code)
);

-- drop table TestsQueryPage
create table TestsQueryPage
(
	Id integer,
    Name varchar2(32),
    Description varchar2(256),
    constraint Pk_TestsQueryPage primary key (Id)
);

-- drop table TestsSelectQueryTable
create table TestsSelectQueryTable
(
	Id integer,
    Name varchar2(32),
    Amount number(13,4),
    constraint Pk_TestsSelectQueryTable primary key (Id)
);

-- drop table TestsSelectQueryPage
create table TestsSelectQueryPage
(
	IdMaster integer,
    IdChild integer,
    Name varchar2(32),
    Amount number(13,4),
    constraint Pk_TestsSelectQueryPage primary key (IdMaster,IdChild)
);

-- drop table TestsInsert
create table TestsInsert
(
	Id integer,
    ColumnVarChar varchar2(32),
    ColumnDecimal number(13,4),
    ColumnDateTime date,
    ColumnByte smallint,
    ColumnChar char(1),
    constraint Pk_TestsInsert primary key (Id)
);

-- drop table TestsQueryValue
create table TestsQueryValue
(
	TestCode varchar2(128),
	ColumnCharD char(1),
    ColumnCharB char(1),
    ColumnCharNull char(1),
	ColumnVarChar1 varchar2(32),
    ColumnVarChar2 varchar2(256),
    ColumnVarCharNull varchar2(512),
    ColumnVarText1 clob,
    ColumnVarText2 clob,
    ColumnVarTextNull clob,
    ColumnByteN smallint,
    ColumnByteP smallint,
    ColumnByteNull smallint,
    ColumnInt16N smallint,
    ColumnInt16P smallint,
    ColumnInt16Null smallint,
    ColumnInt32N integer,
    ColumnInt32P integer,
    ColumnInt32Null integer,
    ColumnInt64N number,
    ColumnInt64P number,
    ColumnInt64Null number,
    ColumnUByte1 smallint,
    ColumnUByte2 smallint,
    ColumnUByteNull smallint,
    ColumnFloatN float(24),
    ColumnFloatP float(24),
    ColumnFloatNull float(24),
    ColumnDoubleN float(53),
    ColumnDoubleP float(53),
    ColumnDoubleNull float(53),
    ColumnDecimalN number(38),
    ColumnDecimalP number(38),
    ColumnDecimalNull number(38),
    ColumnDateTime1 date,
    ColumnDateTime2 date,
    ColumnDateTimeNull date,
    ColumnVarUByte1 blob,
    ColumnVarUByte2 blob,
    ColumnVarUByteNull blob,
	constraint Pk_TestsQueryValue primary key (TestCode)
);

-- drop table TestsExecuteProcedure
create table TestsExecuteProcedure
(
	Id integer,
    Name varchar2(32),
    Description varchar(256),
    constraint Pk_TestsExecuteProcedure primary key (Id)
);

-- drop procedure Sp_TestsExecuteProcedure
create procedure Sp_TestsExecuteProcedure (Id integer, Name varchar2, Description varchar2) as
begin
    insert into TestsExecuteProcedure values (Id, Name, Description);
end;



















