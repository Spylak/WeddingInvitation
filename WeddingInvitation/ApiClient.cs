namespace WeddingInvitation;

public class ApiClient :HttpClient
{
    public ApiClient(bool isProd)
    {
        if (isProd)
        {
            BaseAddress = new Uri("https://eventinvitation.azurewebsites.net");
        }
        else
        {
            BaseAddress = new Uri("https://localhost:7178");
        }
        
    }
}