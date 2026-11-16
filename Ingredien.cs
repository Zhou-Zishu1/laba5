using System;

namespace laba5
{
    public interface IIngredient : IComparable<IIngredient>
    {
        string Name { get; }
        double Weight { get; }
        double CaloriesPer100g { get; }
        double TotalCalories { get; }
    }

    public abstract class Ingredient : IIngredient
    {
        public string Name { get; }
        public double Weight { get; }
        public double CaloriesPer100g { get; }

        public double TotalCalories => (Weight * CaloriesPer100g) / 100.0;

        protected Ingredient(string name, double weight, double caloriesPer100g)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название не может быть пустым.", nameof(name));
            if (weight <= 0)
                throw new ArgumentOutOfRangeException(nameof(weight), "Вес должен быть больше нуля.");
            if (caloriesPer100g < 0)
                throw new ArgumentOutOfRangeException(nameof(caloriesPer100g), "Калорийность не может быть отрицательной.");

            Name = name;
            Weight = weight;
            CaloriesPer100g = caloriesPer100g;
        }

        public int CompareTo(IIngredient? other)
        {
            if (other is null) return 1;
            return CaloriesPer100g.CompareTo(other.CaloriesPer100g);
        }

        public override string ToString()
        {
            return $"{Name,-18} | {Weight,5:F0} г | {CaloriesPer100g,5:F1} ккал/100г | {TotalCalories,5:F1} ккал";
        }
    }

    public abstract class Vegetable : Ingredient
    {
        public string CuttingType { get; }

        protected Vegetable(string name, double weight, double caloriesPer100g, string cuttingType = "Кубики")
            : base(name, weight, caloriesPer100g)
        {
            CuttingType = cuttingType;
        }
    }

    public class Tomato : Vegetable
    {
        public string Variety { get; }

        public Tomato(double weight, string variety, string cuttingType = "Дольки")
            : base($"Помидор {variety}", weight, 18.0, cuttingType)
        {
            Variety = variety;
        }
    }

    public class Cucumber : Vegetable
    {
        public bool IsGround { get; }

        public Cucumber(double weight, bool isGround, string cuttingType = "Полукольца")
            : base(isGround ? "Огурец грунтовый" : "Огурец парниковый", weight, 15.0, cuttingType)
        {
            IsGround = isGround;
        }
    }

    public class Onion : Vegetable
    {
        public bool IsSpicy { get; }

        public Onion(double weight, bool isSpicy, string cuttingType = "Кольца")
            : base(isSpicy ? "Лук репчатый" : "Лук сладкий", weight, 41.0, cuttingType)
        {
            IsSpicy = isSpicy;
        }
    }

    public class OliveOil : Ingredient
    {
        public OliveOil(double weight)
            : base("Оливковое масло", weight, 884.0)
        {
        }
    }
}
