from math import log

f = open("base_exp.txt")
bexps = []

l=1
for line in f:
    bexps += [(l,eval("(" + line + ")"))]
    l+=1

def order((base,exp)):
    return exp*log(base)

bexps.sort(key= lambda tup: order(tup[1]))

print bexps[-1][0]
