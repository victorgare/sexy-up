using System.Web;
using System.Web.Mvc;

namespace SexyUp.Web.Libraries.FlashMessage
{
    public static class FlashMessage
    {
        private const string SessionName = "FlashMessage";

        public enum Type : byte
        {
            Info,
            Warning,
            Error,
            Success
        }

        public static void Info(string message)
        {
            SetMessage(Type.Info, message);
        }

        public static void Warning(string message)
        {
            SetMessage(Type.Warning, message);
        }

        public static void Error(string message)
        {
            SetMessage(Type.Error, message);
        }

        public static void Success(string message)
        {
            SetMessage(Type.Success, message);
        }

        private static void SetMessage(Type type, string message)
        {
            string method;

            switch (type)
            {
                case Type.Info:
                    method = "info";
                    break;
                case Type.Warning:
                    method = "warning";
                    break;
                case Type.Error:
                    method = "error";
                    break;
                default:
                    method = "success";
                    break;
            }

            var html = $@"<script type='text/javascript'>
                             toastr.{method}('{message.Replace("'", string.Empty)}');
                         </script>";

            SetMessageSession(html);
        }

        public static HtmlString RenderFlashMessage(this HtmlHelper html)
        {
            var result = GetMessageSession();

            DeleteMessageSession();

            return new HtmlString(result);
        }

        private static void SetMessageSession(string data)
        {
            var context = new HttpContextWrapper(HttpContext.Current);

            context.Session.Add(SessionName, data);
        }

        private static string GetMessageSession()
        {
            var context = new HttpContextWrapper(HttpContext.Current);

            var result = context.Session[SessionName];

            return result?.ToString() ?? string.Empty;
        }

        private static void DeleteMessageSession()
        {
            var context = new HttpContextWrapper(HttpContext.Current);

            context.Session.Remove(SessionName);
        }
    }
}