using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace chess
{
    public static class Utilities
    {
        /// <summary>
        /// Creates a border for this texture with either a single color, 
        /// or a gradient of the initial color.
        /// 
        /// This will directly change the color data of this Texture2D;
        /// this means that the original color data must be saved if needed,
        /// by calling GetData.
        /// </summary>
        /// <param name="color">The color of the border.</param>
        /// <param name="size">The size in pixels of the border.</param>
        /// <param name="useGradient">Set 'true' to use a gradient in the border. This changes the color a bit in each corner, based on the initial color.</param>
        /// <param name="dev">Optional: Controls how much the RGB components should increase when using a gradient. Does nothing for a single color.</param>
        public static void CreateBorder(this Texture2D texture, Color color, int size, bool useGradient, int dev = 35)
        {
            Color top = color;
            Color right = new Color(top.R + dev, top.G + dev, top.B + dev);
            Color bottom = new Color(right.R + dev, right.G + dev, right.B + dev);
            Color left = new Color(right.R + dev, right.G + dev, right.B + dev);
            Color currentColor = color;

            int totalPixels = texture.Width * texture.Height;
            Color[] border = new Color[totalPixels];
            texture.GetData(border);

            int i = 0;
            int x = 0;
            int y = 0;

            int endOfX = texture.Width - size;
            int endOfY = texture.Height - size;


            while (i < totalPixels)
            {
                if (useGradient)
                {
                    if (y < size) currentColor = top;
                    if (x >= endOfX) currentColor = right;
                    if (y >= endOfY) currentColor = bottom;
                    if (x < size) currentColor = left;
                }

                // This checks if x or y are at the corners of the texture.
                if (x < size || x >= endOfX || y < size || y >= endOfY)
                {
                    border[i] = currentColor;
                }
                x++;

                // Jump to next row when at the last column
                if (x == texture.Width)
                {
                    x = 0;
                    y++;
                }
                i++;
            }
            texture.SetData(border);
        }
    }
}
