#just some thoughts
"""
For a given k, like k=5, try all permutations of using just #1,
then using 1 and 2, then 1,2,and 3, and so on
if you found something at a given level, make sure to find
all things at that level to be sure
"""

from print_timing import *
from operator import *
import itertools

def product(numiter):
    return reduce(lambda a,b: a*b, numiter)

def usageLists(items, total,inpList=[]):
    #print inpList
    if total==0:
        #print inpList,"fun"
        return [tuple(inpList)]

    if len(items)==1:
        return [tuple(inpList + [items[0]]*total)]
    
    retLists = []
    for headUse in range(total+1):
        retLists += usageLists(items[1:],total-headUse,inpList + [items[0]]*headUse)
    return retLists


def findProductSum(numlist,k):
    solutions = set()
    numPool = []
    for n in numlist:
        numPool += [n]
    for numset in usageLists(numPool,k):
        if sum(numset)==product(numset):
            solutions.add((sum(numset),numset))
    return solutions

@arg_timing
def findMinimalProductSum(k):
    i=1
    numlist = [1]
    while 1:
        sol = findProductSum(numlist,k)
        if len(sol):
            return min(list(sol),itemgetter(0))
        i+=1
        numlist += [i]
        
        
        
