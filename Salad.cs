using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace laba5
{
    public class WeightComparer : IComparer<IIngredient>
    {
        public int Compare(IIngredient? x, IIngredient? y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (x is null) return -1;
            if (y is null) return 1;

            return x.Weight.CompareTo(y.Weight);
        }
    }

    public class Salad : IEnumerable<IIngredient>
    {
        public string Name { get; set; }
        private readonly List<IIngredient> _ingredients = new List<IIngredient>();

        public Salad(string name = "Салат")
        {
            Name = name;
        }

        public void AddIngredient(IIngredient ingredient)
        {
            ArgumentNullException.ThrowIfNull(ingredient);
            _ingredients.Add(ingredient);
        }

        public double CalculateTotalCalories()
        {
            return _ingredients.Sum(ing => ing.TotalCalories);
        }

        public void SortIngredients(IComparer<IIngredient>? comparer = null)
        {
            _ingredients.Sort(comparer);
        }

        public List<Vegetable> FindVegetablesByCalorieRange(double min, double max)
        {
            return _ingredients
                .OfType<Vegetable>()
                .Where(veg => veg.CaloriesPer100g >= min && veg.CaloriesPer100g <= max)
                .ToList();
        }

        public IEnumerator<IIngredient> GetEnumerator() => _ingredients.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
