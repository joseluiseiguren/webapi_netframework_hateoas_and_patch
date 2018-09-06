using System.Linq;
using System.Reflection;

namespace webapiexample.Extensions
{
    public static class CommonExtensions
    {
        public static bool HasProperty(this object obj, string propertyName)
        {
            PropertyInfo prop = obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (null != prop && prop.CanWrite)
            {
                return true;
            }

            return false;
        }
    }
}