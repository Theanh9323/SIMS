using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text.Json;

namespace EcommerceMVC.Helper
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session,string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }
        public static T? Get<T>(this ISession sesstion, string key)
        {
            var value = sesstion.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}
