use Lazy;
delete from TestsUpsert where TestCode = 'Sample';
insert into TestsUpsert (TestCode, Id, Item) values ('Sample', 1, 'Lazy');
insert into TestsUpsert (TestCode, Id, Item) values ('Sample', 2, 'Vinke');
insert into TestsUpsert (TestCode, Id, Item) values ('Sample', 3, 'Tests');
insert into TestsUpsert (TestCode, Id, Item) values ('Sample', 4, 'Database');
select * from TestsUpsert where TestCode = 'Sample' order by Id;

update TestsUpsert set TestCode = 'Sample', Id = 7, Item = 'Isaac' where TestCode = 'Sample' and Id = 3;
update TestsUpsert set TestCode = 'Sample', Id = 8, Item = 'Bezerra' where TestCode = 'Sample' and Id = 4;
insert into TestsUpsert (TestCode, Id, Item) values ('Sample', 9, 'Saraiva');
select * from TestsUpsert where TestCode = 'Sample' order by Id;
