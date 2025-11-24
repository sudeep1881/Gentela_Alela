using gentela_Alela.DTOs;
using gentela_Alela.Image_Services;
using gentela_Alela.Infrastructure.Email;
using gentela_Alela.Models;
using gentela_Alela.Views.View_Model;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;




namespace gentela_Alela.Controllers
{
    public class LoginController(GentleProjectContext db, IEmailSender email, IWebHostEnvironment web) : Controller
    {
        private readonly GentleProjectContext _db = db;

        private readonly IWebHostEnvironment _web = web;

        private readonly IEmailSender _email = email;

        #region--Role(DDL) Method------
        public async Task<IEnumerable<SelectListItem>> GetRole()
        {
            var RoleName = _db.Roles.Where(s => s.Isdeleted == false && s.Id == 2).Select(s => new SelectListItem
            {
                Text = s.RoleName,
                Value = s.Id.ToString()
            }).ToList();
            return RoleName;
        }
        #endregion

        #region----Login Register Page--------

        #region--Get Method------------
        public async Task<IActionResult> Index(int? id)
        {
            var loginvm = new PersonalDetailsVM
            {
                RoleList = await GetRole()
            };
            if (id.HasValue && id != null)
            {
                var rode = _db.PersonalDetails.Where(s => s.Isdeleted == false && s.Id == id).FirstOrDefault();
                if (rode == null)
                {
                    TempData["error"] = "Data not Found";
                    return RedirectToAction();
                }
                loginvm.personalReg = rode;
            }
            return View(loginvm);
        }
        #endregion

        #region---Post Method------

        [HttpPost]
        public async Task<IActionResult> Index(PersonalDetailsVM loginvm, IFormFile fileProfileImage)
        {
            loginvm.personalReg.Isdeleted = false;
            await SaveImageAsyncMethodLogin(loginvm, fileProfileImage);
            await DeleteImageAsyncMethodLogin(loginvm, fileProfileImage);

            if (loginvm.personalReg.Id == 0)
            {
                _db.PersonalDetails.Add(loginvm.personalReg);
            }
            else
            {
                _db.PersonalDetails.Update(loginvm.personalReg);
            }
            _db.SaveChanges();
            TempData["success"] = "Data Saved Successfully";
            return RedirectToAction();
        }



        private async Task SaveImageAsyncMethodLogin(PersonalDetailsVM loginvm, IFormFile fileProfileImage)
        {
            var profilePath = $"{_web.WebRootPath}{ImageService.ImageProfile}";
            var fileName = await ImageService.SaveImageAsync(fileProfileImage, profilePath);
            loginvm.personalReg.ProfileImage = $"{ImageService.ImageProfile}{fileName}";
        }

        private async Task DeleteImageAsyncMethodLogin(PersonalDetailsVM loginvm, IFormFile fileProfileImage)
        {
            var objfromdb = await _db.PersonalDetails.FirstOrDefaultAsync(s => s.Isdeleted == false && s.Id == loginvm.personalReg.Id);
            if (fileProfileImage == null)
            {
                loginvm.personalReg.ProfileImage = objfromdb?.ProfileImage;
            }
            else
            {
                ImageService.DeleteImage(_web.WebRootPath, objfromdb?.ProfileImage);
            }
        }




        #endregion


        #endregion


        #region-----Login Page -------

        #region----Get Method------
        public IActionResult LoginMethod()
        {
            var personalDetaisvm = new PersonalDetailsVM();

            return View(personalDetaisvm);
        }
        #endregion



