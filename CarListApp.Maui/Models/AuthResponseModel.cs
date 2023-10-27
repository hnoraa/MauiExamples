namespace CarListApp.Maui.Models
{
    public class AuthResponseModel
    {
        public AuthResponseModel(string userId, string username, string token)
        {
            UserId = userId;
            Username = username;
            AccessToken = token;
        }

        public string UserId { get; set; }

        public string Username { get; set; }

        public string AccessToken { get; set; }
    }
}