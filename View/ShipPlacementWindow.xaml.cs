using Models;
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
    /// Interaction logic for ShipPlacementWindow.xaml
    /// </summary>
    public partial class ShipPlacementWindow : Window
    {
        ShipPlacementViewModel ViewModel { get; }

        public FieldPage Frame { set; get; }

        public ICommand Command;

        


        public ShipPlacementWindow(ShipPlacementViewModel model)
        {
            Command = new RelayCommand(Bla);
            ViewModel = model;
            this.DataContext = ViewModel;

            InitializeComponent();
            Frame = new FieldPage(ViewModel.FieldViewModel,20);
            Frame.InitializeComponent();
            //Frame.Resources.Add("FieldSize", 50.0);

            Field.Navigate(Frame);
            

        }

        public void Bla(object a)
        {
            Debug.WriteLine("ASDASDSADASDASD");

        }
    }
}
