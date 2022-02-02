using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeGame
{
    class Snake
    {
        public Snake()
        {
            SnakeCoordinates = new DoublyLinkedList<SnakeCoordinates>();
            SnakeCoordinates.AddLast(new SnakeCoordinates(Console.WindowHeight / 2, Console.WindowWidth / 2));
            SnakeCoordinates.AddLast(new SnakeCoordinates(Console.WindowHeight / 2, Console.WindowWidth / 2 + 1));
            SnakeCoordinates.AddLast(new SnakeCoordinates(Console.WindowHeight / 2, Console.WindowWidth / 2 + 2));
            SnakeCoordinates.AddLast(new SnakeCoordinates(Console.WindowHeight / 2, Console.WindowWidth / 2 + 3));
            SnakeCoordinates.AddLast(new SnakeCoordinates(Console.WindowHeight / 2, Console.WindowWidth / 2 + 4));
            SnakeCoordinates.AddLast(new SnakeCoordinates(Console.WindowHeight / 2, Console.WindowWidth / 2 + 5));
            SnakeCoordinates.AddLast(new SnakeCoordinates(Console.WindowHeight / 2, Console.WindowWidth / 2 + 6));
            SnakeCoordinates.AddLast(new SnakeCoordinates(Console.WindowHeight / 2, Console.WindowWidth / 2 + 7));
            SnakeCoordinates.AddLast(new SnakeCoordinates(Console.WindowHeight / 2, Console.WindowWidth / 2 + 8));
            SnakeCoordinates.AddLast(new SnakeCoordinates(Console.WindowHeight / 2, Console.WindowWidth / 2 + 9));
            SnakeCoordinates.AddLast(new SnakeCoordinates(Console.WindowHeight / 2, Console.WindowWidth / 2 + 10));
        }
        public DoublyLinkedList<SnakeCoordinates> SnakeCoordinates { get; set; }
        public bool GotBonus { get; set; } = false;
        public string Direction { get; set; } = "right";

        public void ChangeDirection(ConsoleKey key)
        {
            if (key == ConsoleKey.LeftArrow && Direction != "right")
            {
                Direction = "left";
            }
            else if (key == ConsoleKey.RightArrow && Direction != "left")
            {
                Direction = "right";
            }
            else if (key == ConsoleKey.DownArrow && Direction != "up")
            {
                Direction = "down";
            }
            else if (key == ConsoleKey.UpArrow && Direction != "down")
            {
                Direction = "up";
            }
        }
        public bool Move()
        {
            if (GotBonus == false)
            {
                SnakeCoordinates poppedCoordinates = SnakeCoordinates.GetFirst();
                Console.SetCursorPosition(poppedCoordinates.Col, poppedCoordinates.Row);
                Console.Write(" ");
            }
            else
            {
                GotBonus = false;
            }
            int newCol = SnakeCoordinates.PeekLast().Col;
            int newRow = SnakeCoordinates.PeekLast().Row;
            if (Direction == "right")
            {
                newCol++;
            }
            else if (Direction == "left")
            {
                newCol--;
            }
            else if (Direction == "down")
            {
                newRow++;
            }
            else if (Direction == "up")
            {
                newRow--;
            }
            if (newRow < 0 || newCol < 0 ||newRow >= Console.WindowHeight || newCol >= Console.WindowWidth)
            {
                return false;
            }
            else
            {
                SnakeCoordinates.AddLast(new SnakeCoordinates(newRow, newCol));
                WriteSnake();
                return true;
            }
        }
        public void WriteSnake()
        {
            int iterationCount = 1;
            Console.ResetColor();
            foreach (var coordinate in SnakeCoordinates)
            {
                if (iterationCount == SnakeCoordinates.Count())
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                    WriteCoordinatesOnConsole(coordinate.Col, coordinate.Row);
                

                iterationCount++;
            }
        }

        public bool snakeHitSnake()
        {
            int headRow = SnakeCoordinates.PeekLast().Row;
            int headCol = SnakeCoordinates.PeekLast().Col;
            int iterationCount = 1;
            foreach (var coordinate in this.SnakeCoordinates)
            {
                if (iterationCount != SnakeCoordinates.Count())
                {
                    if (coordinate.Row == headRow && coordinate.Col == headCol)
                    {
                        return true;
                    }
                }
                iterationCount++;
            }
            return false;
        }

        private void WriteCoordinatesOnConsole(int row, int col)
        {
            Console.SetCursorPosition(row, col);
            Console.Write("█");
        }
    }
}
