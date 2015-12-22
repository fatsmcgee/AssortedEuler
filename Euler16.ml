let mul2 num = 
	let rec helper = function
		carry,[] -> if carry=1 then [carry] else []
		| carry,hd::tl -> 
			let res = (2*hd+carry) in
			let newdig = res mod 10 in
			let carry = if res>=10 then 1 else 0 in
			newdig::(helper (carry,tl))
	in	
	helper (0,num)

let one = [1]

let pow2 n =
	let rec helper n res =
		if n=0 then res else (helper (n-1) (mul2 res))
	in
	helper n one

let num = (pow2 1000)

let answer = List.fold_left (+) 0 num
			
	

