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
    /// Interaction logic for GameOverPage.xaml
    /// </summary>
    public partial class GameOverPage : Page
    {
        private GameOverViewModel ViewModel;
        public GameOverPage(GameOverViewModel vm)
        {
            ViewModel = vm;
            Debug.WriteLine(vm.LooserScore + " -- " + vm.WinnerScore + " --- " + vm.Result);
            DataContext = ViewModel;
            InitializeComponent();
        }
    }
}
