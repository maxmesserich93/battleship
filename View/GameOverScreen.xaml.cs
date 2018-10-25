using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for GameOverScreen.xaml
    /// </summary>
    public partial class GameOverScreen : Page
    {
        private GameOverViewModel ViewModel;
        public GameOverScreen(GameOverViewModel vm)
        {
            ViewModel = vm;
            Debug.WriteLine(vm.LooserScore + " -- " + vm.WinnerScore + " --- " + vm.Result);
            DataContext = ViewModel;
            InitializeComponent();
        }
    }
}
