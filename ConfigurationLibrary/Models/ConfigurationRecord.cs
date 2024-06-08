using System;
using System.Collections.Generic;

namespace SecilStoreExam.Models;

public partial class ConfigurationRecord
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Type { get; set; }

    public string? Value { get; set; }

    public bool? IsActive { get; set; }

    public string? ApplicationName { get; set; }
}
