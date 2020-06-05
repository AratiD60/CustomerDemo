using System;
using System.Collections.Generic;
using System.Globalization;
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
            List<Customer> customerList = new List<Customer>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5555/api/Customer"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    customerList = JsonConvert.DeserializeObject<List<Customer>>(apiResponse);
                }
            }
            return View(customerList);
        }

        [HttpGet]
        public async Task<IActionResult> Index(string nameSearch)
        {
            ViewData["GetCustomer"] = nameSearch;
            List<Customer> customerList = new List<Customer>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5555/api/Customer"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    customerList = JsonConvert.DeserializeObject<List<Customer>>(apiResponse);
                }
            }

            var customerQry = from x in customerList select x;
            if (!string.IsNullOrEmpty(nameSearch))
            {
                TextInfo txtInfo = new CultureInfo("en-us", false).TextInfo;
                var name = txtInfo.ToTitleCase(nameSearch);

                customerQry = customerList.Where(x => x.FirstName.Contains(name) || x.LastName.Contains(name));
            }
            return View(customerQry.ToList());
        }
        public ViewResult Add() => View();

        [HttpPost]
        public async Task<IActionResult> Add(Customer customer)
        {
            Customer newCustomer = new Customer();
            using (var httpClient = new HttpClient())
            {           
                StringContent content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:5555/api/Customer/create", content))
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
            //return View(newCustomer);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            if(string.IsNullOrEmpty(Id))
            {
                return RedirectToAction("Index");
            }
            List<Customer> customerList = new List<Customer>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5555/api/Customer"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    customerList = JsonConvert.DeserializeObject<List<Customer>>(apiResponse);
                }
            }
            var geCustomerDetails = customerList.Where(x => x.Id.Equals(Id)).FirstOrDefault();
            return View(geCustomerDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Customer customer)
        {
            Customer newCustomer = new Customer();
            using (var httpClient = new HttpClient())
            {

                StringContent content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:5555/api/Customer/update", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    newCustomer = JsonConvert.DeserializeObject<Customer>(apiResponse);
                }
            }
            //return View(newCustomer);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string customerId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://localhost:5555/api/Customer/" + customerId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index");
        }

        
    }
}
