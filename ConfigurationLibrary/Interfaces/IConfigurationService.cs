using SecilStoreExam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationLibrary.Interfaces
{
    internal interface IConfigurationService
    {
        Task<List<ConfigurationRecord>> GetConfigurations();
        Task<ConfigurationRecord> GetConfiguration(int id);
        Task AddConfiguration(ConfigurationRecord configurationRecord);
        Task UpdateConfiguration(ConfigurationRecord configurationRecord);
        Task DeleteConfiguration(int id);
    }
}
