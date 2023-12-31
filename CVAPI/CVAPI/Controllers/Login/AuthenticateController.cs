﻿using CVAPI.TokenAuthentication;
using FastNetCoreLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Text;
using CVAPI.Models;

namespace CVAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly ITokenManager tokenManager;
        public AuthenticateController(ITokenManager tokenManager)
        {
            this.tokenManager = tokenManager;
        }
        [HttpGet]
        public ActionResult Get([FromHeader] string Authorization)
        {
            try
            {
                var strngpram = Authorization.ToString();
                var credentials = strngpram.Split(':');

                var username = TokenGenerator.Decrypt(credentials[0]);
                var password = TokenGenerator.Decrypt(credentials[1]);

                if (tokenManager.Authenticate(username, password))
                {
                    var tokenresult = tokenManager.tokensresult();

                    Response.Headers.Add("token", tokenresult);
                    return StatusCode(200, new { tokenresult });

                    //return Ok(new { TokenList = tokenManager.NewToken() });
                }
                else
                {
                    ModelState.AddModelError("Unauthorized", "You are not authorized");
                    return Unauthorized(ModelState);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, "error" + e.Message);
            }

        }

    }
}