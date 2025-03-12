using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_NF_crud.Models.Repositories
{
    internal interface IRepository<T> where T : class
    {
        void create(T entity);
        int update(T entity);
        int delete(T entity);
        HashSet<T> All();
        T findById(int id);
        HashSet<T> findByKeyWord(string name);
    }
}
