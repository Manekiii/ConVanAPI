using CVAPI.Models;
using CVAPI.TokenAuthentication;
using FastNetCoreLibrary;
using Microsoft.AspNetCore.Mvc;

namespace CVAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        Response response = new Response();
        private readonly IConfiguration _configuration;
        private readonly ConvandbContext _coredbcontext;
        private readonly ITokenManager tokenManager;

        public AccessController(IConfiguration configuration, ConvandbContext _context, ITokenManager tokenManager)
        {
            _coredbcontext = _context;
            _configuration = configuration;
            this.tokenManager = tokenManager;
        }

        [HttpPost]
        public ActionResult Get([FromBody] Login login)
        {
            try
            {
                string loginname = login.email;
                string password = PasswordEncryptor.HashPassword(login.password); 
                string IncPassword = string.Empty;

                /* var getUserAlias = _coredbcontext.Users
                     .Where(a => a.Email == loginname);

                 if (getUserAlias.Count() > 0)
                 {
                     loginname = getUserAlias.First().Username;
                 }

                 //CHECK THE DEBUG PASSWORD
                 var userDebugPassword = _coredbcontext.CoreSystems.Where(d => d.Debugpassword == password);

                 if (userDebugPassword.Count() > 0)
                 {
                     var userdebugpass = _coredbcontext.CoreVUsers.Where(u => u.Username == loginname).FirstOrDefault();
                     IncPassword = userdebugpass.Userpass;
                 }
                 else
                 {
                     IncPassword = PasswordEncryptor.HashPassword(password);
                 }*/

                //var qUserProfile = _coredbcontext.CoreVUsers.Where(m => m.username == loginname & m.userpass == IncPassword);
                var qUserProfile = _coredbcontext.Users.Where(x => x.Username == loginname && x.Password == password).Select(x => new
                {
                    UserId = x.Id,
                    x.Email,
                    x.Firstname,
                    x.Lastname,
                    x.Middlename,
                    x.Nickname,
                    x.Userroleid,
                    x.Status
                }).ToList(); 


                if (qUserProfile.Count() > 0)
                {

                    if (qUserProfile.FirstOrDefault().Status == 0 && qUserProfile.FirstOrDefault()!.Userroleid == 1)
                    {
                        response.message = "This account is Inactive, contact your admin and try again.";
                        response.status = "FAILURE";
                        return StatusCode(400, response);
                    }

                    if (tokenManager.Authenticate(loginname, password))
                    {
                        var tokenresult = tokenManager.tokensresult();

                        Response.Headers.Add("token", tokenresult);
                        response.token = tokenresult;

                        //return Ok(new { TokenList = tokenManager.NewToken() });
                    }
                    else
                    {
                        ModelState.AddModelError("Unauthorized", "You are not authorized");
                        return Unauthorized(ModelState);
                    }

                    response.objParam1 = qUserProfile.FirstOrDefault();
                    response.stringParam2 = PasswordEncryptor.GetMd5Hash(qUserProfile.FirstOrDefault().UserId.ToString()).ToString();
                    response.status = "SUCCESS";
                    //response.stringParam1 = TokenGenerator.Encrypt(loginname) + ":" + TokenGenerator.Encrypt(password);
                    return StatusCode(200, response);
                }
                else
                {
                    if (_coredbcontext.Users.Any(u => u.Username == loginname && u.Password != password))
                    {
                        response.message = "Wrong Password";
                        response.status = "FAILURE";
                        return StatusCode(400, response);
                    }
                    else if (_coredbcontext.Users.Any(u => u.Username != loginname && u.Password == password))
                    {
                        response.message = "Wrong email";
                        response.status = "FAILURE";
                        return StatusCode(400, response);
                    }
                    else
                    {
                        response.message = "Wrong email and Password";
                        response.status = "FAILURE";
                        return StatusCode(400, response);
                    }
                }
            }
            catch (Exception e)
            {
               return StatusCode(500, "error" + e.Message);
            }
        }
    }
    public class Login
    {
        public string email { get; set; }
        public string password { get; set; }
    }


    public class Response
    {
        public string status { get; set; }
        public string message { get; set; }
        public string stringParam1 { get; set; }
        public string stringParam2 { get; set; }
        public string token { get; set; }
        public object objParam1 { get; set; }
        public object objParam2 { get; set; }
        public object objMenuList { get; set; }
    }
}