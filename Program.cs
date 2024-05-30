using System.Text.Json;
using System.Text;

namespace Cinderella;

class Program
{
    static List<Character> Characters = new List<Character>();
    static int lastId = 0;
    static void Main()
    {
        Console.Title = "Казка - Попелюшка";
        Console.InputEncoding = Encoding.GetEncoding("UTF-8");
        Console.OutputEncoding = Encoding.GetEncoding("UTF-8");
        LoadData();
        while (true)
        {
            AddTitle();
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\t\t Головне меню ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("┌────────────┬─────────────────────────────┐");
            Console.WriteLine("│    Дiї     │          Пояснення          │");
            Console.WriteLine("├───┬────────┼─────────────────────────────┤");
            Console.WriteLine("│ 1 │ CREATE │ Додання персонажу           │");
            Console.WriteLine("│ 2 │ READ   │ Показ всіх персонажів       │");
            Console.WriteLine("│ 3 │ UPDATE │ Оновлення персонажу         │");
            Console.WriteLine("│ 4 │ DELETE │ Видалення персонажу         │");
            Console.WriteLine("│ 5 │ SEARCH │ Пошук персонажу             │");
            Console.WriteLine("│ 6 │ EXIT   │ Вихід                       │");
            Console.WriteLine("└───┴────────┴─────────────────────────────┘");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(" ОБЕРІТЬ НОМЕР ДІЇ: ");
            Console.ForegroundColor = ConsoleColor.White;
            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1": AddCharacter(); break;
                case "2": ShowAllCharacters(); break;
                case "3": UpdateCharacter(); break;
                case "4": DeleteCharacter(); break;
                case "5": SearchCharacters(); break;
                case "6": Environment.Exit(0); break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n Неправильний вибір. Спробуйте ще раз...");
                    Thread.Sleep(1500);
                    Console.Clear();
                    break;
            }
        }
}

static void LoadData()
{
    if (File.Exists("data.json"))
    {
        string json = File.ReadAllText("data.json");
        Characters = JsonSerializer.Deserialize<List<Character>>(json);

        foreach (var Character in Characters)
        {
            if (Character.Id > lastId)
            {
                lastId = Character.Id;
            }
        }
    }
}

static void UpdateIds()
{
    int newId = 1;
    foreach (var Character in Characters)
    {
        Character.Id = newId;
        newId++;
    }
    lastId = newId - 1;
}
static void SaveData()
{
    string json = JsonSerializer.Serialize(Characters);
    File.WriteAllText("data.json", json);
}
static void AddCharacter()
{
    AddTitle();
    Console.BackgroundColor = ConsoleColor.Blue;
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("\t\t Додання персонажу ");
    Console.BackgroundColor = ConsoleColor.Black;

    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.Write("\nВведіть ім'я персонажу: ");
    Console.ForegroundColor = ConsoleColor.White;
    string? name = Console.ReadLine();

    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.Write("\nДодайте опис персонажу: ");
    Console.ForegroundColor = ConsoleColor.White;
    string? description = Console.ReadLine();

    lastId++;
    Characters.Add(new Character { Id = lastId, Name = name, Description = description });
    SaveData();
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("\nПерсонажа успішно додано!");
    Console.WriteLine("\nДля повернення на головне меню натисніть \nбудь-яку клавішу...");
    Console.ReadKey();
    Console.Clear();
}
static void AddTitle()
{
    Console.Clear();
    Console.BackgroundColor = ConsoleColor.DarkYellow;
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("                   Казка                   ");
    Console.WriteLine("                 ПОПЕЛЮШКА                 ");
    Console.WriteLine();
}
static void ShowAllCharacters()
{
    AddTitle();
    Console.BackgroundColor = ConsoleColor.Blue;
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("\t Показ всіх персонажів ");
    Console.BackgroundColor = ConsoleColor.Black;
    Console.WriteLine();

    if (Characters.Count == 0) Console.WriteLine("Персонажі відсутні!");
    else
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("Перелік персонажів:");
        foreach (var Character in Characters)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{Character.Id}. {Character.Name} - {Character.Description}.\n");
        }
    }
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("\nДля повернення на головне меню натисніть \nбудь-яку клавішу...");
    Console.ReadKey();
    Console.Clear();
}
static void UpdateCharacter()
{
    AddTitle();
    Console.BackgroundColor = ConsoleColor.Blue;
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("\t\t Оновлення персонажу ");
    Console.BackgroundColor = ConsoleColor.Black;
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.Write("Введіть ID персонажу для оновлення: ");
    Console.ForegroundColor = ConsoleColor.White;
    int id = Convert.ToInt32(Console.ReadLine());
    Character? CharacterToUpdate = Characters.Find(Character => Character.Id == id);
    Console.ForegroundColor = ConsoleColor.Red;
    if (CharacterToUpdate == null) Console.WriteLine("\nПерсонажа не знайдено!");
    else
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write("\nВведіть ім'я персонажу: ");
        Console.ForegroundColor = ConsoleColor.White;
        CharacterToUpdate.Name = Console.ReadLine();

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write("\nДодайте опис персонажу: ");
        Console.ForegroundColor = ConsoleColor.White;
        CharacterToUpdate.Description = Console.ReadLine();

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nІнформацію про персонажа успішно оновлено!");
        SaveData();
    }
    Console.WriteLine("\nДля повернення на головне меню натисніть \nбудь-яку клавішу...");
    Console.ReadKey();
    Console.Clear();
}
static void DeleteCharacter()
{
    AddTitle();
    Console.BackgroundColor = ConsoleColor.Blue;
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("\t\t Видалення персонажа ");
    Console.BackgroundColor = ConsoleColor.Black;
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.Write("Введіть ID персонажа для видалення: ");
    Console.ForegroundColor = ConsoleColor.White;
    int id = Convert.ToInt32(Console.ReadLine());
    Character? CharacterToDelete = Characters.Find(Character => Character.Id == id);
    Console.ForegroundColor = ConsoleColor.Red;
    if (CharacterToDelete == null) Console.WriteLine("\nПерсонажа не знайдено!");
    else
    {
        Characters.Remove(CharacterToDelete);
        Console.WriteLine("\nПерсонажа успішно видалено!");
        UpdateIds();
        SaveData();
    }
    Console.WriteLine("\nДля повернення на головне меню натисніть \nбудь-яку клавішу...");
    Console.ReadKey();
    Console.Clear();
}
static void SearchCharacters()
{
    AddTitle();
    Console.BackgroundColor = ConsoleColor.Blue;
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("\t\t Пошук персонажа ");
    Console.BackgroundColor = ConsoleColor.Black;
    Console.WriteLine();

    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.Write("Введіть ім'я персонажа: ");
    Console.ForegroundColor = ConsoleColor.White;
    string? searchTerm = Console.ReadLine()?.Trim();
    if (!string.IsNullOrWhiteSpace(searchTerm))
    {
        bool foundResults = false;
        foreach (var Character in Characters)
        {
            if (Character.Name != null && Character.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            {
                if (!foundResults)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\nРезультати пошуку:");
                    foundResults = true; // Установка флага в true, если найден хотя бы один результат
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{Character.Id}. {Character.Name} – {Character.Description}.\n");
            }
        }
        if (!foundResults)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nРезультати не знайдено.\n");
        }
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nПорожній запит! Введіть ім'я персонажа для пошуку.");
    }
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Для повернення на головне меню натисніть \nбудь-яку клавішу...");
    Console.ReadKey();
    Console.Clear();
}
}
class Character
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}