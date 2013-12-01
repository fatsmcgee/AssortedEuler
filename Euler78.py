from print_timing import *
import itertools

@arg_timing
def p(n):
    return pb(n,n)

def pb(num,biggest):
    if num <=1:
        return 1
    total = 0
    for i in range(1,biggest+1): #i is the next element we're using
        total+=pb(num-i,min(i,num-i))
    return total


def pdyn(num):
    table = []
    for i in range(num): 
        table += [[1]*(i+1)] 
    for n in range(1,num): #0 indexing, so n is actually n+1
        for biggest in range(1,n+1): #biggest is actually biggest+1
            total = 0
            for i in range(1,biggest+2):
                total+= 1 if n-i<0 else table[n-i][min(i-1,n-i)]
            table[n][biggest]=total
    return table[num-1][num-1]



def nexttable(table):
    newLen = len(table)+1
    #add row on to each old column
    for i in range(newLen-1):
        table[i] += [table[i][newLen-2]] #up until the first new column, new row is the same

    table += [[1]*newLen]
    
    n=newLen-1 #0 indexing, so n is actually n+1
    for biggest in range(1,n+1): #biggest is actually biggest+1
        total = 0
        for i in range(1,biggest+2):
            total+= 1 if n-i<0 else table[n-i][min(i-1,n-i)]
        table[n][biggest]=total
    for biggest in range(n+1,newLen):
        table[n][biggest] = table[n][n]
    return table

def pgenerator():
    curTable = [[1]]
    curN = 0
    while 1:
        yield curTable[curN][curN]
        curTable = nexttable(curTable)
        curN+=1

@arg_timing
def pgendyn(n):
    p = pgenerator()
    [p.next() for i in xrange(n-1)]
    return p.next()

ps = pgenerator()

def isprime(n):
	return all([n%i for i in range(2,(n/2)+1)])


def factorize(n):
	lis = {}
	pcand = 2
	while n>1:
		if n%pcand==0:
			lis[pcand]=0
			while n%pcand==0:
				lis[pcand]+=1
				n/=pcand
		pcand+=1
		while not isprime(pcand):
			pcand+=1
	return lis


#we need (2*5 *2*5 *2*5)^2
#so 2^6*5^6
