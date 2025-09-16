using System;
using System.Text.RegularExpressions;

namespace PracW1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Варіанти:");
                Console.WriteLine("1 - Перевірка логіна");
                Console.WriteLine("2 - Фільтр визначених виразів (заміна слова Дурень)");
                Console.WriteLine("0 - Вийти");
                Console.Write("Оберіть пункт: ");
                string choice = Console.ReadLine();

                if (choice == "0") break;
                if (choice == "1")
                    RunLoginCheck();
                else if (choice == "2")
                    RunFilter();
                else
                {
                    Console.WriteLine("Неправильний вибір. Натисніть Enter і спробуйте ще раз...");
                    Console.ReadLine();
                }
            }
        }

        static void RunLoginCheck()
        {
            Console.Clear();
            Console.WriteLine("Перевірка логіна (2-10 символів, тільки літери і цифри, перший символ не цифра)");
            Console.Write("Введіть логін: ");
            string login = Console.ReadLine();

            bool ok = IsValidLogin(login);
            Console.WriteLine(ok ? "Логін коректний." : "Логін некоректний.");
            Console.WriteLine("\nПриклади перевірки:");
            string[] samples = { "Artem", "a1", "1abc", "user_name", "1a" };
            foreach (var s in samples)
            {
                Console.WriteLine($"{s} -> {IsValidLogin(s)}");
            }

            Console.WriteLine("\nНатисніть Enter, щоб повернутись у меню...");
            Console.ReadLine();
        }

        static bool IsValidLogin(string login)
        {
            if (string.IsNullOrEmpty(login)) return false;
            Regex rx = new Regex(@"^(?!\d)[A-Za-z0-9]{2,10}$");
            return rx.IsMatch(login);
        }

        static void RunFilter()
        {
            Console.Clear();
            Console.WriteLine("Фільтр виразів. Приклад заміни форми слова 'дурень' (різні закінчення) на '[заміна]'.");
            Console.WriteLine("Введіть рядок (або натисніть Enter для прикладу):");
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                input = "Той дурень зробив помилку. Я не люблю дурняка і дурна поведінка - це погано.";
            }

            string result = FilterText(input);
            Console.WriteLine("\nРезультат:");
            Console.WriteLine(result);

            Console.WriteLine("\nЩе приклади:");
            string[] examples = {            
                "Такий дурняк робить шкоду.",
                "Не називай його дурaком!"
            };
            foreach (var ex in examples)
            {
                Console.WriteLine($"'{ex}' -> '{FilterText(ex)}'");
            }

            Console.WriteLine("\nНатисніть Enter, щоб повернутись у меню...");
            Console.ReadLine();
        }

        static string FilterText(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;

            Regex rxCyrillic = new Regex(@"\b(?i)(дур[а-яіїє’']*)\b", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
            string temp = rxCyrillic.Replace(text, "[заміна]");

            Regex rxLatin = new Regex(@"\b(?i)(durak[a-z']*)\b", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
            temp = rxLatin.Replace(temp, "[zamina]");

            return temp;
        }
    }
}
