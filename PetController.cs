using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Petshop.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PetController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetPetPaths()
        {
            HttpClient httpClient = new HttpClient(); 
            string swaggerJson = await httpClient.GetStringAsync("https://petstore.swagger.io/v2/swagger.json"); 
            JObject swaggerObject = JObject.Parse(swaggerJson); 
            JObject pathsObject = (JObject)swaggerObject["paths"]; 
            List<string> petPaths = new List<string>(); 
            foreach (JProperty endpointProperty in pathsObject.Properties())
            {
                if (endpointProperty.Name == "/pet")
                {
                    petPaths.Add(endpointProperty.Name);
                }
            }

            return Ok(petPaths);
        }
    }
}