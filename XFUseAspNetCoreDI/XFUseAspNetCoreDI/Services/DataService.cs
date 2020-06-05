using System;
using System.Collections.Generic;
using System.Text;
using XFUseAspNetCoreDI.Models;

namespace XFUseAspNetCoreDI.Services
{
    public class DataService : IDataService
    {
        public ICollection<Person> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
