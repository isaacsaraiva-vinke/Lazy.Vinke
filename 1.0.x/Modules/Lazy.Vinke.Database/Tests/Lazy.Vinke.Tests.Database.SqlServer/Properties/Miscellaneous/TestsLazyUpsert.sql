delete from TestsUpsert where TestCode = 'Sample';
insert into TestsUpsert (TestCode, Id, Item) values ('Sample', 1, 'Lazy');
insert into TestsUpsert (TestCode, Id, Item) values ('Sample', 2, 'Vinke');
insert into TestsUpsert (TestCode, Id, Item) values ('Sample', 3, 'Tests');
insert into TestsUpsert (TestCode, Id, Item) values ('Sample', 4, 'Database');
select * from TestsUpsert where TestCode = 'Sample' order by Id;

declare @TestCode varchar(64) = 'Sample';
declare @Id integer = 7;
declare @Item varchar(256) = 'Isaac';
declare @keyTestCode varchar(64) = 'Sample';
declare @keyId integer = 3;
merge into TestsUpsert D
using (
	select @keyTestCode keyTestCode, @keyId keyId
) S on (
	D.TestCode = S.keyTestCode and D.Id = S.keyId
)
when not matched then
	insert (TestCode,Id,Item) values (@TestCode,@Id,@Item)
when matched then
	update set TestCode = @TestCode,Id = @Id,Item = @Item;
select * from TestsUpsert where TestCode = 'Sample' order by Id;
