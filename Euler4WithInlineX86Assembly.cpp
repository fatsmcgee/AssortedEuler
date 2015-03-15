// AsmTest.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

int IsPal(int n){
	int digits[100];
	int i = 0;
	int isPal = 1;

	_asm {

		start:
			cmp n,0
			je check_palindrome

			//edx:eax/ebx --> eax = result, edx = remainder
			mov eax,n
			xor edx,edx
			mov ebx,10
			div ebx // eax has result, edx has the next digit

			mov n,eax

			//digits[i] = edx
			mov esi,i
			lea eax, digits
			mov [eax+4*esi],edx
		
			inc esi
			mov i,esi
		
			jmp start
		
		check_palindrome :

			//squeeze equality
			mov eax,i
			dec eax
			mov ebx,0

		loop_palindrome:

			cmp eax, ebx
			jle end

			mov ecx, digits[eax*4]
			mov edx, digits[ebx*4]

			//if ecx!=edx return false
			cmp ecx,edx
			jne falsify

			dec eax
			inc ebx

			jmp loop_palindrome

		falsify:
			mov isPal,0
		end:


	}
	return isPal;
}

int Euler4(){

	int largest = 0;
	
	_asm{
		//for eax in 100..999, for ebx in 100..999
		mov eax, 100
		mov ebx, 100

		loop_eax:
			cmp eax,1000
			jge end
			
		loop_ebx:
			cmp ebx,1000
			jge end_loop_ebx

			
			//ecx = eax; eax=eax*ebx
			mov ecx,eax
			mul ebx
			xchg eax,ecx //now eax is as before, ecx = eax*ebx


			//first save registers, then use ecx as arg
			push eax
			push ebx
			push ecx

			push ecx
			call IsPal

			//restore registers, set result to edx
			mov edx,eax
			//pop once to pop pushed argument (due to CDECL), once to start popping stored arguments
			pop ecx
			pop ecx
			pop ebx
			pop eax

			//not pal, continue
			cmp edx,1
			jne not_largest_pal

			//not largest, continue
			cmp ecx,largest
			jle not_largest_pal
			mov largest,ecx

			
		not_largest_pal:
			inc ebx
			jmp loop_ebx
		
		end_loop_ebx:
			mov ebx,100
			inc eax
			jmp loop_eax
		end:

	}

	return largest;
}

int _tmain(int argc, _TCHAR* argv[])
{
	printf("%d", Euler4());
	return 0;
}



