using System;
using System.Threading;
using System.Threading.Tasks;
using LearnEveryDay.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnEveryDay.Data
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions<BaseContext> opt) : base(opt)
        {

        }

        
    }
}
