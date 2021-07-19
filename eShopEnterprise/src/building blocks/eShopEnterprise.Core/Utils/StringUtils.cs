using System.Linq;

namespace eShopEnterprise.Core.Utils
{
    public static class StringUtils
    {
        public static string ApenasNumeros(this string texto)
        {
            return new string(texto.Where(char.IsDigit).ToArray());
        }
    }
}
