using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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
using static ViewModel.FieldViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for FieldPage.xaml
    /// </summary>
    public partial class FieldPage : Page
    {
        public double Size { set; get; }
        public FieldViewModel ViewModel;

        public FieldPage(FieldViewModel viewModel, double _fieldSize)
        {

            Resources.Add("FieldSize", _fieldSize);
            ViewModel = viewModel;
            DataContext = ViewModel;
            //ViewModel.TileClickHandler(null);
            

           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;

            var field = b.DataContext as CoordinateViewModel;
            ViewModel.TileClick?.Execute(field.Position);


        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Button b = sender as Button;

            var field = b.DataContext as CoordinateViewModel;
            ViewModel.TileHover?.Execute(field.Position);

        }
    }

}
