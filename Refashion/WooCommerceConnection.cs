using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WooCommerceNET;
using WooCommerceNET.WooCommerce.v2;

namespace Refashion
{
    public sealed class WooCommerceConnection
    {
        // Field
        private static WooCommerceConnection instance = null;
        private static WCObject wc;

        // TODO: There might be a better way to do this rather than having a local List
        private static List<ProductTag> sellerTags;

        // Constructor
        private WooCommerceConnection()
        {
            RestAPI rest = new RestAPI("http://refashion.dk/softwaretesting/wp-json/wc/v2/", "ck_4a8cc1550b991506880200fb4100b1d69d5a5ab3", "cs_a4fcb4c1ad3e1162563bd68b3fb1692be06d451e", requestFilter: RequestFilter);
            wc = new WCObject(rest);
        }

        // Get Instance
        public static WooCommerceConnection Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new WooCommerceConnection();
                }
                return instance;
            }
        }

        // TODO discuss if these functions should be in their own class
        // TODO: The tag id from woocommerce could be given a variable in Seller, so it's easier for retrieval. We need to fix the database for this
        public async void InsertTagWC(Seller seller)
        {
            ProductTag sellerTag = new ProductTag()
            {
                name = seller.TagString
            };

            await wc.Tag.Add(sellerTag);

            sellerTags = await wc.Tag.GetAll();
        }
        
        public async void DeleteTagWC(Seller seller)
        {
            if (sellerTags == null)
                sellerTags = await wc.Tag.GetAll();

            int? id = null;

            foreach (var tag in sellerTags)
            {
                if (tag.name.Equals(seller.TagString))
                {
                    id = tag.id;
                    break;
                }
            }

            if (id != null)
                await wc.Tag.Delete(id.Value, force:true);
        }

        private static void RequestFilter(HttpWebRequest request)
        {
            request.UserAgent = "WooCommerce.NET";
        }
    }
}
