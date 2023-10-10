using FaturaTakip.Models.Customer.response;
using FaturaTakip.Models.Invoice.request;
using FaturaTakip.Models.Invoice.response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace FaturaTakip.Controllers
{
	public class Invoices : Controller
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		public Invoices(IHttpContextAccessor httpContextAccessor)
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
				var dbUserInvoices = client.GetAsync("/api/fatura/getbysirketbyid/" + activeUserIdStr).Result;
				if (dbUserInvoices.IsSuccessStatusCode)
				{
					var responseContent = dbUserInvoices.Content.ReadAsStringAsync().Result;
					var invoicesResponseList = JsonConvert.DeserializeObject<List<InvoiceResponseModel>>(responseContent);
					return View(invoicesResponseList);
				}
				else
				{
					ViewBag.ErrorMessage = "Şuanda işleminizi gerçekleştiremiyoruz. Lütfen daha sonra tekrar deneyin.";
					return View();
				}
			}
		}
		public IActionResult Add(InvoiceRequestModel request)
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
				if (request.OdenecekTutar == 0)
				{
					var customersForDropDownResult = client.GetAsync("/api/musteri/getbyidfordropdown/" + activeUserIdStr).Result;
					if (customersForDropDownResult.IsSuccessStatusCode)
					{
						var responseContent = customersForDropDownResult.Content.ReadAsStringAsync().Result;
						var customersForDropDown = JsonConvert.DeserializeObject<List<CustomerModelForDropdown>>(responseContent);
						var selectList = new SelectList(customersForDropDown, "MusteriID", "MusteriFullName");
						ViewData["MusteriListesi"] = selectList;
						return View();
					}
					return View();
				}
				else
				{
					request.Durum = true;
					request.MusteriID = request.MusteriID;
					request.SirketID = int.Parse(activeUserIdStr);
					var json = System.Text.Json.JsonSerializer.Serialize(request);
					var content = new StringContent(json, Encoding.UTF8, "application/json");
					var response = client.PostAsync("/api/fatura/create", content).Result;
					if (response.IsSuccessStatusCode)
					{
						var responseContent = response.Content.ReadAsStringAsync().Result;
						return RedirectToAction("Index", "Invoices");
					}
					else
					{
						ViewBag.ErrorMessage = "Fatura kaydı başarısız. Lütfen tekrar deneyin.";
						return View();
					}

				}
			}

		}
		public IActionResult Edit(int invoiceID, InvoiceRequestModel request)
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
				if (request.SiparisNo == null)
				{
					var dbSelectedInvoices = client.GetAsync("/api/fatura/getbyid/" + invoiceID).Result;
					if (dbSelectedInvoices.IsSuccessStatusCode)
					{
						var responseContent = dbSelectedInvoices.Content.ReadAsStringAsync().Result;
						var invoiceResponse = JsonConvert.DeserializeObject<InvoiceResponseModel>(responseContent);
						var customersForDropDownResult = client.GetAsync("/api/musteri/getbyidfordropdown/" + activeUserIdStr).Result;
						if (customersForDropDownResult.IsSuccessStatusCode)
						{
							var responseContentDropdown = customersForDropDownResult.Content.ReadAsStringAsync().Result;
							var customersForDropDown = JsonConvert.DeserializeObject<List<CustomerModelForDropdown>>(responseContentDropdown);
							var selectList = new SelectList(customersForDropDown, "MusteriID", "MusteriFullName");
							ViewData["MusteriListesi"] = selectList;
							TempData["InvoiceID"] = invoiceID.ToString();
							return View(invoiceResponse);
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
					request.Durum = true;
					request.SirketID = int.Parse(activeUserIdStr);
					var json = System.Text.Json.JsonSerializer.Serialize(request);
					var content = new StringContent(json, Encoding.UTF8, "application/json");
					var response = client.PutAsync("/api/fatura/update/" + TempData["InvoiceID"], content).Result;
					if (response.IsSuccessStatusCode)
					{
						var responseContent = response.Content.ReadAsStringAsync().Result;
						return RedirectToAction("Index", "Invoices");
					}
					else
					{
						ViewBag.ErrorMessage = "Fatura kaydı başarısız. Lütfen tekrar deneyin.";
						return View();
					}
				}
				return View();
			}
		}
		public IActionResult Delete(int invoiceID)
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
				var dbUserInvoices = client.DeleteAsync("/api/fatura/delete/" + invoiceID).Result;
				return RedirectToAction("Index", "Invoices");
			}
			
		}
	}
}
