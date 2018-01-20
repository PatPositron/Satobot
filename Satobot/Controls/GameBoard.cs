using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Satobot.Controls
{
    public sealed class GameBoard : Control
    {
        private const int TileSize = 13;
        private readonly List<DrawTile> Tiles;

        public GameBoard()
        {
            Tiles = new List<DrawTile>();

            Size = new Size(110, 110);
            DoubleBuffered = true;

            NewTiles();
        }

        public void NewTiles()
        {
            Tiles.Clear();

            for (var y = 0; y < 5; y++)
            {
                for (var x = 0; x < 5; x++)
                {
                    Tiles.Add(new DrawTile
                    {
                        DrawRectangle = new Rectangle(x * TileSize + 2 * x, y * TileSize + 2 * y, TileSize, TileSize),
                        DrawTileStyle = DrawTileStyle.Blank
                    });
                }
            }

            Invalidate();
        }

        public void SetTile(int tile, DrawTileStyle style)
        {
            Tiles[tile - 1].DrawTileStyle = style;

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(BackColor);

            foreach (var tile in Tiles)
                tile.Draw(g);
        }
    }

    public class DrawTile
    {
        public Rectangle DrawRectangle { get; set; }
        public DrawTileStyle DrawTileStyle { get; set; }

        public void Draw(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.FromArgb(51, 51, 51)), DrawRectangle);

            switch (DrawTileStyle)
            {
                case DrawTileStyle.Clicked:
                    g.FillRectangle(new SolidBrush(Color.FromArgb(51, 221, 0)),
                        Rectangle.Inflate(DrawRectangle, -1, -1));
                    break;
                case DrawTileStyle.ClickedBomb:
                    g.FillRectangle(new SolidBrush(Color.FromArgb(221, 0, 0)),
                        Rectangle.Inflate(DrawRectangle, -1, -1));
                    break;
                case DrawTileStyle.Bomb:
                    g.FillRectangle(new SolidBrush(Color.FromArgb(68, 17, 17)),
                        Rectangle.Inflate(DrawRectangle, -1, -1));
                    g.FillRectangle(new SolidBrush(Color.FromArgb(255, 0, 0)),
                        Rectangle.Inflate(DrawRectangle, -3, -3));
                    break;
            }
        }
    }

    public enum DrawTileStyle
    {
        Blank,
        Clicked,
        ClickedBomb,
        Bomb
    }
}
