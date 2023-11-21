using Nhs.Utility.Common;

namespace Pro.Web.Api.Library.Business.Utils
{
    public static class DictionaryHelper
    {
        /// <summary>A Dictionary&lt;string,object&gt; extension method that adds an int.</summary>
        /// <param name="dictionary">The dictionary to act on.</param>
        /// <param name="keyName">   Name of the key.</param>
        /// <param name="value">     True to value.</param>
        public static void AddInt(this Dictionary<string, object> dictionary, string keyName, int value)
        {
            if (value <= 0)
            {
                return;
            }

            if (dictionary.ContainsKey(keyName))
            {
                dictionary[keyName] = value;
            }
            else
            {
                dictionary.Add(keyName, value);
            }
        }

        /// <summary>A Dictionary&lt;string,object&gt; extension method that adds a decimal.</summary>
        /// <param name="dictionary">The dictionary to act on.</param>
        /// <param name="keyName">   Name of the key.</param>
        /// <param name="value">     True to value.</param>
        public static void AddDecimal(this Dictionary<string, object> dictionary, string keyName, decimal value)
        {
            if (value <= 0)
            {
                return;
            }

            if (dictionary.ContainsKey(keyName))
            {
                dictionary[keyName] = value;
            }
            else
            {
                dictionary.Add(keyName, value);
            }
        }

        /// <summary>A Dictionary&lt;string,object&gt; extension method that adds a double.</summary>
        /// <param name="dictionary">The dictionary to act on.</param>
        /// <param name="keyName">   Name of the key.</param>
        /// <param name="value">     True to value.</param>
        public static void AddDouble(this Dictionary<string, object> dictionary, string keyName, double value)
        {
            if (dictionary.ContainsKey(keyName))
            {
                dictionary[keyName] = value;
            }
            else
            {
                dictionary.Add(keyName, value);
            }
        }

        /// <summary>A Dictionary&lt;string,object&gt; extension method that adds a string.</summary>
        /// <param name="dictionary">The dictionary to act on.</param>
        /// <param name="keyName">   Name of the key.</param>
        /// <param name="value">     True to value.</param>
        public static void AddString(this Dictionary<string, object> dictionary, string keyName, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }

            if (dictionary.ContainsKey(keyName))
            {
                dictionary[keyName] = value;
            }
            else
            {
                dictionary.Add(keyName, value);
            }
        }

        /// <summary>A Dictionary&lt;string,object&gt; extension method that adds a string range.</summary>
        /// <param name="dictionary">The dictionary to act on.</param>
        /// <param name="keyName">   Name of the key.</param>
        /// <param name="value">     True to value.</param>
        public static void AddStringRange(this Dictionary<string, object> dictionary, string keyName, IList<string> value)
        {
            if (value == null || !value.Any())
            {
                return;
            }

            var stringValue = value.ToArray().Join(value[0].Contains(",") ? ";" : ",");

            if (dictionary.ContainsKey(keyName))
            {
                dictionary[keyName] = stringValue;
            }
            else
            {
                dictionary.Add(keyName, stringValue);
            }
        }

        /// <summary>A Dictionary&lt;string,object&gt; extension method that adds an int range.</summary>
        /// <param name="dictionary">The dictionary to act on.</param>
        /// <param name="keyName">   Name of the key.</param>
        /// <param name="value">     True to value.</param>
        public static void AddIntRange(this Dictionary<string, object> dictionary, string keyName, IList<int> value)
        {
            if (value == null || !value.Any())
            {
                return;
            }

            var stringValue = value.Where(x => x > 0).ToArray().Join(",");

            if (dictionary.ContainsKey(keyName))
            {
                dictionary[keyName] = stringValue;
            }
            else
            {
                dictionary.Add(keyName, stringValue);
            }
        }

        /// <summary>A Dictionary&lt;string,object&gt; extension method that adds a bool.</summary>
        /// <param name="dictionary">  The dictionary to act on.</param>
        /// <param name="keyName">     Name of the key.</param>
        /// <param name="value">       True to value.</param>
        /// <param name="excludeFalse">(Optional) True to exclude, false to include the false.</param>
        public static void AddBool(this Dictionary<string, object> dictionary, string keyName, bool value, bool excludeFalse = false)
        {
            // Do not include false values when the excludeFalse flag is set to true.
            if (!value && excludeFalse)
            {
                return;
            }

            if (dictionary.ContainsKey(keyName))
            {
                dictionary[keyName] = value;
            }
            else
            {
                dictionary.Add(keyName, value);
            }
        }

        /// <summary>A Dictionary&lt;string,object&gt; extension method that adds a bool as int.</summary>
        /// <param name="dictionary">The dictionary to act on.</param>
        /// <param name="keyName">   Name of the key.</param>
        /// <param name="value">     True to value.</param>
        public static void AddBoolAsInt(this Dictionary<string, object> dictionary, string keyName, bool value)
        {
            if (!value)
            {
                return;
            }

            if (dictionary.ContainsKey(keyName))
            {
                dictionary[keyName] = 1;
            }
            else
            {
                dictionary.Add(keyName, 1);
            }
        }

        /// <summary>A Dictionary&lt;string,object&gt; extension method that adds a bool as custom
        /// value.</summary>
        /// <param name="dictionary"> The dictionary to act on.</param>
        /// <param name="keyName">    Name of the key.</param>
        /// <param name="value">      True to value.</param>
        /// <param name="customValue">The custom value.</param>
        public static void AddBoolAsCustomValue(this Dictionary<string, object> dictionary, string keyName, bool value, object customValue)
        {
            if (!value)
            {
                return;
            }

            if (dictionary.ContainsKey(keyName))
            {
                dictionary[keyName] = customValue;
            }
            else
            {
                dictionary.Add(keyName, customValue);
            }
        }
    }
}
