using Microsoft.AspNetCore.Mvc;
using SecilStoreExam.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/Config")]
public class ConfigurationController : ControllerBase
{
    private readonly IConfigurationService _configurationService;

    public ConfigurationController(IConfigurationService configurationService)
    {
        _configurationService = configurationService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ConfigurationRecord>>> GetConfigurations()
    {
        return Ok(await _configurationService.GetConfigurations());
    }


    [HttpPost]
    public async Task<ActionResult<ConfigurationRecord>> AddConfiguration(ConfigurationRecord config)
    {
        await _configurationService.AddConfiguration(config);
        return CreatedAtAction(nameof(GetConfigurations), new { id = config.Id }, config);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateConfiguration(int id, ConfigurationRecord config)
    {
        if (id != config.Id)
        {
            return BadRequest();
        }

        await _configurationService.UpdateConfiguration(config);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteConfiguration(int id)
    {
        await _configurationService.DeleteConfiguration(id);
        return NoContent();
    }
}