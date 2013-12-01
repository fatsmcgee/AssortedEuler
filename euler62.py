import itertools

import threading

from print_timing import *



def digits(n):
	return [int(c) for c in list(str(n))]
    
def number(digits):
	return sum(10**t[0]*t[1] for t in enumerate(digits[::-1]))

def areperms(n1,n2):
    return sorted(digits(n1))==sorted(digits(n2))

@print_timing
def findfirstnperms(n):
    cubes = [0]
    for i in itertools.count(1):
        #print i
        curcube = i**3
        cubes +=[curcube]

        nums = [curcube]
        curdigs = len(digits(curcube))
        for prev in range(i-1,0,-1):
            prevcube = cubes[prev]
            prevdigs = len(digits(prevcube))
            if prevdigs < curdigs:
                break
            if areperms(curcube,prevcube):
                nums+=[prevcube]
                if len(nums) == n:
                    return nums
"""
def findfirstnperms(n):
    foundEvent = threading.Event()
    printLock = threading.Lock()
    @print_timing
    def ThreadProc(thread,foundEvent,printLock):
        for i in itertools.count(1+thread,6):
            if foundEvent.is_set():
                break
            if thread == 2:
                print i
            cube = i**3
            result = checkn(cube,n)
            if result:
                printLock.acquire()
                if foundEvent.is_set():
                    printLock.release()
                    break
                print i,cube,result
                foundEvent.set()
                printLock.release()
                break
    threads = [threading.Thread(target=ThreadProc,args=(i,foundEvent,printLock)) for i in range(6)]
    for t in threads:
        t.start()
    for t in threads:
        t.join()
        print "one thread bites the dust"
"""


def iscube(n):
    croot = n ** (1.0/3)
    diff = croot-int(croot)
    if diff <.001:
        return int(croot)**3==n
    elif diff > .999:
        return int(croot+1)**3 == n
    else:
        return False


def unique(iterator):
    s = set()
    for item in iterator:
        if item in s:
            continue
        else:
            s.add(item)
            yield item

def checkn(num,n):
    numbers = []
    numdigs = len(digits(num))
    digperms = unique(itertools.permutations(digits(num)))
    for digs in digperms:
        if digs[0] == 0:
            continue
        num = number(digs)
        if iscube(num):
            numbers+= [num]
            if len(numbers)==n:
                return numbers
    return False

