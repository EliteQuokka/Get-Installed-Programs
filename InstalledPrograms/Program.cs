using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using Microsoft.Win32;

namespace InstalledPrograms
{
    class Program
    {

        public static List<string> programs { get; set; }
        public static int counter { get; set; }
        public static string programTrim { get; set; }


        static void Main(string[] args)
        {
            Configurations();
            Console.WriteLine("Programms: ");
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("-");
            }
            Console.Write("\n");
            GetPrograms();
            Console.ReadKey();
        }

        private static void Configurations()
        {
            Console.Title ="Installed programs";
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
        }
        private static void GetPrograms()
        {
            string displayName;
            RegistryKey key;
            programs = new List<string>();

            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall");
            foreach (String keyName in key.GetSubKeyNames())
            {
                RegistryKey subkey = key.OpenSubKey(keyName);
                displayName = subkey.GetValue("DisplayName") as string;
                if (displayName != null)
                {
                    programs.Add(displayName);
                }
            }

            programs.Sort();
            Filter();

        }
        private static void Filter()
        {
            do
            {
                counter = 0;

                try
                {
                    foreach (string program in programs)
                    {
                        if (program.StartsWith(" "))
                        {
                            programTrim = program.Substring(1);
                            Remover();
                        }
                        counter++;
                    }
                }
                catch
                {

                }
            } while (counter < programs.Count);

            foreach (string program in programs)
            {
                Console.WriteLine(program);
            }
        }
        private static void Remover()
        {
            programs.RemoveAt(counter);
            programs.Add(programTrim);
            programs.Sort();
        }
    }
}
