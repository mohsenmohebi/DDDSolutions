using DDDApi.Commands;
using DDDApi.Dtos;
using DDDApi.Entities;
using DDDApi.Exceptions;
using DDDApi.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserViewModel>> GetUserById(int id)
    {
        var user = await _mediator.Send(new GetByIdQuery<User>(id));
        if (user == null)
        {
            return NotFound();
        }

        var viewModel = new UserViewModel
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email
        };

        return Ok(viewModel);
    }

    [HttpPost("resetpassword")]
    public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
    {
        try
        {
            var command = new ResetPasswordCommand(dto);
            await _mediator.Send(command);
            return Ok();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}