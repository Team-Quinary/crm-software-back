using crm_software_back.Data;
using crm_software_back.DTOs;
using crm_software_back.Models;
using crm_software_back.Services.LoginUserServices;
using EmailService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace crm_software_back.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly ILoginUserService _loginUserService;
        private readonly IEmailSender _emailSender;

        public UserService(DataContext context, ILoginUserService loginUserService, IEmailSender emailSender)
        {
            _context = context;
            _loginUserService = loginUserService;
            _emailSender = emailSender;
        }

        public async Task<User?> getUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            return user;
        }

        public async Task<List<User>?> getUsers()
        {
            var notes = await _context.Users.ToListAsync();

            return notes;
        }

        public async Task<User?> postUser(User newUser)
        {
            var user = await _context.Users.Where(user => 
                user.Username.Equals(newUser.Username) || user.Email.Equals(newUser.Email)
            ).FirstOrDefaultAsync();

            if (user != null)
            {
                return null;
            }

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            var newDTOuser = new DTOUser()
            {
                Username = newUser.Username,
                Password = newUser.Password
            };

            await _loginUserService.postLoginUser(newDTOuser);

            _emailSender.SendEmail(newUser.Email, newUser.Username, newUser.FirstName);

            return await _context.Users.Where(user => user.Email.Equals(newUser.Email)).FirstOrDefaultAsync();
        }

        public async Task<User?> putUser(int userId, User newUser)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return null;
            }

            user.Type = (newUser.Type == "") ? user.Type : newUser.Type;
            user.Username = (newUser.Username == "") ? user.Username : newUser.Username;
            user.Password = (newUser.Password == "") ? user.Password : newUser.Password;
            user.FirstName = (newUser.FirstName == "") ? user.FirstName : newUser.FirstName;
            user.LastName = (newUser.LastName == "") ? user.LastName : newUser.LastName;
            user.ContactNo = (newUser.ContactNo == "") ? user.ContactNo : newUser.ContactNo;
            user.Email = (newUser.Email == "") ? user.Email : newUser.Email;



            //var image = newUser.ProfilePic;

            //if (image == null)
            //{
            //    return null;
            //}

            //byte[] fileBytes;

            //using (var ms = new MemoryStream())
            //{
            //    image.CopyTo(ms);
            //    fileBytes = ms.ToArray();
            //}

            //var filePath = $"./Gallery/{image.EventId}.png";

            //using (var stream = new FileStream(filePath, FileMode.Create))
            //{
            //    await stream.WriteAsync(fileBytes);
            //}

            //var newImage = new Image
            //{
            //    EventId = image.EventId,
            //    Path = $"{image.EventId}.png",
            //    Description = image.Description
            //};

            //await _context.Images.AddAsync(newImage);
            //_context.SaveChanges();



            //user.ProfilePic = (newUser.ProfilePic == "") ? user.ProfilePic : newUser.ProfilePic;

            await _context.SaveChangesAsync();

            var newDTOuser = new DTOUser()
            {
                Username = newUser.Username,
                Password = newUser.Password
            };

            await _loginUserService.putLoginUser(userId, newDTOuser);

            return user;
        }

        public async Task<User?> deleteUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return null;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<DTODashBoard> getDashboardData()
        {
            var projectCount = await _context.Projects.CountAsync();
            var customerCount = await _context.Customers.CountAsync();
            var techLeadCount = await _context.Users.Where(user => user.Type == "Tech Lead").CountAsync();

            var completed = await _context.Projects.Where(project => project.Status == "Completed").CountAsync();
            var ongoing = await _context.Projects.Where(project => project.Status == "Ongoing").CountAsync();
            var suspended = await _context.Projects.Where(project => project.Status == "Suspended").CountAsync();

            var days = new List<String>();
            var newProjects = new List<int>();
            var payments = new List<double>();

            for (int i = 0; i < 5; i++)
            {
                var day = DateTime.Now.Date.AddDays(-i);

                days.Insert(0, $"{day:MMMM dd}");

                var newProjectCount = await _context.Projects.Where(project =>
                    project.StartDate.Date == day.Date
                ).CountAsync();

                newProjects.Insert(0, newProjectCount);

                double paymentsTotal = 0;

                var paymentsOfDay = await _context.Payments.Where(payment =>
                    payment.Date.Date == day.Date
                ).ToListAsync();

                if (!paymentsOfDay.IsNullOrEmpty())
                {
                    foreach (var payment in paymentsOfDay)
                    {
                        paymentsTotal += payment.Amount;
                    }
                }

                payments.Insert(0, paymentsTotal);
            }

            var result = new DTODashBoard
            {
                ProjectCount = projectCount,
                CustomerCount = customerCount,
                TechLeadCount = techLeadCount,
                Completed = completed,
                Ongoing = ongoing,
                Suspended = suspended,
                LastDays = days,
                NewProjects = newProjects,
                Payments = payments
            };

            return result;
        }
    }
}
