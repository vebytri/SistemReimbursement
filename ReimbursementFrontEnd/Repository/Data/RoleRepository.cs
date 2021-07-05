using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ReimbursementFrontEnd.Base;
using SistemReimbursement.Models;
using SistemReimbursement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace ReimbursementFrontEnd.Repository.Data
{
  
    public class RoleRepository : GeneralRepository<Role, int>
    {
        private readonly Address address;
        private readonly HttpClient httpClient;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;

        public RoleRepository(Address address, string request = "roles/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _contextAccessor.HttpContext.Session.GetString("JWToken"));
        }
     
        public async Task<List<RoleVM>> GetAllRole()
        {
            List<RoleVM> entities = new List<RoleVM>();
            using (var response = await httpClient.GetAsync(request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<RoleVM>>(apiResponse);
            }
            return entities;
        }
     

    }
}
