using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModel;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private LoginViewModel ViewModel { get; }




        public MainWindow()
        {
            //Create the viewmodel
            ViewModel = new LoginViewModel();
            //Set the datacontaext of the window to the viewmodel
            //Now we can use the properties defined in the view model
            this.DataContext = ViewModel;
            InitializeComponent();
        }
    }
}
