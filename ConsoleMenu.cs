using System;
using System.Collections.Generic;

namespace laba5
{
    public class SaladConsoleMenu
    {
        private readonly Salad _salad;

        public SaladConsoleMenu(ISaladBuilder builder)
        {
            _salad = builder
            .SetName("Авторский салат")
            .Build();
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("\n=== МЕНЮ ШЕФ-ПОВАРА ===");
                Console.WriteLine("1. Показать состав и калорийность салата");
                Console.WriteLine("2. Добавить ингредиент");
                Console.WriteLine("3. Сортировать по весу");
                Console.WriteLine("4. Сортировать по калорийности (на 100г)");
                Console.WriteLine("5. Найти овощи по диапазону калорийности");
                Console.WriteLine("0. Выход");
                Console.Write("Выбор: ");

                string? choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        PrintSalad();
                        break;
                    case "2":
                        AddIngredientFromConsole();
                        break;
                    case "3":
                        _salad.SortIngredients(new WeightComparer());
                        Console.WriteLine("Отсортировано по весу.");
                        PrintSalad();
                        break;
                    case "4":
                        _salad.SortIngredients();
                        Console.WriteLine("Отсортировано по калорийности.");
                        PrintSalad();
                        break;
                    case "5":
                        SearchVegetablesFromConsole();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный пункт меню.");
                        break;
                }
            }
        }

        private void PrintSalad()
        {
            Console.WriteLine($"Салат «{_salad.Name}» | Всего: {_salad.CalculateTotalCalories():F1} ккал");
    
            bool isEmpty = true;
            foreach (var ing in _salad)
            {
                Console.WriteLine($"  - {ing}");
                isEmpty = false;
            }

            if (isEmpty)
            {
                Console.WriteLine("  Салат пока пуст. Выберите пункт 2, чтобы добавить ингредиенты.");
            }
        }

        private void AddIngredientFromConsole()
        {
            Console.WriteLine("Выберите продукт: 1 - Помидор, 2 - Огурец, 3 - Лук, 4 - Оливковое масло");
            Console.Write("Тип: ");
            string? type = Console.ReadLine();

            Console.Write("Вес (г): ");
            if (!double.TryParse(Console.ReadLine(), out double weight) || weight <= 0)
            {
                Console.WriteLine("Ошибка: введите корректный положительный вес.");
                return;
            }

            IIngredient? item = type switch
            {
                "1" => new Tomato(weight, "Розовый"),
                "2" => new Cucumber(weight, true),
                "3" => new Onion(weight, true),
                "4" => new OliveOil(weight),
                _ => null
            };

            if (item is null)
            {
                Console.WriteLine("Неизвестный тип продукта.");
                return;
            }

            _salad.AddIngredient(item);
            Console.WriteLine($"Добавлено: {item.Name} ({weight} г).");
        }

        private void SearchVegetablesFromConsole()
        {
            Console.Write("Мин. калорийность (ккал/100г): ");
            bool isMinValid = double.TryParse(Console.ReadLine(), out double minCal);

            Console.Write("Макс. калорийность (ккал/100г): ");
            bool isMaxValid = double.TryParse(Console.ReadLine(), out double maxCal);

            if (!isMinValid || !isMaxValid || minCal > maxCal)
            {
                Console.WriteLine("Ошибка: проверьте границы диапазона.");
                return;
            }

            List<Vegetable> found = _salad.FindVegetablesByCalorieRange(minCal, maxCal);
            Console.WriteLine($"Найденные овощи ({minCal}–{maxCal} ккал/100г):");

            if (found.Count == 0)
            {
                Console.WriteLine("  Ничего не найдено.");
                return;
            }

            foreach (var veg in found)
            {
                Console.WriteLine($"  - {veg}");
            }
        }
    }
}
