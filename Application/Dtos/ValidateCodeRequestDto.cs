namespace API.Controllers.Authentication
{
    public class ValidateCodeRequestDto
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}