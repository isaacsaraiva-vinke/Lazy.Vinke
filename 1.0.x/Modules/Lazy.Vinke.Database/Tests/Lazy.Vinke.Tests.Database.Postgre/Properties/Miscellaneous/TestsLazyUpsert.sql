delete from TestsUpsert where TestCode = 'Sample';
insert into TestsUpsert (TestCode, Id, Item) values ('Sample', 1, 'Lazy');
insert into TestsUpsert (TestCode, Id, Item) values ('Sample', 2, 'Vinke');
insert into TestsUpsert (TestCode, Id, Item) values ('Sample', 3, 'Tests');
insert into TestsUpsert (TestCode, Id, Item) values ('Sample', 4, 'Database');
select * from TestsUpsert where TestCode = 'Sample' order by Id;

do $$
	declare vTestCode varchar = 'Sample';
	declare vId integer = 7;
	declare vItem varchar = 'Isaac';
	declare vKeyTestCode varchar = 'Sample';
	declare vKeyId integer = 3;
begin
	merge into TestsUpsert D
	using (
		select vKeyTestCode keyTestCode, vKeyId keyId
	) S on (
		D.TestCode = S.keyTestCode and D.Id = S.keyId
	)
	when not matched then
		insert (TestCode,Id,Item) values (vTestCode,vId,vItem)
	when matched then
		update set TestCode = vTestCode,Id = vId,Item = vItem;
end $$;
select * from TestsUpsert where TestCode = 'Sample' order by Id;
