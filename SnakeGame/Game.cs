using System;
using System.Collections.Generic;
using System.Threading;

namespace SnakeGame
{
    public class Game
    {
        private static readonly Random Rand = new Random();
        private ConsoleKeyInfo _keypress;

        // Define game area.
        private const int Height = 20;
        private const int Width = 60;

        // Define fruits.
        private static readonly IList<Fruit> Fruits = new List<Fruit>()
        {
            new Fruit {Weight = 10, Symbol = 'F'},
            new Fruit {Weight = 15, Symbol = 'A'},
            new Fruit {Weight = 5, Symbol = 'T'},
            new Fruit {Weight = 20, Symbol = 'S'},
            new Fruit {Weight = 50, Symbol = 'G'}
        };

        // Define new list for fruit coordinates.
        private static readonly Fruit Fruit = new Fruit();

        // Define initial score.
        private int _score;

        private bool _gameOver;

        private bool _reset;
        
        private static int _gameSpeed;

        /**
         * Print game introduction banner
         */
        public void ShowBanner()
        {
            Console.SetWindowSize(Width, Height + 6);

            Console.WriteLine("||===============================================================||");
            Console.WriteLine("||---------------------------------------------------------------||");
            Console.WriteLine("||--------------------------- Welcome to ------------------------||");
            Console.WriteLine("||    ____              _           ____                         ||");
            Console.WriteLine("||   / ___| _ __   __ _| | _____   / ___| __ _ _ __ ___   ___    ||");
            Console.WriteLine("||   \\___ \\| '_ \\ / _` | |/ / _ \\ | |  _ / _` | '_ ` _ \\ / _ \\   ||");
            Console.WriteLine("||    ___) | | | | (_| |   <  __/ | |_| | (_| | | | | | |  __/   ||");
            Console.WriteLine("||   |____/|_| |_|\\__,_|_|\\_\\___|  \\____|\\__,_|_| |_| |_|\\___|   ||");
            Console.WriteLine("||===============================================================||");
            Console.WriteLine("||                                                               ||");
            Console.WriteLine("||                                                               ||");
            Console.WriteLine("||                      PRESS ANY KEY TO PLAY                    ||");
            Console.WriteLine("||                                                               ||");
            Console.WriteLine("||===============================================================||");
            Console.WriteLine();
            Console.WriteLine("          Controller - Use arrow buttons to move the snake         ");
            Console.WriteLine("                     - Press S to pause                            ");
            Console.WriteLine("                     - Press R to reset the game                   ");
            Console.WriteLine("                     - Press ESC to quit the game                  ");
            Console.WriteLine();

            _keypress = Console.ReadKey(true);

            if (_keypress.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
            else
            {
                Console.Clear();
            }
        }

        /**
         * Setup game.
         */
        public static Snake Setup()
        {
            // Place firs random fruit on random place.
            Fruit.Y = Rand.Next(1, Height - 1);
            Fruit.X = Rand.Next(1, Width - 1);
            Fruit.Id = Rand.Next(0, Fruits.Count - 1);

            // Hide cursor.
            Console.CursorVisible = false;

            // Set console color
            Console.ForegroundColor = ConsoleColor.Yellow;

            _gameSpeed = 50;

            // Create new snake object.
            var snake = new Snake
            {
                Direction = "RIGHT",
                NTail = 0,
                HeadX = Rand.Next(1, Width - 1),
                HeadY = Rand.Next(1, Height - 1),
                TailX = new int[100],
                TailY = new int[100]
            };

            return snake;
        }

        /**
         * Read key press and set snake direction.
         */
        private Snake CheckInput(Snake snake)
        {
            var direction = "";
            var preDirection = "";

            while (Console.KeyAvailable)
            {
                _keypress = Console.ReadKey(true);
                
                preDirection = direction;

                switch (_keypress.Key)
                {
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                    case ConsoleKey.S:
                        direction = "STOP";
                        break;
                    case ConsoleKey.LeftArrow:
                        direction = "LEFT";
                        break;
                    case ConsoleKey.RightArrow:
                        direction = "RIGHT";
                        break;
                    case ConsoleKey.UpArrow:
                        direction = "UP";
                        break;
                    case ConsoleKey.DownArrow:
                        direction = "DOWN";
                        break;
                }

                snake.Direction = direction;
                snake.PreDirection = preDirection;
            }

            return snake;
        }

        /**
         * Gama main logic.
         */
        private Snake Logic(Snake snake)
        {
            var preX = snake.TailX[0];
            var preY = snake.TailY[0];
            
            // Move snake.
            if (snake.Direction != "STOP")
            {
                snake.TailX[0] = snake.HeadX;
                snake.TailY[0] = snake.HeadY;

                for (var i = 1; i < snake.NTail; i++)
                {
                    var tempX = snake.TailX[i];
                    var tempY = snake.TailY[i];
                    snake.TailX[i] = preX;
                    snake.TailY[i] = preY;

                    preX = tempX;
                    preY = tempY;
                }
            }

            if (snake.Direction == "RIGHT")
            {
                // Move right.
                snake.HeadX++;
            }
            else if (snake.Direction == "LEFT")
            {
                // Move left.
                snake.HeadX--;
            }
            else if (snake.Direction == "UP")
            {
                // Move forvard.
                snake.HeadY--;
            }
            else if (snake.Direction == "DOWN")
            {
                // Move back.
                snake.HeadY++;
            }
            else if (snake.Direction == "STOP")
            {
                while (true)
                {
                    Console.Clear();
                    
                    Console.CursorLeft = Width / 2 - 6;
                    
                    Console.WriteLine("GAME PAUSED");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("    - Press S to resume the game");
                    Console.WriteLine("    - Press R to reset the game");
                    Console.Write("    - Press ESC to quit the game");

                    _keypress = Console.ReadKey(true);
                    if (_keypress.Key == ConsoleKey.Escape)
                    {
                        // Close game.
                        Environment.Exit(0);
                    }
                    else if (_keypress.Key == ConsoleKey.R)
                    {
                        // Reset game.
                        _reset = true;
                        break;
                    }
                    else if (_keypress.Key == ConsoleKey.S)
                    {
                        // Resume game.
                        break;
                    }
                }

                snake.Direction = snake.PreDirection;
            }

            // Ff the snake collided with borders.
            if (snake.HeadX <= 0 || snake.HeadX >= Width - 1 || snake.HeadY <= 0 ||
                snake.HeadY >= Height - 1)
            {
                _gameOver = true;
            }
            else
            {
                _gameOver = false;
            }
            
            if (snake.HeadX == Fruit.X && snake.HeadY == Fruit.Y)
            {
                // Ate fruit.
                _score += Fruits[Fruit.Id].Weight;
                snake.NTail++;
                Fruit.X = Rand.Next(1, Width - 1);
                Fruit.Y = Rand.Next(1, Height - 1);
                Fruit.Id = Rand.Next(0, Fruits.Count - 1);
            }

            for (var i = 1; i < snake.NTail; i++)
            {
                // if the snake collided with itself.
                if (snake.TailX[i] == snake.HeadX && snake.TailY[i] == snake.HeadY)
                {
                    if (snake.IsHorizontal() || snake.IsVertical())
                    {
                        _gameOver = false;
                    }
                    else
                    {
                        _gameOver = true;
                    }
                }

                if (snake.TailX[i] != Fruit.X || snake.TailY[i] != Fruit.Y) continue;

                Fruit.X = Rand.Next(1, Width - 1);
                Fruit.Y = Rand.Next(1, Height - 1);
                Fruit.Id = Rand.Next(0, Fruits.Count - 1);
            }

            return snake;
        }

        /**
         * Render game area with objects.
         */
        private void Render(Snake snake)
        {
            Console.SetCursorPosition(0, 0);

            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j < Width; j++)
                {
                    if (i == 0 || i == Height - 1)
                    {
                        // Paint horizontal border.
                        Console.Write("-");
                    }
                    else if (j == 0 || j == Width - 1)
                    {
                        // Paint vertical border.
                        Console.Write("|");
                    }
                    else if (j == Fruit.X && i == Fruit.Y)
                    {
                        // Add random fruit.
                        Console.Write(Fruits[Fruit.Id].Symbol);
                    }
                    else if (j == snake.HeadX && i == snake.HeadY)
                    {
                        // Paint snake head
                        Console.Write("@");
                    }
                    else
                    {
                        Fruit.IsPrinted = false;

                        // Print snake tail.
                        for (var k = 0; k < snake.NTail; k++)
                        {
                            if (snake.TailX[k] != j || snake.TailY[k] != i) continue;

                            Console.Write("o");
                            Fruit.IsPrinted = true;
                        }

                        if (!Fruit.IsPrinted)
                        {
                            Console.Write(" ");
                        }
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("Your Score: " + _score);
        }

        /**
         * Get lose message.
         */
        private void Lose()
        {
            Console.CursorTop = Height + 3;
            Console.CursorLeft = Width / 2 - 4;

            Console.Clear();

            Console.WriteLine("YOU DIED");
            Console.WriteLine("Your score is " + _score);
            Console.WriteLine("\nPress R to reset the game");
            Console.Write("Press ESC to quit the game");

            // Wait key press.
            while (true)
            {
                _keypress = Console.ReadKey(true);

                if (_keypress.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }
                else if (_keypress.Key == ConsoleKey.R)
                {
                    _reset = true;
                    break;
                }
            }
        }

        /**
         * Update gamePlay.
         */
        public void Update(Snake snake)
        {
            while (!_gameOver)
            {
                snake = CheckInput(snake);
                snake = Logic(snake);
                Render(snake);

                if (_reset)
                {
                    break;
                }

                Thread.Sleep(_gameSpeed);
            }

            if (_gameOver)
            {
                Lose();
            }

            Console.Clear();
        }
    }
}