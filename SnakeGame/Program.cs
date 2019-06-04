
namespace SnakeGame
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            // Create new game object.
            var game = new Game();

            // Setup game properties.
            var snake = Game.Setup();
            
            // Show introduction banner
            game.ShowBanner();

            // Start game.
            while (true)
            {
                game.Update(snake);
            }
        }
    }
    
}