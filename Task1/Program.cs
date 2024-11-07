using System.Diagnostics;

namespace Task1;

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
        // Относительный путь к файлу в корне проекта
        string relativePath = "Text1.txt"; // Указываем путь относительно директории проекта

        // Получаем полный путь к файлу
        string filePath = GetFilePath(relativePath);

        // Если файл найден, запускаем метод для проверки производительности
        Console.WriteLine($"Файл найден по пути: {filePath}");
        ComparePerformance(filePath);
    }

    /// <summary>
    /// Метод для поиска файла сначала в относительном пути корня проекта,
    /// если не найден, запрашивает полный путь у пользователя
    /// </summary>
    /// <param name="relativePath">Относительный путь к файлу</param>
    /// <returns>Полный путь к файлу</returns>
    static string GetFilePath(string relativePath)
    {
        // Получаем полный путь на основе текущего каталога и относительного пути
        string projectDirectory = Directory.GetCurrentDirectory();
        string fullPath = Path.Combine(projectDirectory, relativePath);

        // Проверяем, существует ли файл по относительному пути
        if (File.Exists(fullPath))
        {
            return fullPath;
        }

        // Если файл не найден, запрашиваем у пользователя полный путь
        Console.WriteLine($"Файл не найден по пути: {fullPath}");
        Console.WriteLine("Введите полный путь к файлу: ");
        
        // Цикл запроса полного пути у пользователя
        while (true)
        {
            fullPath = Console.ReadLine() ?? string.Empty;

            if (File.Exists(fullPath))
            {
                break;
            }

            Console.WriteLine("Файл не найден, пожалуйста, попробуйте снова.");
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