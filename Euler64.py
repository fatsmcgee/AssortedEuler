import math
from fractions import Fraction

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


def rootperiod(n):
    first = int(math.sqrt(n))
    seqtuple = (1,n,-first)
    seqset = set(seqtuple)
    period = 1
    while 1:
        nextres,seqtuple = next(*seqtuple)
        if seqtuple in seqset:
            return period-1
        seqset.add(seqtuple)
        period +=1

total = 0
for i in range(2,10001):
    if int(i**(1.0/2)) - (i**(1.0/2)) != 0:
        if rootperiod(i)%2==1:
            total+=1
print total
        

