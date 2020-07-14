using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refashion
{
    public sealed class TagManipulator
    {

        private static TagManipulator instance;

        private TagManipulator() { }

        public static TagManipulator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TagManipulator();
                }
                return instance;
            }
        }

        public string tagExtender(int tagLength, int tag)
        {
            string tagString = tag.ToString();

            // Check if it has the correct length
            if (tagString.Length < tagLength)
            {

                int count = tagString.Length;
                string placeholder = "";

                while (count < tagLength)
                {
                    placeholder += "0";
                    count++;
                }

                tagString = placeholder + tagString;
            }

            tagString += "#";

            return tagString;
        }
    }
}
