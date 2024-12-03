namespace SnakeGameConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            Coord gridDimensions = new Coord(50, 20);
            Coord snakePos = new Coord(10, 1);
            Coord applePos = new Coord(rnd.Next(1, gridDimensions.X - 1), rnd.Next(1, gridDimensions.Y - 1));
            int frameDelay = 50;
            Direction movementDirection = Direction.Down;
            List<Coord> snakePosHis = new();
            int snakeTail = 1;
            int score = 0;

            while (true)
            {
                Console.Clear();
                snakePos.ApplyMovementDirection(movementDirection);

                Console.WriteLine($"Your score is: {score}");
                for (int y = 0; y < gridDimensions.Y; y++)
                {
                    for (int x = 0; x < gridDimensions.X; x++)
                    {
                        Coord currentCoord = new Coord(x, y);
                        if (snakePos.Equals(currentCoord) || snakePosHis.Contains(currentCoord))
                        {
                            Console.Write("\u25a0");
                        }
                        else if (applePos.Equals(currentCoord))
                        {
                            Console.Write("@");
                        }
                        else if (x == 0 || y == 0 || x == gridDimensions.X - 1 || y == gridDimensions.Y - 1)
                        {
                            Console.Write("$");
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }

                    Console.WriteLine();
                }

                if (snakePos.Equals(applePos))
                {
                    snakeTail++;
                    score++;
                    applePos = new Coord(rnd.Next(1, gridDimensions.X - 1), rnd.Next(1, gridDimensions.Y - 1));
                }
                else if (snakePos.X == 0 || snakePos.Y == 0 || 
                         snakePos.X == gridDimensions.X - 1 || snakePos.Y == gridDimensions.Y - 1 ||
                         snakePosHis.Contains(snakePos))
                {
                    score = 0;
                    snakeTail = 1;
                    snakePos = new Coord(10, 1);
                    applePos = new Coord(rnd.Next(1, gridDimensions.X - 1), rnd.Next(1, gridDimensions.Y - 1)); 
                    snakePosHis.Clear();
                    movementDirection = Direction.Down;
                    continue;
                }

                snakePosHis.Add(new Coord(snakePos.X, snakePos.Y));
                if (snakePosHis.Count > snakeTail)
                {
                    snakePosHis.RemoveAt(0);
                }

                DateTime time = DateTime.Now;
                while ((DateTime.Now - time).Milliseconds < frameDelay)
                {
                    if (Console.KeyAvailable)
                    {
                        ConsoleKey key = Console.ReadKey().Key;
                        switch (key)
                        {
                            case ConsoleKey.LeftArrow:
                                movementDirection = Direction.Left;
                                break;
                            case ConsoleKey.RightArrow:
                                movementDirection = Direction.Right;
                                break;
                            case ConsoleKey.UpArrow:
                                movementDirection = Direction.Up;
                                break;
                            case ConsoleKey.DownArrow:
                                movementDirection = Direction.Down;
                                break;
                        }
                    }
                }
            }
        }
    }
}
