using System;
using System.Collections.Generic;
using System.Text;
using XFUseAspNetCoreDI.Models;

namespace XFUseAspNetCoreDI.Services
{
    public interface IDataService
    {
        ICollection<Person> FindAll();
    }
}
