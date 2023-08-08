using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseDL
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options)
           : base(options)
        {

        }
        


    }
}
