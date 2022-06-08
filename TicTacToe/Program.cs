using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    internal class Program
    {
        public static bool quitGame = false;
        public static bool playerTurn = true;
        public static char[,] board; // Plateau de jeu

        static void Main(string[] args)
        {
            while(!quitGame)
            {
                board = new char[3, 3]
                {
                    { ' ', ' ', ' ' },
                    { ' ', ' ', ' ' },
                    { ' ', ' ', ' ' },
                };
                while(!quitGame)
                {
                    if (playerTurn)
                    {
                        PlayerTurn();
                    }
                    else
                    {
                        ComputerTurn();
                    }

                    playerTurn = !playerTurn;
                }
                if(!quitGame)
                {
                    Console.WriteLine("Appuyer sur [Escape] pour quitter");
                    GetKey:
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.Escape:
                            quitGame = true;
                            Console.Clear();
                            break;
                        default:
                            goto GetKey;
                    }
                }
            }
        }

        public static void PlayerTurn()
        {
            var (row, column) = (0, 0);
            bool moved = false;

            while(!quitGame && !moved)
            {
                Console.Clear();
                RenderBoard();
                Console.WriteLine();
                Console.WriteLine("Choisir une case valide puis appuyer sur [Enter].");
                Console.SetCursorPosition(column * 6 + 1, row * 4 + 1);
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Escape:
                        quitGame = true;
                        Console.Clear();
                        break;
                    case ConsoleKey.RightArrow:
                        if(column >= 2)
                        {
                            column = 0;
                        }
                        else
                        {
                            column = column + 1;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (column <= 0)
                        {
                            column = 2;
                        }
                        else
                        {
                            column = column - 1;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (row >= 2)
                        {
                            row = 0;
                        }
                        else
                        {
                            row = row + 1;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (row <= 0)
                        {
                            row = 2;
                        }
                        else
                        {
                            row = row - 1;
                        }
                        break;
                    case ConsoleKey.Enter:
                        if (board[row, column] is ' ')
                        {
                            board[row, column] = 'X';
                            moved = true;
                        }
                        break;
                }
            }
        }

        public static void ComputerTurn()
        {
            var emptyBox = new List<(int X, int Y)>();
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    if(board[i, j] == ' ')
                    {
                        emptyBox.Add((i, j));
                    }
                }
            }
            var (X, Y) = emptyBox[new Random().Next(0, emptyBox.Count)];
            board[X, Y] = 'O';
        }

        public static void RenderBoard()
        {
            Console.WriteLine();
            Console.WriteLine($" {board[0, 0]}  |  {board[0, 1]}  |  {board[0, 2]}");
            Console.WriteLine("    |     |");
            Console.WriteLine("----+-----+----");
            Console.WriteLine("    |     |");
            Console.WriteLine($" {board[1, 0]}  |  {board[1, 1]}  |  {board[1, 2]}");
            Console.WriteLine("    |     |");
            Console.WriteLine("----+-----+----");
            Console.WriteLine("    |     |");
            Console.WriteLine($" {board[2, 0]}  |  {board[2, 1]}  |  {board[2, 2]}");
        }
    }
}
