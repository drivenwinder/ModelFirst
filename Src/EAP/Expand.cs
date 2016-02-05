using System.Linq;
using System.Xml.Linq;
using System;
using System.ComponentModel;
using System.Collections.Specialized;
using EAP.Utils;
using System.Xml;

namespace EAP
{
    public static class Expand
    {
        public static string ToSafeString(this object obj)
        {
            if (object.Equals(obj, null))
                return string.Empty;
            return obj.ToString();
        }

        public static T ConvertTo<T>(this object obj, T defaultValue, bool ignoreException = true)
        {
            if (ignoreException)
            {
                try
                {
                    if (obj != null)
                    {
                        if (obj is T)
                            return (T)obj;

                        var sourceType = obj.GetType();
                        var targetType = typeof(T);

                        if (targetType.IsEnum)
                            return (T)Enum.Parse(targetType, obj.ToString(), true);

                        if (sourceType.IsAssignableFrom(typeof(IConvertible)) &&
                            sourceType.IsAssignableFrom(typeof(IConvertible)))
                            return (T)Convert.ChangeType(obj, targetType);

                        var converter = TypeDescriptor.GetConverter(obj);
                        if (converter != null && converter.CanConvertTo(targetType))
                            return (T)converter.ConvertTo(obj, targetType);

                        converter = TypeDescriptor.GetConverter(targetType);
                        if (converter != null && converter.CanConvertFrom(sourceType))
                            return (T)converter.ConvertFrom(obj);

                        return defaultValue;
                    }
                    else
                    {
                        Type t = typeof(T);
                        if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
                            return default(T);
                    }
                    return defaultValue;
                }
                catch
                {
                    return defaultValue;
                }
            }
            return obj.ConvertTo<T>();
        }

        public static T ConvertTo<T>(this object obj)
        {
            if (obj != null)
            {
                if (obj is T)
                    return (T)obj;

                var sourceType = obj.GetType();
                var targetType = typeof(T);

                if (targetType.IsEnum)
                    return (T)Enum.Parse(targetType, obj.ToString(), true);

                if (sourceType.IsAssignableFrom(typeof(IConvertible)) &&
                    sourceType.IsAssignableFrom(typeof(IConvertible)))
                    return (T)Convert.ChangeType(obj, targetType);

                var converter = TypeDescriptor.GetConverter(obj);
                if (converter != null && converter.CanConvertTo(targetType))
                    return (T)converter.ConvertTo(obj, targetType);

                converter = TypeDescriptor.GetConverter(targetType);
                if (converter != null && converter.CanConvertFrom(sourceType))
                    return (T)converter.ConvertFrom(obj);

                throw new InvalidOperationException("Can't convert from type {0} to type {1}."
                            .FormatArgs(sourceType.Name, targetType.Name));
            }
            else
            {
                Type t = typeof(T);
                if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
                    return default(T);
            }
            throw new ArgumentNullException("obj");
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNotEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static string FormatArgs(this string str, params object[] args)
        {
            return string.Format(str, args);
        }

        public static XElement SingleOrDefault(this XElement element, string name)
        {
            return element.Name == name ? element : element.Elements(name).SingleOrDefault();
        }

        public static T GetValue<T>(this XmlElement element, T defaultValue)
        {
            if (element == null || element.InnerText.IsNullOrEmpty())
                return defaultValue;
            if (defaultValue is Enum)
                return (T)Enum.Parse(typeof(T), element.InnerText);
            if (typeof(T) == typeof(Guid))
                return (T)(object)(new Guid(element.InnerText));
            if (defaultValue is IConvertible)
                return (T)Convert.ChangeType(element.InnerText, typeof(T));
            return element.InnerText.ConvertTo<T>();
        }

        public static T GetAttributeValue<T>(this XmlElement element, string attribute, T defaultValue)
        {
            if (element == null)
                return defaultValue;
            var attr = element.GetAttribute(attribute);
            if (attr.IsNullOrEmpty())
                return defaultValue;
            if (defaultValue is Enum)
                return (T)Enum.Parse(typeof(T), attr);
            if (typeof(T) == typeof(Guid))
                return (T)(object)(new Guid(attr));
            if (defaultValue is IConvertible)
                return (T)Convert.ChangeType(attr, typeof(T));
            return attr.ConvertTo<T>();
        }

        public static void CreateElement(this XmlElement element, string name, string innerText)
        {
            var e = element.OwnerDocument.CreateElement(name);
            e.InnerText = innerText;
            element.AppendChild(e);
        }

        public static Uri RemoveQueryString(this Uri uri, string keyToRemove)
        {
            string url = uri.OriginalString;
            //if first parameter, leave ?, take away trailing &         
            string pattern = @"\?" + keyToRemove + "[^&]*&?";
            url = System.Text.RegularExpressions.Regex.Replace(url, pattern, "?");
            //if subsequent parameter, take away leading &         
            pattern = "&" + keyToRemove + "[^&]*";
            url = System.Text.RegularExpressions.Regex.Replace(url, pattern, "");
            if (uri.IsAbsoluteUri)
                return new Uri(url);
            return new Uri(url, UriKind.Relative);
        }

        public static Uri AddQueryString(this Uri uri, string key, string value)
        {
            string url = uri.RemoveQueryString(key).OriginalString;
            if (url.IndexOf('?') == 0)
                url += "?";
            if (uri.Query.Length > 1)
                url += "&";
            if (uri.IsAbsoluteUri)
                return new Uri(url + key + "=" + value);
            return new Uri(url + key + "=" + value, UriKind.Relative);
        }

        public static T ToModel<T>(this IOrderedDictionary values) where T : new()
        {
            return ObjectAdapter.ToModel<T>(values);
        }
    }
}