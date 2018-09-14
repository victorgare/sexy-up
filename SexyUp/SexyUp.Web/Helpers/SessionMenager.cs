using System.Collections.Generic;
using System.Web;

namespace SexyUp.Web.Helpers
{
    public static class SessionMenager
    {
        public static void SetObject<T>(T item, string key)
        {
            HttpContext.Current.Session[key] = item;
        }

        public static void SetObject<T>(List<T> itens, string key)
        {
            HttpContext.Current.Session[key] = itens;
        }

        public static T GetObject<T>(string key)
        {
            return (T)HttpContext.Current.Session[key];
        }

        public static List<T> GetObjectsList<T>(string key)
        {
            return (List<T>)HttpContext.Current.Session[key];
        }
    }
}