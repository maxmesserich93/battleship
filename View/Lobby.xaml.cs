using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using ViewModel.Lobby;

namespace View
{
    /// <summary>
    /// Interaction logic for Lobby.xaml
    /// </summary>
    public partial class Lobby : Page
    {
        LobbyViewModel ViewModel { get; }
        public Lobby(LobbyViewModel vm)
        {
            ViewModel = vm;
            this.DataContext = ViewModel;

            InitializeComponent();
        }



    }
}
