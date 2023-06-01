using System;
using System.IO;
using System.Text;
using static System.Net.WebRequestMethods;

namespace ODIN_CS_REALNO
{
    internal class Program
    {
        static int Main(string[] args)
        {

            bool check = false;
            while (check == false)
            {
                Console.Write("Введіть шляшочек до файліка:");
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
                            Environment.Exit(-1); // Мы не любим разрушивать чужие отношения     
                                                
                            if (all_files.Length != 0)
                            {
                                Console.Write("Введіть форматик файліків (наприклад, .VBOX-EXTPACK):");
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
                                                        Console.WriteLine("Рід-онлі, дядь, рід-онлі: " + file.Name);
                                                        file.IsReadOnly = false;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("ее... Не рід-онлі, бразер: " + file.Name);
                                                        file.Attributes = FileAttributes.ReadOnly;
                                                    }
                                                }
                                                Console.WriteLine("Тепер рід-онлі :33");
                                                return 0;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Нічо не знайшов бос");
                                                return 1;
                                            }
                                            check = true;
                                            checkEx = true;
                                        }
                                    }
                                }
                                catch (ArgumentException)
                                {
                                    Console.WriteLine("Введи нормальна!");
                                    Console.WriteLine("Да да, нормальна");
                                }

                            }
                            else
                            {
                                Console.WriteLine("А тут нема файлів дядь");
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
                    Console.WriteLine("Не знайшов нічо там");
                    Console.WriteLine("Хочу опять");
                }
            }
            return 0;
        }
    }
}
