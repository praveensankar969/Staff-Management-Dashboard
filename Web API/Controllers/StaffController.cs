using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using API.Services;
using Microsoft.Extensions.Configuration;
using API.Models;
using Persistence.APIHandler;
using Model.DTO;
using System;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StaffController : ControllerBase
    {
        private readonly StaffContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private readonly TokenService _tokenService;
        private readonly IConfiguration _config;

        public StaffController(StaffContext context, IHttpContextAccessor httpContext, TokenService tokenService, IConfiguration config)
        {
            this._config = config;
            this._tokenService = tokenService;
            this._httpContext = httpContext;
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<StaffWebAPI>>> GetAllStaff()
        {
            var user = this._httpContext.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var userPrivilege = this._httpContext.HttpContext.User.FindFirstValue("Type");
            if (user == null || !userPrivilege.Equals("Admin"))
            {
                return Unauthorized();
            }
            SQLProcedure pro = new SQLProcedure(this._config);
            return await pro.GetAllData();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StaffWebAPI>> GetStaff(int id)
        {
            var user = this._httpContext.HttpContext.User.FindFirstValue(ClaimTypes.Name);

            if (user == null)
            {
                return Unauthorized();
            }
            SQLProcedure pro = new SQLProcedure(this._config);
            var staff = await pro.GetDataOfId(id);
            if (staff != null)
            {
                return staff;
            }
            else
            {
                return NotFound();
            }

        }

        [AllowAnonymous]
        [HttpPost("addstaff")]
        public async Task<ActionResult<string>> AddStaffDetails(StaffUpdateDTO staff)
        {

            SQLProcedure pro = new SQLProcedure(this._config);
            int res = await pro.Insert(staff);

            return Ok();

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<string>> UpdateStaffDetail(int id, StaffUpdateDTO staffDTO)
        {
            var user = this._httpContext.HttpContext.User.FindFirstValue(ClaimTypes.Name);

            if (user == null)
            {
                return Unauthorized();
            }
            SQLProcedure pro = new SQLProcedure(this._config);
            var staff = await pro.GetDataOfId(id);

            if (staff != null)
            {
                await pro.Update(id, staffDTO);

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteStaff(int id)
        {
            var user = this._httpContext.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var userPrivilege = this._httpContext.HttpContext.User.FindFirstValue("Type");
            if (user == null || !userPrivilege.Equals("Admin"))
            {
                return Unauthorized();
            }
            SQLProcedure pro = new SQLProcedure(this._config);
            await pro.DeleteDataOfId(id);
            return Ok();
        }

    }
}