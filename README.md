Ever wanted to share your DLL mods, but not the actual DLL because of legal reasons?
SALTINE solves that!

SALTINE is a format for sharing and downloading perm modifications to DLL's. 
It does this by looking at every single changed byte in between 2 DLL's, and transcribing what they are.
Then, it obfucates the byte changed by making it a refrence to a byte in the original file- I.E, you need the original file in order to use the transcription.

Right now it uses RFIND to pick the byte locations (so you can't look in the header to guess,) , but I can obfuscate it more if needed :D

--USAGE (command line only)--

TRANSCRIBE:

python SALTINE_TRANSCRIBER.py "<ORIG_FILE_GLOBALPATH>" "<MODIFIED_FILE_GLOBALPATH>" "<MOD_NAME>"

Creates a .txt transcribe file with the naming SALTINE_CHANGEDBYTES_<name of project>.txt

APPLY:

SALTINE_APPLIER.exe "<FILE_TOMODIFY>" "<MOD_NAME>"

Applies changes to FILE_TOMODIFY and creates a copy of the original as ORIGINALFOR_<name of project>.dll
