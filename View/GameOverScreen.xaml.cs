﻿using System;
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
using System.Windows.Shapes;

namespace View
{
    /// <summary>
    /// Interaction logic for GameOverScreen.xaml
    /// </summary>
    public partial class GameOverScreen : Window
    {
        
        public GameOverScreen(bool winner, string winnerName)
        {
            InitializeComponent();
            Winner.Text = winnerName + " wins the game!";
        }
    }
}
