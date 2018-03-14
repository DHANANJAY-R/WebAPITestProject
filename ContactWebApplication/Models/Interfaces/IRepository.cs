using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactWebApplication.Models.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();

        T Get(object id);

        T Add(T item);

        void Delete(T item);

        bool Update(T item);
    }
}
