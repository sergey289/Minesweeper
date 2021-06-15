using Minesweeper.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Minesweeper.Views
{
   
    public sealed partial class GamePage : Page
    {
        readonly GameViewModel vm = new GameViewModel();

        public GamePage() => this.InitializeComponent();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = vm;

            int[] rowsCols = (int[])e.Parameter;
            vm.GenerateGrid(rowsCols[0], rowsCols[1]);
        }
    }
}
