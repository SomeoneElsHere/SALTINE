import sys

#idea:
#Make a dict with all the positions and changed values
#use r find to find the changed value byte in the orig file
#make a dict with all the positions and corresponding byte positions in the original file
#ex
# we changed 8 at pos 10 to 9
# 9 is last found in the orig file at 11
# dict is 10 :: 11
# 10 being the position of the byte to change and 11 being the position of the changed byte value in the orig file.
#if not found in the orig file, prefix with x and say the byte
#ex.
# 10 :: x9

#open files
ORIGFILE = open(sys.argv[1],'rb')
MODIFIEDFILE = open(sys.argv[2],'rb')

#make dict for pos :: changed value
changedvalues  = dict()
#make dict for pos :: value position in orig
origvalues = dict()

origbigger = False
modifiedbigger = False
samesize = False

pos = 0


# read files and close them
ORIG = ORIGFILE.read()
MODIFIED = MODIFIEDFILE.read()

ORIGFILE.close()
MODIFIEDFILE.close()


# look for flags for which is bigger
if len(ORIG) > len(MODIFIED):
    origbigger = True
if len(MODIFIED) > len(ORIG):
    modifiedbigger = True
if len(MODIFIED) == len(ORIG):
    samesize = True
print("Making dict..\n")
# if orig bigger write down new bytes and changed bytes
if origbigger:
    while pos < len(MODIFIED):
        if ORIG[pos] != MODIFIED[pos]:
            changedvalues.update({pos : MODIFIED[pos]})
        pos = pos + 1
    while pos < len(MODIFIED):
        changedvalues.update({pos : MODIFIED[pos]})
        pos = pos + 1

# if modified bigger write down only modified
if modifiedbigger:
    while pos < len(ORIG):
        changedvalues.update({pos : MODIFIED[pos]})
        pos = pos + 1

# same size? write down modified
if samesize:
    while pos < len(MODIFIED):
        changedvalues.update({pos : MODIFIED[pos]})
        pos = pos + 1

# find corresponding values in orig
for pos,value in changedvalues.items():
    cpos = ORIG.rfind(value)
    if cpos == -1:
        origvalues.update({pos : "x"+str(value)})
    else:
        origvalues.update({pos : cpos})

#make file
NEWFILE = open("SALTINE_CHANGEDBYTES_"+sys.argv[3]+".txt","x")

print("Writing down transcription..\n")
#write down flags on first line
if origbigger:
    NEWFILE.write("1::"+str(len(MODIFIED))+"\n")
if modifiedbigger:
    NEWFILE.write("2::"+str(len(MODIFIED))+"\n")
if samesize:
    NEWFILE.write("0\n")

#write down data
for position, posoriginal in origvalues.items():
    NEWFILE.write(str(position)+"::"+str(posoriginal)+"\n")

#flush and close
NEWFILE.flush()
NEWFILE.close()