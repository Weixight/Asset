using Asset.Web.Data;

using OurHr.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OurHr.UTL
{
    public class RequestProssing
    {
        private readonly ApplicationDbContext _context;

        public RequestProssing(ApplicationDbContext context)
        {
            _context = context;

        }

      
    }
}
