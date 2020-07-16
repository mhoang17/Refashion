using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refashion.Database
{
    public class BagDML : RefashionDML<Bag>
    {
        private BaseDML<Bag> baseDML;

        public BagDML()
        {
            baseDML = new BaseDML<Bag>("bags", new List<string>()
                {
                    "sellerId",
                    "added_at"
                });
        }
        public void Delete_Multiple(List<Bag> bags)
        {
            if(bags.Any(bag => bag.BagID == 0))
            {
                throw new ArgumentException("All Bags must have a valid Id");
            }
            bool success = baseDML.Delete_Multiple(bags.Select(bag => bag.BagID).ToList());

            Console.WriteLine("Success: " + success);
        }

        public void Delete_Single(Bag bag)
        {
            if(bag.BagID == 0)
            {
                throw new ArgumentException("Bag must have a valid Id");
            }

            bool success = baseDML.Delete_Multiple(new List<int> { bag.BagID });

            Console.WriteLine("Success: " + success);
        }

        public List<Bag> GetAll()
        {
            return baseDML.GetAll(mapToBag);
        }

        public List<int> Insert_Multiple(List<Bag> bags)
        {
            return baseDML.Insert_Multiple(bags, mapBagToStrings);
        }

        private List<string> mapBagToStrings(Bag bag)
        {
            List<string> row = new List<string>()
            {
                bag.SellerTag.ToString(),
                bag.RegistrationDate.ToString("yyyy-MM-dd H:mm:ss")
            };

            return row;
        }

        public int Insert_Single(Bag bag)
        {
            return baseDML.Insert_Multiple(new List<Bag> { bag }, mapBagToStrings).First();
        }

        public List<Bag> Select_Multiple(string conditions)
        {
            //Dictionary<string, string> conditionDictionary = ParseConditionsToDictionary(conditions);
            List<Bag> bags = baseDML.Select_Multiple(conditions, mapToBag);
            return bags;
        }

        public Bag Select_Single(string conditions)
        {
            Bag bag = new Bag(0);
            //Dictionary<string, string> conditionDictionary = ParseConditionsToDictionary(conditions);
            bag = baseDML.Select_Multiple(conditions, mapToBag, 1).First();

            return bag;
        }

        // TODO: Move to another class as this is also in SellerDML
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

        public void Update_Multiple(List<Bag> bags)
        {
            if (bags.FindAll(bag => bag.BagID == 0).Count > 0)
            {
                throw new ArgumentException("All bags must have a valid Id");
            }

            baseDML.Update_Multiple(bags, mapBagWithIdToStrings);
        }

        // Might not be the most efficient for updating a single element
        public void Update_Single(Bag bag)
        {
            Update_Multiple(new List<Bag> { bag });
        }

        private List<string> mapBagWithIdToStrings(Bag bag)
        {
            List<string> row = new List<string>()
            {
                bag.BagID.ToString(),
                bag.SellerTag.ToString(),
                bag.RegistrationDate.ToString("yyyy-MM-dd H:mm:ss")
            };

            return row;
        }

        private Bag mapToBag(MySqlDataReader reader)
        {
            int id = reader.GetInt32("id");
            int sellerId = reader.GetInt32("sellerId");
            DateTime addedDate = reader.GetDateTime("added_at");

            return new Bag(id, sellerId, addedDate);
        }
    }
}
