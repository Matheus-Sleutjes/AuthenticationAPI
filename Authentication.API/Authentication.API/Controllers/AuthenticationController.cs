using Authentication.Application.Contract;
using Authentication.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var entity = _authenticationService.Find(id);
            return Ok(entity);
        }

        [HttpPost]
        public IActionResult Post([FromBody]UserDto request)
        {
            _authenticationService.Add(request);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody]UserDto request, Guid id) 
        {
            var entity = _authenticationService.Update(request, id);
            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _authenticationService.Delete(id);
            return Ok();
        }
    }
}
