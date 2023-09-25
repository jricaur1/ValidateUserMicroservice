using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ValidateUserMS.Controllers;

[ApiController]
[Route("[controller]")]
public class ValidateUserMicroserviceController : ControllerBase
{
    private const string URL = "https://govcarpeta-76300fb42a5a.herokuapp.com/apis/validateCitizen/";
    private readonly ILogger<ValidateUserMicroserviceController> _logger;

    public ValidateUserMicroserviceController(ILogger<ValidateUserMicroserviceController> logger)
    {
        _logger = logger;
    }

    [HttpGet("validate/{id}")]
    public async Task<string> Get(string id)
    {
        Task<string> task =  GetUserInfo(id);
        string result = await task;
        return result;
    }

    private async Task<string> GetUserInfo(string urlParameter)
    {
        string petitionResponse = "";
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(URL);
        
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

         HttpResponseMessage response = client.GetAsync(urlParameter).Result; 
            if (response.StatusCode == HttpStatusCode.NoContent){
                    petitionResponse = "No existe";
                }
            else
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var responseAttempt = JsonConvert.DeserializeObject<string>(jsonString);
                if (responseAttempt != null){
                    petitionResponse = responseAttempt.Split(':')[2];
                }
            }
            
            return petitionResponse;
    }
}
