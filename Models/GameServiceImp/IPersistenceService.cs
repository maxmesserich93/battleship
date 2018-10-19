using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.GameServiceImp
{
    public interface IPersistenceService
    {
         List<GameInformation> GetSavedGames();
         void LoadGameWithBot(string id);
    }
}
