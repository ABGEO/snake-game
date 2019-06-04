namespace SnakeGame
{
    public class Snake
    {
        /**
         * Snake direction.
         */
        public string Direction { get; set; }

        /**
         * Snake last direction.
         */
        public string PreDirection { get; set; }

        /**
         * Snake length (Number of tail elements).
         */
        public int NTail { get; set; }

        /**
         * Snake head X coordinate.
         */
        public int HeadX { get; set; }

        /**
         * Snake head Y coordinate.
         */
        public int HeadY { get; set; }

        /**
         * Tail on X.
         */
        public int[] TailX { get; set; }

        /**
         * Tail on Y.
         */
        public int[] TailY { get; set; }

        public bool IsHorizontal()
        {
            return (Direction == "LEFT" && PreDirection != "UP") &&
                   (Direction == "LEFT" && PreDirection != "DOWN") ||
                   (Direction == "RIGHT" && PreDirection != "UP") &&
                   (Direction == "RIGHT" && PreDirection != "DOWN");
        }

        public bool IsVertical()
        {
            return (Direction == "UP" && PreDirection != "LEFT") &&
                   (Direction == "DOWN" && PreDirection != "LEFT") &&
                   (Direction == "UP" && PreDirection != "RIGHT");
        }
    }
}