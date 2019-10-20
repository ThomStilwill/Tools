using System;
using System.IO;
using System.Linq;

namespace NodeCleaner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Traversing...");

            Traverse(@"D:\Cloud\GoogleDrive\Projects\NG");

            Console.WriteLine("Complete, press any key.");
            Console.ReadKey();
        }




        public static void Traverse(string path)
        {
            string lastFolderName = Path.GetFileName(Path.GetDirectoryName(path+@"/"));

            if (lastFolderName == "node_modules")
            {
                Console.Write(path);
                var aggregate = DeleteDirectories(path);
                Console.WriteLine($": Directories={aggregate.DirectoryCount}, FileCount: {aggregate.FileCount}, FileSize: {aggregate.FileSize}");
                return;
            }

            var directories = Directory.EnumerateDirectories(path);
            
            foreach (var directory in directories)
            {
               Traverse(directory);
            }
        }

        static Aggregate DeleteDirectories(string path)
        {
            long fileSize = 0;
            long fileCount = 0;
            long directoryCount = 1;

            foreach (var d in Directory.EnumerateDirectories(path))
            {
                var aggregate = DeleteDirectories(d);

                directoryCount += aggregate.DirectoryCount;
                fileSize += aggregate.FileSize;
                fileCount += aggregate.FileCount;
            }

            var entries = Directory.EnumerateFileSystemEntries(path);

            foreach (var entry in entries)
            {
                //File.Delete(entry);
                fileSize += entry.Length;
                fileCount++;
            }

            return new Aggregate(){DirectoryCount = directoryCount, FileSize = fileSize, FileCount = fileCount};
        }
    }
}
