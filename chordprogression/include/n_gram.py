import math


def n_gram(target, n):
	return [ target[idx:idx + n] for idx in range(len(target) - n + 1)]


artists = []
global similarity
similarity = 0.0

chords1 = [];
chords2 = [];

target1 = "C-Em-Am-Em7"

target2 = "C-Am-Dm-G"

def additional(chords1, chords2, a, b):
	similarity = 0.0;
	if(chords1[a] == chords2[b] and a == b):
		similarity = similarity + 0.3
		print("(+0.3)") 
	if(chords1[a] == ['C'] and chords2[b] == ['Em'] and a == b):
		similarity = similarity + 0.3
		print("(+0.3)")
	if(chords1[a] == ['C'] and chords2[b] == ['Am'] and a == b):
		similarity = similarity + 0.1
		print("(+0.1)")
	if(chords1[a] == ['Dm'] and chords2[b] == ['F'] and a == b):
		similarity = similarity + 0.3
		print("(+0.3)")
	if(chords1[a] == ['Em'] and chords2[b] == ['C'] and a == b):
		similarity = similarity + 0.3
		print("(+0.3)")
	if(chords1[a] == ['Em'] and chords2[b] == ['Am'] and a == b):
		similarity = similarity + 0.1
		print("(+0.1)")
	if(chords1[a] == ['F'] and chords2[b] == ['Dm'] and a == b):
		similarity = similarity + 0.3
		print("(+0.3)")
	if(chords1[a] == ['Am'] and chords2[b] == ['C'] and a == b):
		similarity = similarity + 0.1
		print("(+0.1)")
	if(chords1[a] == ['Am'] and chords2[b] == ['Em'] and a == b):
		similarity = similarity + 0.1
		print("(+0.3)")
	if((chords1[a] == ['C'] and chords2[b] == ['CM7'] and a == b) or (chords1[a] == ['CM7'] and chords2[b] == ['C'] and a == b)):
	    similarity = similarity + 0.3
	    print("(+0.3)")
	if(chords1[a] == ['Dm'] and chords2[b] == ['Dm7'] and a == b) or (chords1[a] == ['Dm7'] and chords2[b] == ['Dm'] and a == b):
	    similarity = similarity + 0.3
	    print("(+0.3)")
	if(chords1[a] == ['Em'] and chords2[b] == ['Em7'] and a == b) or (chords1[a] == ['Em7'] and chords2[b] == ['Em'] and a == b):
	    similarity = similarity + 0.3
	    print("(+0.3)")
	if(chords1[a] == ['F'] and chords2[b] == ['FM7'] and a == b) or (chords1[a] == ['FM7'] and chords2[b] == ['F'] and a == b):
	    similarity = similarity + 0.3
	    print("(+0.3)")
	if(chords1[a] == ['G'] and chords2[b] == ['G7'] and a == b) or (chords1[a] == ['G7'] and chords2[b] == ['G'] and a == b):
	    similarity = similarity + 0.3
	    print("(+0.3)")
	if(chords1[a] == ['Am'] and chords2[b] == ['Am7'] and a == b) or (chords1[a] == ['Am7'] and chords2[b] == ['Am'] and a == b):
	    similarity = similarity + 0.3
	    print("(+0.3)")
	if(chords1[a] == ['Bdim'] and chords2[b] == ['Bm7-5'] and a == b) or (chords1[a] == ['Bm7-5'] and chords2[b] == ['Bdim'] and a == b):
	    similarity = similarity + 0.3
	    print("(+0.3)")
	return similarity




def similardegree(target1,target2):
	target1split = target1.split('-')
	target2split = target2.split('-')
	chordslist = [];
	similarity=0.0
	for i in range(1,len(target1split)+1):
		chordslist = n_gram(target1split, i)
		for j in range(0,len(chordslist)):
			chords1.append(chordslist[j])
	   #print(chords1)
	for i in range(1,len(target2split)+1):
		chordslist = n_gram(target2split, i)
		for j in range(0,len(chordslist)):
			chords2.append(chordslist[j])
	   
	totalCount = 0.0 
	equalCount = 0.0
	chords1ForAdd = chords1
	chords2ForAdd = chords2
	   
	for i in range(len(chords1)):
		   totalCount = totalCount + 1
		   for j in range(len(chords2)):
			  similarity += additional(chords1ForAdd, chords2ForAdd, i,j)
			  if chords1[i] == chords2[j]:
				  equalCount = equalCount + 1
				  break
			   
			   
	similarity = similarity + equalCount/totalCount
	print("chord progression1 : " , target1)
	print("chord progression2 : " , target2)
	print("equalCount : " , int(equalCount))
	print("total check Count : " , int(totalCount))
	print("degree of similar : " , similarity)
	return similarity

#similardegree(target1, target2)


