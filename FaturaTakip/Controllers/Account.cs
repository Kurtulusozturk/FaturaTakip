﻿using FaturaTakip.Models.Account.request;
using FaturaTakip.Models.Account.response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;


namespace FaturaTakip.Controllers
{
    public class Account : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Account(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public IActionResult Login(LoginRequestModel request)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5191");
            var json = System.Text.Json.JsonSerializer.Serialize(request);
            //Api endpoint
            var response = client.GetAsync("/api/sirket/getbyemail/" + request.Email + "?sifre=" + request.Password).Result;
            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var loginResponseModel = JsonConvert.DeserializeObject<LoginResponseModel>(responseContent);
                // Verileri değişkenlere alın
                HttpContext httpContext = _httpContextAccessor.HttpContext;
                string activeUserEmail = loginResponseModel.email;
                string activeUserToken = loginResponseModel.token;
                int activeUserId = loginResponseModel.id;
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddHours(1),
                    HttpOnly = true,
                };
                httpContext.Response.Cookies.Append("ActiveUserEmail", activeUserEmail, cookieOptions);
                httpContext.Response.Cookies.Append("ActiveUserToken", activeUserToken, cookieOptions);
                httpContext.Response.Cookies.Append("ActiveUserId", activeUserId.ToString(), cookieOptions);
                return RedirectToAction("Index", "Invoices");
            }
            else
            {
                Console.WriteLine("Error: " + response.StatusCode);
                ViewBag.ErrorMessage = "Giriş başarısız. Lütfen tekrar deneyin."; // Hata mesajını göstermek için ViewBag kullanabilirsiniz.
                return View();
            }
        }
		public IActionResult Register(RegisterRequestModel request)
        {
			//bcrypt şifre hashleme
			string sifre = BCrypt.Net.BCrypt.HashPassword(request.Sifre);
            request.Sifre = sifre;
			request.Durum = true;
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5191");
            var json = System.Text.Json.JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = client.PostAsync("/api/sirket/create", content).Result;
            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                Console.WriteLine("Error: " + response.StatusCode);
                ViewBag.ErrorMessage = "Kayıt olma başarısız. Lütfen tekrar deneyin.";
                return View();
            }
        }
        [HttpPost]
        public IActionResult EditProfile()
        {
            return View();
        }
    }
}