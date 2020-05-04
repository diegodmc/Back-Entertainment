using System.Collections.Generic;
using Back_Entertainment.Models;

namespace Back_Entertainment.Repository
{
    public interface IPriceActionRepository
    {
        PriceAction GetPriceAction(string code);
        void CreatePriceAction(PriceAction price);
        void UpdatePriceAction(PriceAction price);
       
    }
}