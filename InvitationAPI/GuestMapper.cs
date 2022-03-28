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
                Name = value[0].ToString(),
                Answer = value[1].ToString(),
                Date = value[2].ToString(),
                Song = value[3].ToString()
            };
            items.Add(item);
        }
        return items;
    }
    public static IList<IList<object>> MapToRangeData(Guest item)
    {
        var objectList = new List<object>() { item.Name, item.Answer,item.Date,item.Song};
        var rangeData = new List<IList<object>> { objectList };
        return rangeData;
    }
}