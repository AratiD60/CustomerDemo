using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CustomerDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CustomerDemo.Controllers
{
    public class CustomerController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Customer> reservationList = new List<Customer>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:54262/api/Customer"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservationList = JsonConvert.DeserializeObject<List<Customer>>(apiResponse);
                }
            }
            return View(reservationList);
        }

        public ViewResult AddReservation() => View();

        [HttpPost]
        public async Task<IActionResult> AddCustomer(Customer customer)
        {
            Customer newCustomer = new Customer();
            using (var httpClient = new HttpClient())
            {
               // httpClient.DefaultRequestHeaders.Add("Key", "Secret@123");
                StringContent content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:54262/api/Customer/create", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    try
                    {
                        newCustomer = JsonConvert.DeserializeObject<Customer>(apiResponse);
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Result = apiResponse;
                        return View();
                    }
                }
            }
            return View(newCustomer);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(Customer customer)
        {
            Customer newCustomer = new Customer();
            using (var httpClient = new HttpClient())
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(customer.Id.ToString()), "Id");
                content.Add(new StringContent(customer.FirstName), "FirstName");
                content.Add(new StringContent(customer.LastName), "LastName");
                content.Add(new StringContent(customer.Email), "Email");
                content.Add(new StringContent(customer.ContactNumber), "ContactNumber");

                using (var response = await httpClient.PutAsync("http://localhost:54262/api/Customer/update", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    newCustomer = JsonConvert.DeserializeObject<Customer>(apiResponse);
                }
            }
            return View(newCustomer);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCustomer(int customerId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://localhost:54262/api/Customer/delete" + customerId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index");
        }
    }
}
