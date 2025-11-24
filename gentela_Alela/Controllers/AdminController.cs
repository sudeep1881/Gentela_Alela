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
    public class AdminController(GentleProjectContext db, IWebHostEnvironment web, IEmailSender email) : Controller
    {
        private readonly GentleProjectContext _db = db;

        private readonly IWebHostEnvironment _web = web;

        private readonly IEmailSender _email = email;

        #region----Role (DDL) Method-------
        public async Task<IEnumerable<SelectListItem>> GetRole()
        {
            var roleList = _db.Roles.Where(s => s.Isdeleted == false).Select(s => new SelectListItem
            {
                Text = s.RoleName,
                Value = s.Id.ToString()
            }).ToList();

            return roleList;
        }
        #endregion

        #region---Country (DDL) Method-------
        public async Task<IEnumerable<SelectListItem>> GetCountry()
        {
            var CountryList = _db.Countries.Select(s => new SelectListItem
            {
                Text = s.CountryName,
                Value = s.Id.ToString()
            }).ToList();
            return CountryList;
        }
        #endregion

        #region----State (DDl)Method----
        public JsonResult GetState(int CountryId)
        {
            var states = _db.States
      .Where(s => s.CountryId == CountryId)
      .Select(s => new { s.Id, s.StateName })
      .ToList();

            return Json(states);
        }
        #endregion



        #region---District (DDl)Method-----
        public JsonResult GetDistrict(int stateId)
        {
            var districts = _db.Districts
                .Where(d => d.StateId == stateId)
                .Select(d => new { d.Id, d.DistrictName })
                .ToList();

            return Json(districts);
        }
        #endregion

        #region----Karnataka District(DDl)
        public async Task<IEnumerable<SelectListItem>> GetKarnatakaDsitrict()
        {
            var disticts = _db.Districts.Where(s => s.StateId == 1).Select(s => new SelectListItem
            {
                Text = s.DistrictName,
                Value = s.Id.ToString()
            }).ToList();

            return disticts;
        }
        #endregion

        #region------KA D Taluk (DDL)---
        [HttpGet]
        public JsonResult KADTaluk(int districtId)
        {
            var taluk = _db.Taluks
                .Where(s => s.DistictId == districtId)
                .Select(s => new { s.Id, s.TalukName })
                .ToList();

            return Json(taluk);
        }
        #endregion

        #region---Admin Home Page-----
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region---Role Method-----------

        #region---Get Method----
        public IActionResult Role(int? id)
        {
            var rolevm = new RoleVM();
            if (id.HasValue && id != 0)
            {
                var role = _db.Roles.Where(s => s.Isdeleted == false && s.Id == id).FirstOrDefault();
                if (role == null)
                {
                    TempData["error"] = "No Data Found";
                    return RedirectToAction();

                }
                rolevm.RoleReg = role;
            }
            return View(rolevm);
        }
        #endregion

        #region----Post Method----------
        [HttpPost]
        public IActionResult Role(RoleVM rolevm)
        {
            rolevm.RoleReg.Isdeleted = false;
            if (rolevm.RoleReg.Id == 0)
            {
                _db.Roles.Add(rolevm.RoleReg);

            }
            else
            {
                _db.Roles.Update(rolevm.RoleReg);
            }
            _db.SaveChanges();
            TempData["success"] = "Data Saved Successfully";
            return RedirectToAction();
        }
        #endregion
        #endregion

        #region--Role List------

        #region---Get Method-----------
        public IActionResult RoleList(int? id)
        {
            var rolevm = new RoleVM();
            return View(rolevm);
        }
        #endregion


        #region--fetch Method-----
        public IActionResult RoleListFetchMethod()
        {
            var DataList = _db.Roles.Where(s => s.Isdeleted == false).ToList();
            return Json(new { data = DataList });
        }
        #endregion

        #region----Delete Method----
        [HttpDelete]
        public IActionResult RoleListDeleteMethod(int id)
        {
            var role = _db.Roles.FirstOrDefault(s => s.Isdeleted == false && s.Id == id);
            if (role == null)
            {
                return Json(new { success = false, message = "Data Not Found" });
            }
            role.Isdeleted = true;
            _db.Roles.Update(role);
            _db.SaveChanges();
            return Json(new { success = true, message = "Data Delete Successfully" });
        }
        #endregion
        #endregion

        #region---Personal Details Method----------

        #region----Get Method----------
        public async Task<IActionResult> PersonalDetails(int? id)
        {
            var pernaolvm = new PersonalDetailsVM
            {
                RoleList = await GetRole(),
                CountryList = await GetCountry()
            };

            if (id.HasValue && id != 0)
            {
                var role = _db.PersonalDetails.Where(s => s.Isdeleted == false && s.Id == id).FirstOrDefault();
                if (role == null)
                {
                    TempData["error"] = "Data Not Found";
                    return RedirectToAction();
                }
                pernaolvm.personalReg = role;
            }
            return View(pernaolvm);
        }
        #endregion

        #region---Post Method--------
        [HttpPost]
        public async Task<IActionResult> PersonalDetails(PersonalDetailsVM personalvm, IFormFile fileProfileImage)
        {
            personalvm.personalReg.Isdeleted = false;

            await TrySaveImageAsync(personalvm, fileProfileImage);
            await TryDeleteImageAsync(personalvm, fileProfileImage);
            if (personalvm.personalReg.Id == 0)
            {
                _db.PersonalDetails.Add(personalvm.personalReg);
            }
            else
            {
                _db.PersonalDetails.Update(personalvm.personalReg);
            }
            _db.SaveChanges();
            TempData["success"] = "Data Saved Successfully";
            return RedirectToAction();
        }



        private async Task TrySaveImageAsync(PersonalDetailsVM personalvm, IFormFile fileProfileImage)
        {
            if (fileProfileImage != null)
            {
                var profilePath = $"{_web.WebRootPath}{ImageService.ImageProfile}";
                var fileName = await ImageService.SaveImageAsync(fileProfileImage!, profilePath);
                personalvm.personalReg.ProfileImage = $"{ImageService.ImageProfile}{fileName}";
            }
        }

        private async Task TryDeleteImageAsync(PersonalDetailsVM personalvm, IFormFile fileProfileImage)
        {
            var objfromdb = await _db.PersonalDetails.Where(s => s.Isdeleted == false && s.Id == personalvm.personalReg.Id).FirstOrDefaultAsync();
            if (fileProfileImage == null)
            {
                personalvm.personalReg.ProfileImage = objfromdb?.ProfileImage;
            }
            else
            {
                ImageService.DeleteImage(_web.WebRootPath, objfromdb?.ProfileImage);
            }
        }


        #endregion

        #endregion

        #region---Personal Details Fetch List------

        #region--Get Method--------
        public IActionResult PeronalDetails_List()
        {
            return View();
        }
        #endregion

        #region---Fetch Method---------
        [HttpPost]
        public IActionResult PersonalMethodFetchMethod()
        {
            var DataList = _db.PersonalDetails.Where(s => s.Isdeleted == false).Include(s => s.Role).Include(s => s.District).Include(s => s.State).
                Include(s => s.Country).Select(s => new PersonalDetailsDTOs
                {
                    IdDTOs = s.Id,
                    RoleNameDTOs = s.Role!.RoleName,
                    FullNameDTOs = s.FullName,
                    GenderDTOs = s.Gender,
                    DobDTOs = s.Dob,
                    EmailDTOs = s.Email,
                    PhoneNumber = s.PhoneNo,
                    CountryDTOs = s.Country!.CountryName,
                    StateDTOs = s.State!.StateName,
                    DistrictDTOs = s.District!.DistrictName,
                    ProfileImageDTOs = s.ProfileImage,


                }).ToList();


            return Json(new { data = DataList });
        }
        #endregion

        #region--Delete Method------
        [HttpDelete]
        public IActionResult DeleteHandlerPersonalList(int id)
        {
            var role = _db.PersonalDetails.FirstOrDefault(s => s.Isdeleted == false && s.Id == id);
            if (role == null)
            {
                return Json(new { success = false, message = "Data Not Found" });
            }
            role.Isdeleted = true;
            _db.PersonalDetails.Update(role);
            _db.SaveChanges();
            return Json(new { success = true, message = "Data Delete Successfully" });

        }
        #endregion

        #endregion

        #region---View Profile--------
        public IActionResult ViewProfile(int? id)
        {
            return View();
        }
        #endregion

        #region---Edit Profile-------

        #region----Get Method-------------
        public async Task<IActionResult> EditProfile(int? id)
        {
            var PersonalVM = new PersonalDetailsVM();


            int? userid = HttpContext.Session.GetInt32(SD.KeyUser);

            var UserName = _db.PersonalDetails.Where(s => s.Isdeleted == false && s.Id == userid).FirstOrDefault();

            PersonalVM.personalReg = UserName!;
            return View(PersonalVM);
        }
        #endregion

        #region---Post Method------
        [HttpPost]
        public IActionResult EditProfile(PersonalDetailsVM persnolvm)
        {
            persnolvm.personalReg.Isdeleted = false;
            if (persnolvm.personalReg.Id != 0)
            {
                _db.PersonalDetails.Update(persnolvm.personalReg);
            }
            _db.SaveChanges();
            TempData["Success"] = "Application Submitted Succesfully";
            return RedirectToAction("Index", "Admin");
        }
        #endregion


        #endregion

        #region---Send Otp---
        [HttpPost]
        public async Task<IActionResult> SendOtpAsync(ForgotPasswordVM model, string email)
        {
            if (string.IsNullOrEmpty(email))
                return Json(new { success = false, message = "Email is required" });

            // Generate OTP
            var otp = new Random().Next(1000, 9999).ToString();

            // Save OTP in Session
            HttpContext.Session.SetString("EmailOtp", otp);
            HttpContext.Session.SetString("PendingEmail", email);

            // Send OTP to email
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


                return Json(new { success = true, message = "OTP sent to your email." });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error sending email: " + ex.Message });
            }
        }

        #endregion

        #region--Verify OTP----
        [HttpPost]
        public IActionResult VerifyOtp(string enteredOtp)
        {
            var storedOtp = HttpContext.Session.GetString("EmailOtp");
            var newEmail = HttpContext.Session.GetString("PendingEmail");
            int? userId = HttpContext.Session.GetInt32(SD.KeyUser);

            if (storedOtp == null || newEmail == null)
                return Json(new { success = false, message = "Session expired. Try again." });

            if (enteredOtp == storedOtp)
            {
                // OTP valid → update email
                var user = _db.PersonalDetails.FirstOrDefault(x => x.Id == userId);
                if (user != null)
                {
                    user.Email = newEmail;
                    _db.Update(user);
                    _db.SaveChanges();

                }
                HttpContext.Session.Remove("EmailOtp");
                HttpContext.Session.Remove("PendingEmail");


                return Json(new { success = true, message = "Email updated successfully!" });
            }
            return Json(new { success = false, message = "Invalid OTP!" });
        }

        #endregion

        #region-----New Password--------

        #region---Get Method------
        public IActionResult NewPassword(int? id)
        {
            var persnolvm = new PersonalDetailsVM();
            if (id.HasValue && id != 0)
            {
                var role = _db.PersonalDetails.Where(s => s.Isdeleted == false && s.Id == id).FirstOrDefault();
                if (role == null)
                {
                    TempData["error"] = "Data Not Found";
                    return RedirectToAction();
                }
                persnolvm.personalReg = role;
            }
            return View(persnolvm);
        }
        #endregion

        #region----Post Method------
        [HttpPost]
        public IActionResult NewPassword(PersonalDetailsVM persnolvm, string? Password)
        {
            int? userid = HttpContext.Session.GetInt32(SD.KeyUser);

            var user = _db.PersonalDetails.Where(s => s.Isdeleted == false && s.Id == userid).FirstOrDefault();



            if (user == null)
            {
                TempData["error"] = "User Not Found";
                return View(persnolvm);
            }

            if (user.Password != persnolvm.personalReg.Password)
            {
                TempData["error"] = "Old Password is Not Correct";
                return View(persnolvm);
            }

            user.Password = Password;
            _db.PersonalDetails.Update(user);
            _db.SaveChanges();
            TempData["success"] = "Reset Password Successfully";
            return RedirectToAction();

        }
        #endregion
        #endregion

        #region--Apply Services------
        public IActionResult ApplyServices()
        {
            return View();
        }
        #endregion

        #region----Application Of Yuvanidhi-----

        #region--Get Method--------------
        public async Task<IActionResult> Application_Yuvanidhi(int? id)
        {
            var yuvanidhiVm = new yuvanidhi_ApplicationDetailsVM
            {
                KADistictReg = await GetKarnatakaDsitrict(),

            };


            if (id.HasValue && id != 0)
            {
                var user = _db.YuvanidhiApplicantDetails.Where(s => s.Isdeleted == false && s.Id == id).FirstOrDefault();
                var education = _db.EducationDetails.Where(s => s.Isdelted == false && s.Id == id).FirstOrDefault();
                var domicile = _db.DomicileVerifications.Where(s => s.Isdeleted == false && s.Id == id).FirstOrDefault();
                var Communicate = _db.CommunicationDetails.Where(s => s.Isdeleted == false && s.Id == id).FirstOrDefault();
                var additionalDetails = _db.AddtionalDetails.Where(s => s.Isdeleted == false && s.Id == id).FirstOrDefault();

                if (user == null && education == null && domicile == null)
                {
                    TempData["error"] = "Data Not Found";
                    return RedirectToAction();
                }
                yuvanidhiVm.applicantReg = user!;
                yuvanidhiVm.educationReg = education!;
                yuvanidhiVm.domicileReg = domicile!;
                yuvanidhiVm.communicateReg = Communicate!;
                yuvanidhiVm.additionalReg = additionalDetails!;
            }
            return View(yuvanidhiVm);
        }
        #endregion

        #region----POst Method---------
        [HttpPost]
        public async Task<IActionResult> Application_Yuvanidhi(yuvanidhi_ApplicationDetailsVM yuvanishiVM, IFormFile fileProfileImage)
        {
            yuvanishiVM.applicantReg.Isdeleted = false;
            yuvanishiVM.educationReg.Isdelted = false;
            yuvanishiVM.domicileReg.Isdeleted = false;
            yuvanishiVM.communicateReg.Isdeleted = false;
            yuvanishiVM.additionalReg.Isdeleted = false;
            await TrySaveImageAsyncAplliant(yuvanishiVM, fileProfileImage);
            await TryDeleteImageAsyncApplicant(yuvanishiVM, fileProfileImage);
            if (yuvanishiVM.applicantReg.Id == 0)
            {
                _db.YuvanidhiApplicantDetails.Add(yuvanishiVM.applicantReg);

            }
            else
            {
                _db.YuvanidhiApplicantDetails.Update(yuvanishiVM.applicantReg);
            }

            _db.SaveChanges();




            if (yuvanishiVM.educationReg.Id == 0)
            {
                _db.EducationDetails.Add(yuvanishiVM.educationReg);

            }
            else
            {
                _db.EducationDetails.Update(yuvanishiVM.educationReg);
            }

            _db.SaveChanges();
            yuvanishiVM.applicantReg.EducationId = yuvanishiVM.educationReg.Id;



            if (yuvanishiVM.domicileReg.Id == 0)
            {
                _db.DomicileVerifications.Add(yuvanishiVM.domicileReg);
            }
            else
            {
                _db.DomicileVerifications.Update(yuvanishiVM.domicileReg);
            }
            _db.SaveChanges();
            yuvanishiVM.applicantReg.DomicineId = yuvanishiVM.domicileReg.Id;


            if (yuvanishiVM.communicateReg.Id == 0)
            {
                _db.CommunicationDetails.Add(yuvanishiVM.communicateReg);
            }
            else
            {
                _db.CommunicationDetails.Update(yuvanishiVM.communicateReg);
            }

            _db.SaveChanges();

            yuvanishiVM.applicantReg.CommunicationId = yuvanishiVM.communicateReg.Id;



            if (yuvanishiVM.additionalReg.Id == 0)
            {
                _db.AddtionalDetails.Add(yuvanishiVM.additionalReg);
            }
            else
            {
                _db.AddtionalDetails.Update(yuvanishiVM.additionalReg);
            }


            _db.SaveChanges();

            yuvanishiVM.applicantReg.AdditionalId = yuvanishiVM.additionalReg.Id;
            _db.SaveChanges();

            TempData["success"] = "Data saved Successfully";
            return RedirectToAction();
        }



        private async Task TrySaveImageAsyncAplliant(yuvanidhi_ApplicationDetailsVM yuvanishiVM, IFormFile fileProfileImage)
        {
            if (fileProfileImage != null)
            {
                var uploadPath = $"{_web.WebRootPath}{ImageService.ImageProfile}";
                var filename = await ImageService.SaveImageAsync(fileProfileImage, uploadPath);
                yuvanishiVM.applicantReg.Photo = $"{ImageService.ImageProfile}{filename}";
            }
        }

        private async Task TryDeleteImageAsyncApplicant(yuvanidhi_ApplicationDetailsVM yuvanishiVM, IFormFile fileProfileImage)
        {
            var objfromdb = await _db.YuvanidhiApplicantDetails.Where(s => s.Isdeleted == false && s.Id == yuvanishiVM.applicantReg.Id).FirstOrDefaultAsync();
            if (fileProfileImage == null)
            {
                yuvanishiVM.applicantReg.Photo = objfromdb?.Photo;
            }
            else
            {
                ImageService.DeleteImage(_web.WebRootPath, objfromdb?.Photo);
            }
        }
        #endregion
        #endregion


        #region----Application of yuvanidhi List---------

        #region-----Get Method--------
        public IActionResult YuvanidhiApplicationList()
        {
            return View();
        }
        #endregion


         
        #region---Fetch Method-------
        [HttpPost]
        public IActionResult YuvanidhiApplicationFetchMethod()
        {
            var DataList = _db.YuvanidhiApplicantDetails.Where(s => s.Isdeleted == false).Include(s => s.Domicine).Include(s => s.Education).Include(s => s.Communication).
             Include(s => s.Additional).Select(s => new YuvanidhiApplicationListDTOs
             {
                 IdDTOs = s.Id,
                 AdharNameDTOs = s.AdharName,
                 DobDTOs=s.Dob,
                 GenderDTOs = s.Gender,
                 PhotoDTOs = s.Photo,
                 PernmentAdressDTOs = s.PernmentAdress,
                 DistinctDTOs = s.Distinct!.DistrictName,
                 TalukDTOs = s.Taluk!.TalukName,
                 PincodeDTOs = s.Pincode,
                 CourseDTOs = s.Education!.CourseType,
                 LevelDTOs = s.Education!.Level,
                 PassoutDTOs = s.Education!.YearOfPassout,
                 UniversityDTOs = s.Education!.UniversityName,
                 RegisterNODTOs = s.Education!.RegistrationNumber,
                 DomicineName = s.Domicine!.DomicileOption,
                 DomicineCardNo = s.Domicine!.RationOrcetNumber,
                 MobileNoDTOs = s.Communication!.MobileNo,
                 EmailDTOs = s.Communication!.Email,
                 CategoryDTOs = s.Additional!.Caste,
                 DisabilityDTOs = s.Additional!.Disability,
                


             }).ToList();

            return Json(new { data = DataList });
        }

        #endregion

        #region---Delete Method-----------
        [HttpDelete]
        public IActionResult YuvanidhiApplicationListDeleteMethod(int id)
        {
            var role = _db.YuvanidhiApplicantDetails.FirstOrDefault(s => s.Isdeleted == false && s.Id == id);

            if (role == null)
            {
                return Json(new { success = false, message = "Data Not Found" });
            }
            role.Isdeleted = true;
            _db.YuvanidhiApplicantDetails.Update(role);
            _db.SaveChanges();
            return Json(new { success = true, message = "Data Delete Successfully" });
        }
        #endregion
        #endregion

    }
}

