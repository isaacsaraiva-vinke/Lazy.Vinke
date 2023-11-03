alter session set "_oracle_script" = true;
drop user lazy cascade;
create user lazy identified by lazy;
grant all privileges to lazy;

create table Transaction_CommitRollback
(
	Id smallint,
    Content varchar2(256),
    constraint Pk_Transaction_CommitRollback primary key (Id)
);

create table QueryValue_DataAdapterFill
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
	constraint Pk_QueryValue_DataAdapterFill primary key (TestCode)
);

create table QueryRecord_DataAdapterFill
(
	Id smallint,
    Name varchar2(64),
    Birthdate date,
    constraint Pk_QueryRecord_DataAdapterFill primary key (Id)
);

create table QueryTable_DataAdapterFill
(
	Code varchar2(8),
    Elements blob,
    Active char(1),
    constraint Pk_QueryTable_DataAdapterFill primary key (Code)
);

create table QueryPage_DataAdapterFill
(
	Id integer,
    Name varchar2(32),
    Description varchar2(256),
    constraint Pk_QueryPage_DataAdapterFill primary key (Id)
);

create table QueryLike_DataAdapterFill
(
	TestId integer,
    Content varchar2(256),
    Notes clob,
    constraint Pk_QueryLike_DataAdapterFill primary key (TestId)
);

create table QueryProc_ExecuteNonQuery
(
	Id integer,
    Name varchar2(32),
    Description varchar(256),
    constraint Pk_QueryProc_ExecuteNonQuery primary key (Id)
);

create procedure ExecuteProcedure_ExecuteNonQuery (Id integer, Name varchar2, Description varchar2) as
begin
    insert into QueryProc_ExecuteNonQuery values (Id, Name, Description);
end;



















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

-- drop table TestsSelectQueryTable
create table TestsSelectQueryTable
(
	Id integer,
    Name varchar2(32),
    Amount number(13,4),
    constraint Pk_TestsSelectQueryTable primary key (Id)
);






















