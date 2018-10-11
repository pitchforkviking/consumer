namespace Consumer.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Newtonsoft.Json;
    using RestSharp;

    public class ApplicationUser
    {
        public string EmailID { get; set; }

        public string Password { get; set; }

    }

    public class ApplicationEvent
    {
        public string EmailID { get; set; }

        public string EventTitle { get; set; }

        public string EventDate { get; set; }

        public int Days { get; set; }

        public bool IsCountDown { get; set; }

    }

    public class ApplicationEventResponse
    {
        public string Key { get; set; }
        public string EventTitle { get; set; }

        public int Days { get; set; }

        public bool IsCountDown { get; set; }

    }

    public class ConsumerController : ApiController
    {
        private string ApiUrl = System.Configuration.ConfigurationManager.AppSettings["ApiUrl"];

        [HttpPost]
        public IHttpActionResult SignIn(ApplicationUser applicationUser)
        {
            try
            {
                RestClient restClient = new RestClient(ApiUrl);
                RestRequest restRequest = new RestRequest("/api/Provider/SignIn", Method.POST);

                restRequest.AddParameter("application/json", JsonConvert.SerializeObject(applicationUser), ParameterType.RequestBody);

                IRestResponse restResponse = restClient.Execute(restRequest);
                var responseContent = restResponse.Content;

                if(restResponse.StatusCode == HttpStatusCode.OK)
                {
                    return Ok();
                }

                return BadRequest(responseContent);
            }
            catch(Exception exception)
            {
                return BadRequest(exception.Message??exception.InnerException.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult SignUp(ApplicationUser applicationUser)
        {
            try
            {
                RestClient restClient = new RestClient(ApiUrl);
                RestRequest restRequest = new RestRequest("/api/Provider/SignUp", Method.POST);

                restRequest.AddParameter("application/json", JsonConvert.SerializeObject(applicationUser), ParameterType.RequestBody);

                IRestResponse restResponse = restClient.Execute(restRequest);
                var responseContent = restResponse.Content;

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    return Ok();
                }

                return BadRequest(responseContent);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message ?? exception.InnerException.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult Add(ApplicationEvent applicationEvent)
        {
            try
            {
                RestClient restClient = new RestClient(ApiUrl);
                RestRequest restRequest = new RestRequest("/api/Provider/AddEvent", Method.POST);

                restRequest.AddParameter("application/json", JsonConvert.SerializeObject(applicationEvent), ParameterType.RequestBody);

                IRestResponse restResponse = restClient.Execute(restRequest);
                var responseContent = restResponse.Content;

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    return Ok();
                }

                return BadRequest(responseContent);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message ?? exception.InnerException.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult List(string partitionKey)
        {
            try
            {
                RestClient restClient = new RestClient(ApiUrl);
                RestRequest restRequest = new RestRequest("/api/Provider/GetEvents", Method.GET);

                restRequest.AddParameter("partitionKey", partitionKey);

                IRestResponse restResponse = restClient.Execute(restRequest);
                var responseContent = restResponse.Content;
                var response = JsonConvert.DeserializeObject<List<ApplicationEventResponse>>(responseContent);

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    return Ok(response);
                }

                return BadRequest("No Val!!!");
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message ?? exception.InnerException.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult Delete(string partitionKey, string rowKey)
        {
            try
            {
                RestClient restClient = new RestClient(ApiUrl);
                RestRequest restRequest = new RestRequest("/api/Provider/DeleteEvent", Method.GET);

                restRequest.AddParameter("partitionKey", partitionKey);
                restRequest.AddParameter("rowKey", rowKey);

                IRestResponse restResponse = restClient.Execute(restRequest);                

                if (restResponse.StatusCode == HttpStatusCode.OK)
                {
                    return Ok();
                }

                return BadRequest("Failed");
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message ?? exception.InnerException.Message);
            }
        }
    }
}
