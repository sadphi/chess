using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace chess
{
    public static class Board
    {
        private static Texture2D _textureDark;
        private static Texture2D _textureLight;
        private static Color[]   _colorDark  = new Color[] { Color.Black };
        private static Color[]   _colorLight = new Color[] { Color.Beige };

        private static int _tileWidth = 128;
        private static int _tileHeight = 128;
        private static int _boardOffsetHoriz = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 3.5f);
        private static int _boardOffsetVert  = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height  / 6;

        public static void Initialize(GraphicsDevice graphicsDevice)
        {
            _textureDark = new Texture2D(graphicsDevice, 1, 1);
            _textureDark.SetData(_colorDark);

            _textureLight = new Texture2D(graphicsDevice, 1, 1);
            _textureLight.SetData(_colorLight);
        }

        public static void Update(GameTime gameTime)
        {
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            Texture2D firstColor = _textureLight;
            Texture2D curColor = firstColor;
            for (int i = 0; i < 8; i++)
            {
                int yPos = _boardOffsetVert + i * _tileHeight;
                
                for (int j = 0; j < 8; j++)
                {
                    int xPos = _boardOffsetHoriz + j * _tileWidth;
                    spriteBatch.Draw(curColor, new Rectangle(xPos, yPos, _tileWidth, _tileHeight), Color.White);
                    
                    if (j != 7) curColor = SwitchCurrentColor(curColor); //Don't switch color last tile
                }
            }
        }

        private static Texture2D SwitchCurrentColor(Texture2D currentColor)
        {
            if (currentColor == _textureLight)
            {
                return _textureDark;
            }
            return _textureLight;
        }

        public static Color[] GetColorDark()
        {
            return _colorDark;
        }

        public static Color[] GetColorLight()
        {
            return _colorLight;
        }
    }
}
