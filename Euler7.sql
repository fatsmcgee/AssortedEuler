--Get the 10,001st prime number

declare @numberPrime table (
	idx int,
	isPrime bit
)

insert into @numberPrime values(1,0)

declare @i int = 0
while @i < 18
begin
	declare @size int = (select COUNT(*) from @numberPrime)
	insert into @numberPrime select @size+idx,1 from @numberPrime
	set @i += 1
end

set @size = (select COUNT(*) from @numberPrime)
declare @nextPrime int = 2

declare @PRIME_LIMIT int = 500
while @nextPrime < @PRIME_LIMIT
begin
	
	set @i = @nextPrime+@nextPrime
	while @i < @PRIME_LIMIT
	begin
		update @numberPrime set isPrime=0 where idx=@i
		set @i += @nextPrime
	end
	
	set @nextPrime = (select top 1 idx from @numberPrime where idx>@nextPrime)
end

select * from @numberPrime where idx<@PRIME_LIMIT