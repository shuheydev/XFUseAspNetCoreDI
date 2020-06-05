using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using XFUseAspNetCoreDI.Models;

namespace XFUseAspNetCoreDI.Services
{
    public class MockDataService : IDataService
    {
        private readonly ILogger<MockDataService> _logger;
        
        public MockDataService(ILogger<MockDataService> logger)
        {
            this._logger = logger;
        }
        public ICollection<Person> FindAll()
        {
            var people = new List<Person>();

            for (int i = 0; i < 100; i++)
            {
                people.Add(new Person { Name = $"Person{i}", Age = i });
            }

            _logger.LogCritical("MockDataServiceから呼ばれました");

            return people;
        }
    }
}
