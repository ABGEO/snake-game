namespace SnakeGame
{
    /**
     * Class for definition fruits.
     */
    public class Fruit
    {
        /**
         * Fruit ID.
         */
        public int Id { get; set; }

        /**
         * Fruit weight for adding to score.
         */
        public int Weight { get; set; }

        /**
         * Fruit symbol.
         */
        public char Symbol { get; set; }

        /**
         * Fruit X coordinate.
         */
        public int X { get; set; }

        /**
         * Fruit Y coordinate.
         */
        public int Y { get; set; }
        
        /**
         * Current fruit print status.
         */
        public bool IsPrinted { get; set; }
    }
}