using System;
using System.IO;
using System.Text;
using static System.Net.WebRequestMethods;

namespace AK
{
    internal class Program
    {
        static int Main(string[] args)
        {

            bool check = false;
            while (check == false)
            {
                Console.Write("Enter the path of folder:");
                string Input = Console.ReadLine();
                try
                {
                    if (Input != "")
                    {
                        DirectoryInfo Path = new DirectoryInfo(Input);
                        FileInfo[] all_files = Path.GetFiles();
                        bool checkEx = false;
                        while (checkEx == false)
                        {
                            if (all_files.Length != 0)
                            {
                                Console.Write("Enter files extension (for example: .txt):");
                                string extension = Console.ReadLine();
                                extension = extension.Replace(" ", "");
                                try
                                {
                                    if (extension != "")
                                    {
                                        if (all_files != null)
                                        {
                                            IEnumerable<FileInfo> files =
                                            from file in all_files
                                            where file.Extension == extension
                                            orderby file.Name
                                            select file;
                                            if (files.Count() != 0)
                                            {
                                                foreach (FileInfo file in files)
                                                {
                                                    FileAttributes attrib = file.Attributes;
                                                    if (attrib == FileAttributes.ReadOnly)
                                                    {
                                                        Console.WriteLine("Exist read-only file: " + file.Name);
                                                        file.IsReadOnly = false;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Exist not read-only file: " + file.Name);
                                                        file.Attributes = FileAttributes.ReadOnly;
                                                    }
                                                }
                                                Console.WriteLine("Attributes was changed");
                                                return 0;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Files whith entered extension not found");
                                                return 1;
                                            }
                                            check = true;
                                            checkEx = true;
                                        }
                                    }
                                }
                                catch (ArgumentException)
                                {
                                    Console.WriteLine("Files extension entered incorrectly");
                                    Console.WriteLine("Try again");
                                }

                            }
                            else
                            {
                                Console.WriteLine("No files in the folder");
                                checkEx = true;
                                return -1;
                            }
                        }

                    }

                }
                catch (ArgumentException)
                {
                    Console.Clear();
                }
                catch (DirectoryNotFoundException)
                {
                    Console.WriteLine("Enter path not found");
                    Console.WriteLine("Try again");
                }
            }
            return 0;
        }
    }
}