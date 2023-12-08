using System.Text.Json;

namespace Bookstore.Extensions
{
	public static class ISessionExtensions
	{
		public static void SetObject<T>(this ISession session, string key, T value) where T : class
		{
			session.SetString(key, JsonSerializer.Serialize(value));
		}

		public static T GetObject<T>(this ISession session, string key) where T : class
		{
			if (!session.Keys.Contains(key))
				return null;

			return JsonSerializer.Deserialize<T>(session.GetString(key));
		}
	}
}
