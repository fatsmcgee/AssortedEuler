declare @i int = 100
declare @threeDigits table(
	Number int
)
while @i<1000
begin
	insert into @threeDigits values(@i)
	set @i = @i + 1
end

declare @ProductStrs table (Product varchar(10))
insert into @ProductStrs select cast(TOne.Number*TTwo.Number as varchar) Product
from @threeDigits as TOne
cross join @threeDigits as TTwo

select max(cast(Product as int)) from @ProductStrs
where Product=REVERSE(Product)