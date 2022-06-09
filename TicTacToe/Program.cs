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
        public static char userChoice;
        public static char computerChoice;

        static void Main(string[] args)
        {
            

            while (!quitGame)
            {
                board = new char[3, 3]
                {
                    { ' ', ' ', ' ' },
                    { ' ', ' ', ' ' },
                    { ' ', ' ', ' ' },
                };
                Console.Clear();
                Console.WriteLine("Choisir une valeur entre X et O");
                userChoice = Convert.ToChar(Console.ReadLine());
                


                if (userChoice == 'O')
                {
                    computerChoice = 'X';
                }
                if (userChoice == 'X')
                {
                    computerChoice = 'O';
                }

                while (!quitGame)
                {
                    if (playerTurn)
                    {
                        PlayerTurn(userChoice);
                        if(CheckLines(userChoice))
                        {
                            EndGame("You Win");
                            break;
                        }
                    }
                    else
                    {
                        ComputerTurn(computerChoice);
                        if (CheckLines(computerChoice))
                        {
                            EndGame("You loosed");
                            break;
                        }
                    }

                    playerTurn = !playerTurn;

                    if (CheckDraw())
                    {
                        EndGame("Draw !");
                        break;
                    }

                }
                if(!quitGame)
                {
                    Console.WriteLine("Appuyer sur [Escape] pour quitter, [ENTER] pour rejouer.");
                    GetKey:
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.Escape:
                            quitGame = true;
                            Console.Clear();
                            break;
                        case ConsoleKey.Enter:
                            break;
                        default:
                            goto GetKey;
                    }
                }
            }
        }

        public static void PlayerTurn(char userChoice)
        {
            var (row, column) = (0, 0);
            bool moved = false;

            while(!quitGame && !moved)
            {
                Console.Clear();
                RenderBoard();
                Console.WriteLine();
                Console.WriteLine("Choisir une case valide puis appuyer sur [Enter].");
                Console.WriteLine("Choix de l'utilisateur " + userChoice);
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
                            board[row, column] = userChoice;
                            moved = true;
                        }
                        break;
                }
            }
        }

        public static void ComputerTurn(char computerChoice)
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
            board[X, Y] = computerChoice;

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

        public static bool CheckLines(char c) =>
            board[0, 0] == c && board[1, 0] == c && board[2, 0] == c ||
            board[0, 1] == c && board[1, 1] == c && board[2, 1] == c ||
            board[0, 2] == c && board[1, 2] == c && board[2, 2] == c ||
            board[0, 0] == c && board[0, 1] == c && board[0, 2] == c ||
            board[1, 0] == c && board[1, 1] == c && board[1, 2] == c ||
            board[2, 0] == c && board[2, 1] == c && board[2, 2] == c ||
            board[0, 0] == c && board[1, 1] == c && board[2, 2] == c ||
            board[2, 0] == c && board[1, 1] == c && board[0, 2] == c;

        public static bool CheckDraw() =>
            board[0, 0] != ' ' && board[1, 0] != ' ' && board[2, 0] != ' ' &&
            board[0, 1] != ' ' && board[1, 1] != ' ' && board[2, 1] != ' ' &&
            board[0, 2] != ' ' && board[1, 2] != ' ' && board[2, 2] != ' ';


        public static void EndGame(string msg)
        {
            Console.Clear();
            RenderBoard();
            Console.WriteLine(msg);
        }


    }
}
