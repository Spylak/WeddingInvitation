using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;

namespace InvitationAPI;

public class GoogleSheetsHelper
{
    public SheetsService Service { get; set; }
    const string APPLICATION_NAME = "Invitation";
    static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
    public GoogleSheetsHelper()
    {
        InitializeService();
    }
    private void InitializeService()
    {
        var credential = GetCredentialsFromFile();
        Service = new SheetsService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = APPLICATION_NAME
        });
    }
    private GoogleCredential GetCredentialsFromFile()
    {
        bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
        GoogleCredential credential;
        string jsonFile = isDevelopment ? "googleSheetsDev.json" : "googleKey.json";
        using (var stream = new FileStream(jsonFile, FileMode.Open, FileAccess.Read))
        {
            credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
        }
        return credential;
    }
}