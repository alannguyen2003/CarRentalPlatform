using System.Text.Json;

namespace CarRentalPlatform.Configuration;

public static class SessionHelper
{
    public static void SetObjectAsJson(this ISession session, string key, object value)
    {
        session.SetString(key, JsonSerializer.Serialize(value));
    }

    public static T? GetObjectFromJson<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default(T) : JsonSerializer.Deserialize<T>(value);
    }

    public static void RemoveSession(this ISession session, string key)
    {
        session.Remove(key);
    }

    public static void ClearSession(this ISession session)
    {
        session.Clear();
    }
}