using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ViewModel.Service;

namespace ViewModel
{
    public abstract class AbstractServiceViewModel
    {
        public ICommand CloseCommand
        {
            get
            {
                return new RelayCommand(param =>
                {
                    Debug.WriteLine("asasdasdasdasdasdasd");
                    if (MessageBox.Show("Are you sure you want to quit?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    {

                    }
                    else
                    {
                        GameService.Close();
                        Application.Current.Shutdown();
                    }

                }
                );
            }
        }
        public AbstractGameServiceViewModel GameService { get; }


        public AbstractServiceViewModel(AbstractGameServiceViewModel gameService)
        {
            GameService = gameService;
        }

        public AbstractServiceViewModel(AbstractServiceViewModel vm)
        {
            GameService = vm.GameService;
        }

    }
}
