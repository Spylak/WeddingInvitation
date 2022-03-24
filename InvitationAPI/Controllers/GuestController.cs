using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Microsoft.AspNetCore.Mvc;

namespace InvitationAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class GuestController : ControllerBase
{
    const string SPREADSHEET_ID = "1j467TdfWUMXwADQNp3fimvUxQjGTfkVc_brK-TnfaMY";
    const string SHEET_NAME = "WeddingInvitation";
    SpreadsheetsResource.ValuesResource _googleSheetValues;
    public GuestController(GoogleSheetsHelper googleSheetsHelper)
    {
        _googleSheetValues = googleSheetsHelper.Service.Spreadsheets.Values;
    }
    [HttpGet]
    public IActionResult Get()
    {
        var range = $"{SHEET_NAME}!A:D";
        var request = _googleSheetValues.Get(SPREADSHEET_ID, range);
        var response = request.Execute();
        var values = response.Values;
        return Ok(GuestMapper.MapFromRangeData(values));
    }
    [HttpGet("{rowId}")]
    public IActionResult Get(int rowId)
    {
        var range = $"{SHEET_NAME}!A{rowId}:D{rowId}";
        var request = _googleSheetValues.Get(SPREADSHEET_ID, range);
        var response = request.Execute();
        var values = response.Values;
        return Ok(GuestMapper.MapFromRangeData(values).FirstOrDefault());
    }
    [HttpPost]
    public IActionResult Post(Guest item)
    {
        var range = $"{SHEET_NAME}!A:D";
        var valueRange = new ValueRange
        {
            Values = GuestMapper.MapToRangeData(item)
        };
        var appendRequest = _googleSheetValues.Append(valueRange, SPREADSHEET_ID, range);
        appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
        appendRequest.Execute();
        return CreatedAtAction(nameof(Get), item);
    }
    [HttpPut("{rowId}")]
    public IActionResult Put(int rowId, Guest item)
    {
        var range = $"{SHEET_NAME}!A{rowId}:D{rowId}";
        var valueRange = new ValueRange
        {
            Values = GuestMapper.MapToRangeData(item)
        };
        var updateRequest = _googleSheetValues.Update(valueRange, SPREADSHEET_ID, range);
        updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
        updateRequest.Execute();
        return NoContent();
    }
    [HttpDelete("{rowId}")]
    public IActionResult Delete(int rowId)
    {
        var range = $"{SHEET_NAME}!A{rowId}:D{rowId}";
        var requestBody = new ClearValuesRequest();
        var deleteRequest = _googleSheetValues.Clear(requestBody, SPREADSHEET_ID, range);
        deleteRequest.Execute();
        return NoContent();
    }
}