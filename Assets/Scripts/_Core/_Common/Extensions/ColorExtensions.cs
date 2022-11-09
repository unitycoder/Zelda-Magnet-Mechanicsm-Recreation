using UnityEngine;

namespace Core.Common.Extensions
{
    public static class ColorExtensions
    {
        public static Color Alphaed(this Color c, float alpha)
        {
            return new Color(c.r, c.g, c.b, alpha);
        }
    }
}