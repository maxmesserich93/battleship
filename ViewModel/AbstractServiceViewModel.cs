using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public abstract class AbstractServiceViewModel
    {

        public AbstractCallback Callback { get; }
        public AbstractGameService GameService { get; }

        public AbstractServiceViewModel(AbstractCallback abstractCallback, AbstractGameService gameService)
        {
            Callback = abstractCallback;
            GameService = gameService;
        }

        public AbstractServiceViewModel(AbstractServiceViewModel vm)
        {
            Callback = vm.Callback;
            GameService = vm.GameService;
        }

    }
}
