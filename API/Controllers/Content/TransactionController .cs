namespace API.Controllers.Content
{
    using Application.Interfaces;
    using Application.Interfaces.Content.Policy;
    using Domain.Models;
    using Infrastructure.Content.Services;
	using Infrastructure.Identity.Models;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

	
	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class TransactionController : ControllerBase
	{
	
	private const string ApiBaseUrl = "https://api.budpay.com/api/v2/";
	private readonly IPolicy _policyService;
		private readonly ITransaction _transactionService;
	private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;


        public TransactionController(
		IPolicy policyService,
        HttpClient httpClient,

        UserManager<AppUser> userManager,
		ITransaction transactionService,
        IConfiguration configuration)
	{
		_policyService = policyService;
		_userManager = userManager;
            _configuration = configuration;
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            

        _transactionService = transactionService;
		  
			
		}

		[HttpPost("initialize")]
		public async Task<IActionResult> InitializeTransaction( TransactionRequest request)
		{
            var payload = new
            {
                email = request.Email, // Provide a valid email address here
                amount =request.Amount,             // Provide a valid amount here
                callback =request.Callback,
            };
            var user = await _userManager.FindByIdAsync(User.Claims.FirstOrDefault(t => t.Type == "UserId").Value);
		    if (user == null)
			    return BadRequest("Invalid User");
			var apiUrl = ApiBaseUrl + "transaction/initialize";

            var jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "sk_test_5a79p4peopxivb0eohtxpzefa7kz4eg3lxysx09");
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));


            var response = await _httpClient.PostAsync(apiUrl, content);
            var responseData = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                //var responseData = await response.Content.ReadAsStringAsync();
                var responseObj = JsonConvert.DeserializeObject<ApiResponse>(responseData);

                // Save relevant data to database
                var transactionData = new Transaction
                {
                    Authorization_Url = responseObj.Data.Authorization_Url,
                    AccessCode = responseObj.Data.AccessCode,
                    Reference = responseObj.Data.Reference,
					Amount =double.Parse(request.Amount),
					UserId = user.Id,
					DateTime = DateTime.UtcNow,
                };

                // Save transactionData to your database using Entity Framework Core or other ORM
                await _transactionService.SaveResponse(transactionData);

                return Ok(responseObj);
            }
            else
            {
                return BadRequest(responseData);
            }
        }

		[HttpPost("CompleteTransaction")]
		public async Task<ActionResult<int>> CompleteTransaction(Transaction transactionResponse)
		{
            var user = await _userManager.FindByIdAsync(User.Claims.FirstOrDefault(t => t.Type == "UserId").Value);
            if (user == null)
                return BadRequest("Invalid User");
            try
            {
                
           
                var apiUrl = ApiBaseUrl + "transaction/initialize";


                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "sk_test_5a79p4peopxivb0eohtxpzefa7kz4eg3lxysx09");
                _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));


                
                
                var response = await _httpClient.GetAsync($"transaction/verify/:{transactionResponse.Reference}");
                var responseData = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var responseObj = JsonConvert.DeserializeObject<TransactionVerificationResponse>(responseData);
                    Transaction result = new()
                    {

                        PaymentRef = responseObj.Data.Reference,
                        Amount = double.Parse(responseObj.Data.Amount),
                        UserId = user.Id,
                        DateTime = DateTime.UtcNow,
                        Reference = responseObj.Data.Reference,
                        Status = responseObj.Data.Status,
                    };

                    // Save transactionData to your database using Entity Framework Core or other ORM
                    return await _transactionService.UpdateResponse(result);
                }
                }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
            var transactionData = new Transaction
            {
                
                Reference = transactionResponse.Reference,
                Amount = transactionResponse.Amount,
                UserId = user.Id,
                DateTime = DateTime.UtcNow,
            };

            // Save transactionData to your database using Entity Framework Core or other ORM
            return await _transactionService.UpdateResponse(transactionData);


        }
	}

	public class TransactionRequest
	{
		public string Email { get; set; }
		public string Amount { get; set; }
		public string? Callback { get; set; }
	}
    public class ApiResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public TransactionDTO Data { get; set; }
    }

   
}


