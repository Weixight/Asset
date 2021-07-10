using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OurHr.Models
{
    public class UserRight
    {
        [Key]
        public int RightId { get; set; }
        public string RightName { get; set; }
        public string RightStatus { get; set; } = "False";

    }
}
