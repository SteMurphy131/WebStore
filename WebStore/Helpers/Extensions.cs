using System;
using Microsoft.AspNetCore.Http;

namespace WebStore.Helpers
{
    public static class Extensions
    {
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }
    }
}