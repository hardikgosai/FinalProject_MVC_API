using AutoMapper;
using Domain.Models;
using Getri_FinalProject_MVC_API.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Repository;

namespace Getri_FinalProject_MVC_API.APIController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserProfileRepository userProfileRepository;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserController(IUserProfileRepository _userProfileRepository, IUserRepository _userRepository, IMapper _mapper)
        {
            userProfileRepository = _userProfileRepository;
            userRepository = _userRepository;
            mapper = _mapper;
        }

        [HttpGet("ListUsers")]
        public ActionResult GetUsers()
        {
            List<User> lstUser = new List<User>();
            lstUser = userRepository.GetUsers().ToList();
            foreach (var u in lstUser)
            {
               User user = new User();
               UserProfile userProfile = userProfileRepository.GetUserProfile(u.Id);
                user.Id = u.Id;
                user.UserName = u.UserName;
                user.Email = u.Email;
                user.Password = u.Password;
                user.ModifiedDate = u.ModifiedDate;
                user.IPAddress = u.IPAddress;
                user.Profile = new UserProfile();                
                userProfile.FirstName = userProfile.FirstName;
                userProfile.LastName = userProfile.LastName;
                userProfile.ContactNo = userProfile.ContactNo;
                userProfile.Address = userProfile.Address;
                userProfile.ModifiedDate = u.ModifiedDate; 
                userProfile.IPAddress = u.IPAddress;
                user.Profile.Id = u.Id;
                lstUser.Add(user);
            }

            return Ok(lstUser);
        }

        [HttpGet("GetUserById")]
        public ActionResult GetUser(int id)
        {
            return Ok(userRepository.GetUser(id));
        }

        [HttpPost("CreateUsers")]
        public int CreateUser(CreateUserDTO model)
        {
            User userEntity = new User();
            userEntity.UserName = model.UserName;
            userEntity.Email = model.Email;
            userEntity.Password = model.Password;
            userEntity.ModifiedDate = DateTime.UtcNow;
            userEntity.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            userEntity.Profile = new UserProfile();
            userEntity.Profile.FirstName = model.FirstName;
            userEntity.Profile.LastName = model.LastName;
            userEntity.Profile.ContactNo = model.ContactNo;
            userEntity.Profile.Address = model.Address;
            userEntity.Profile.ModifiedDate = DateTime.UtcNow;
            userEntity.Profile.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            userRepository.InsertUser(userEntity);
            return 1;
        }

        [HttpPut("UpdateUser")]
        public int UpdateUser(UpdateUserDTO model)
        {
            User userEntity = new User();
            userEntity.Id = model.Id;
            userEntity.UserName = model.UserName;
            userEntity.Email = model.Email;
            userEntity.Password = model.Password;
            userEntity.ModifiedDate = DateTime.UtcNow;
            userEntity.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            userEntity.Profile = new UserProfile();
            userEntity.Profile.Id = model.Id;
            userEntity.Profile.FirstName = model.FirstName;
            userEntity.Profile.LastName = model.LastName;
            userEntity.Profile.ContactNo = model.ContactNo;
            userEntity.Profile.Address = model.Address;
            userEntity.Profile.ModifiedDate = DateTime.UtcNow;
            userEntity.Profile.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            userRepository.UpdateUser(userEntity);

            return 1;
        }

        [HttpDelete("DeleteUser")]
        public int DeleteUser(int id)
        {
            userRepository.DeleteUser(id);
            return 1;
        }   
    }
}
