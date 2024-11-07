using System.Diagnostics;

namespace task1;

class Program
{
    /// <summary>
    /// Точка входа в программу
    /// Программа считывает путь к текстовому файлу, считывает его содержимое и разбивает на слова.
    /// Затем измеряет время вставки слов в List<T> и LinkedList<T>.
    /// Выводит результаты в консоль.
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        string filePath;
        
        /// <summary>
        /// Считывание пути к файлу
        /// Пока не будет введен существующий путь к файлу, программа будет запрашивать его у пользователя.
        /// </summary>
        while (true)
        {
            Console.WriteLine("Введите полный путь к текстовому файлу, пример: '/home/user/Загрузки/Text1.txt'");
            filePath = Console.ReadLine();
            
            // Проверка, существует ли файл
            if (File.Exists(filePath))
            {
                break; // Прерываем цикл, если файл найден
            }
            else
            {
                Console.WriteLine($"Файл не найден: {filePath}. Пожалуйста, попробуйте снова.");
            }
        }

        // Считываем содержимое файла и разбиваем его на слова
        string[] words = File.ReadAllText(filePath).Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        /// <summary>
        /// Измерение времени вставки в List<T>
        /// </summary>
        /// <typeparam name="T">Тип элементов коллекции</typeparam>
        /// <param name="words">Массив слов, которые будут вставлены в коллекцию</param>
        /// <returns>Время вставки слов в List<T></returns>
        var list = new List<string>();
        Stopwatch listStopwatch = Stopwatch.StartNew();
        foreach (var word in words)
        {
            list.Add(word);
        }
        listStopwatch.Stop();
        Console.WriteLine($"Время вставки для List<T>: {listStopwatch.ElapsedMilliseconds} мс");

        // Измерение времени вставки в LinkedList<T>
        /// <summary>
        /// Измерение времени вставки в LinkedList<T>
        /// </summary>
        /// <typeparam name="T">Тип элементов коллекции</typeparam>
        /// <param name="words">Массив слов, которые будут вставлены в коллекцию</param>
        /// <returns>Время вставки слов в LinkedList<T></returns>
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