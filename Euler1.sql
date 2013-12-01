Declare @NumberList TABLE(
	Number int
)

declare @i int
set @i=1

while @i<=1000
begin
	insert into @NumberList values(@i)
	set @i = @i+1
end


select sum(Number) from @NumberList
where Number%5=0 or Number%3=0