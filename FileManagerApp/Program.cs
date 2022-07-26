// check if file exists in location
bool fileExists = File.Exists(@"C:\TestFiles\DummyFile.txt");

if (!fileExists)
{
    // create a file 
    File.Create(@"C:\TestFiles\DummyFile.txt");
}

// encrypt and decrypt 
//File.Encrypt(@"C:\TestFiles\DummyFile.txt");
//File.Decrypt(@"C:\TestFiles\DummyFile.txt");

// copy to new file
File.Copy(@"C:\TestFiles\DummyFile.txt", @"c:\TestFiles\CopyFile.txt");

// get date and time when a file was accessed last  
DateTime lastAccessTime = File.GetLastAccessTime(@"C:\TestFiles\DummyFile.txt");

// get when the file was written last
DateTime lastWriteTime = File.GetLastWriteTime(@"C:\DummyFile.txt");

// move file to new location
File.Move(@"C:\TestFiles\DummyFile.txt", @"C:\DummyFile.txt");

// open file and returns FileStream for reading bytes from the file
FileStream fs = File.Open(@"C:\DummyFile.txt", FileMode.OpenOrCreate);

// open file and return StreamReader for reading string from the file
StreamReader sr = File.OpenText(@"C:\DummyFile.txt");

// delete file
File.Delete(@"C:\DummyFile.txt");