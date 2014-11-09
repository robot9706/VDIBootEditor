using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace VDIBootEditor
{
    static class Program
    {
        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(int dwProcessId);
        private const int ATTACH_PARENT_PROCESS = -1;

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern int FreeConsole();

        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                AttachConsole(ATTACH_PARENT_PROCESS);

                Console.WriteLine("VDIBootEditor v1.0 by Robot9706 - 2014");
                Console.WriteLine();
                Console.WriteLine("Usage: VDIBootEditor.exe read/write <vdiFile> <rawFile>");

                if (args.Length == 3)
                {
                    string vdiFile = args[1];
                    if (File.Exists(vdiFile))
                    {
                        string rawFile = args[2];
                        if (args[0].ToLower() == "write")
                        {
                            if (File.Exists(rawFile))
                            {
                                VDI vdi = new VDI();
                                VDIError err = vdi.OpenFile(vdiFile);
                                if (err == VDIError.NoError)
                                {
                                    err = vdi.WriteMBR(rawFile);
                                    if (err == VDIError.NoError)
                                    {
                                        Console.WriteLine("Done!");
                                    }
                                    else
                                    {
                                        if (vdi.Exception == null)
                                        {
                                            Console.WriteLine("Unable to write MBR to VDI: " + err.ToString());
                                        }
                                        else
                                        {
                                            Console.WriteLine("Unable to write MBR to VDI: " + vdi.Exception.Message);
                                        }
                                    }
                                }
                                else
                                {
                                    if (vdi.Exception == null)
                                    {
                                        Console.WriteLine("Unable to open VDI: " + err.ToString());
                                    }
                                    else
                                    {
                                        Console.WriteLine("Unable to open VDI: " + vdi.Exception.Message);
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("File not found: " + rawFile);
                            }
                        }
                        else if (args[0].ToLower() == "read")
                        {
                            VDI vdi = new VDI();
                            VDIError err = vdi.OpenFile(vdiFile);
                            if (err == VDIError.NoError)
                            {
                                if (vdi.ReadMBR(rawFile))
                                {
                                    Console.WriteLine("Done!");
                                }
                                else
                                {
                                    Console.WriteLine("Unable to read MBR from VDI: " + vdi.Exception.Message);
                                }
                            }
                            else
                            {
                                if (vdi.Exception == null)
                                {
                                    Console.WriteLine("Unable to open VDI: " + err.ToString());
                                }
                                else
                                {
                                    Console.WriteLine("Unable to open VDI: " + vdi.Exception.Message);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Unknown command.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("File not found: " + vdiFile);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid command.");
                }

                FreeConsole();
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new EditorForm());
            }
        }
    }
}
