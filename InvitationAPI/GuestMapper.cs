namespace InvitationAPI;

public static class GuestMapper
{
    public static List<Guest> MapFromRangeData(IList<IList<object>> values)
    {
        var items = new List<Guest>();
        foreach (var value in values)
        {
            Guest item = new()
            {
                Email = value[0].ToString(),
                Answer = value[1].ToString()
            };
            items.Add(item);
        }
        return items;
    }
    public static IList<IList<object>> MapToRangeData(Guest item)
    {
        var objectList = new List<object>() { item.Email, item.Answer};
        var rangeData = new List<IList<object>> { objectList };
        return rangeData;
    }
}