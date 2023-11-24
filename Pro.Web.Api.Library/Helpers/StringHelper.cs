using Nhs.Utility.Common;
using Pro.Web.Api.Library.Constants.Enums;
using System.Text.RegularExpressions;

namespace Pro.Web.Api.Library.Helpers
{
    public static class StringHelper
    {
        /// <summary>
        /// This Email Regex will allow:
        /// Uppercase and lowercase Latin letters A to Z and a to z
        /// Digits 0 to 9
        /// Special characters !#$%&'*+-/=?^_`{|}~
        /// The last ending part after last dot operator will allow either alphabet with length 2-10 or allow number with length 1-3
        /// </summary>
        private const string EmailRegex = "^(([^<>()\\[\\]\\\\.,;:\\s@\"]+(\\.[^<>()\\[\\]\\\\.,;:\\s@\"]+)*)|(\".+\"))@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}])|(([a-zA-Z\\-0-9]+\\.)+[a-zA-Z]{2,}))$";
        public static string OnlyNumber(this string text)
        {
            return CommonUtils.ConvertPhoneToNumber(text);
        }

        public static bool IsNotNullOrWhiteSpaceOrEmpty(this string text)
        {
            return !string.IsNullOrEmpty(text) && !string.IsNullOrWhiteSpace(text);
        }

        public static bool IsNullOrWhiteSpaceOrEmpty(this string text)
        {
            return string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text);
        }

        public static bool IsValidUrl(this string url)
        {
            if (string.IsNullOrEmpty(url))
                return false;

            const string urlRegEx =
                @"(?:(?:(?:https?):)?\/\/)(?:\S+(?::\S*)?@)?(?:(?!(?:10|127)(?:\.\d{1,3}){3})(?!(?:169\.254|192\.168)(?:\.\d{1,3}){2})(?!172\.(?:1[6-9]|2\d|3[0-1])(?:\.\d{1,3}){2})(?:[1-9]\d?|1\d\d|2[01]\d|22[0-3])(?:\.(?:1?\d{1,2}|2[0-4]\d|25[0-5])){2}(?:\.(?:[1-9]\d?|1\d\d|2[0-4]\d|25[0-4]))|(?:(?:[a-z\u00a1-\uffff0-9]-*)*[a-z\u00a1-\uffff0-9]+)(?:\.(?:[a-z\u00a1-\uffff0-9]-*)*[a-z\u00a1-\uffff0-9]+)*(?:\.(?:[a-z\u00a1-\uffff]{2,})).?)(?::\d{2,5})?(?:[/?#]\S*)?$";

            var re = new Regex(urlRegEx);
            return re.Match(url).Success;
        }

        public static bool IsEmailValid(this string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;
            var re = new Regex(EmailRegex);
            return re.Match(email).Success;
        }

        public static string ReplaceEmail(this string text, string replaceText)
        {
            if (string.IsNullOrEmpty(text))
                return text;
            var emailRegex = new Regex(EmailRegex);
            return emailRegex.Replace(text, replaceText);

        }
        public static string ReplacePhone(this string text, string replaceText)
        {
            if (string.IsNullOrEmpty(text))
                return text;
            var phoneRegex = new Regex(@"(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}");
            return phoneRegex.Replace(text, replaceText);
        }

        /// <summary>
        /// Format text to JSON LD tags on HTML.
        /// </summary>
        /// <param name="text">String to change.</param>
        /// <returns>String with \\, \\n and \'.</returns>
        public static string FormatTextToJsonLd(this string text)
        {
            return text.Replace("\\", "\\\\").Replace("\n", "\\n").Replace('"', ' ');
        }

        /// <summary>
        /// Return the value if not NullOrWhiteSpaceOrEmpty else return null
        /// </summary>
        /// <param name="text">String to be check</param>
        /// <returns>string value or null</returns>
        public static string Value(this string text)
        {
            if (text.IsNotNullOrWhiteSpaceOrEmpty()) return text;

            return null;
        }

        /// <summary>
        /// Returns a string with quotes around it
        /// </summary>
        /// <param name="text">String to wrap in quotes</param>
        /// <returns>string value</returns>
        public static string QuotedValue(this string text)
        {
            return "\"" + text + "\"";
        }

        public static SsoAttributes FromString(this string attribute)
        {
            if (Enum.TryParse<SsoAttributes>(attribute, true, out var result))
            {
                return result;
            }
            return default(SsoAttributes);
        }
    }
}
