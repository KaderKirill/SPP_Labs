using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SPP_Project2
{
    class CopyFiles
    {

        private int copied;
       
        private Queue<string> files = new Queue<string>();
        private Queue<string> dirs = new Queue<string>();
        private string originalDir;
        private string finalDir;
        private TaskQueue pull;
        private long freespace;
        private long dirspace;
        
        public CopyFiles(string poriginalDir,string pfianlDir) {
            if (isDirectory(poriginalDir) && isDirectory(pfianlDir))
            {
                this.finalDir = pfianlDir;
                this.originalDir = poriginalDir;
                DriveInfo info = new DriveInfo(@"D:\");
                this.freespace = info.TotalFreeSpace;
                dirspace = 0;

            }
            else {
                throw new Exception("directories does't exists");
            }
        }
        public long getCopiedLenght() {
            return dirspace;
        }


        public int getCopied() {
            return copied;
        }
        private void getSubDirectories(string path) {
            string[] pfiles = Directory.GetFiles(path);
            string[] pdirs = Directory.GetDirectories(path);
            for (int i = 0; i < pfiles.Length; i++) {
             
                files.Enqueue(pfiles[i]);
            }
            for (int i = 0; i < pdirs.Length; i++) {
                dirs.Enqueue(pdirs[i]);
                getSubDirectories(pdirs[i]);
            }
        }

        private int TestTask(Queue<string> files) {
            int count = 0;
            if (files.Count == 0) {
                return count;
            }
            string original = files.Dequeue();
            string final = files.Dequeue();
            while(files.Count>0) {
                string path = files.Dequeue();
                if (path.Equals("")) {
                    return count;
                }
                StringBuilder builder = new StringBuilder();
                builder.Append(final);
                builder.Append(path.Substring(original.Length));
                FileInfo file = new FileInfo(path);
                FileInfo copied = new FileInfo(builder.ToString());
                //Console.WriteLine(builder.ToString());
                if (!copied.Exists)
                {
                    file.CopyTo(builder.ToString());
                    count++;
                }
            }
            return count;
        }
        private void TestCopyFile() {
            int size = files.Count;
            int threads = 10+size/100;
            int maxBlock = 5;
            pull = new TaskQueue(threads);
            while(files.Count>0) {
                Queue<string> filess = new Queue<string>();
                filess.Enqueue(originalDir);
                filess.Enqueue(finalDir);
                long lenght = 0;
                do {
                    filess.Enqueue(files.Dequeue());
                    //lenght += sizefiles.Dequeue();
                    lenght++;
                } while (lenght < maxBlock && files.Count > 0);
                
                pull.EnqueueTask(TestTask, filess);
            }
            pull.Finalize();
            copied = pull.getCopied();
        }
                //to singlethreadcopy
        private void CopyFileWithoutThreads() {
            int size = files.Count;
            copied = size;  
            for (int i = 0; i < size; i++)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(finalDir);
                string path;
                path = files.Dequeue();
                builder.Append(path.Substring(originalDir.Length));
                FileInfo file = new FileInfo(path);
                file.CopyTo(builder.ToString());                
                //Console.WriteLine(builder.ToString());
            }
        }
    
        
        private void getInfo() {
            int size = dirs.Count;
            for (int i = 0; i < size; i++)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(finalDir);
                builder.Append(dirs.Dequeue().Substring(originalDir.Length));
                Directory.CreateDirectory(builder.ToString());
            }
            
            Console.WriteLine();
            
        }
        public void Copy(bool isMultithreading) {
            getSubDirectories(originalDir);
            getInfo();
            if (dirspace > freespace)
            {
                throw new Exception("There is no free space on disk ");
            }
            else {
                if (isMultithreading)
                {
                    //CopyFile();
                    TestCopyFile();
                }
                else {
                    CopyFileWithoutThreads();
                }
            }
            
        }

        private bool isDirectory(string dir) {
            return new DirectoryInfo(dir).Exists;
        }
    }
}
