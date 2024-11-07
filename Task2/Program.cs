namespace Task2;

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
        string relativePath = "../Text1.txt"; // Указываем путь относительно директории Task1 или Task2


        // Получаем полный путь к файлу
        string filePath = GetFilePath(relativePath);

        // Если файл найден, запускаем метод для проверки производительности
        Console.WriteLine($"Файл найден по пути: {filePath}");

        // Читаем и анализируем файл
        string text = File.ReadAllText(filePath);
        FindTopWords(text);
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

        fullPath = Path.GetFullPath(fullPath);


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
    /// Метод для поиска 10 наиболее часто встречающихся слов в тексте
    /// </summary>
    /// <param name="text">Текст для анализа</param>
    static void FindTopWords(string text)
    {
        // Убираем все знаки пунктуации
        var noPunctuationText = new string(text.Where(c => !char.IsPunctuation(c)).ToArray());

        // Разбиваем текст
        var words = noPunctuationText.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                                        .Select(word => word.ToLower()) // Приводим все слова к нижнему регистру
                                        .ToList();

        // Считаем частоту встречаемости слов
        var wordFrequency = words.GroupBy(word => word)
                                    .Select(group => new { Word = group.Key, Count = group.Count() })
                                    .OrderByDescending(word => word.Count)
                                    .Take(10)
                                    .ToList();

        // Выводим 10 самых частых слов
        Console.WriteLine("Топ 10 самых часто встречающихся слов:");
        Console.WriteLine();
        foreach (var word in wordFrequency)
        {
            Console.WriteLine($"{word.Word}: {word.Count}");
        }
    }
}