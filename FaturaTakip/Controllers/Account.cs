using FaturaTakip.Models.Account.request;
using FaturaTakip.Models.Account.response;
using Microsoft.AspNetCore.Http;
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
		public IActionResult Login(LoginRequestModel request)
		{
			string activeUserTokens = _httpContextAccessor.HttpContext.Request.Cookies["ActiveUserToken"];
			if (activeUserTokens == null)
			{
				if (request.Email == null || request.Password == null)
				{
					return View();
				}
				else
				{
					var client = new HttpClient();
					client.BaseAddress = new Uri("http://localhost:5191");
					var dbPasswordResponse = client.GetAsync("/api/sirket/getpassword/" + request.Email).Result;
					if (dbPasswordResponse.IsSuccessStatusCode)
					{
						var dbPassword = dbPasswordResponse.Content.ReadAsStringAsync().Result;
						if (BCrypt.Net.BCrypt.Verify(request.Password, dbPassword))
						{
							var response = client.GetAsync("/api/sirket/getbyemail/" + request.Email + "?sifre=" + dbPassword).Result;
							if (response.IsSuccessStatusCode)
							{
								var responseContent = response.Content.ReadAsStringAsync().Result;
								var loginResponseModel = JsonConvert.DeserializeObject<LoginResponseModel>(responseContent);
								HttpContext httpContext = _httpContextAccessor.HttpContext;
								string activeUserEmail = loginResponseModel.email;
								string activeUserToken = loginResponseModel.token;
								int activeUserId = loginResponseModel.id;
								//cookie
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
								ViewBag.ErrorMessage = "Giriş başarısız. Lütfen tekrar deneyin."; // Hata mesajını göstermek için ViewBag kullanabilirsiniz.
								return View();
							}
						}
						else
						{
							ViewBag.ErrorMessage = "Girdiğiniz şifre yanlış. Lütfen tekrar deneyin.";
							return View();
						}
					}
					else
					{
						ViewBag.ErrorMessage = "Girdiğiniz email adresi bulunamadı. Lütfen tekrar deneyin.";
						return View();
					}
				}
			}
			else
			{
				return RedirectToAction("Index", "Invoices");
			}

		}
		public IActionResult Register(RegisterRequestModel request)
		{
			string activeUserToken = _httpContextAccessor.HttpContext.Request.Cookies["ActiveUserToken"];
			if (activeUserToken == null)
			{
				if (request.Sifre == null || request.Eposta == null)
				{
					return View();
				}
				else
				{
					request.Sifre = CreatePassword(request.Sifre);
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
						ViewBag.ErrorMessage = "Kayıt olma başarısız. Lütfen tekrar deneyin.";
						return View();
					}
				}
			}
			else
			{
				return RedirectToAction("Index", "Invoices");
			}

		}
		public IActionResult EditProfile(RegisterRequestModel request, string verifyPassword, string password)
		{
			string activeUserToken = _httpContextAccessor.HttpContext.Request.Cookies["ActiveUserToken"];
			string activeUserIdStr = _httpContextAccessor.HttpContext.Request.Cookies["ActiveUserId"];
			string activeUserEmail = _httpContextAccessor.HttpContext.Request.Cookies["ActiveUserEmail"];
			if (activeUserToken == null)
			{
				return RedirectToAction("Login", "Account");
			}
			else
			{
				var client = new HttpClient();
				client.DefaultRequestHeaders.Add("Authorization", "Bearer " + activeUserToken);
				client.BaseAddress = new Uri("http://localhost:5191");
				if (request.Adres == null)
				{
					var dbSelectedUser = client.GetAsync("/api/sirket/getbyid/" + activeUserIdStr).Result;
					if (dbSelectedUser.IsSuccessStatusCode)
					{
						var responseContent = dbSelectedUser.Content.ReadAsStringAsync().Result;
						var userResponse = JsonConvert.DeserializeObject<RegisterRequestModel>(responseContent);
						return View(userResponse);
					}
					else
					{
						ViewBag.ErrorMessage = "Şuanda işleminizi gerçekleştiremiyoruz. Lütfen daha sonra tekrar deneyin.";
						return View();

					}
				}
				else
				{
					request.Durum = true;
					if (verifyPassword != null && password != null && verifyPassword == password)
					{
						request.Sifre = CreatePassword(password);
						var json = System.Text.Json.JsonSerializer.Serialize(request);
						var content = new StringContent(json, Encoding.UTF8, "application/json");
						var response = client.PutAsync("/api/sirket/update/" + activeUserIdStr, content).Result;
						if (response.IsSuccessStatusCode)
						{
							var responseContent = response.Content.ReadAsStringAsync().Result;
							return RedirectToAction("Index", "Invoices");
						}
						else
						{
							ViewBag.ErrorMessage = "Profil kaydı başarısız. Lütfen tekrar deneyin.";
							return View();
						}
					}
					else if (verifyPassword == null && password == null)
					{
						var dbPasswordResponse = client.GetAsync("/api/sirket/getpassword/" + activeUserEmail).Result;
						if (dbPasswordResponse.IsSuccessStatusCode)
						{
							request.Sifre = dbPasswordResponse.Content.ReadAsStringAsync().Result;
							var json = System.Text.Json.JsonSerializer.Serialize(request);
							var content = new StringContent(json, Encoding.UTF8, "application/json");
							var response = client.PutAsync("/api/sirket/update/" + activeUserIdStr, content).Result;
							if (response.IsSuccessStatusCode)
							{
								var responseContent = response.Content.ReadAsStringAsync().Result;
								return RedirectToAction("Index", "Invoices");
							}
							else
							{
								ViewBag.ErrorMessage = "Profil kaydı başarısız. Lütfen tekrar deneyin.";
								return View();
							}
						}
						else
						{
							ViewBag.ErrorMessage = "Şuanda işleminizi gerçekleştiremiyoruz. Lütfen daha sonra tekrar deneyin.";
							return View();
						}
					}
					else
					{
						ViewBag.ErrorMessage = "Şifreniz eşleşmiyor. Eğer şifreniz ile ilgili herhangi bi işlem yapmak istemiyorsanız lütfen boş bırakın.";
						return View();
					}

				}
			}
		}
		public IActionResult Logout()
		{
			_httpContextAccessor.HttpContext.Response.Cookies.Delete("ActiveUserEmail");
			_httpContextAccessor.HttpContext.Response.Cookies.Delete("ActiveUserToken");
			_httpContextAccessor.HttpContext.Response.Cookies.Delete("ActiveUserId");
			return RedirectToAction("Login", "Account");
		}
		public string CreatePassword(string password)
		{
			return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(6));
		}
	}
}
