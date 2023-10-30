using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace mongoDB_API.Controllers;

using mongoDB_API.Models;
using mongoDB_API.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PhonesController : ControllerBase
{
    private readonly PhonesService _phonesService;
    private readonly UsersService _usersService;

    public PhonesController(PhonesService phonesService) =>
        _phonesService = phonesService;

    [HttpGet]
    public async Task<List<Phone>> Get() =>
        await _phonesService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Phone>> Get(string id)
    {
        var phone = await _phonesService.GetAsync(id);

        if (phone is null)
        {
            return NotFound();
        }

        return phone;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Phone newPhone)
    {
        await _phonesService.CreateAsync(newPhone);

        return CreatedAtAction(nameof(Get), new { id = newPhone.Id }, newPhone);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Phone updatedPhone)
    {
        var phone = await _phonesService.GetAsync(id);

        if (phone is null)
        {
            return NotFound();
        }

        updatedPhone.Id = phone.Id;

        await _phonesService.UpdateAsync(id, updatedPhone);

        return NoContent();
    }

 //   [HttpDelete("{id:length(24)}")]
 //   public async Task<IActionResult> Delete(string id)
  //  {
  //      var phone = await _phonesService.GetAsync(id);

  //      if (phone is null)
  //      {
  //          return NotFound();
   //     }

    //    await _phonesService.RemoveAsync(id);

     //   return NoContent();
   // }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> DeleteCascade(string id)
    {
        var phone = await _phonesService.GetAsync(id);

        if (phone is null)
        {
            return NotFound();
        }

        string userId = phone.User.Id;
        
        if (userId is null)
        {
            return NotFound();
        }
        
        await _phonesService.RemoveAsync(id);
        
        await _usersService.RemoveAsync(userId);

        return NoContent();
    }
}