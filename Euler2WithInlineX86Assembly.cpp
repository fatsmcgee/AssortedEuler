// AsmTest.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

int Euler2(){
	int sum = 0;
	_asm{
		mov eax,1
		mov ebx,1

		start_loop:
		cmp ebx, 4000000
		jg end

		mov ecx,ebx //ecx = ebx temporarily
		add ebx,eax
		mov eax,ecx

		mov ecx,ebx //ecx = fib to test for evenness
		and ecx,1
		cmp ecx,0
		jne start_loop
		add sum,ebx

		jmp start_loop

		end:
	}

	return sum;
}

int _tmain(int argc, _TCHAR* argv[])
{
	int sum = Euler2();
	return 0;
}



