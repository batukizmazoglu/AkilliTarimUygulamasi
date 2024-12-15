using AkilliTarimUygulamasi.Models;
using AkilliTarimUygulamasi.Services;
using Microsoft.AspNetCore.Mvc;

[Route("Users")]
public class UsersController : Controller
{
    private readonly IGenericService<User> _userService;

    public UsersController(IGenericService<User> userService)
    {
        _userService = userService;
    }

    

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] User newUser)
    {
        if (newUser == null)
        {
            return BadRequest("User data is required");
        }

        // Kullanıcıyı oluştur
        var createdUser = await _userService.CreateAsync(newUser);

        if (createdUser == null)
        {
            return StatusCode(500, "An error occurred while creating the user.");
        }

        return Ok(createdUser);
    }

    // View Döndüren Action
    [HttpGet("Read")]
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPut("Update")]

    public async Task<IActionResult> Update([FromBody] User updatedUser)
    {
        if (updatedUser == null || updatedUser.Id <= 0)
        {
            return BadRequest("Invalid user data.");
        }

        var existingUser = await _userService.GetByIdAsync(updatedUser.Id);
        if (existingUser == null)
        {
            return NotFound("User not found.");
        }

        // Güncelleme işlemi
        existingUser.Name = updatedUser.Name;
        existingUser.Email = updatedUser.Email;
        existingUser.Role = updatedUser.Role;

        var success = await _userService.UpdateAsync(existingUser); // Güncelleme çağrısı
        if (!success)
        {
            return StatusCode(500, "An error occurred while updating the user.");
        }

        return Ok(existingUser);
    }


    
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _userService.DeleteAsync(id);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }

    
    // API Endpoint: JSON veri döndüren action
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll(string search = "", string sortColumn = "id", string sortOrder = "asc")
    {
        var users = await _userService.GetAllAsync(); // Bu metot her zaman güncel veriyi alıyor mu?

        if (!string.IsNullOrEmpty(search))
        {
            users = users.Where(u => u.Name.Contains(search) || u.Email.Contains(search)).ToList();
        }

        switch (sortColumn.ToLower())
        {
            case "id":
                users = sortOrder == "asc"
                    ? users.OrderBy(u => u.Id).ToList()
                    : users.OrderByDescending(u => u.Id).ToList();
                break;
            case "name":
                users = sortOrder == "asc"
                    ? users.OrderBy(u => u.Name).ToList()
                    : users.OrderByDescending(u => u.Name).ToList();
                break;
            case "email":
                users = sortOrder == "asc"
                    ? users.OrderBy(u => u.Email).ToList()
                    : users.OrderByDescending(u => u.Email).ToList();
                break;
            default:
                users = users.OrderBy(u => u.Id).ToList();
                break;
        }

        return Ok(users);
    }

}