using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Task1
{
    class Program
    {
        /// <summary>
        /// Точка входа в программу
        /// Поиск файла по относительному пути и сравнение производительности List<T> и LinkedList<T> при вставке
        /// </summary>
        /// <param name="args">Аргументы командной строки</param>
        /// <returns>Код возврата</returns>
        /// <remarks>Для запуска программы необходимо указать относительный путь к файлу</remarks>
        static void Main(string[] args)
        {
            string relativePath = "Text1.txt"; // Указываем путь относительно директории проекта

            // Путь к файлу с проверкой
            string filePath = GetFilePath(relativePath);

            // Если файл найден, запускаем метод для проверки производительности
            if (filePath != null)
            {
                Console.WriteLine($"Файл найден по пути: {filePath}");
                ComparePerformance(filePath);
            }
        }

        /// <summary>
        /// Метод для поиска файла с циклом запроса пути
        /// Если файл не найден, запрашивает путь к файлу заново
        /// </summary>
        /// <param name="relativePath">Относительный путь к файлу</param>
        /// <returns>Полный путь к файлу</returns>
        static string GetFilePath(string relativePath)
        {
            string projectDirectory = Directory.GetCurrentDirectory();
            string fullPath = Path.Combine(projectDirectory, relativePath);

            while (!File.Exists(fullPath))
            {
                Console.WriteLine($"Файл не найден по пути: {fullPath}");
                Console.Write("Пожалуйста, введите относительный путь к файлу заново: ");
                relativePath = Console.ReadLine() ?? string.Empty;

                fullPath = Path.Combine(projectDirectory, relativePath);
            }

            return fullPath;
        }

        /// <summary>
        /// Метод для сравнения производительности List<T> и LinkedList<T> при вставке
        /// </summary>
        /// <param name="filePath"></param>
        static void ComparePerformance(string filePath)
        {

            string[] words = File.ReadAllText(filePath).Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            // Измерение времени вставки в List<T>
            var list = new List<string>();
            Stopwatch listStopwatch = Stopwatch.StartNew();
            foreach (var word in words)
            {
                list.Add(word);
            }
            listStopwatch.Stop();
            Console.WriteLine($"Время вставки для List<T>: {listStopwatch.ElapsedMilliseconds} мс");

            // Измерение времени вставки в LinkedList<T>
            var linkedList = new LinkedList<string>();
            Stopwatch linkedListStopwatch = Stopwatch.StartNew();
            foreach (var word in words)
            {
                linkedList.AddLast(word);
            }
            linkedListStopwatch.Stop();
            Console.WriteLine($"Время вставки для LinkedList<T>: {linkedListStopwatch.ElapsedMilliseconds} мс");
        }
    }
}
