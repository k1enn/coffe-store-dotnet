using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Expense_Tracker_App.Models
{
    public class User
    {
        int ID { get; set; } 
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}