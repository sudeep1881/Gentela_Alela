using gentela_Alela.Image_Services;
using gentela_Alela.Models;
using gentela_Alela.Views.View_Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace gentela_Alela.Controllers
{
    public class CitizineController : Controller
    {
        private readonly GentleProjectContext _db;
        private readonly IWebHostEnvironment _web;


        public CitizineController(GentleProjectContext db, IWebHostEnvironment web)
        {
            _db = db;
            _web = web;
        }


        #region-----Main Page------
        public IActionResult Home(int? id)
        {
            return View();
        }
        #endregion


        #region--Home Page ---------

        #region--Get  Method------
        public IActionResult Index(int? id)
        {
            return View();
        }
        #endregion

        #region--Fetch  Method-------
        [HttpPost]
        public IActionResult HomePageFetchMethod()
        {
            var DataList = _db.Registrations.Where(s => s.IsDeleted == false).ToList();
            return Json(new { data = DataList });
        }
        #endregion

        #region----Delete Method----
        [HttpDelete]
        public IActionResult HomePageDeleteMethod(int id)
        {
            var rode = _db.Registrations.FirstOrDefault(s => s.IsDeleted == false && s.Id == id);
            if (rode == null)
            {
                return Json(new { success = false, message = "Data Not Found" });
            }
            rode.IsDeleted = true;
            _db.Registrations.Update(rode);
            _db.SaveChanges();
            return Json(new { success = true, message = "Data Delete Successfully" });
        }
        #endregion

        #region---Download Excel---
        [HttpPost]
        public IActionResult DownloadExcelHomePage()
        {
            var result = _db.Registrations.Where(s => s.IsDeleted == false).ToList();

            var DataList = new {
                dataAllows = 1,
                data = new { result }
            
            };
                return new JsonResult(DataList);

        }
        #endregion

        #endregion

        #region------User registaration-------

        #region---GET Method----------------
        public IActionResult Registration(int? id)
        {
            var registrationvm = new RegistrationVM();
            if (id.HasValue && id != 0)
            {
                var role = _db.Registrations.Where(s => s.IsDeleted == false && s.Id == id).FirstOrDefault();
                if (role == null)
                {
                    TempData["error"] = "Data Not Found";
                    return RedirectToAction();
                }
                registrationvm.registrationReg = role;
            }
            return View(registrationvm);
        }
        #endregion


        #region---Post Method----------------
        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationVM registervm,IFormFile fileProfileImage)
        {
            registervm.registrationReg.IsDeleted = false;
            await SaveImageAsync(registervm, fileProfileImage);
            await TryRegistrationDeleteAsync(registervm, fileProfileImage);
            
            if (registervm.registrationReg.Id == 0)
            {
                _db.Registrations.Add(registervm.registrationReg);
            }
            else
            {
                _db.Registrations.Update(registervm.registrationReg);
            }
            _db.SaveChanges();
            TempData["success"] = "Data Saved Successfully";
            return RedirectToAction();
        }


        private async Task SaveImageAsync(RegistrationVM registervm, IFormFile fileProfileImage)
        {
            if (fileProfileImage != null)
            {
                var uploadPath = $"{_web.WebRootPath}{ImageService.ImageProfile}";
                var fileName = await ImageService.SaveImageAsync(fileProfileImage, uploadPath);
                registervm.registrationReg.ProfileImage = $"{ImageService.ImageProfile}{fileName}";
            }
        }



        private async Task TryRegistrationDeleteAsync(RegistrationVM registervm, IFormFile fileProfileImage)
        {
            var objfromdb = await _db.Registrations.Where(s => s.IsDeleted == false && s.Id == registervm.registrationReg.Id).FirstOrDefaultAsync();

            if (fileProfileImage == null)
            {
                registervm.registrationReg.ProfileImage = objfromdb?.ProfileImage;
            }
            else
            {
                ImageService.DeleteImage(_web.WebRootPath, objfromdb?.ProfileImage);
            }
        }



        #endregion

        #region-----Fetch Method-----------
        [HttpPost]
        public IActionResult RegistrationFetchMethod(string? name,string? email)
        {
            var dataList = _db.Registrations.Where(s => s.IsDeleted == false
            &&((name!=null)? s.Name==name :true)
            &&((email!=null)?s.Email==email :true)).ToList();
            return Json(new { data = dataList });
        }
        #endregion

      #region---Download Excel------------
            [HttpPost]
            public IActionResult RegistrationExcelDownload()
            {
                var result = _db.Registrations.Where(s => s.IsDeleted == false).ToList();

                var datalist = new
                {
                    downloadAllows = 1,
                    data = new { result }
                };

                return new JsonResult(datalist);
            }
            #endregion



        #region-----Delete Method------
        [HttpDelete]
        public IActionResult RegistartionDeleteMethod(int id)
        {
            var role = _db.Registrations.Where(s => s.IsDeleted == false && s.Id == id).FirstOrDefault();
            if (role == null)
            {
                return Json(new { success = false, message = "Data Not Found" });
            }
            role.IsDeleted = true;
            _db.Registrations.Update(role);
            _db.SaveChanges();
            return Json(new { success = true, message = "Data Delete Successfully" });
        }
        #endregion

        #endregion

    }
}
