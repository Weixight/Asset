using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using OurHr.Models;

namespace Asset.Web.Models
{
    public enum Continents
    {
        None, Africa, Asia, Australia, Europe, America
    }

    public enum ExperienceLevels
    {
        None,Novice, Intermediate, Advanced, Expert, Master
    }

    public class User: ApplicationUser
    {
        public Continents Continent { get; set; }
        public ExperienceLevels ExperienceLevel { get; set; }
    }
}
