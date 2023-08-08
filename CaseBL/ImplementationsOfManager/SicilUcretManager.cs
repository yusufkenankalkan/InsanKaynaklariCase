using AutoMapper;
using CaseBL.InterfacesOfManager;
using CaseDL.InterfacesOfRepo;
using CaseEL.Models;
using CaseEL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseBL.ImplementationsOfManager
{
    public class SicilUcretManager : Manager<SicilUcretVM, SicilUcret, int>, ISicilUcretManager
    {
        public SicilUcretManager(ISicilUcretRepo repo, IMapper mapper) : base(repo, mapper, null)
        {
        }
    }
}
