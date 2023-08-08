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
    public class SicilManager : Manager<SicilVM, Sicil, int>, ISicilManager
    {
        public SicilManager(ISicilRepo repo, IMapper mapper) : base(repo, mapper, null)
        {
        }
    }
}
