
Console.WriteLine("Getting orig file and saving it at " + "ORIGINALFOR_" + args[1] + ".dll" + " ...");

//saving origfile
var ORIG = File.ReadAllBytes(args[0]);
var ORIG2 = File.ReadAllBytes(args[0]);
var sizeofORIG = ORIG.Length;
File.WriteAllBytes("ORIGINALFOR_" + args[1] + ".dll", ORIG);


//get changed data

var lines = File.ReadAllLines("SALTINE_CHANGEDBYTES_" + args[1] + ".txt");

//set flags

bool origbigger = false;
bool changedbigger = false;
bool same = false;

if (lines[0][0] == '1')
{
    origbigger = true;
}
else if (lines[0][0] == '2')
{
    changedbigger = true;
}
else
{
    same = true;
}

//find size of changed

int sizeofCHANGED = Int32.Parse((lines[0]).Substring(3));

//make dicts
Dictionary<int,int> changingbytes  = new Dictionary<int,int>();
Dictionary<int, int> savedtypes = new Dictionary<int, int>();

for(int i = 1; i< lines.Length; i++)
{
    if (lines[i].IndexOf('x') != -1)
    {
        int pos = Int32.Parse(lines[i].Substring(0, lines[i].IndexOf(':')));
        int value = Int32.Parse(lines[i].Substring(lines[i].IndexOf(':')+3));
        savedtypes.Add(pos, value);
    }
    else
    {
        int pos = Int32.Parse(lines[i].Substring(0, lines[i].IndexOf(':')));
        int value = Int32.Parse(lines[i].Substring(lines[i].IndexOf(':') + 2));
        changingbytes.Add(pos, value);
    }
}

Console.WriteLine("Formatting File.....");
// format file
var CHANGINGFILE = File.Open(args[0], FileMode.Truncate);
CHANGINGFILE.Flush();
CHANGINGFILE.Close();
//write data and close
var starttime = DateTime.Now;

var currentPos = 0;
var currentest = 0;
var linepos = 1;

while(currentPos < sizeofCHANGED)
{
    var startofchange = DateTime.Now;
    var iftyp = savedtypes.TryGetValue(currentPos, out int val);
    if( iftyp == false)
    {
        bool bytpos = changingbytes.TryGetValue(currentPos, out int val2);
        if(bytpos == true)
        {
            ORIG2[currentPos] = ORIG[val2];
            linepos += 1;
        }
    }
    else
    {
        ORIG2[currentPos] = ORIG[val];
        linepos += 1;
    }
    var endofchange = DateTime.Now;
    var timetotakefor1 = endofchange - startofchange;
    currentest = (currentest + (timetotakefor1).Seconds);
    currentPos += 1;
    Console.WriteLine(currentPos + " out of " + sizeofCHANGED + ".." + " check back at " + (starttime.AddSeconds(((currentest*lines.Length) / linepos))));
}

Console.WriteLine("Writing Data... " + DateTime.Now);
File.WriteAllBytes(args[0],ORIG2);

Console.WriteLine("Done!");


