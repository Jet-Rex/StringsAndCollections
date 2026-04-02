using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

namespace StringsAndCollections {
  internal class Program {
    static void Main(string[] args) {
      string directoryPath, filePath, fileContent, phonePattern, phoneReplacement;
      string[] txtFiles;
      List<string> wrongWords;
      var mistakesList = new Dictionary<string, List<string>> {
        ["привет"] = new List<string> { "првиет", "пирвет", "превед" },
        ["хорошо"] = new List<string> { "хоршо", "харашо", "хорощо" },
        ["пока"]   = new List<string> { "поко", "пка", "покка" }
      };
      phonePattern = @"\(012\)\s*345-67-89";
      phoneReplacement = "+380 12 345 67 89";

      Console.Write("Введите путь к папке с файлами: ");
      directoryPath = Console.ReadLine();
      //directoryPath = @"C:\Texts";
      txtFiles = Directory.GetFiles(directoryPath, "*.txt");
      for (int index = 0; index < txtFiles.Length; index++)
      {
        filePath = txtFiles[index];
        fileContent = File.ReadAllText(filePath);
        foreach (var pair in mistakesList) {
          string correctWord = pair.Key;
          wrongWords = pair.Value;
          foreach (string wrongWord in wrongWords) {
            fileContent = fileContent.Replace(wrongWord, correctWord);
          }
        }
        fileContent = Regex.Replace(fileContent, phonePattern, phoneReplacement);

        File.WriteAllText(filePath, fileContent);
      }
      Console.WriteLine($"Обработано файлов: {txtFiles.Length}");
    }
  }
}