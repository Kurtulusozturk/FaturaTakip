using FaturaTakip.Models.Customer.request;
using FaturaTakip.Models.Customer.response;
using FaturaTakip.Models.Invoice.request;
using FaturaTakip.Models.Invoice.response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace FaturaTakip.Controllers
{
	public class Customers : Controller
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		public Customers(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}
		public IActionResult Index()
		{
			string activeUserToken = _httpContextAccessor.HttpContext.Request.Cookies["ActiveUserToken"];
			string activeUserIdStr = _httpContextAccessor.HttpContext.Request.Cookies["ActiveUserId"];
			if (activeUserToken == null)
			{
				return RedirectToAction("Login", "Account");
			}
			else
			{
				var client = new HttpClient();
				client.DefaultRequestHeaders.Add("Authorization", "Bearer " + activeUserToken);
				client.BaseAddress = new Uri("http://localhost:5191");
				var dbUserCustomers = client.GetAsync("/api/musteri/getbysirketid/" + activeUserIdStr).Result;
				if (dbUserCustomers.IsSuccessStatusCode)
				{
					var responseContent = dbUserCustomers.Content.ReadAsStringAsync().Result;
					var CustomersResponseList = JsonConvert.DeserializeObject<List<CustomersResponseModel>>(responseContent);
					return View(CustomersResponseList);
				}
				else
				{
					ViewBag.ErrorMessage = "Şuanda işleminizi gerçekleştiremiyoruz. Lütfen daha sonra tekrar deneyin.";
					return View();
				}
			}
		}
		public IActionResult Add(CustomersRequestModel request)
		{
			string activeUserToken = _httpContextAccessor.HttpContext.Request.Cookies["ActiveUserToken"];
			string activeUserIdStr = _httpContextAccessor.HttpContext.Request.Cookies["ActiveUserId"];
			if (activeUserToken == null)
			{
				return RedirectToAction("Login", "Account");
			}
			else
			{
				var client = new HttpClient();
				client.DefaultRequestHeaders.Add("Authorization", "Bearer " + activeUserToken);
				client.BaseAddress = new Uri("http://localhost:5191");
				if (request.Soyad == null)
				{
					return View();
				}
				else
				{
					request.SirketID = int.Parse(activeUserIdStr);
					var json = System.Text.Json.JsonSerializer.Serialize(request);
					var content = new StringContent(json, Encoding.UTF8, "application/json");
					var response = client.PostAsync("/api/musteri/create", content).Result;
					if (response.IsSuccessStatusCode)
					{
						var responseContent = response.Content.ReadAsStringAsync().Result;
						return RedirectToAction("Index", "Customers");
					}
					else
					{
						ViewBag.ErrorMessage = "Müşteri kaydı başarısız. Lütfen tekrar deneyin.";
						return View();
					}
				}
			}
		}
		public IActionResult Edit(int customerID, CustomersRequestModel request)
		{
			string activeUserToken = _httpContextAccessor.HttpContext.Request.Cookies["ActiveUserToken"];
			string activeUserIdStr = _httpContextAccessor.HttpContext.Request.Cookies["ActiveUserId"];
			if (activeUserToken == null)
			{
				return RedirectToAction("Login", "Account");
			}
			else
			{
				var client = new HttpClient();
				client.DefaultRequestHeaders.Add("Authorization", "Bearer " + activeUserToken);
				client.BaseAddress = new Uri("http://localhost:5191");
				if (request.Soyad == null)
				{
					var dbSelectedInvoices = client.GetAsync("/api/musteri/getbyid/" + customerID).Result;
					if (dbSelectedInvoices.IsSuccessStatusCode)
					{
						var responseContent = dbSelectedInvoices.Content.ReadAsStringAsync().Result;
						var customerResponse = JsonConvert.DeserializeObject<CustomersResponseModel>(responseContent);
						TempData["CustomerID"] = customerID.ToString();
						return View(customerResponse);
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
					request.SirketID = int.Parse(activeUserIdStr);
					var json = System.Text.Json.JsonSerializer.Serialize(request);
					var content = new StringContent(json, Encoding.UTF8, "application/json");
					var response = client.PutAsync("/api/musteri/update/" + TempData["CustomerID"], content).Result;
					if (response.IsSuccessStatusCode)
					{
						var responseContent = response.Content.ReadAsStringAsync().Result;
						return RedirectToAction("Index", "Customers");
					}
					else
					{
						ViewBag.ErrorMessage = "Fatura kaydı başarısız. Lütfen tekrar deneyin.";
						return View();
					}
				}
			}
		}
		public IActionResult Delete(int customerID)
		{
			string activeUserToken = _httpContextAccessor.HttpContext.Request.Cookies["ActiveUserToken"];
			if (activeUserToken == null)
			{
				return RedirectToAction("Login", "Account");
			}
			else
			{
				var client = new HttpClient();
				client.DefaultRequestHeaders.Add("Authorization", "Bearer " + activeUserToken);
				client.BaseAddress = new Uri("http://localhost:5191");
				var dbUserInvoices = client.DeleteAsync("/api/musteri/delete/" + customerID).Result;
				return RedirectToAction("Index", "Customers");
			}
		}
	}
}
