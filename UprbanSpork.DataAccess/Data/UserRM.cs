using System;
using System.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UrbanSpork.DataAccess.Data
{
    public class UserRM
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public Boolean IsActive { get; set; }
        public JsonObject Assets { get; set; }
    }
}