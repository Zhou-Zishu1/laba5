namespace laba5
{
    public interface ISaladBuilder
    {
        ISaladBuilder SetName(string name);
        ISaladBuilder AddIngredient(IIngredient ingredient);
        Salad Build();
    }

    public class VegetableSaladBuilder : ISaladBuilder
    {
        private Salad _salad = new Salad();

        public ISaladBuilder SetName(string name)
        {
            _salad.Name = name;
            return this;
        }

        public ISaladBuilder AddIngredient(IIngredient ingredient)
        {
            _salad.AddIngredient(ingredient);
            return this;
        }

        public Salad Build()
        {
            Salad result = _salad;
            _salad = new Salad();
            return result;
        }
    }
}
