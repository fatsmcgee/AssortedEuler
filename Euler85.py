# -*- coding: utf-8 -*-
"""
Created on Sat Mar 14 14:58:19 2015

@author: gumpy
"""

def num_rects(w,h):
    total = 0    
    for sub_w in range(1,w+1):
        for sub_h in range(1,h+1):
            num_across = w-sub_w+1
            num_down = h-sub_h +1
            total += num_across*num_down
    return total
    
def Euler85():
    best_w = None
    best_h = None
    best_val = 0
    val = None
    threshold = 2500000
    target = 2000000

    w = 1    
    while True:
        h = 1
        val = 0
        while val < threshold:
            val = num_rects(w,h)
            if abs(val-target) < abs(best_val-target):
                best_val = val
                best_w,best_h = w,h
            h+=1
            
        if h<4:
            break
        print w
        w+=1
        
    print best_val,best_w,best_h
    
Euler85()