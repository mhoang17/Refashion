using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refashion.Database
{
    interface RefashionDML<T>
    {
        void Insert_Multiple(List<T> elements);

        void Insert_Single(T element);

        void Delete_Multiple(List<T> element);

        void Delete_Single(T element);

        List<T> Select_Multiple(string condition);

        T Select_Single(string condition);

        void Update_Multiple(List<T> element);

        void Update_Single(T element);

        List<T> GetAll();
    }
}
