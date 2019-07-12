using System.Collections;
using System.Reflection;

public static class PropertyInfoUtils
{
    public static bool IsCollection(this PropertyInfo prop)
    {
        return typeof(IEnumerable).IsAssignableFrom(prop.PropertyType) && prop.PropertyType != typeof(string);
    }
}