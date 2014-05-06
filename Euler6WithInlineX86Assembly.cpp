// AsmTest.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

int Euler6(){

	int result;
	_asm{
		//eax = sum of squares
		//ebx = square of sums
		//ecx = 1 to 100
		mov eax,0
		mov ebx,0
		
		mov ecx,1

	start:
		cmp ecx,101
		jge end
		
		add ebx,ecx

		//eax = eax + ecx^2
		xchg eax,esi
		xor eax,eax
		mov eax,ecx
		mul ecx
		add eax,esi

		inc ecx
		jmp start
	end:
		//now eax is set, ebx = sum
		xchg eax,ebx
		mul eax
		//eax = square of sums, ebx = sum of squares
		sub eax,ebx
		mov result,eax

	}
	return result;
}


int _tmain(int argc, _TCHAR* argv[])
{
	auto result = Euler6();
	return 0;
}



