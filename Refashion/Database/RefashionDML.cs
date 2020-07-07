using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refashion.Database
{
    interface RefashionDML
    {
        void Insert_Multiple(List<Seller> sellers);

        void Insert_Single(Seller seller);

        void Delete_Multiple(List<Seller> seller);

        void Delete_Single(Seller seller);

        List<Seller> Select_Multiple(string condition);

        Seller Select_Single(string condition);

        void Update_Multiple(List<Seller> sellers);

        void Update_Single(Seller seller);

        List<Seller> GetAll();
    }
}
