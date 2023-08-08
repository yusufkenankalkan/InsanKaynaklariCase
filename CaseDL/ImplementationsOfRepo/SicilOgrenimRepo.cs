using CaseDL.InterfacesOfRepo;
using CaseEL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseDL.ImplementationsOfRepo
{
    public class SicilOgrenimRepo : Repository<SicilOgrenim, int>, ISicilOgrenimRepo
    {
        public SicilOgrenimRepo(MyContext context) : base(context)
        {

        }
    }
}
