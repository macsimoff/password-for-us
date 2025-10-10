using System.ComponentModel;
using pw4us.Resources;

namespace pw4us.Attributes;

public class LocalizedDescriptionAttribute(string resourceName) : DescriptionAttribute
{
    public override string Description
    {
        get
        {
            var value = DescriptionResources.ResourceManager.GetString(resourceName);
            return string.IsNullOrEmpty(value) ? "" : value!;
        }
    }
}