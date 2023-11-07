delete from TestsIndate where TestCode = 'Sample';
insert into TestsIndate (TestCode, Id, Item) values ('Sample', 1, 'Lazy');
insert into TestsIndate (TestCode, Id, Item) values ('Sample', 2, 'Vinke');
insert into TestsIndate (TestCode, Id, Item) values ('Sample', 3, 'Tests');
insert into TestsIndate (TestCode, Id, Item) values ('Sample', 4, 'Database');
select * from TestsIndate where TestCode = 'Sample' order by Id;

declare @TestCode varchar(64) = 'Sample';
declare @Id integer = 3;
declare @Item varchar(256) = 'Isaac';
merge into TestsIndate D
using (
	select @TestCode TestCode, @Id Id
) S on (
	D.TestCode = S.TestCode and D.Id = S.Id
)
when not matched then
	insert (TestCode,Id,Item) values (@TestCode,@Id,@Item)
when matched then
	update set TestCode = @TestCode,Id = @Id,Item = @Item;
select * from TestsIndate where TestCode = 'Sample' order by Id;
