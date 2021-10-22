namespace MVCClient.Services
{
    public interface IUserService
    {
        bool ContainsRole(string accessToken, string role);
    }
}