using Microsoft.AspNetCore.Components;

namespace Banana.Razor.Extensions
{
    public static class ParameterViewExtension
    {
        public static void ToOutput(this ParameterView parameters, string componentName)
        {
            foreach (var parameter in parameters)
            {
                Console.WriteLine($"Parameter in '{componentName}', Name:'{parameter.Name}', Value:'{parameter.Value}', Cascading:{parameter.Cascading}");
            }
        }
    }
}
