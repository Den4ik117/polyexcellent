namespace Polyexcellent
{
    internal static class Program
    {
        private static void Main()
        {
            var monopoly = new Game();
            monopoly.Create();
            monopoly.Start();
        }
    }
}