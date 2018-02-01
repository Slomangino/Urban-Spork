﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Json;
using System.Text;

namespace UrbanSpork.DataAccess.DataAccess
{
    public class User
    {
       [Key]
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public bool IsActive { get; set; }

        [Column(TypeName = "date")]
        public  DateTime InitialApprovedDate { get; set; }

        [Column(TypeName = "json")]
        public string Access { get; set; }

        [Column(TypeName = "json")]
        public string Equipment { get; set; }
    }
}