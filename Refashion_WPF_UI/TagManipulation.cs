using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refashion_WPF_UI
{
    class TagManipulation
    {

        public int sellerTagDecreaser(string sellerTagString)
        {
            // Tag length (since a tag is always in the format 0000# we know the first 4 characters are numbers.
            int tagLength = 4;

            // Removes #
            string sellerTagPlaceholder = sellerTagString.Substring(0, tagLength);

            // Removes 0's if they lie in the front (eg. 0044 becomes 44)
            if (sellerTagPlaceholder[0].ToString() == "0")
            {
                while (sellerTagPlaceholder[0].ToString() == "0")
                {
                    // Remove the first character
                    sellerTagPlaceholder = sellerTagPlaceholder.Remove(0, 1);
                }
            }

            return int.Parse(sellerTagPlaceholder);
        }
    }
}
