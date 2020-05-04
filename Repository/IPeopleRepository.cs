using System.Collections.Generic;
using Back_Entertainment.Models;

namespace Back_Entertainment.Repository
{
    public interface IPeopleRepository
    {
        People GetPeople(string email);
        void CreatePeople(People people);
        void UpdatePeople(People people);
       
    }
}