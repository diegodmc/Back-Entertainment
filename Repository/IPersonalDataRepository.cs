using System.Collections.Generic;
using Back_Entertainment.Models;

namespace Back_Entertainment.Repository
{
    public interface IPersonalDataRepository
    {
        PersonalData GetPersonalData(string email);
        void CreatePersonalData(PersonalData personal);
        void UpdatePersonalData(PersonalData personal);
       
    }
}