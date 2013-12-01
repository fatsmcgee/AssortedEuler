--sum of squares vs square of sums, 1 to 100

declare @NumberSquares table(
	Number int,
	Square int
)

declare @i int = 1

while @i<=100
begin
	insert into @NumberSquares values(@i,@i*@i)
	set @i = @i+1
end

select * from @NumberSquares
select  square(sum(Number)) - SUM(Square) from @NumberSquares