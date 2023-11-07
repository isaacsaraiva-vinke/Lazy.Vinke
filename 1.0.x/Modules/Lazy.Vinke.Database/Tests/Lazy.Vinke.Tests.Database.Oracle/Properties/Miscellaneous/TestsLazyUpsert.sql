delete from TestsUpsert where TestCode = 'Sample';
insert into TestsUpsert (TestCode, Id, Item) values ('Sample', 1, 'Lazy');
insert into TestsUpsert (TestCode, Id, Item) values ('Sample', 2, 'Vinke');
insert into TestsUpsert (TestCode, Id, Item) values ('Sample', 3, 'Tests');
insert into TestsUpsert (TestCode, Id, Item) values ('Sample', 4, 'Database');
select * from TestsUpsert where TestCode = 'Sample' order by Id;

declare
    vTestCode varchar2(64);
    vId integer;
    vItem varchar2(256);
    vKeyTestCode varchar2(64);
    vKeyId integer;
begin
    select 'Sample' into vTestCode from dual;
    select 7 into vId from dual;
    select 'Isaac' into vItem from dual;
    select 'Sample' into vKeyTestCode from dual;
    select 3 into vKeyId from dual;
    
    merge into (select M.rowid rId, M.* from TestsUpsert M) D
    using (
        select T.rowid rId from (
            select vKeyTestCode keyTestCode, vKeyId keyId from dual
        ) U left join TestsUpsert T on (
            T.TestCode = U.keyTestCode and T.Id = U.keyId
        )
    ) S on (
        D.rId = S.rId
    )
    when not matched then
        insert (TestCode,Id,Item) values (vTestCode,vId,vItem)
    when matched then
        update set D.TestCode = vTestCode,D.Id = vId,D.Item = vItem;
    commit;
end;
/
select * from TestsUpsert where TestCode = 'Sample' order by Id;
