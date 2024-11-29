using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApp1
{
    internal class ChessBoard
    {
        private readonly bool[,] _chessBoard = new bool[8, 8];

        public bool[,] GetChessBoard()
        {
            return _chessBoard;
        }

        public void DisplayConsoleBool()
        {
            Console.WriteLine("**********************************************");
            for (var i = 0; i < _chessBoard.GetLength(0); i++)
            {
                Console.Write(i + "\t");
                for (var j = 0; j < _chessBoard.GetLength(1); j++)
                {
                    if (i == 0)
                    {
                        Console.Write(j + "\t");
                    }
                    else
                    {
                        Console.Write(_chessBoard[i, j] + "\t");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("**********************************************");
        }//Bool values board in console

        public void DisplayConsolePretty(Tower tower, Pawn pawn)
        {
            Console.WriteLine("**********************************************");
            Console.Write("  ");
            for (var i = 1; i < _chessBoard.GetLength(1) + 1; i++)
            {
                Console.Write(i);
            }
            Console.WriteLine();
            for (var i = 0; i < _chessBoard.GetLength(0); i++)
            {
                Console.Write((char)(i + 65) + " ");
                for (var j = 0; j < _chessBoard.GetLength(0); j++)
                {
                    if (i == tower.GetX() && j == tower.GetY())
                    {
                        Console.Write("\u2656");
                    }
                    else if (i == pawn.GetX() && j == pawn.GetY())
                    {
                        Console.Write("\u265F");
                    }
                    else if (_chessBoard[i, j])
                    {
                        Console.Write("\u25C6");
                    }
                    else if (j % 2 == 0)
                    {
                        Console.Write("\u2592");
                    }
                    else
                    {
                        Console.Write("\u2588");
                    }
                }

                Console.WriteLine();
            }
            Console.WriteLine("**********************************************");
        } //Pretty display in console

        public void DisplayGUI(Grid grid, Tower tower, Pawn pawn)
        {
            for (int i = 0; i < 10; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++) 
                {
                    if (j==0 || j==9 || i==0 || i==9) {
                        for (int k = 1; k <= 8; k++) {
                            Rectangle rectangle = new Rectangle()
                            {
                                Fill = Brushes.Black
                            };
                            Grid.SetColumn(rectangle, i);
                            Grid.SetRow(rectangle, j);
                            grid.Children.Add(rectangle);
                            Label label = new Label()
                            {
                                Content = i==0 && j==0 || i==0 && j==9 || i==9 && j==0 || i==9 && j==9?"":
                                j==0||j==9? $"{i}":$"{(char)(j+64)}",
                                Foreground = Brushes.White,
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalContentAlignment = VerticalAlignment.Center,
                                FontSize = 35,
                            };
                            Grid.SetColumn(label, i);
                            Grid.SetRow(label, j);
                            grid.Children.Add(label);
                        }
                    }
                    else 
                    {
                    Rectangle rectangle = new Rectangle()
                    {
                    Fill = _chessBoard[i-1, j-1] ? (i-1 + j-1) % 2 == 0 ? Brushes.Green : Brushes.LightGreen:
                    (i-1 + j-1) % 2 == 0 ? Brushes.SaddleBrown : Brushes.BlanchedAlmond
                    };
                    Grid.SetColumn(rectangle, i);
                    Grid.SetRow(rectangle, j);
                    grid.Children.Add(rectangle);
                        if (i-1 == tower.GetX() && j-1 == tower.GetY())
                        {
                            Label label = new Label()
                            {
                                Content = "\u265C",
                                Foreground = Brushes.Brown,
                                FontSize = 35,
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalContentAlignment = VerticalAlignment.Center
                            };
                            Grid.SetColumn(label, i);
                            Grid.SetRow(label, j);
                            grid.Children.Add(label);
                        }
                        else if (i-1 == pawn.GetX() && j-1 == pawn.GetY())
                        {
                            Label label = new Label()
                            {
                                Content = "\u265A",
                                Foreground = Brushes.SandyBrown,
                                FontSize = 35,
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalContentAlignment = VerticalAlignment.Center
                            };
                            Grid.SetColumn(label, i);
                            Grid.SetRow(label, j);
                            grid.Children.Add(label);
                        }
                    }
                }
            }
        }
    }


    internal class Tower
    {
        private readonly int _x;
        private readonly int _y;

        public Tower()
        {
            var random = new Random();
            _x = random.Next(0, 8);
            _y = random.Next(0, 8);
        }

        public int GetX()
        {
            return _x;
        }

        public int GetY()
        {
            return _y;
        }

        public void TowerMoves(bool[,] board)
        {
            for (var i = 0; i < board.GetLength(0); i++)
            {
                board[_x, i] = true;
                board[i, _y] = true;

            }
        }
    }


    internal class Pawn
    {
        private readonly int _x;
        private readonly int _y;

        public Pawn()
        {
            var random = new Random();
            _x = random.Next(0, 8);
            _y = random.Next(0, 8);
        }

        public int GetX()
        {
            return _x;
        }

        public int GetY()
        {
            return _y;
        }

        public void IsCaptured(ChessBoard chessBoard)
        {
            Console.WriteLine(chessBoard.GetChessBoard()[GetX(), GetY()] ? "Captured" : "Not captured");
            Console.WriteLine("**********************************************");
        }//Checking captured pawn in Console
    }


    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ChessBoard chessBoard = new ChessBoard();
            Tower tower = new Tower();
            Pawn pawn = new Pawn();

            tower.TowerMoves(chessBoard.GetChessBoard());
            chessBoard.DisplayGUI(MainGrid, tower, pawn);
        }

    }
}