using System;
using System.IO;

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
                Console.WriteLine(path);
                return;
            }

            var directories = Directory.GetDirectories(path);
            foreach (var directory in directories)
            {
               Traverse(directory);
            }
        }
    }
}
