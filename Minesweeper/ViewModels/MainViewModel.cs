using Minesweeper.Views;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Minesweeper.ViewModels
{
    class MainViewModel
    {
        public int Rows { get; set; }
        public int Columns { get; set; }




        public void Start()
        {
            if (Window.Current.Content is Frame frame)
                frame.Navigate(typeof(GamePage), new int[] { Rows, Columns });
        }


    }
}
