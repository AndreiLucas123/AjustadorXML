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
#else
        static string FolderPath = Directory.GetCurrentDirectory();
#endif
        static int moveds = 0;


        /// <summary>
        /// Move um arquivo para o seu diretório certo
        /// </summary>
        static void MoveFileCorrectly(string fileName)
        {
            var lastModif = File.GetLastWriteTime(fileName);
            string newDir = "CFe-" + DateTime.Now.Year;
            newDir = Path.Combine(FolderPath, newDir);
            // Ajusta para o ano
            Directory.CreateDirectory(newDir);
            // Ajusta para o mes
            newDir = Path.Combine(newDir, "CFe-" + lastModif.ToString("MMMM"));
            Directory.CreateDirectory(newDir);
            var dest = Path.Combine(newDir, Path.GetFileName(fileName));
            File.Move(fileName, dest);
        }


        /// <summary>
        /// Função principal
        /// </summary>
        static void Main(string[] args)
        {
            Console.WriteLine($"Preparando para mover arquivos");
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
                        MoveFileCorrectly(file);
                        moveds++;
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
