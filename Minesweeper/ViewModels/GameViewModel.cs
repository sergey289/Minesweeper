using System;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Minesweeper.ViewModels
{
    class GameViewModel
    {


        public Grid GameGrid { get; set; }

        public GameViewModel() => GameGrid = new Grid();

        private string[,] bombs;

        int counterOfBombs = 0;

        string bomb = "Bomb";

        internal void GenerateGrid(int rows, int columns)
        {
            

            AddBombs(rows, columns);

            for (int r = 0; r < bombs.GetLength(0); r++)
            {
                GameGrid.RowDefinitions.Add(new RowDefinition());

                for (int c = 0; c < bombs.GetLength(1); c++)
                {
                    if (r == 0)
                        GameGrid.ColumnDefinitions.Add(new ColumnDefinition());


                    Button btn = new Button();
                    AddFlag(btn);
                    btn.Click += (s, e) => GetNumber(btn, Grid.GetRow(btn), Grid.GetColumn(btn));
                    btn.RightTapped += (s, e) => SetFlag(btn, Grid.GetRow(btn), Grid.GetColumn(btn));
                    Grid.SetRow(btn, r);
                    Grid.SetColumn(btn, c);
                    GameGrid.Children.Add(btn);

                }


            }


        }

        private void AddFlag(Button btn)
        {
            var pol = new Polygon
            {
                Points = new PointCollection {
                    new Point(0, 0),
                    new Point(30,20),
                    new Point(10,40),
                    new Point(10,80),
                    new Point(0,80) },
                Fill = new SolidColorBrush(Colors.Red)
            };
            pol.Visibility = Visibility.Collapsed;
            btn.Content = pol;
        }

        private void GetNumber(Button btn, int r, int c)
        {

            if (bombs[r, c] == bomb)
            {
                btn.Background = new SolidColorBrush(Colors.Red);
                btn.Content = bombs[r, c];
                GameOver();
            }
            else
            {
                btn.Background = new SolidColorBrush(Colors.SkyBlue);
                SetNumber(r, c);
                btn.Content = bombs[r, c];
            }

        }

        private void SetFlag(Button btn, int row, int col)
        {
            if (bombs[row, col] == bomb)
            {
                --counterOfBombs;


                if (counterOfBombs == 0)
                {
                    YouWin();
                    counterOfBombs = 0;
                }
            }

            var pol = (Polygon)btn.Content;
            pol.Visibility = pol.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;

        }

        Random ran = new Random();

        private bool Chack(int col, int row)
        {

            if (col < bombs.GetLength(1) && col >= 0 && row < bombs.GetLength(0) && row >= 0)
            {
                return true;

            }
            else
            {
                return false;
            }


        } // Checking coordinates if they valid

        private void AddBombs(int c, int r)  // Random generation of bombs on the field                                                           
        {

            bombs = new string[c, r];

            int randomRow;
            int randomCol;


            for (int row = ran.Next(bombs.GetLength(0)); row < bombs.GetLength(0); row++)
            {

                for (int col = ran.Next(bombs.GetLength(1)); col < bombs.GetLength(1); col++)
                {
                    randomRow = ran.Next(row);
                    randomCol = ran.Next(col);

                    if (bombs[randomRow, randomCol] != bomb)
                    {
                        bombs[randomRow, randomCol] = bomb;
                        counterOfBombs++;
                    }

                }



            }


        }

        private void SetNumber(int row, int col)
        {

            int numberOfbombsInArea = 0;


            if (Chack(row, col + 1) && bombs[row, col + 1] == bomb)
            {
                numberOfbombsInArea++;
            }

            if (Chack(row, col - 1) && bombs[row, col - 1] == bomb)
            {
                numberOfbombsInArea++;
            }

            if (Chack(row - 1, col - 1) && bombs[row - 1, col - 1] == bomb)
            {
                numberOfbombsInArea++;
            }

            if (Chack(row - 1, col) && bombs[row - 1, col] == bomb)
            {
                numberOfbombsInArea++;
            }

            if (Chack(row - 1, col + 1) && bombs[row - 1, col + 1] == bomb)
            {
                numberOfbombsInArea++;
            }

            if (Chack(row + 1, col - 1) && bombs[row + 1, col - 1] == bomb)
            {
                numberOfbombsInArea++;

            }

            if (Chack(row + 1, col) && bombs[row + 1, col] == bomb)
            {
                numberOfbombsInArea++;
            }

            if (Chack(row + 1, col + 1) && bombs[row + 1, col + 1] == bomb)
            {
                numberOfbombsInArea++;
            }
            if (bombs[row, col] != bomb)
            {
                bombs[row, col] = numberOfbombsInArea.ToString();
            }



        }  // Quantity of bombs in the area

        public async void GameOver()
        {
            counterOfBombs = 0;
            string massage = "Game Over";
            MessageDialog messageDialog = new MessageDialog(massage);
            await messageDialog.ShowAsync();
          

        }

        public async void YouWin()
        {
            counterOfBombs = 0;
            string massage = "You Win";
            MessageDialog messageDialog = new MessageDialog(massage);
            await messageDialog.ShowAsync();

        }

    



}
    }
