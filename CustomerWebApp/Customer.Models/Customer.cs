using System;
using System.Collections.Generic;
using System.Text;

 namespace CustomerDemo.Models
{
   public class Customer
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public DateTime LastUpdatedDateTimeUtc { get; set; }
    }
}
