delete from TestsIndate where TestCode = 'Sample';
insert into TestsIndate (TestCode, Id, Item) values ('Sample', 1, 'Lazy');
insert into TestsIndate (TestCode, Id, Item) values ('Sample', 2, 'Vinke');
insert into TestsIndate (TestCode, Id, Item) values ('Sample', 3, 'Tests');
insert into TestsIndate (TestCode, Id, Item) values ('Sample', 4, 'Database');
select * from TestsIndate where TestCode = 'Sample' order by Id;

do $$
	declare vTestCode varchar = 'Sample';
	declare vId integer = 3;
	declare vItem varchar = 'Isaac';
begin
	merge into TestsIndate D
	using (
		select vTestCode TestCode, vId Id
	) S on (
		D.TestCode = S.TestCode and D.Id = S.Id
	)
	when not matched then
		insert (TestCode,Id,Item) values (vTestCode,vId,vItem)
	when matched then
		update set TestCode = vTestCode,Id = vId,Item = vItem;
end $$;
select * from TestsIndate where TestCode = 'Sample' order by Id;
