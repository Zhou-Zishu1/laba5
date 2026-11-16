namespace laba5
{
    internal class Program
    {
        private static void Main()
        {
            ISaladBuilder builder = new VegetableSaladBuilder();
            var menu = new SaladConsoleMenu(builder);

            menu.Run();
        }
    }
}
