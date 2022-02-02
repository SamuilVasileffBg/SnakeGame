using System;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace SnakeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            int bonusRow = 0;
            int bonusCol = 0;
            bool bonusExists = false;
            Console.WindowHeight = 25;
            Console.WindowWidth = 50;
            Console.CursorVisible = false;
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            Console.Title = "SnakeGame";
            
            int iterationCount = 0;
            int[] bonusCoordinates = new int[2];
            int commandValue = 0;
            while (true)
            {
                Console.Clear();
                Console.SetCursorPosition(25, 12);
                Console.Write("Start");
                Console.SetCursorPosition(25, 13);
                Console.Write("Highscores");
                Console.SetCursorPosition(25, 14);
                Console.Write("Exit");
                Console.SetCursorPosition(23, 12 + commandValue);
                Console.Write("->");
                ConsoleKey commandKey = Console.ReadKey().Key;
                if (commandKey == ConsoleKey.UpArrow && commandValue != 0)
                {
                    commandValue--;
                }
                else if (commandKey == ConsoleKey.DownArrow && commandValue != 2)
                {
                    commandValue++;
                }
                else if (commandKey == ConsoleKey.Enter)
                {
                    if (commandValue == 0)
                    {
                        Console.Clear();
                        StartGame(ref bonusRow, ref bonusCol, ref bonusExists, ref iterationCount, ref bonusCoordinates);
                    }
                    else if (commandValue == 1)
                    {
                        DisplayHighscores();
                    }
                    else if (commandValue == 2)
                    {
                        Environment.Exit(0);
                    }
                }
            }
        }

        private static void DisplayHighscores()
        {
            while (true)
            {
                Console.Clear();
                Dictionary<string, Player> playersDictionary = new Dictionary<string, Player>();
                Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);
                Console.Write("Highscores:");

                if (File.Exists(@".\highscores.txt"))
                {
                    string[] allHighscores = File.ReadAllLines(@".\highscores.txt");
                    foreach (var nameHighscore in allHighscores)
                    {
                        string[] nameHighscoreArray = nameHighscore.Split(" -> ");
                        string playerName = nameHighscoreArray[0];
                        int score = int.Parse(nameHighscoreArray[1]);
                        if (!playersDictionary.ContainsKey(playerName))
                        {
                            playersDictionary.Add(playerName, new Player(playerName, new List<int>()));
                        }
                        playersDictionary[playerName].Score.Add(score);
                    }

                    int foreachIterationCount = 0;
                    foreach (var player in playersDictionary.OrderByDescending(x => x.Value.Highscore))
                    {
                        if (foreachIterationCount > 9)
                        {
                            break;
                        }
                        Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 + foreachIterationCount + 1);
                        Console.Write($"{player.Value.Name} -> {player.Value.Highscore}");
                        foreachIterationCount++;
                    }
                }
                    Console.SetCursorPosition(5, Console.WindowHeight / 2 - 2);
                    Console.Write("Click R to remove all highscores.");
                    Console.SetCursorPosition(5, Console.WindowHeight / 2 - 1);
                    Console.Write("Click Enter to return to the main menu.");
                    ConsoleKey commandKey = Console.ReadKey().Key;
                    if (commandKey == ConsoleKey.R)
                    {
                        File.Delete(@".\highscores.txt");
                    }
                    else if (commandKey == ConsoleKey.Enter)
                    {
                        return;
                    }
                }
        }

        private static void StartGame(ref int bonusRow, ref int bonusCol, ref bool bonusExists, ref int iterationCount, ref int[] bonusCoordinates)
        {
            Snake snake = new Snake();
            snake.WriteSnake();
            int highscore = 0;
            while (true)
            {
                highscore++;
                //Console.Clear();
                if (bonusExists)
                {
                    //Console.SetCursorPosition(0, 0);
                    //Console.Write($"Snake Coordinates -> Row: {snake.SnakeCoordinates.PeekLast().Row} Col: {snake.SnakeCoordinates.PeekLast().Col}");
                    //Console.SetCursorPosition(0, 1);
                    //Console.Write($"Bonus Coordinates -> {bonusCoordinates[0]} {bonusCoordinates[1]}");
                    Console.SetCursorPosition(bonusCol, bonusRow);
                    Console.Write("█", Console.ForegroundColor = ConsoleColor.DarkYellow);
                }
                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey().Key;
                    if (key == ConsoleKey.Escape)
                    {
                        Console.Write("debugging");
                        Console.Clear();
                        break;
                    }
                    snake.ChangeDirection(key);
                }
                if (snake.Move() == false)
                {
                    EnterHighscore(highscore);
                    break;
                }
                if (snake.snakeHitSnake())
                {
                    EnterHighscore(highscore);
                    break;
                }

                if (bonusExists)
                {
                    if (snake.SnakeCoordinates.PeekLast().Row == bonusCoordinates[0] && snake.SnakeCoordinates.PeekLast().Col == bonusCoordinates[1])
                    {
                        highscore += 50;
                        snake.GotBonus = true;
                        bonusExists = false;
                        iterationCount = 0;
                    }
                }

                if (iterationCount == 10 && bonusExists == false)
                {
                    bonusExists = true;
                    while (true)
                    {
                        Random random = new Random();
                        bonusRow = random.Next(0, Console.WindowHeight / 2);
                        bonusCol = random.Next(0, Console.WindowWidth / 2);
                        bool returnBool = true;
                        foreach (var coordinate in snake.SnakeCoordinates)
                        {
                            if (coordinate.Col == bonusCol || coordinate.Row == bonusRow)
                            {
                                returnBool = false;
                                break;
                            }
                        }
                        if (returnBool == false)
                        {
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                    bonusCoordinates = new int[2] { bonusRow, bonusCol };
                }
                iterationCount++;
                Thread.Sleep(50);
            }
        }

        private static void EnterHighscore(int highscore)
        {
            while (true)
            {
                Console.Clear();
                Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);
                Console.Write("Enter your name: ");
                string name = Console.ReadLine();
                if (name.Contains("->"))
                {
                    Console.Clear();
                    Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);
                    Console.Write("Name cannot contain ,,->''");
                    Thread.Sleep(2000);
                    continue;
                }
                else
                {
                    File.AppendAllText(@".\highscores.txt", $"{name} -> {highscore}" + Environment.NewLine);
                    return;
                }
            }
        }
    }
}
