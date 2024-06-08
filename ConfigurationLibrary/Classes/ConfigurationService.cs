using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SecilStoreExam.Models;
using ConfigurationLibrary;

public interface IConfigurationService
{
    Task<List<ConfigurationRecord>> GetConfigurations();
    Task<ConfigurationRecord> GetConfiguration(int id);
    Task AddConfiguration(ConfigurationRecord configurationRecord);
    Task UpdateConfiguration(ConfigurationRecord configurationRecord);
    Task DeleteConfiguration(int id);
}

public class ConfigurationService : IConfigurationService
{
    private readonly SecilStore _context;

    public ConfigurationService(SecilStore context)
    {
        _context = context;
    }

    public async Task<List<ConfigurationRecord>> GetConfigurations()
    {
        return await _context.ConfigurationRecords.Where(c => c.IsActive == true).ToListAsync();
    }

    public async Task<ConfigurationRecord> GetConfiguration(int id)
    {
        return await _context.ConfigurationRecords.FindAsync(id);
    }

    public async Task AddConfiguration(ConfigurationRecord configurationRecord)
    {
        _context.ConfigurationRecords.Add(configurationRecord);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateConfiguration(ConfigurationRecord configurationRecord)
    {
        _context.Entry(configurationRecord).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteConfiguration(int id)
    {
        var configurationRecord = await _context.ConfigurationRecords.FindAsync(id);
        if (configurationRecord != null)
        {
            _context.ConfigurationRecords.Remove(configurationRecord);
            await _context.SaveChangesAsync();
        }
    }
}