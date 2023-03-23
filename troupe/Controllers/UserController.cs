using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using troupe.Repositories;

namespace troupe.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]

    public IActionResult Get()
    {
        return Ok(_userRepository.GetAllUsers());
    }
}
