using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CVAPI.Models;
using Newtonsoft.Json;
using Microsoft.Identity.Client;
using FastNetCoreLibrary;
using NuGet.Protocol.Plugins;

namespace CVAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ConvandbContext _context;

        public UsersController(ConvandbContext context)
        {
            _context = context;
        }

        [HttpPost("GetUserBranch")]
        public async Task<IActionResult> GetUserBranch([FromForm]string username)
        {
            try
            {
                var user = await _context.Users.Where(x => x.Username == username).FirstOrDefaultAsync();

                if(user == null)
                {
                    if(_context.Users.Any(x => x.Username == username))
                    {
                        return BadRequest("Wrong Email");
                    }
                }

                var data = await _context.UserBranches.Where(x => x.UserId == user.Id).Select(x => new
                {
                    BranchId = x.Branch.Id,
                    BranchCode = x.Branch.Code,
                    BranchName = x.Branch.Name
                }).ToListAsync();

                return Ok(data);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("GetUserMenu")]
        public async Task<IActionResult> GetUserMenu([FromForm]int userId)
        {

            try
            {
                var menu = await _context.UserMenus.Where(x => x.UserId == userId).ToListAsync();
                List<ParentMenu> uMenu = new List<ParentMenu>();

                foreach (var parent in menu)
                {
                    List<SubMenu> sub = new List<SubMenu>();

                    var temp = _context.Menus.Where(x => x.Id == parent.MenuId).FirstOrDefault();

                    sub.Add(new SubMenu
                    {
                        MenuId = temp.Id,
                        MenuName = temp.MenuName,
                        CanAdd = parent.CanAdd,
                        CanEdit = parent.CanEdit,
                        CanDelete = parent.CanDelete,
                        MenuSort = temp.Sort,
                    });

                    var tempParent = _context.Menus.FirstOrDefault(x => x.Id == temp.ParentMenuId);

                    // Check if uMenu already contains a ParentMenu with the same ParentMenuId
                    var existingParent = uMenu.FirstOrDefault(m => m.ParentMenuId == tempParent!.Id);

                    if (existingParent == null)
                    {
                        // If it doesn't exist, create a new ParentMenu and add it to uMenu
                        uMenu.Add(new ParentMenu
                        {
                            ParentMenuId = tempParent!.Id,
                            ParentMenuName = tempParent!.MenuName,
                            ParentMenuSort = tempParent!.Sort,
                            IsOpen = false,
                            SubMenus = sub
                        });
                    }
                    else
                    {
                        // If it exists, simply add the SubMenu to the existing ParentMenu
                        existingParent.SubMenus.AddRange(sub);
                    }
                }

                // Sort the submenus by MenuSort
                foreach (var parentMenu in uMenu)
                {
                    parentMenu.SubMenus = parentMenu.SubMenus.OrderBy(x => x.MenuSort).ToList();
                }

                return Ok(uMenu.OrderBy(x => x.ParentMenuSort));
            }
            catch (Exception e)
            {
                return BadRequest(new { errorMessage = e.Message, innerError = e.InnerException });
            }
            /*try
            {
                var menu = await _context.UserMenus.Where(x => x.UserId == userId).ToListAsync();
                List<ParentMenu> uMenu = new();

                foreach (var parent in menu)
                {
                    List<SubMenu> sub = new();

                    var temp = _context.Menus.Where(x => x.Id == parent.MenuId).FirstOrDefault();

                    sub.Add(new SubMenu
                    {
                        MenuId = temp.Id,
                        MenuName = temp.MenuName,
                        CanAdd = parent.CanAdd,
                        CanEdit = parent.CanEdit,
                        CanDelete = parent.CanDelete,
                        MenuSort = temp.Sort,
                    });

                    var tempParent = _context.Menus.FirstOrDefault(x => x.Id == temp.ParentMenuId);


                    if(uMenu.Count() == 0)
                    {
                        uMenu.Add(new ParentMenu
                        {
                            ParentMenuId = tempParent!.Id,
                            ParentMenuName = tempParent!.MenuName,
                            ParentMenuSort = tempParent!.Sort,
                            IsOpen = false,
                            SubMenus = sub
                        });
                    }
                    else
                    {
                        foreach(var m in uMenu)
                        {
                            if(m.ParentMenuId != tempParent!.Id)
                            {
                                uMenu.Add(new ParentMenu
                                {
                                    ParentMenuId = tempParent!.Id,
                                    ParentMenuName = tempParent!.MenuName,
                                    ParentMenuSort = tempParent!.Sort,
                                    IsOpen = false,
                                    SubMenus = sub
                                });
                            }
                            else
                            {
                                m.SubMenus!.Add(new SubMenu
                                {
                                    MenuId = temp.Id,
                                    MenuName = temp.MenuName,
                                    CanAdd = parent.CanAdd,
                                    CanEdit = parent.CanEdit,
                                    CanDelete = parent.CanDelete,
                                    MenuSort = temp.Sort,
                                });
                            }
                        }
                    }
                   
                }

                return Ok(uMenu);
            }catch(Exception e)
            {
                return BadRequest(new { errorMessage = e.Message, innerError = e.InnerException });
            }*/
        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromForm] String userRegistration, [FromForm] int branchId)
        {
            try
            {
                // Deserialize the registration data
                User newUser = JsonConvert.DeserializeObject<User>(userRegistration)!;

                // Check for duplicate email
                var duplicateUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == newUser.Email || x.Username == newUser.Username);
                if (duplicateUser != null)
                {
                    if(duplicateUser.Username == newUser.Username)
                    {
                        return BadRequest("Username already taken");
                    }
                    return BadRequest("Email already taken");
                }

                // Set default user role
                newUser.Userroleid = 1;

                //Password Encryptor - Hash
                newUser.Password = PasswordEncryptor.HashPassword(newUser.Password);
                // Add the user
                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();

                // Define a list of menu IDs to add
                var menuIds = new List<int> { 7, 8, 9 };

                // Add user menu entries for each menu ID
                foreach (var menuId in menuIds)
                {
                    await _context.AddAsync(new Models.UserMenu
                    {
                        MenuId = menuId,
                        UserId = newUser.Id,
                        CanAdd = 1,
                        CanEdit = 1,
                        CanDelete = 0
                    });
                }

                // Save changes to user menus
                await _context.SaveChangesAsync();

                // Add user branch entry
                /*await _context.AddAsync(new Models.UserBranch
                {
                    BranchId = branchId,
                    UserId = newUser.Id
                });
*/
                // Save changes to user branches
                //await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message, InnerException = e.InnerException });
            }
        }

        [HttpPost("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var userlist = await _context.Users.Where(x => x.Userroleid != 4).Select(x => new
                {
                    x.Id,
                    x.Lastname,
                    x.Firstname,
                    x.Middlename,
                    x.Nickname,
                    x.Contactnumber,
                    x.Email,
                    x.Address,
                    userrole = new {x.Userrole.Id, x.Userrole.Name}
                }).ToListAsync();

                return Ok(userlist);
            }catch(Exception e)
            {
                return BadRequest(new { ErrorMessagge = e.Message, InnerException = e.InnerException });
            }
        }

        [HttpPost("ChangeUserRole")]
        public async Task<IActionResult> ChangeUserRole(int userid, int userroleid, int modifiedbyuserid)
        {
            try
            {
                var data = await _context.Users.Where(x => x.Id == userid).FirstOrDefaultAsync();
                data.Userroleid = userroleid;
                data.Modifiedbyuserid = modifiedbyuserid;
                data.Modifieddate = DateTime.Now;


                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessagge = e.Message, InnerException = e.InnerException });
            }
        }

        [HttpPost("ChangeUserDetails")]
        public async Task<IActionResult> ChangeUserDetails(string userdetails, int modifiedbyuserid)
        {
            try
            {
                User userdata = JsonConvert.DeserializeObject<User>(userdetails)!;

                var data = await _context.Users.Where(x => x.Id == userdata.Id).FirstOrDefaultAsync();

                data.Lastname = userdata.Lastname;
                data.Firstname = userdata.Firstname;
                data.Middlename = userdata.Middlename;
                data.Nickname = userdata.Nickname;
                data.Contactnumber = userdata.Contactnumber;
                data.Modifiedbyuserid = modifiedbyuserid;
                data.Modifieddate = DateTime.Now;

                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessagge = e.Message, InnerException = e.InnerException });
            }
        }

        /*[HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromForm]String userRegistration, [FromForm] int branchId)
        {
            try
            {
                User newUser = JsonConvert.DeserializeObject<User>(userRegistration)!;

                var dup = _context.Users.Where(x => x.Email == newUser.Email).FirstOrDefault();

                if (dup != null)
                {
                    return BadRequest("Email already taken");
                }
                newUser.Userrole = 1;
                await _context.Users.AddAsync(newUser);

                await _context.SaveChangesAsync();

                await _context.AddAsync(new Models.UserMenu
                {
                    MenuId = 7,
                    UserId = newUser.Id,
                    CanAdd = 1,
                    CanEdit = 1,
                    CanDelete = 0
                });

                await _context.AddAsync(new Models.UserMenu
                {
                    MenuId = 8,
                    UserId = newUser.Id,
                    CanAdd = 1,
                    CanEdit = 1,
                    CanDelete = 0
                });

                await _context.AddAsync(new Models.UserMenu
                {
                    MenuId = 9,
                    UserId = newUser.Id,
                    CanAdd = 1,
                    CanEdit = 1,
                    CanDelete = 0
                });



                await _context.AddAsync(new Models.UserMenu
                {
                    MenuId = 10,
                    UserId = newUser.Id,
                    CanAdd = 1,
                    CanEdit = 1,
                    CanDelete = 0
                });
                await _context.SaveChangesAsync();

                await _context.AddAsync(new Models.UserBranch
                {
                    BranchId = branchId,
                    UserId = newUser.Id
                });
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message, InnerException = e.InnerException });
            }
        }*/
    }


    public class ParentMenu
    {
        public int? ParentMenuId { get; set; }
        public String? ParentMenuName { get; set; }
        public int? ParentMenuSort { get; set; }
        public bool IsOpen { get; set; }
        public List<SubMenu>? SubMenus { get; set; }
    }

    public class SubMenu
    {
        public int? MenuId { get; set; }
        public String? MenuName { get; set; }
        public int? CanAdd { get; set; }
        public int? CanEdit { get; set; }
        public int? CanDelete { get; set; }
        public int? MenuSort { get; set; }

    }

    public class NewUser
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? Nickname { get; set; }
        public int? UserRole { get; set; }
        public int? BranchId { get; set; }
    }
}
