using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refashion.Database
{
    public class ProductDML : RefashionDML<Product>
    {
        private BaseDML<Product> baseDML;
        public ProductDML()
        {
            baseDML = new BaseDML<Product>("products", new List<string>()
                {
                    "bagId",
                    "woocommerceId"
                });
        }
        public void Delete_Multiple(List<Product> products)
        {
            if (products.Any(product => product.Id == 0))
            {
                throw new ArgumentException("All Products must have a valid Id");
            }
            bool success = baseDML.Delete_Multiple(products.Select(product => product.Id).ToList());

            Console.WriteLine("Success: " + success);
        }

        public void Delete_Single(Product product)
        {
            if (product.Id == 0)
            {
                throw new ArgumentException("Product must have a valid Id");
            }

            bool success = baseDML.Delete_Multiple(new List<int> { product.Id });

            Console.WriteLine("Success: " + success);
        }

        public List<Product> GetAll()
        {
            return baseDML.GetAll(mapToProduct);
        }

        public List<int> Insert_Multiple(List<Product> products)
        {
            return baseDML.Insert_Multiple(products, mapProductToStrings);
        }

        public int Insert_Single(Product product)
        {
            return baseDML.Insert_Multiple(new List<Product> { product }, mapProductToStrings).First();
        }

        public List<Product> Select_Multiple(string conditions)
        {
            return baseDML.Select_Multiple(conditions, mapToProduct);
        }

        public Product Select_Single(string conditions)
        {
            return baseDML.Select_Multiple(conditions, mapToProduct, 1).First();
        }

        public void Update_Multiple(List<Product> products)
        {
            if (products.Any(product => product.Id == 0))
            {
                throw new ArgumentException("All Products must have a valid Id");
            }

            baseDML.Update_Multiple(products, mapProductWithIdToStrings);
        }

        public void Update_Single(Product product)
        {
            Update_Multiple(new List<Product> { product });
        }

        private Product mapToProduct(MySqlDataReader reader)
        {
            int id = reader.GetInt32("id");
            int bagId = reader.GetInt32("bagId");
            int woocommerceId = reader.GetInt32("woocommerceId");

            return new Product(id, bagId, woocommerceId);
        }

        private List<string> mapProductToStrings(Product product)
        {
            List<string> row = new List<string>()
            {
                product.BagId.ToString(),
                product.WooCommerceId.ToString()
            };

            return row;
        }

        private List<string> mapProductWithIdToStrings(Product product)
        {
            List<string> row = new List<string>()
            {
                product.Id.ToString(),
                product.BagId.ToString(),
                product.WooCommerceId.ToString()
            };

            return row;
        }
    }
}
