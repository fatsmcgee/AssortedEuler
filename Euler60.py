"""Find the lowest sum for a set of five primes for which
any two primes concatenate to form another prime.
"""

from primes import *
import itertools

mapbound = 1000000
pmap = primemap(mapbound)
pmap = dict(enumerate(pmap))

searchbound = 20000
pto = primesto(searchbound)


def isprime(n):
    if pmap.has_key(n):
        return pmap[n]
    elif n&1==0 or n%3==0 or n%5==0 or n%7==0 or n%11==0:
        pmap[n]=False
        return False
    else:    
        res = mrabin(n)
        pmap[n]=res
        return res

def intconcat(n1,n2):
    n2digs = 0
    mulfactor=1
    while mulfactor<n2:
        mulfactor*=10
    return n1*mulfactor + n2

def allpairsprime(pairs):
    for p in pairs:
        if not isprime(intconcat(p[0],p[1])):
            return False
    return True

def permgood(perm):
    pairs = itertools.permutations(perm,2)
    return allpairsprime(pairs)

goodpairs = []

pnum = len(pto)
print len(pto),"primes up to",searchbound
print (pnum*(pnum-1)*(pnum-2)*(pnum-3)*(pnum-4))/120,"possible 5-tuples"

perms = itertools.combinations(pto,2)
for perm in perms:
    if permgood(perm):
        goodpairs.append(perm)

print "good pairs calculated with length of",len(goodpairs)

goodtris = filter(lambda tup: permgood(tup),((p[0],p[1],p2) for p in goodpairs for p2 in pto))

print "good tris calculated with length of",len(goodtris)

goodquads = filter(lambda tup: permgood(tup),((p[0],p[1],p[2],p3) for p in goodtris for p3 in pto))

print "good quads calculated with length of",len(goodquads)

goodpents = filter(lambda tup: permgood(tup),((p[0],p[1],p[2],p[3],p4) for p in goodquads for p4 in pto))

print "good 5-tuples calculated with length of",len(goodpents)

"""
print "Tests started"
i=0
winners=[]

perms = itertools.combinations(pto,5)
for perm in perms:
    if i%1000000==0:
        print i,"trials out of about 6 billion"
    pairs = list(itertools.permutations(perm,2))
    winner = True
    for p in pairs:
        if not isprime(intconcat(p[0],p[1])):
            winner = False
            break
    if winner:
        winners.append(zip(pairs,[isprime(intconcat(p[0],p[1])) for p in pairs]))
    i+=1
print "FINISHED"
"""
