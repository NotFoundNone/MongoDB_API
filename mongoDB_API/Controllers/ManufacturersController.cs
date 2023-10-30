using Microsoft.AspNetCore.Mvc;
using mongoDB_API.Models;
using mongoDB_API.Services;

namespace mongoDB_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ManufacturersController : ControllerBase 
{
    private readonly ManufacturersService _manufacturersService;

    public ManufacturersController(ManufacturersService manufacturersService) =>
        _manufacturersService = manufacturersService;

    [HttpGet]
    public async Task<List<Manufacturer>> Get() => await _manufacturersService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Manufacturer>> Get(string id)
    {
        var manufacturer = await _manufacturersService.GetAsync(id);

        if (manufacturer is null)
        {
            return NotFound();
        }

        return manufacturer;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Manufacturer newManufacturer)
    {
        await _manufacturersService.CreateAsync(newManufacturer);

        return CreatedAtAction(nameof(Get), new { id = newManufacturer.Id }, newManufacturer);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Manufacturer updatedManufacturer)
    {
        var manufacturer = await _manufacturersService.GetAsync(id);

        if (manufacturer is null)
        {
            return NotFound();
        }

        updatedManufacturer.Id = manufacturer.Id;

        await _manufacturersService.UpdateAsync(id, updatedManufacturer);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var manufacturer = await _manufacturersService.GetAsync(id);

        if (manufacturer is null)
        {
            return NotFound();
        }

        await _manufacturersService.RemoveAsync(id);

        return NoContent();
    }
}
