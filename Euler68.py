import itertools
import time

start = time.time()

valids = []
firstSet = set(range(1,10))
for firstSelected in itertools.permutations(firstSet,2):
    firstElts = (10,firstSelected[0],firstSelected[1])
    total = sum(firstElts)
    secondSet =  firstSet.difference(firstElts)
    for secondElts in itertools.permutations(secondSet,2):
        if sum(secondElts) + firstElts[2]!= total:
            continue
        thirdSet = secondSet.difference(secondElts)
        for thirdElts in itertools.permutations(thirdSet,2):
            if sum(thirdElts)+secondElts[1] != total:
                continue
            fourthSet = thirdSet.difference(thirdElts)
            for fourthElts in itertools.permutations(fourthSet,2):
                if sum(fourthElts) + thirdElts[1] != total:
                    continue
                fifthSet = fourthSet.difference(fourthElts)
                for fifthElt in fifthSet:
                    if fifthElt + fourthElts[1] + firstElts[1] == total:
                        valids += [[firstElts,secondElts,thirdElts,fourthElts,fifthElt]]
        

def chain(valid):
    l = [valid[0]]
    l += [(valid[1][0],valid[0][2],valid[1][1])]
    for i in range(2,4):
        l+= [(valid[i][0],valid[i-1][1],valid[i][1])]
    l += [(valid[4],valid[3][1],valid[0][1])]
    topind = max(enumerate(l), key=lambda n: -n[1][0])[0]
    return l[topind:]+l[:topind]

def digits(chain):
    l = []
    for c in chain:
        l+=list(c)
    return l

def num(diglist):
    strep = "".join([str(n) for n in diglist])
    return int(strep)



print max([num(digits(chain(v))) for v in valids])

end = time.time()
print "%f seconds"%(end-start)
#sixteens = [n for n in [num(chain(valid)) for valid in valids] if len(str(n))==16]
