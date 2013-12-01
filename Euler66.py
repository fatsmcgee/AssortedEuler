import math
from fractions import Fraction
import itertools

def next(top,root,plus):
    newbottom = root-plus**2
    if newbottom%top==0:
        newbottom,top = newbottom/top,1
    else:
        print "wtf!"
    plusser = int(float(top)*(math.sqrt(root) -plus)/newbottom)
    return (plusser,(newbottom,root,-plus-plusser*newbottom))

def rootseq(n):
    first = int(math.sqrt(n))
    yield first
    seqtuple = (1,n,-first)
    while 1:
        nextres,seqtuple = next(*seqtuple)
        yield nextres

def fracseq(seq,times):
    total = Fraction(seq.next())
    if times>1:
        return total + Fraction(1,fracseq(seq,times-1))
    return total

def lowestx(d):
    i = 1
    while 1:
        frac = rootconverge(d,i)
        x,y=frac.numerator,frac.denominator
        if x**2 - d*(y**2) == 1:
            return x
        i+=1

def rootconverge(root,n):
    return fracseq(rootseq(root),n)

def isroot(n):
    root = n**(1.0/2)
    return abs(root-int(root))<.0000001

highest = 0
highd = 0
for i in range(2,1001):
    if not isroot(i):
        res = lowestx(i)
        if res>highest:
            highest = res
            highd = i
print highd,highest
