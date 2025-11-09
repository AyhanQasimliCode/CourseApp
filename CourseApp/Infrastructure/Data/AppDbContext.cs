using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public static class AppDbContext<T> where T : BaseEntity
    {
        public static List<T> datas;

        static AppDbContext()
        {
            datas = new List<T>();
        }
    }
}
