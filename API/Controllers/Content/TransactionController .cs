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
    using System.Threading.Tasks;

	
	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class TransactionController : ControllerBase
	{
	private readonly IHttpClientFactory _clientFactory;
	private const string ApiBaseUrl = "https://api.budpay.com/api/v2/";
	private readonly IPolicy _policyService;
		private readonly ITransaction _transactionService;
	private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;


        public TransactionController(IHttpClientFactory clientFactory,
		IPolicy policyService,
		UserManager<AppUser> userManager,
		ITransaction transactionService,
        IConfiguration configuration)
	{
		_policyService = policyService;
		_userManager = userManager;
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri("https://api.budpay.com/api/v2/");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer YOUR_SECRET_KEY");
            _transactionService = transactionService;
		  
			_clientFactory = clientFactory;
		}

		[HttpPost("initialize")]
		public async Task<IActionResult> InitializeTransaction([FromBody] TransactionRequest request)
		{
		var user = await _userManager.FindByIdAsync(User.Claims.FirstOrDefault(t => t.Type == "UserId").Value);
		if (user == null)
			return BadRequest("Invalid User");
		var client = _clientFactory.CreateClient();
			var apiUrl = ApiBaseUrl + "transaction/initialize";

			var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(request), System.Text.Encoding.UTF8, "application/json");

			client.DefaultRequestHeaders.Add("Authorization", "Bearer sk_test_5a79p4peopxivb0eohtxpzefa7kz4eg3lxysx09");

			var response = await client.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var responseObj = JsonConvert.DeserializeObject<ApiResponse>(responseData);

                // Save relevant data to database
                var transactionData = new Transaction
                {
                    AuthorizationUrl = responseObj.Data.AuthorizationUrl,
                    AccessCode = responseObj.Data.AccessCode,
                    Reference = responseObj.Data.Reference,
					Amount = request.Amount,
					UserId = user.Id,
					DateTime = DateTime.UtcNow,
                };

                // Save transactionData to your database using Entity Framework Core or other ORM
                await _transactionService.SaveResponse(transactionData);

                return Ok(responseObj);
            }
            else
            {
                return BadRequest("Failed to initialize transaction.");
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
                var response = await _httpClient.GetAsync($"transaction/verify/{transactionResponse.Reference}");
                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
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
		public double Amount { get; set; }
		public string? Callback { get; set; }
	}
    public class ApiResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public Transaction Data { get; set; }
    }

   
}


