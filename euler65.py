from fractions import *
import itertools

def fracseq(seq,times):
    total = Fraction(seq.next())
    if times>1:
        return total + Fraction(1,fracseq(seq,times-1))
    return total

def efraction(i):
    def eIter():
        for n in 2,1,2:
            yield n
        k=2
        while 1:
            yield 1
            yield 1
            yield 2*k
            k+=1
    return fracseq(eIter(),i)

hundreth = efraction(100)
numer = hundreth.numerator
print sum(ord(c)-ord('0') for c in str(numer))
