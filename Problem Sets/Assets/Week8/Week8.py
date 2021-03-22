
"""
Make a function for each of the below.

You can run this script by calling either "python Week8.py" (Mac/Linux) or "py Week8.py" (Windows) from
a command line interface once you've navigated to the correct folder.

cd (change directory) 
	EX: cd Desktop/AGP_ProblemSets/Week8/ 
ls (list current directory) 

"""

# Return true if even, false if odd
def isEven(input) :
	if (input % 2 ) == 0 :
		return True
	else:
		return False 


# Return the product of the input and all positive integers below it.
def factorial(input) :
	result = 1
	for i in range(1,input+1) :
		result = result * i
	return result

# Given a list of numbers, return the difference between the largest and smallest.
def widthOfList(input) :
	print(input[0])
	largest = input[0]
	smallest = input[0]
	for i in input:
		if (i>= largest):
			largest = i
		if (i<= smallest):
			smallest = i
	return largest - smallest

"""

Write a function that takes in a number and determines whether it's the same upside down.

6090609		True 
6996		False 		(becomes 9669)
806908		True

"""
def sameUpsideDown(input) :
	string = str(input)
	for i in range(0,int(len(string)/2)):
		if not ((string[i]=='6' and string[len(string)-i-1]=='9') or (string[i]=='0' and string[len(string)-i-1]=='0') or (string[i]=='8' and string[len(string)-i-1]=='8') or (string[i]=='9' and string[len(string)-i-1]=='6')):
			return False
	return True


# Read the provided list of words, write to "output.txt" all words of given length that start with that letter.
def allWordsOfLength(length, startingLetter) :
	listOfWords=[]
	with open('wordlist.txt') as theList:
		for line in theList.readlines():
			for word in line.split('\n'):
				if  (len(word)==length) and (word[0] == startingLetter):
					listOfWords.append(word)
	f = open("output.txt", "w")
	for word in listOfWords:
		f.write(word+"\n")
	return 0

# ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ #
# Don't edit below this line.

class bcolors:
    HEADER = '\033[95m'
    OKBLUE = '\033[94m'
    OKGREEN = '\033[92m'
    WARNING = '\033[93m'
    FAIL = '\033[91m'
    ENDC = '\033[0m'
    BOLD = '\033[1m'
    UNDERLINE = '\033[4m'

def printTest(text, color = "") :
	print (color + text + bcolors.WARNING)

check = lambda a : bcolors.OKGREEN + 'Correct ' if a else bcolors.FAIL + 'Incorrect '

printTest("\nPython Tests:", bcolors.BOLD)

printTest("Is Even Tests:", bcolors.HEADER)

printTest(check(isEven(2)) + 'for isEven returning correctly for even input.')
printTest(check(not isEven(3)) + 'for isEven returning correctly for odd input.')

printTest("\nFactorial Tests:", bcolors.HEADER)

printTest(check(factorial(4) == 24) + 'for factorial w/ input 4.')
printTest(check(factorial(8) == 40320) + 'for factorial w/ input 8.')

printTest("\nWidth of List Tests:", bcolors.HEADER)

printTest(check(widthOfList([1, 3]) == 2) + 'for width of list with two numbers.')
printTest(check(widthOfList([1, 2, 3, 4, 5]) == 4) + 'for width of list with more than two nubmers.')
printTest(check(widthOfList([1, 1, 1, 1, 1, 1]) == 0) + 'for width of list with the same number for all entries.')

printTest("\nSame Upside Down:", bcolors.HEADER)

printTest(check(sameUpsideDown(806908)) + 'for 806908.')
printTest(check(not sameUpsideDown(284392)) + 'for 284392.')
printTest(check(not sameUpsideDown(6996)) + 'for 6996.')
printTest(check(sameUpsideDown(806908)) + 'for 806908.')


printTest("\nWords of Length Test:", bcolors.HEADER)
allWordsOfLength(2, 'E')
with open('output.txt') as filehandle : 
	printTest(check(len(filehandle.readlines()) == 1) + 'found all 2 letter words starting with E.')

allWordsOfLength(3, 'A')
with open('output.txt') as filehandle : 
	printTest(check(len(filehandle.readlines()) == 7) + 'found all 3 letter words starting with A.')

allWordsOfLength(6, 'E')
with open('output.txt') as filehandle : 
	printTest(check(len(filehandle.readlines()) == 36) + 'found all 6 letter words starting with E.')

