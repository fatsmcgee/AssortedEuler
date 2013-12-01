declare @number bigint = 600851475143 
declare @biggestFactor int = 1
declare @factor int = 3

while @number >1
begin
	while @number%@factor=0
	begin
		set @biggestFactor = @factor
		set @number = @number/@factor
	end
	
	set @factor = @factor + 1
end

select @biggestFactor