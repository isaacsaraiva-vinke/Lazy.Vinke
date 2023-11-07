delete from TestsIndate where TestCode = 'Sample';
insert into TestsIndate (TestCode, Id, Item) values ('Sample', 1, 'Lazy');
insert into TestsIndate (TestCode, Id, Item) values ('Sample', 2, 'Vinke');
insert into TestsIndate (TestCode, Id, Item) values ('Sample', 3, 'Tests');
insert into TestsIndate (TestCode, Id, Item) values ('Sample', 4, 'Database');
select * from TestsIndate where TestCode = 'Sample' order by Id;

declare
    vTestCode varchar2(64);
    vId integer;
    vItem varchar2(256);
begin
    select 'Sample' into vTestCode from dual;
    select 3 into vId from dual;
    select 'Isaac' into vItem from dual;
    
    merge into (select M.rowid rId, M.* from TestsIndate M) D
    using (
        select T.rowid rId from (
            select vTestCode TestCode, vId Id from dual
        ) U left join TestsIndate T on (
            T.TestCode = U.TestCode and T.Id = U.Id
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
select * from TestsIndate where TestCode = 'Sample' order by Id;
