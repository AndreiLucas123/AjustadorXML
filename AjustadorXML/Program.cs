using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AjustadorXML
{
    class Program
    {
#if DEBUG
        const string FolderPath = "ExampleUse";
        const string ToPath = "ExampleUse\\Antigos";
#else
        static string FolderPath = Directory.GetCurrentDirectory();
        static string ToPath = Path.Combine(Directory.GetCurrentDirectory(), "Antigos");
#endif
        static int moveds = 0;
        static bool dirChecked = false;

        static void Main(string[] args)
        {
            Console.WriteLine($"Preparando para mover arquivos");
            Console.WriteLine(ToPath);
            // Se o diretorio existe
            if (Directory.Exists(FolderPath))
            {
                // Pega os arquivos
                var files = Directory.GetFiles(FolderPath);
                foreach (var file in files)
                {
                    // Se o arquivo é um xml
                    if (Path.GetExtension(file) == ".xml")
                    {
                        var lastModif = File.GetLastWriteTime(file);
                        var timeStamp = DateTime.Now - lastModif;
                        if (timeStamp.Days > 31)
                        {
                            if (!dirChecked)
                            {
                                dirChecked = true;
                                Directory.CreateDirectory(ToPath);
                            }
                            var dest = Path.Combine(ToPath, Path.GetFileName(file));

                            moveds++;
                            Console.WriteLine($"Moving {moveds}");
                            File.Move(file, dest);
                        }
                    }
                }
                Console.WriteLine($"Arquivos movidos {moveds}");
            }
            else
            {
                Console.WriteLine("Diretorio buscado não existe");
            }
            Console.ReadKey();
        }
    }
}
