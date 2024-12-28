using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_Aanchal_NoteBook.Models;
using My_Aanchal_NoteBook.Repository.Interface;
using Newtonsoft.Json;
using System.Drawing.Printing;
using System.Text.RegularExpressions;
using Tesseract;

namespace My_Aanchal_NoteBook.Controllers
{
    
    public class DashboardController : Controller
    {
        private readonly IMilkEntry milkEntry;
        private readonly IRegisteration registeration;
        private readonly IWebHostEnvironment _webHostEnv;

        public DashboardController(IMilkEntry milkEntry , IRegisteration registeration , IWebHostEnvironment webHostEnv)
        {
            this.milkEntry = milkEntry;
            this.registeration = registeration;
            this._webHostEnv = webHostEnv;
        }

        public  IActionResult DashboardMethod()
        {
            var username = HttpContext.Session.GetString("Username");
            var userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Username = username;
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("IsLoggedIn")))
            {
                return RedirectToAction("SignIn", "Registeration");
            }
            return View();
        }

        public async Task<IActionResult> Index(User model)
        {

            var username = HttpContext.Session.GetString("Username");
            var userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Username = username;

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("IsLoggedIn")))
            {
                return RedirectToAction("SignIn", "Registeration");
            }
            

            var milkEntryData = await milkEntry.GetAllRecord(model , userid.Value);
            return View(milkEntryData);
        }


        [HttpGet]
        public ActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var userId = HttpContext.Session.GetInt32("Userid");

            if (!userId.HasValue)
            {
                return RedirectToAction("SignIn", "Registeration");
            }
            if (file != null && file.Length > 0)
            {
                var uploadsPath = Path.Combine(_webHostEnv.WebRootPath, "Uploads");
                if (!Directory.Exists(uploadsPath))
                {
                    Directory.CreateDirectory(uploadsPath);
                }
                //var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName); // Generate unique file name
                var filePath = Path.Combine(uploadsPath, Path.GetFileName(file.FileName));
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                try
                {
                    string extractedText = ExtractTextFromImage(filePath);
                    MilkEntry receipt = ParseReceiptText(extractedText);

                    if (receipt.TotalPrice <= 0)
                    {
                        TempData["RecieptMsg"] = "System is unable to read your reciept, please do the manual entry.";
                        return RedirectToAction("CreateMilkEntries");
                    }

                    receipt.Image = "/Uploads/" + Path.GetFileName(file.FileName);
                    
                    TempData["UploadedReceipt"] = JsonConvert.SerializeObject(receipt);
                    TempData.Keep("UploadedReceipt");
                    
                    return RedirectToAction("CreateMilkEntries");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Error: " + ex.Message;
                }
            }
            else
            {
                ViewBag.Message = "Please upload a valid image file.";
            }
            
            //await milkEntry.CreateMilkEntry(model, userId.Value);
            return RedirectToAction("Index", "Dashboard");
        }

        private string ExtractTextFromImage(string imagePath)
        {
            var tessDataPath = Path.Combine(_webHostEnv.WebRootPath, "tessdata");
            using (var engine = new TesseractEngine(tessDataPath, "eng", EngineMode.Default))
            {
                using (var img = Pix.LoadFromFile(imagePath))
                {
                    using (var page = engine.Process(img))
                    {
                        return page.GetText();
                    }
                }
            }
        }
        private MilkEntry ParseReceiptText(string text)
        {
            var receipt = new MilkEntry();
            var lines = text.Split('\n');
            string fullText = string.Join("\n", lines);
            string patternss = @"FAT:.*";
            var matches = Regex.Match(fullText, patternss, RegexOptions.Singleline);

            if (matches.Success)
            {
                string contentAfterFAT = fullText.Substring(matches.Index);
                var linesAfterFAT = contentAfterFAT.Split('\n')
                    .Where(line => !string.IsNullOrWhiteSpace(line))
                    .Select(line => line.Trim())
                    .ToList();
                string cleanedContent = string.Join("\n", linesAfterFAT);
                for (int i = 0; i < linesAfterFAT.Count; i++)
                {
                    string extractedNumber = null;
                    string x = linesAfterFAT[i].Replace(" ", "").Replace("_", ".");
                    string pattern = @"\d+(\.\d+)?";
                    Match match = Regex.Match(x, pattern);

                    if (match.Success)
                    {
                        extractedNumber = match.Value;
                    }
                    else
                    {
                        extractedNumber = "0.0";
                    }
                    if (i == 0)
                    {
                        receipt.Fat = decimal.Parse(extractedNumber);
                    }
                    if (i == 1)
                    {
                        receipt.Snf = decimal.Parse(extractedNumber);
                    }
                    if (i == 2)
                    {
                        receipt.Quantity = decimal.Parse(extractedNumber);
                    }
                    if (i == 3)
                    {
                        receipt.Rate = decimal.Parse(extractedNumber);
                    }
                }
            }
            else
            {
                Console.WriteLine("No match found!");
            }
            receipt.TotalPrice = Math.Round(receipt.Quantity * receipt.Rate, 2);
            receipt.Bonus = Math.Round(receipt.TotalPrice * 0.097m, 2);
            
            receipt.EntryDate = DateTime.Now;
            return receipt;
        }

        [HttpGet]
        public IActionResult CreateMilkEntries()
        {
            var isLoggedIn = HttpContext.Session.GetString("IsLoggedIn");
            if (string.IsNullOrEmpty(isLoggedIn) || isLoggedIn != "true")
            {
                return RedirectToAction("SignIn", "Registeration");
            }

            var usercode = HttpContext.Session.GetString("Code") ?? "";
            ViewBag.Usercode = usercode;

            var uploadedReceiptJson = TempData["UploadedReceipt"] as string;
            MilkEntry model = uploadedReceiptJson != null
                ? JsonConvert.DeserializeObject<MilkEntry>(uploadedReceiptJson)
                : new MilkEntry();
            //TempData.Keep("UploadedReceipt");

            

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMilkEntries(MilkEntry model , IFormFile file)
        {
            var userId = HttpContext.Session.GetInt32("Userid");

            if (!userId.HasValue)
            {
                return RedirectToAction("SignIn", "Registeration");
            }

            string wwwRootPath = _webHostEnv.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string imagePath = Path.Combine(wwwRootPath, @"images\uploadedimages");

                if (!string.IsNullOrEmpty(model.Image))
                {
                    var oldImagePath = Path.Combine(wwwRootPath, model.Image.Trim('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                using (var fileStream = new FileStream(Path.Combine(imagePath, fileName) , FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                model.Image = @"images\uploadedimages\" + fileName;
            }

            

            model.TotalPrice = model.Quantity * model.Rate;
            await milkEntry.CreateMilkEntry(model , userId.Value);
            return RedirectToAction("Index", "Dashboard");
        }

        
        public async Task<IActionResult> UserProfileSection(User model)
        {
            var username = HttpContext.Session.GetString("Username");
            ViewBag.Username = username;
            var userid = HttpContext.Session.GetInt32("Userid");
            var data = await registeration.Users(model , userid.Value);
            return View(data);
        }

        public async Task<IActionResult> UpdateUser(int id)
        {
            var userData = await registeration.GetById(id);
            return View(userData);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(User model)
        {
            await registeration.Update(model);
            return RedirectToAction("UserProfileSection", "Dashboard");
        }

        public async Task<IActionResult> UserDetails(int id)
        {
            var userData = await registeration.GetById(id);
            return View(userData);
        }
        public async Task<IActionResult> DeleteUser(int id)
        {
            await registeration.DeleteUserById(id);
            return RedirectToAction("SignIn", "Registeration");
        }
        public async Task<IActionResult> UpdateMilkEntery(int id)
        {
            var milkData = await milkEntry.GetMilkEntryById(id);
            return View(milkData);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateMilkEntery(MilkEntry model, IFormFile file)
        {
            var userId = HttpContext.Session.GetInt32("Userid");
            string wwwRootPath = _webHostEnv.WebRootPath;

            var oldData = await milkEntry.GetMilkEntryById(model.Id);

            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string imagePath = Path.Combine(wwwRootPath, @"images\uploadedimages");

                if (!string.IsNullOrEmpty(oldData.Image))
                {
                    var oldImagePath = Path.Combine(wwwRootPath, oldData.Image.Trim('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                // Save new image
                using (var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                model.Image = @"images\uploadedimages\" + fileName;
            }
            else
            {
                // Retain old image if no new file uploaded
                model.Image = oldData.Image;
            }

            // Update database entry
            await milkEntry.UpdateMilkEntry(model);

            return RedirectToAction("Index", "Dashboard");
        }
        public async Task<IActionResult> DetailsOfMilkEntery(int id)
        {
            var milkData = await milkEntry.GetMilkEntryById(id);
            return View(milkData);
        }
        public async Task<IActionResult> DeleteMilkEntery(int id)
        {
            await milkEntry.DeleteMilkEntryById(id);
            return RedirectToAction("Index", "Dashboard");
        }
    }
}
