use Lazy;
delete from TestsUpsert where TestCode = 'Sample';
insert into TestsUpsert (TestCode, Id, Item) values ('Sample', 1, 'Lazy');
insert into TestsUpsert (TestCode, Id, Item) values ('Sample', 2, 'Vinke');
insert into TestsUpsert (TestCode, Id, Item) values ('Sample', 3, 'Tests');
insert into TestsUpsert (TestCode, Id, Item) values ('Sample', 4, 'Database');
select * from TestsUpsert where TestCode = 'Sample' order by Id;

set @TestCode = 'Sample';
set @Id = 3;
set @Item = 'Isaac';
insert into TestsUpsert (TestCode, Id, Item) values (@TestCode, @Id, @Item)
on duplicate key update TestCode = @TestCode, Id = @Id, Item = @Item;
select * from TestsUpsert where TestCode = 'Sample' order by Id;
