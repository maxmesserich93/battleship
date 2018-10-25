﻿
using ConsoleApp1;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
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
using ViewModel.Lobby;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        public ViewModel.LoginViewModel ViewModel { get; }
       
       //public MasterViewModel Master { get; }


        public LoginWindow()
        {

            ViewModel = new LoginViewModel();
            this.DataContext = ViewModel;
            ViewModel.PropertyChanged += Change;
            InitializeComponent();

        }


        public void Change(object sender, PropertyChangedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var next = new MasterWindow(ViewModel.MasterViewModel);
                next.Show();
                this.Close();
            });
        }
    }
}
