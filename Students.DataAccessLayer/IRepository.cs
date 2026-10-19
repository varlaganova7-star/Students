using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students.DataAccessLayer
{



    public interface IRepository<T> where T : class
    {
        void Create(T item);
        List<T> ReadAll();
        T ReadById(int id);
        void Delete(int id);
    }

}
