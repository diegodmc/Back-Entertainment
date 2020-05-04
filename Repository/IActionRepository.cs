using System.Collections.Generic;
using Back_Entertainment.Models;

namespace Back_Entertainment.Repository
{
    public interface IActionB3Repository
    {
        List<ActionB3> GetAllActions();
        void CreateAction(ActionB3 action);
    }
}