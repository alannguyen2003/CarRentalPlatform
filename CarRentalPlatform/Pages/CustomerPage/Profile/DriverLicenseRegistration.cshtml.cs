using BuildObject.Entities;
using CarRentalPlatform.Configuration;
using DataTransferLayer.DataTransfer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Repository.Repository;
using Repository.Repository.Abstract;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CarRentalPlatform.Pages.CustomerPage.Profile
{
    public class DriverLicenseRegistrationModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IWebHostEnvironment _environment;
        private readonly IAccountRepository _accountRepository; // Replace ApiService with the name of your API service
        public LicenseInfo LicenseInfo { get; private set; }

        public DriverLicenseRegistrationModel(IWebHostEnvironment environment, IAccountRepository accountRepository, IHttpClientFactory httpClientFactory)
        {
            _environment = environment;
            _accountRepository = accountRepository;
            _httpClient = httpClientFactory.CreateClient();

        }

        public async Task<IActionResult> OnPostCheckLicenseAsync(IFormFile licenseImage)
        {
            if (licenseImage == null || licenseImage.Length == 0)
            {
                ModelState.AddModelError(string.Empty, "Please upload a driver's license image.");
                return Page();
            }

            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(_environment.WebRootPath, "uploads", licenseImage.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await licenseImage.CopyToAsync(stream);
            }

            LicenseInfo = await ProcessLicenseImageAsync(filePath);

            if (LicenseInfo == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to retrieve license information.");
                return Page();
            }

            // Save temp infor
            SessionHelper.SetObjectAsJson(HttpContext.Session, "LicenseInfo", LicenseInfo);

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            LicenseInfo licenseInfo = SessionHelper.GetObjectFromJson<LicenseInfo>(HttpContext.Session, "LicenseInfo");

            if (licenseInfo == null)
            {
                ModelState.AddModelError(string.Empty, "Session expired or invalid license info.");
                return RedirectToPage("/DriverLicenseRegistration");
            }

            int currentUserID = GetCurrentUserId();
            if (currentUserID == 0)
            {
                return RedirectToPage("/Login");
            }

            await _accountRepository.UpdateDriverLicenseInfo(currentUserID, licenseInfo);
            TempData["Message"] = "Driver's License information has been successfully updated.";

            HttpContext.Session.Remove("LicenseInfo");

            return RedirectToPage("/CustomerPage/Profile/Index");
        }



        public async Task<LicenseInfo> ProcessLicenseImageAsync(string imagePath)
        {
            var requestContent = new MultipartFormDataContent();
            var imageContent = new ByteArrayContent(System.IO.File.ReadAllBytes(imagePath));
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
            requestContent.Add(imageContent, "image", Path.GetFileName(imagePath));

            _httpClient.DefaultRequestHeaders.Add("api-key", "d9zW4VfTTnrHScSQApHfjHKltQev7n4y");

            var response = await _httpClient.PostAsync("https://api.fpt.ai/vision/dlr/vnm", requestContent);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonResponse = JsonConvert.DeserializeObject<ApiResponse>(responseContent);

                if (jsonResponse != null && jsonResponse.Data != null && jsonResponse.Data.Count > 0)
                {
                    var data = jsonResponse.Data[0];
                    return new LicenseInfo
                    {
                        DriverLicense = data.Id,
                        DriverLicenseName = data.Name,
                        Nation = data.Nation,
                        Address = data.Address,
                        PlaceIssue = data.place_issue,
                        Class = data.Class
                    };
                }
            }
                return null;
        }

        public class ApiResponse
        {
            [JsonProperty("data")]
            public List<LicenseData> Data { get; set; }
        }

        public int GetCurrentUserId()
        {
            AccountDto accountDto = SessionHelper.GetObjectFromJson<AccountDto>(HttpContext.Session, "user");
            if (accountDto == null)
            {
                return 0;
            }
            return accountDto.Id;
        }


        public class LicenseData
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Nation { get; set; }
            public string Address { get; set; }
            public string place_issue { get; set; }
            public string Class { get; set; }
        }
    }
}
