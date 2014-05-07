// AsmTest.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

int Euler8(){

	const char * number = "7316717653133062491922511967442657474235534919493496983520312774506326239578318016984801869478851843858615607891129494954595017379583319528532088055111254069874715852386305071569329096329522744304355766896648950445244523161731856403098711121722383113622298934233803081353362766142828064444866452387493035890729629049156044077239071381051585930796086670172427121883998797908792274921901699720888093776657273330010533678812202354218097512545405947522435258490771167055601360483958644670632441572215539753697817977846174064955149290862569321978468622482839722413756570560574902614079729686524145351004748216637048440319989000889524345065854122758866688116427171479924442928230863465674813919123162824586178664583591245665294765456828489128831426076900422421902267105562632111110937054421750694165896040807198403850962455444362981230987879927244284909188845801561660979191338754992005240636899125607176060588611646710940507754100225698315520005593572972571636269561882670428252483600823257530420752963450";


	int max = 7 * 3 * 1 * 6 * 7;
	int last = max;
	
	const char * end = number + sizeof(number);

	_asm{
		xor esi,esi
		xor edi,edi

		mov esi, [number] //esi = first char of cur sequence of 5
		mov edi, esi
		add edi, 5 // edi = next char

		mov ebx,6 //ebx = how long ago we saw last zero

	looper:
		cmp [edi],0
		je end_looper

		//if there is a zero, put zero count at zero otherwise increment zero count
		inc ebx

		xor ecx,ecx
		mov cl, byte ptr[edi]
		sub cl, '0' //ecx = *edi-'0'
		
		cmp ecx, 0
		cmovz ebx,ecx  //if we encounter a zero, set zero count=0 and last=1. also continue
		mov eax,1
		je dont_update

		//eax=last 
		cmovnz eax,last 
		
		// eax *= new char
		xor ecx,ecx
		xor edx,edx
		mov cl, [edi]
		sub cl, '0'
		mul  ecx

		cmp ebx, 5 //don't update if last zero count too low
		jbe dont_update

		//if we haven't gotten 5 non-zeros, 
		// eax/= (moved over char)
		xor ecx, ecx
		xor edx, edx
		mov cl, [esi]
		sub cl, '0'
		div ecx

		cmp eax,max
		jb dont_update

		mov max,eax

	dont_update:
		mov last, eax
		inc esi
		inc edi
		jmp looper
	end_looper:
	}
	return max;
}


int _tmain(int argc, _TCHAR* argv[])
{
	auto result = Euler8();
	return 0;
}



