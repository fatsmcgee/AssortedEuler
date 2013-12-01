
from memoize import *
from primes import *

primesL = primesto(1000000)

@memoize
def gcd(a,b):
    while b:
        a,b=b,a%b
    return a

@memoize
def totient(n):
    tot=0
    for i in range(n):
        if gcd(i,n)==1:
            tot+=1
    return tot

@memoize
def ratio(n):
    pc = primecomponent(n)
    return float(pc)/totient(pc)

def primecomponent(n):
    pc = 1
    pind = 0
    while n>1:
        curPrime = primesL[pind]
        if n%curPrime==0:
            pc*=curPrime
            while n%curPrime==0:
                n/=curPrime
        pind+=1
    return pc
        
maxr = 0
for n in range(1,1000001):
    r = ratio(n)
    if r>maxr:
        maxr=r
        print "New best is %d with %d"%(n,maxr)
    if n%100 == 0:
        print n
print "done"
