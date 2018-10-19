using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModel.Service;

namespace View
{
    interface IViewModelUser<T>  where T : AbstractGameServiceViewModel
    {
        T VM{set;get;}


        void OnClosed(EventArgs e);

}
}
