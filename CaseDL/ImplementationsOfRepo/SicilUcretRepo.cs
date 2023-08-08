using CaseDL.InterfacesOfRepo;
using CaseEL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseDL.ImplementationsOfRepo
{
    public class SicilUcretRepo : Repository<SicilUcret, int>, ISicilUcretRepo
    {
        public SicilUcretRepo(MyContext context) : base(context)
        {

        }
    }
}
