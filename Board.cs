using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace chess
{
    //TODO: Add code that highlights tile when a player has selected a piece
    public static class Board
    {
        private static Texture2D _textureDark;
        private static Texture2D _textureLight;
        private static Texture2D _textureHighlight;

        private static Color[]   _colorDark      = new Color[] { Color.Black };
        private static Color[]   _colorLight     = new Color[] { Color.White };
        private static Color[]   _colorHighlight = new Color[] { Color.Yellow };

        private static readonly int _tileWidth = 128;
        private static readonly int _tileHeight = 128;
        private static int _boardOffsetHoriz = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 3.5f);
        private static int _boardOffsetVert  = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height  / 6;

        private static Tile[,] _tiles = new Tile[8, 8];
        private static Tile _selectedTile;

        /// <summary>
        /// Sets up the board. Must be called first, before any other call.
        /// </summary>
        public static void Initialize(GraphicsDevice graphicsDevice)
        {
            _textureDark = new Texture2D(graphicsDevice, 1, 1);
            _textureDark.SetData(_colorDark);

            _textureLight = new Texture2D(graphicsDevice, 1, 1);
            _textureLight.SetData(_colorLight);

            _textureHighlight = new Texture2D(graphicsDevice, 1, 1);
            _textureHighlight.SetData(_colorHighlight);
            CreateTiles();
        }

        /// <summary>
        /// Update the board.
        /// </summary>
        public static void Update(GameTime gameTime)
        {
        }

        /// <summary>
        /// Draw all board tiles.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public static void Draw(SpriteBatch spriteBatch)
        {
           foreach (Tile t in _tiles)
            {
                t.Draw(spriteBatch);
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

        /// <summary>
        /// Creates all tiles needed for a full board.
        /// </summary>
        private static void CreateTiles()
        {
            Texture2D firstColor = _textureLight;
            Texture2D curColor = firstColor;
            for (int i = 0; i < 8; i++)
            {
                int y = _boardOffsetVert + i * _tileHeight;
                for (int j = 0; j < 8; j++)
                {
                    int x = _boardOffsetHoriz + j * _tileWidth;
                    _tiles[i, j] = new Tile(curColor, _tileWidth, _tileHeight, (i, j), new Rectangle(x, y, _tileWidth, _tileHeight));


                    if (j != 7) curColor = SwitchCurrentColor(curColor); //Don't switch color last tile
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">Tile coordinate.</param>
        /// <returns>The tile at the specified coordinate.</returns>
        public static Tile GetTileAtIndex((int, int) index)
        {
            return _tiles[index.Item1, index.Item2];
        }


        /// <returns>The color for the dark tiles.</returns>
        public static Color[] ColorDark
        {
            get => _colorDark;
        }

        /// <returns>The color for the light tiles.</returns>
        public static Color[] ColorLight
        {
            get => _colorLight;
        }

        public static Texture2D TextureHighlight
        {
            get => _textureHighlight;
        }

        /// <returns>A 2D-array containing all tiles on the board.</returns>
        public static Tile[,] Tiles
        {
            get => _tiles;
        }

        public static Tile SelectedTile
        {
            get => _selectedTile;
            set => _selectedTile = value;
        }
    }

    /// <summary>
    /// Tile for a chess board.
    /// </summary>
    public class Tile
    {
        private Texture2D _texture;
        private static Texture2D _textureHighlight = Board.TextureHighlight;

        private Rectangle _pos;
        private (int, int) _coordinate;
        private int _width;
        private int _height;

        /// <summary>
        /// Creates a new tile.
        /// </summary>
        /// <param name="texture">The texture of this tile.</param>
        /// <param name="width">Tile width.</param>
        /// <param name="height">Tile height.</param>
        /// <param name="coordinate">Tile coordinate.</param>
        /// <param name="pos">Tile position on screen.</param>
        public Tile(Texture2D texture, int width, int height, (int, int) coordinate, Rectangle pos)
        {
            _texture = texture;
            _width = width;
            _height = height;
            _coordinate = coordinate;
            _pos = pos;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Board.SelectedTile == this) spriteBatch.Draw(_textureHighlight, _pos, Color.White);
            else spriteBatch.Draw(_texture, _pos, Color.White);
        }

        /// <returns>This tile's position on the screen(as Rectangle)</returns>
        public Rectangle Position
        {
            get => _pos;
        }

        /// <returns>This tile's coordinate.</returns>
        public (int, int) Coordinate
        {
            get => _coordinate;
        }
    }
}
