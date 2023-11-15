using System;

public struct Date
{
    public int Day;
    public int Month;
    public int Year;

    public Date(int day, int month, int year)
    {
        try
        {
            if (day < 1 || month < 1 || month > 12 || year < 1)
                throw new ArgumentException("Неправильно введені дані");

            int maxDay = DateTime.DaysInMonth(year, month);
            if (day > maxDay)
                throw new ArgumentException("У цьому місяці такого дня не існує");

            Day = day;
            Month = month;
            Year = year;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
            throw;
        }
    }

    public Date(int year)
    {
        if (year < 1)
            throw new ArgumentException("Рік повинен бути додатнім числом");

        Day = 1;
        Month = 1;
        Year = year;
    }

    public Date(DateTime dateTime)
    {
        Day = dateTime.Day;
        Month = dateTime.Month;
        Year = dateTime.Year;
    }

    public Date GetFutureDate(int days)
    {
        try
        {
            DateTime currentDate = new DateTime(Year, Month, Day);
            DateTime futureDate = currentDate.AddDays(days);
            return new Date(futureDate.Day, futureDate.Month, futureDate.Year);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
            return this;
        }
    }

    public Date SubtractDays(int days)
    {
        return GetFutureDate(-days);
    }

    public bool IsLeapYear()
    {
        return DateTime.IsLeapYear(Year);
    }

    public int CompareTo(Date other)
    {
        DateTime thisDate = new DateTime(Year, Month, Day);
        DateTime otherDate = new DateTime(other.Year, other.Month, other.Day);
        return thisDate.CompareTo(otherDate);
    }

    public int DaysDifference(Date other)
    {
        DateTime thisDate = new DateTime(Year, Month, Day);
        DateTime otherDate = new DateTime(other.Year, other.Month, other.Day);
        return (int)(thisDate - otherDate).TotalDays;
    }

    public override string ToString()
    {
        return $"{Day}.{Month}.{Year}";
    }
}

class Program
{
    static void Main()
    {
        int choice;
        do
        {
            Console.Clear();
            Console.WriteLine("Виберіть варіант:");
            Console.WriteLine("1) Визначити майбутню дату через задану кількість днів");
            Console.WriteLine("2) Відняти задану кількість днів від дати");
            Console.WriteLine("3) Визначити, чи рік високосний");
            Console.WriteLine("4) Порівняти дати");
            Console.WriteLine("5) Обчислити різницю в днях між двома датами");
            Console.WriteLine("6) Відсортувати дати");
            Console.WriteLine("7) Знайти мінімальну чи максимальну дату");
            Console.WriteLine("0) Вийти");
            Console.Write("Ваш вибір: ");

            if (int.TryParse(Console.ReadLine(), out choice))
            {
                HandleChoice(choice);
                Console.WriteLine("Натисніть будь-яку клавішу для продовження...");
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Невірний ввід, введіть число");
            }
        } while (choice != 0);
    }

    static void HandleChoice(int choice)
    {
        switch (choice)
        {
            case 1:
                Option1();
                break;

            case 2:
                Option2();
                break;

            case 3:
                Option3();
                break;

            case 4:
                Option4();
                break;

            case 5:
                Option5();
                break;

            case 6:
                Option6();
                break;

            case 7:
                Option7();
                break;

            case 0:
                Console.WriteLine("Виконання програми завершено!");
                break;

            default:
                Console.WriteLine("Невірний вибір, введіть число від 0 до 9");
                break;
        }
    }

    static void Option1()
    {
        Console.Clear();
        Console.WriteLine("Введіть дату, від якої потрібно визначити майбутню дату:");
        Date inputDate = ReadDateFromUser();

        Console.Write("Введіть кількість днів: ");
        int days = int.Parse(Console.ReadLine());

        Date futureDate = inputDate.GetFutureDate(days);
        Console.WriteLine($"Майбутня дата: {futureDate}");
    }

    static void Option2()
    {
        Console.Clear();
        Console.Write("Введіть кількість днів: ");
        int subtractDays = int.Parse(Console.ReadLine());
        Console.WriteLine("Введіть дату:");
        Date inputDate2 = ReadDateFromUser();
        Date subtractedDate = inputDate2.SubtractDays(subtractDays);
        Console.WriteLine($"Результат: {subtractedDate}");
    }

