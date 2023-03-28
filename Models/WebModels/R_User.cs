using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.WebModels
{
    public class R_User
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public int TotalReview { get; set; }
        public decimal AvgReview { get; set; }
        public Int64 PhoneOne { get; set; }
        public List<string> Skills { get; set; }

    }

    public class Skills
    {
        public string Name { get; set; }
    }


    public class TodoModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public int userId { get; set; }
        public bool completed { get; set; }
    }

}
