using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects;

namespace UrbanSpork.Common
{
    public class DeserializedDataRow
    {
        public Dictionary<string, item> items {get; set;}
    }

    public class item
    {
        public UserDTO dto { get; set; }
        public string s { get; set; }
        public int i { get; set; }
        public DateTime dt { get; set; }
    }
}
