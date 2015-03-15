// AsmTest.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

int Euler1(){
	int sum = 0;
	_asm{
		mov ecx, 0

		//while ecx <1000, ecx++ and update sum
		start_loop:
		cmp ecx, 1000
		jg end //if ecx>1000, end

		add ecx, 1

		//divide dx:ax/bx --> result in ax, remainder in dx
		xor eax, eax
		xor edx, edx
		mov ax, cx
		mov bx, 5
		div bx

		//if ecx%5==0, add to sum and go back to start of loop
		cmp dx, 0
		jne not_div_5
		add sum, ecx
		jmp start_loop
		not_div_5 :

		//divide dx:ax/bx --> result in ax, remainder in dx
		xor eax, eax
		xor edx, edx
		mov ax, cx
		mov bx, 3
		div bx

		//if ecx%3==0, add to sum and go back to start of loop
		cmp dx, 0
		jne not_div_3
		add sum, ecx
		jmp start_loop
		not_div_3 :

		jmp start_loop

		end :
	}
	return sum;
}
int _tmain(int argc, _TCHAR* argv[])
{
	return 0;
}



