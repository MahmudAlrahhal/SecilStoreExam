
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using ConfigurationLibrary;
using SecilStoreExam.Models;


public class ConfigurationReader
{
    private readonly string _serviceName;
    private readonly string _connectionString;
    private readonly int _refreshInterval;
    private readonly IMemoryCache _cache;
    private Timer _timer;
    private readonly IMongoCollection<ConfigurationRecord> _collection;

    public ConfigurationReader(string serviceName, string connectionString, int refreshInterval)
    {
        _serviceName = serviceName;
        _connectionString = connectionString;
        _refreshInterval = refreshInterval;
        _cache = new MemoryCache(new MemoryCacheOptions());

        var client = new MongoClient(_connectionString);
        var database = client.GetDatabase("ConfigurationDB");
        _collection = database.GetCollection<ConfigurationRecord>("Configurations");

        LoadConfiguration().Wait();
        _timer = new Timer(async _ => await RefreshConfiguration(), null, _refreshInterval, _refreshInterval);
    }

    private async Task LoadConfiguration()
    {
        var filter = Builders<ConfigurationRecord>.Filter.Eq("ApplicationName", _serviceName) &
                     Builders<ConfigurationRecord>.Filter.Eq("IsActive", true);
        var configurations = await _collection.Find(filter).ToListAsync();

        foreach (var config in configurations)
        {
            _cache.Set(config.Name, config, new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.High));
        }
    }

    private async Task RefreshConfiguration()
    {
        await LoadConfiguration();
    }

    public T GetValue<T>(string key)
    {
        if (_cache.TryGetValue(key, out ConfigurationRecord config))
        {
            return (T)Convert.ChangeType(config.Value, typeof(T));
        }
        throw new KeyNotFoundException($"Key {key} not found in configuration.");
    }
}