using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoDir
{
    class FileNode
    {
        public DirectoryInfo fullPath { get; set; }
        public FileInfo fileName { get; set; }
        public List<FileNode> childNodes { get; set; }

        private string cleanedContent;

        public bool exists { get; set; }
        public DateTime lastModified { get; set; }
        public DateTime lastAccessed { get; set; }

        public int depth { get; set; }
        public string extension { get; set; }
        public bool fileExists { get; set; }
        public bool isRealFile { get; set; }
        public bool isWPDoc { get; set; }

        public string state { get; set; }
        public string stateInitials { get; set; }

        public bool isChild { get; set; }
        
        public bool isParent { get; set; }




        string name { get; set; }
        public FileNode(string filePath, bool isDirectory)
        {
            try
            {
                fullPath = new DirectoryInfo(filePath);
                fileName = new FileInfo(filePath); //not used very often...
                childNodes = new List<FileNode>();
                fullPath.Refresh();
                fileExists = fileName.Exists; //apparently this works weird according to online, works in instantation but shows false elsewhere?
                isRealFile = canOpen();
            }
            catch
            {
                MessageBox.Show("BAD FILE PATH:\n" + filePath.ToString());
                isRealFile = false;
            }
            



            if(fileExists && isRealFile)
            {
                lastModified = fileName.LastWriteTime;
                lastAccessed = fileName.LastAccessTime;
                fileExists = true;
                extension = fullPath.Extension;
                if (extension != ".wpd" && extension != ".frm" && extension != ".WPD" && extension != ".FRM")
                    isWPDoc = false;
                else
                    isWPDoc = true;
            }
            
            if (!isDirectory && isRealFile && isWPDoc)
            {
                GetContents(filePath);
                
            }
            cleanedContent = "";


        }
        public string GetContents(string path)
        {
            byte[] byteBuffer = ReadBytes(path);
            string result = System.Text.Encoding.Default.GetString(byteBuffer);
            //MessageBox.Show(result);
            result = result.Replace((char)8364, (char)32);
            //MessageBox.Show(result2);
            //string result3 = pathNotationClean(result2);
            //MessageBox.Show(result3);
            //string cleanResult = Regex.Replace(result3, @"[^a-zA-Z0-9 :_.\-\\ ]", "");
            cleanedContent = result;//cleanResult;
            //MessageBox.Show(cleanedContent);
            GetChildrenFiles();
            return cleanedContent;// cleanResult; // i dont remember why i returned this, probably old leaving in just in case...i guess?
        }
        private static byte[] ReadBytes(string fp)
        {
            byte[] buffer;
            List<byte> bufferList = new List<byte>();
            string bufferResult;

            FileStream fs = new FileStream(fp, FileMode.Open, FileAccess.Read);
            try
            {
                int length = (int)fs.Length;
                buffer = new byte[length];            // create buffer
                int count;                            // actual number of bytes read
                int sum = 0;                          // total number of bytes read

                // read until Read method returns 0 (end of the stream has been reached)
                while ((count = fs.Read(buffer, sum, length - sum)) > 0)
                    sum += count;  // sum is a buffer offset for next reading
            }
            finally
            {
                fs.Close();
            }

            foreach(byte b in buffer)
            {
                if(b!=0x00)
                {
                    bufferList.Add(b);
                }
            }

            byte[] newBuffer = new byte[bufferList.Count];
            for(int i = 0; i < bufferList.Count; i++)
            {
                newBuffer[i] = bufferList[i];
            }


            return newBuffer;
        }

        private void GetChildrenFiles()
        {
            char[] contentChar = cleanedContent.ToCharArray();
            char[] driveChar = fullPath.FullName.ToCharArray(0,2);
            

            for(int i=0; i < cleanedContent.Length; i++)
            {
                if(contentChar[i].Equals(driveChar[0]) && contentChar[i+1].Equals(driveChar[1]) && contentChar[i+2].Equals((char)92))
                {
                    char[] pathChar = new char[1000];
                    int counter = 0;
                    string[] condition = { ".frm", ".wpd", ".FRM", ".WPD", ".JPG", ".jpg", ".wcm", ".WCM" };
                    bool reachedEnd = false;
                    char[] currentChar = new char[4];
                    currentChar[0] = contentChar[i];
                    currentChar[1] = contentChar[i+1];
                    currentChar[2] = contentChar[i+2];
                    currentChar[3] = contentChar[i+3];
                    string current = new string(currentChar);

                    //while(!contentChar[i].Equals('.') && !contentChar[i + 1].Equals('f') && !contentChar[i+2].Equals('r') && !contentChar[i + 3].Equals('m'))
                    //while (counter <= 999 && (!current.Equals(condition[0]) && !current.Equals(condition[1]) && !current.Equals(condition[2]) && !current.Equals(condition[3]) && !current.Equals(condition[4]) && !current.Equals(condition[5])))
                    while (counter <= 999 && !extensionMatch(current))
                    {
                        
                        pathChar[counter] = contentChar[i];
                        
                        counter++;
                        i++;
                        if ((contentChar.Length - i) > 3)
                        {
                            StringBuilder sb = new StringBuilder(current);
                            sb[0] = contentChar[i];
                            sb[1] = contentChar[i + 1];
                            sb[2] = contentChar[i + 2];
                            sb[3] = contentChar[i + 3];

                            current = sb.ToString();
                        }
                        else
                        {
                            reachedEnd = true;
                            break;
                        }
                        
                    }
                    if(!reachedEnd && counter != 1000) //if the counter gets to a 1000 theres no way its a file lol
                    {
                        pathChar[counter] = contentChar[i]; //add the extension to the new path, could maybe rework it to look at the last 4 in the array instead
                        pathChar[counter + 1] = contentChar[i + 1];
                        pathChar[counter + 2] = contentChar[i + 2];
                        pathChar[counter + 3] = contentChar[i + 3];
                        string pathCond = new string(pathChar);
                        pathCond = pathNotationClean(pathCond, (char)208);
                        pathCond = pathNotationClean(pathCond, (char)212);
                        
                        
                        
                        //MessageBox.Show(pathCond);
                        pathCond = pathCond.Replace((char)8364, (char)32);
                        //MessageBox.Show(pathCond);
                        pathCond = Regex.Replace(pathCond, @"[^a-zA-Z0-9 :_.\-\\ ]", "");
                        string pathString = @"" + pathCond.TrimEnd('\0');
                        //MessageBox.Show(pathString);
                        if(!pathString.Equals(fullPath.FullName))
                            childNodes.Add(new FileNode(pathString, false));
                        pathString = "";

                    }                    
                }
            }
            
            

        }
        private string pathNotationClean(string s, char c)
        {
            char pathNotationChar = new char();
            pathNotationChar = c;
            char[] contentChar = s.ToCharArray();

            for (int i = 0; i < contentChar.Length; i++)
            {
                if (contentChar[i].Equals(pathNotationChar))
                {
                    contentChar[i] = '\0';
                    char nextChar = contentChar[i + 1];
                    //char postChar = contentChar[i + 2];
                    //string postCharString = Regex.Replace(postChar.ToString(), @"[^a-zA-Z0-9 :_.\-\\ ]", "");
                    bool isDelimiter = false;
                    int startingIndex = i;

                    //if (postCharString != "")
                    //isDelimiter = true;
                    try
                    {
                        if (pathNotationChar.Equals((char)212))
                        {
                            if (contentChar[i + 7].Equals((char)212))
                            {
                                for (int j = i; j <= i + 7; j++)
                                {
                                    contentChar[j] = '\0';
                                }
                            }
                            if (contentChar[i + 15].Equals((char)212))
                            {
                                for (int j = i; j <= i + 15; j++)
                                {
                                    contentChar[j] = '\0';
                                }
                            }

                        }
                    }
                    catch { }
                    if (pathNotationChar.Equals((char)208))
                    {
                        if (contentChar[i + 11].Equals((char)208))
                        {
                            for (int j = i; j <= i + 11; j++)
                            {
                                contentChar[j] = '\0';
                            }
                            contentChar[i + 11] = (char)32;
                        }
                        else if (contentChar[i + 14].Equals((char)208))
                        {
                            for (int j = i; j <= i + 14; j++)
                            {
                                contentChar[j] = '\0';
                            }
                            contentChar[i + 14] = (char)32;
                        }

                        else if(contentChar[i + 18].Equals((char)208))
                        {
                            for (int j = i; j <= i + 18; j++)
                            {
                                contentChar[j] = '\0';
                            }
                            contentChar[i + 18] = (char)32;
                        }
                        else if (contentChar[i + 19].Equals((char)208))
                        {
                            for (int j = i; j <= i + 19; j++)
                            {
                                contentChar[j] = '\0';
                            }
                            contentChar[i + 19] = (char)32;
                        }
                        else if (contentChar[i + 20].Equals((char)208))
                        {
                            for (int j = i; j <= i + 20; j++)
                            {
                                contentChar[j] = '\0';
                            }
                            contentChar[i + 20] = (char)32;
                        }
                    }

                    /*while (!nextChar.Equals(pathNotationChar) && !isDelimiter)
                    {

                        contentChar[i + 1] = '\0';
                        i++;
                        nextChar = contentChar[i + 1];//keeps failing here because the character it's scrubbing is showing up in the "random" byte found in the path string, throwing it off when its not a delimiter
                        //postChar = contentChar[i + 2];
                        //postCharString = Regex.Replace(postChar.ToString(), @"[^a-zA-Z0-9 :_.\-\\ ]", ""); //determine if theres a valid filepath character after the delimiter, accounts for problem above
                        //if (postCharString != "")
                        if((i-startingIndex)>=19 && nextChar.Equals(pathNotationChar))
                            isDelimiter = true;

                    }
                    if(!pathNotationChar.Equals((char)208)) //weird D thing
                        contentChar[i + 1] = '\0';
                    else
                        contentChar[i + 1] = (char)32; //space */

                }    
            }
            string result = new string(contentChar);
            result = result.Trim('\0');

            return result;
        }
        private bool canOpen()
        {
            bool isReal = false;
            if (File.Exists(fullPath.FullName))
            {isReal = true;}   
            else
            { isReal = false;}
            return isReal;
        }
        private bool extensionMatch(string extension)
        {
            string[] condition = { ".frm", 
                                   ".wpd",
                                   ".FRM",
                                   ".WPD",
                                   ".JPG",
                                   ".jpg",
                                   ".wcm",
                                   ".WCM" };
            foreach (string s in condition)
            {
                if (extension.Equals(s))
                    return true;
            }
            return false;
        }
    }
}
