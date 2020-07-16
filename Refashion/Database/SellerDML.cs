using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refashion.Database
{
    public class SellerDML : RefashionDML<Seller>
    {
        private MySqlCommand command;
        private DatabaseConnection database;
        private BaseDML<Seller> baseDML;
        public SellerDML()
        {
            database = new DatabaseConnection();
            baseDML = new BaseDML<Seller>("sellers", new List<string>()
                {
                    "name",
                    "email",
                    "address",
                    "postnumber",
                    "city",
                    "phonenumber",
                    "woocommerceId"
                });
        }

        // TODO: create abstraction for try-catch 
        public List<Seller> GetAll()
        {
            return baseDML.GetAll(MapToSeller);
        }

        public Seller Select_Single(string conditions)
        {
            // empty seller in case query returns nothing
            Seller seller = new Seller("", "", "", "", 0, "", 0);

            Dictionary<string, string> conditionDictionary = ParseConditionsToDictionary(conditions);

            seller = baseDML.Select_Multiple(conditionDictionary, MapToSeller, 1).First();

            return seller;
        }

        // TODO: Refactor as this contains same logic as Select_Single but without limit
        public List<Seller> Select_Multiple(string conditions)
        {
            Dictionary<string, string> conditionDictionary = ParseConditionsToDictionary(conditions);

            List<Seller> sellers = baseDML.Select_Multiple(conditionDictionary, MapToSeller);

            return sellers;
        }

        private Dictionary<string, string> ParseConditionsToDictionary(string conditions)
        {
            Dictionary<string, string> conditionDictionary = new Dictionary<string, string>();

            List<string> conditionStrings = new List<string>(conditions.Split(','));
            foreach (string condition in conditionStrings)
            {
                List<string> items = new List<string>(condition.Split(':'));
                if (items.Count > 2)
                {
                    // incorrect format
                    throw new ArgumentException("Conditions must be of the form 'ConditionName:Value'");
                }

                string rowName = items[0].Trim();
                string rowValue = items[1].Trim();

                conditionDictionary.Add(rowName, rowValue);
            }

            return conditionDictionary;
        }

        private Seller MapToSeller(MySqlDataReader reader)
        {
            int id = reader.GetInt32("id");
            string name = reader.GetString("name");
            string email = reader.GetString("email");
            string address = reader.GetString("address");
            int postnumber = reader.GetInt32("postnumber");
            string city = reader.GetString("city");
            string phonenumber = reader.GetString("phonenumber");
            int wooCommerceId = reader.GetInt32("woocommerceId");

            return new Seller(id, name, email, address, city, postnumber, phonenumber, wooCommerceId);
        }

        public int Insert_Single(Seller seller)
        {
            return baseDML.Insert_Multiple(new List<Seller> { seller }, mapSellerToStrings).First();
        }

        // TODO: Take max_allowed_packet into account
        public List<int> Insert_Multiple(List<Seller> sellers)
        {
            return baseDML.Insert_Multiple(sellers, mapSellerToStrings);
        }

        private List<string> mapSellerToStrings(Seller seller)
        {
            List<string> row = new List<string>()
            {
                seller.Name,
                seller.Email,
                seller.Address,
                seller.ZIP.ToString(),
                seller.City,
                seller.PhoneNumber,
                seller.WooCommerceId.ToString()
            };

            return row;
        }

        public void Update_Single(Seller seller)
        {
            Update_Multiple(new List<Seller> { seller });
        }

        public void Update_Multiple(List<Seller> sellers)
        {
            if (sellers.FindAll(seller => seller.Tag == 0).Count > 0)
            {
                throw new ArgumentException("All sellers must have a valid tag");
            }
            baseDML.Update_Multiple(sellers, mapSellerWithTagToStrings);
        }

        private List<string> mapSellerWithTagToStrings(Seller seller)
        {
            List<string> row = new List<string>()
            {
                seller.Tag.ToString(),
                seller.Name,
                seller.Email,
                seller.Address,
                seller.ZIP.ToString(),
                seller.City,
                seller.PhoneNumber,
                seller.WooCommerceId.ToString()
            };

            return row;
        }

        // TODO: Consider implementing soft delete
        public void Delete_Single(Seller seller)
        {
            if(seller.Tag == 0)
            {
                throw new ArgumentException("Seller must have a valid Tag");
            }

            bool success = baseDML.Delete_Multiple(new List<int> { seller.Tag });

            Console.WriteLine("Success: " + success);
        }

        public void Delete_Multiple(List<Seller> sellers)
        {
            if(sellers.Any(seller => seller.Tag == 0))
            {
                throw new ArgumentException("All sellers must have a valid Tag");
            }

            bool success = baseDML.Delete_Multiple(sellers.Select(seller => seller.Tag).ToList());

            Console.WriteLine("Success: " + success);
        }
    }
}
