using System;
using System.Linq;

namespace Surveys.Presentation.Api.Common
{
    public static class Util
    {
        public static bool IsAnyNullOrEmpty(object obj)
        {
            if (obj is null)
                return true;

            var properties = obj.GetType().GetProperties();

            if (properties.Length.Equals(0))
                return true;

            return properties.Any(x => IsNullOrEmpty(x.GetValue(obj)));
        }

        public static bool IsNullOrEmpty(object value)
        {
            if (value is null)
                return true;

            var type = value.GetType();

            return type.IsValueType && Equals(value, Activator.CreateInstance(type));
        }
    }
}