    static void Option3()
    {
        Console.Clear();
        Console.WriteLine("Введіть рік:");
        int leapYear = GetValidInput();
        Date leapYearDate = new Date(leapYear);
        bool isLeapYear = leapYearDate.IsLeapYear();
        Console.WriteLine($"{(isLeapYear ? "Рік високосний" : "Рік не високосний")}");
    }

    static void Option4()
    {
        Console.Clear();
        Console.WriteLine("Введіть дві дати:");
        Date compareDate1 = ReadDateFromUser();
        Date compareDate2 = ReadDateFromUser();
        int compareResult = compareDate1.CompareTo(compareDate2);

        if (compareResult > 0)
        {
            Console.WriteLine($"{compareDate1} більше за {compareDate2}");
        }
        else if (compareResult < 0)
        {
            Console.WriteLine($"{compareDate1} менше за {compareDate2}");
        }
        else
        {
            Console.WriteLine($"{compareDate1} і {compareDate2} рівні");
        }
    }


    static void Option5()
    {
        Console.Clear();
        Console.WriteLine("Введіть дві дати:");
        Date diffDate1 = ReadDateFromUser();
        Date diffDate2 = ReadDateFromUser();
        int daysDiff = diffDate1.DaysDifference(diffDate2);
        Console.WriteLine($"Різниця: {daysDiff} днів");
    }

    static void Option6()
    {
        Console.Clear();
        Console.WriteLine("Введіть кількість дат для сортування:");
        int numDates = int.Parse(Console.ReadLine());
        Date[] datesToSort = ReadDatesFromUser(numDates);
        SortDates(datesToSort);
        Console.WriteLine("Відсортовані дати:");
        foreach (var date in datesToSort)
        {
            PrintDate(date);
        }
    }

    static void Option7()
    {
        Console.Clear();
        Console.WriteLine("Введіть кількість дат, щоб знайти мінімальну чи максимальну дату серед них:");
        int numMinMaxDates = int.Parse(Console.ReadLine());
        Date[] minMaxDates = ReadDatesFromUser(numMinMaxDates);
        Date minDate, maxDate;
        FindMinMaxArea(minMaxDates, out minDate, out maxDate);
        Console.WriteLine($"Мінімальна дата: {minDate}");
        Console.WriteLine($"Максимальна дата: {maxDate}");
    }

    static Date[] ReadDatesFromUser(int n)
    {
        Date[] dates = new Date[n];
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"Введіть дату {i + 1}:");
            dates[i] = ReadDateFromUser();
        }
        return dates;
    }

    static Date ReadDateFromUser()
    {
        int day, month, year;

        do
        {
            Console.Write("Введіть день: ");
        } while (!int.TryParse(Console.ReadLine(), out day) || day < 1 || day > 31);

        do
        {
            Console.Write("Введіть місяць: ");
        } while (!int.TryParse(Console.ReadLine(), out month) || month < 1 || month > 12);

        do
        {
            Console.Write("Введіть рік: ");
        } while (!int.TryParse(Console.ReadLine(), out year) || year < 1);

        return new Date(day, month, year);
    }

    static void SortDates(Date[] dates)
    {
        Array.Sort(dates, (d1, d2) => d1.CompareTo(d2));
    }

    static void FindMinMaxArea(Date[] dates, out Date minDate, out Date maxDate)
    {
        minDate = dates[0];
        maxDate = dates[0];

        foreach (var date in dates)
        {
            if (date.CompareTo(minDate) < 0)
                minDate = date;

            if (date.CompareTo(maxDate) > 0)
                maxDate = date;
        }
    }

    static int GetValidInput()
    {
        int value;
        do
        {
            Console.Write("Введіть число: ");
        } while (!int.TryParse(Console.ReadLine(), out value));

        return value;
    }

    static int GetValidInput(string prompt)
    {
        int value;
        do
        {
            Console.Write(prompt);
        } while (!int.TryParse(Console.ReadLine(), out value));

        return value;
    }

    static void PrintDate(Date date)
    {
        Console.WriteLine($"Дата: {date}");
    }
}
