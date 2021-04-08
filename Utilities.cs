using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace chess
{
    public static class Utilities
    {
        /// <summary>
        /// Creates a border with a single color for this texture.
        /// This will directly change the color data of this Texture2D;
        /// this means that the original color data must be saved if needed,
        /// by calling GetData.
        /// </summary>
        /// <param name="color">The color of the border.</param>
        /// <param name="size">The size in pixels of the border.</param>
        public static void CreateBorder(this Texture2D texture, Color color, int size)
        {
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
                // This checks if x or y are at the corners of the texture.
                if (x < size || x >= endOfX || y < size || y >= endOfY)
                {
                    border[i] = color;
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
