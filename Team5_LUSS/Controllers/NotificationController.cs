using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Team5_LUSS.Models;

namespace Team5_LUSS.Controllers
{
    public class NotificationController : Controller
    {

        [HttpPost]
        public async Task<List<Notification>> Index(int id)
        {
            List<Notification> notifications = new List<Notification>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(getURL() + "/N_ALL/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    notifications = JsonConvert.DeserializeObject<List<Notification>>(apiResponse);
                }
            }
            return notifications;
        }

        [HttpPost]
        public async void ChangeStatus(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(getURL() + "/N_IsRead/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
        }



        public static async void NewRequest(int fromId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(getURL() + "/N_NewRequest/" + fromId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
        }

        public static async void ReadyForCollection(int fromId, int toId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(getURL() + "/N_ReadyForCollection/" + fromId + "/" + toId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
        }

        public static async void DelegateAssigned(int fromId, int toId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(getURL() + "/N_DelegateAssigned/" + fromId + "/" + toId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
        }
        public static async void AdjustmentVoucherForApproval(int fromId, int toId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(getURL() + "/N_ACForApproval/" + fromId + "/" + toId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
        }

        public static async void AdjustmentVoucherApproved(int fromId, int toId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(getURL() + "/N_ACApproved/" + fromId + "/" + toId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
        }

        public static async void AdjustmentVoucherRejected(int fromId, int toId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(getURL() + "/N_ACRejected/" + fromId + "/" + toId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
        }

        public static async void RequestApproved(int fromId, int toId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(getURL() + "/N_RqApproved/" + fromId + "/" + toId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
        }

        public static async void RequestRejected(int fromId, int toId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(getURL() + "/N_RqRejected/" + fromId + "/" + toId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
        }
        public static string getURL()
        {
            return "https://localhost:44312/Notification";
        }
    }
}