        #region--Post Method---
        [HttpPost]
        public async Task<IActionResult> LoginMethod(RegistrationVM logimvm)
        {
            var user = await _db.PersonalDetails.Where(s => s.Isdeleted == false && s.Email!.Trim().ToLower() == logimvm.Emaill!.Trim().ToLower()
            && s.Password!.Trim().ToLower() == logimvm.Passwordd).Include(s => s.District).Include(s => s.Country).Include(s => s.State).Select(s => new LoginPersonalDetailsDTOs
            {
                Id = s.Id,
                FullName = s.FullName,
                Dob = s.Dob,
                Gender = s.Gender,
                CountryId = s.Country!.CountryName,
                StateId = s.State!.StateName,
                DistrictId = s.District!.DistrictName,
                Email = s.Email,
                Password = s.Password,
                RoleId = s.RoleId,
                PhoneNo = s.PhoneNo,
                ProfileImage = s.ProfileImage
            }).FirstOrDefaultAsync();

            if (user != null)
            {
                HttpContext.Session.SetInt32(SD.KeyUser, user.Id);
                HttpContext.Session.SetInt32(SD.KeyRole, (int)user.RoleId!);
                HttpContext.Session.SetString(SD.KeyName, user.FullName!);
                HttpContext.Session.SetString(SD.KeyGender, user.Gender!);
                HttpContext.Session.SetString(SD.KeyDob, user.Dob.ToString()!);
                HttpContext.Session.SetString(SD.KeyEmail, user.Email!);
                HttpContext.Session.SetString(SD.KeyCountry, user.CountryId!.ToString()!);
                HttpContext.Session.SetString(SD.KeyState, user.StateId!.ToString()!);
                HttpContext.Session.SetString(SD.KeyDistrict, user.DistrictId!.ToString()!);
                HttpContext.Session.SetString(SD.KeyPhoneNumber!, user.PhoneNo!.ToString()!);
                HttpContext.Session.SetString(SD.KeyprofileImag!, user.ProfileImage ?? "~/Photos/default-image.png/");



                if (user.RoleId == 1)
                {
                    return RedirectToAction(nameof(AdminController.Index), "Admin");
                }
                else if (user.RoleId == 2)
                {
                    return RedirectToAction(nameof(CitizineController.Home), "Citizine");
                }
                else
                {
                    TempData["error"] = "RoleID Doent Exits in your Account";
                    return RedirectToAction("Index", "LoginController");
                }

            }

            else
            {
                TempData["error"] = "Invalid Email or Password";
                return RedirectToAction("Index");
            }
        }

        #endregion


        #endregion




        #region-----Forget Password Method with Email Sender-----

        #region--Get Method -----
        public IActionResult ForgotPassword()
        {
            var model = new ForgotPasswordVM();
            return View(model);
        }
        #endregion


        #region--Post Method---
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _db.PersonalDetails.FirstOrDefault(u => u.Email == model.Email && u.Isdeleted == false);

            if (user == null)
            {
                TempData["error"] = "Email not found!";
                return View();
            }


            // Generate OTP
            var otp = new Random().Next(100000, 999999).ToString();



            // Store OTP in TempData (or DB if you want more secure)
            HttpContext.Session.SetString("OTP", otp);
            HttpContext.Session.SetString("Email", model.Email!);


            try
            {
                // 1. Load HTML template
                // If your file is in Views / EmailTest / WelcomeEmail.cshtml
                var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Template", "WelcomeEmail.cshtml");
                var htmlBody = await System.IO.File.ReadAllTextAsync(templatePath);

                // 2. Get username from email
                string userName = model.Email!.Split('@')[0];

                // 3. Replace placeholder
                htmlBody = htmlBody.Replace("{UserName}", userName);
                htmlBody = htmlBody.Replace("{Link}", "https://ResultCenter.com");
                htmlBody = htmlBody.Replace("{OTP}", otp);

                // 4. Send email
                await _email.SendAsync(model.Email, "Password Reset OTP", htmlBody);

                TempData["success"] = "Email sent successfully";
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Email failed: {ex.Message}";
            }
            return RedirectToAction("VerifyOtp");
        }
        #endregion

        #endregion



        #region OTP Verify

        #region--Get Method----
        public IActionResult VerifyOtp()
        {
            return View();
        }
        #endregion


        #region--Post Method-----
        [HttpPost]
        public IActionResult VerifyOtp(string otp)
        {
            // Get stored OTP and Email from Session
            var storedOtp = HttpContext.Session.GetString("OTP");
            var email = HttpContext.Session.GetString("Email");

            if (storedOtp == null || email == null)
            {
                TempData["error"] = "Session expired. Please request a new OTP.";
                return RedirectToAction("ForgetPassword");
            }

            if (storedOtp == otp)
            {
                // Clear OTP after successful verification (optional but recommended)
                HttpContext.Session.Remove("OTP");

                return RedirectToAction("ResetPassword", new { email });
            }

            TempData["error"] = "Invalid OTP!";
            return View();
        }
        #endregion
        #endregion



        #region---Reset Password Method----

        #region---Get Method-----
        public IActionResult ResetPassword(string email)
        {
            var model = new ResetPasswordVM { Email = email };
            return View(model);
        }
        #endregion

        #region--Post Method----
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _db.PersonalDetails.FirstOrDefault(u => u.Email == model.Email && u.Isdeleted == false);

            if (user == null)
            {
                TempData["error"] = "User not found!";
                return View(model);
            }

            // Update password in DB
            user.Password = model.NewPassword;  // ⚠️ better to hash password in real projects
            _db.PersonalDetails.Update(user);
            _db.SaveChanges();

            TempData["success"] = "Password reset successfully. Please login.";
            return RedirectToAction("Index", "Login");
        }
        #endregion

        #endregion






    }
}
