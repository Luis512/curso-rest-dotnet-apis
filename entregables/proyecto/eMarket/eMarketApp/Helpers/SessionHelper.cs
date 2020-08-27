using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace eMarketApp.Helpers
{
    public static class SessionHelper
    {
        /// <summary>
        /// Creates/Updates the cart in the current session.
        /// </summary>
        /// <param name="session">Current <see cref="HttpContext.Session"/></param>
        /// <param name="key">The key value in the session</param>
        /// <param name="value">The key's value.</param>
        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        /// <summary>
        /// Gets the cart in the current session.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="session">Current <see cref="HttpContext.Session"/></param>
        /// <param name="key">The key value in the session</param>
        /// <returns></returns>
        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// Cleans the current shopping cart.
        /// </summary>
        /// <param name="session"></param>
        public static void CleanCart(this ISession session)
        {
            session.Clear();
        }
    }
    
}
