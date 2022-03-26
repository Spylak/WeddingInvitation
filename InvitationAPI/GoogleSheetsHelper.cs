using System.Text.Json;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Microsoft.Extensions.Options;

namespace InvitationAPI;

public class GoogleSheetsHelper
{
    public SheetsService Service { get; set; }
    const string APPLICATION_NAME = "Invitation";
    private GoogleCreds _googleCreds { get; set; }
    static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
    public GoogleSheetsHelper(IOptions<GoogleCreds> googleCreds)
    {
        _googleCreds = googleCreds.Value;
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
        JsonCredentialParameters parameters = new JsonCredentialParameters();
        if (isDevelopment)
        {
            using (var stream = new FileStream("googleSheetsDev.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            }
        }
        else
        {
            try
            {
                parameters.Type = _googleCreds.type;
                parameters.ProjectId = _googleCreds.project_id;
                parameters.PrivateKeyId = _googleCreds.private_key_id;
                parameters.PrivateKey = _googleCreds.private_key;
                parameters.ClientEmail = _googleCreds.client_email;
                parameters.ClientId = _googleCreds.client_id;
                credential = GoogleCredential.FromJsonParameters(parameters);
          
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        return credential;
    }
}