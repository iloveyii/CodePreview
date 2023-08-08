using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace Receipt.Models
{
    public class Writer
    {
        public static int SPACE_SMALL = 4;
        public static int SPACE_MEDIUM = 7;
        public static int SPACE_LARGE = 11;

        public static int FONT_SMALL = 8;
        public static int FONT_MEDIUM = 11;
        public static int FONT_LARGE = 16;

        public static string initialFont = "monospace";
        public static int initialFontSize = 7;
        public static int initialWidth = 280;
        public static int initialColumns = 2;
        public static int initialXPad = 5;
        public static int initialYPad = 15;
        public static int initialVSpace = 15;
        public static int initialNextColXPad = 15;

        private Graphics g;
        private Font font;
        private float fontHeight;
        private SolidBrush brush;
        private int startX;
        private int startY;
        private int vSpacing;
        private int offset;
        private int colNum;
        private int initialColWidth;
        private int colWidth;

        public Writer(Graphics graphics) { 
            g = graphics;
            font = new Font(initialFont, initialFontSize);
            fontHeight = font.GetHeight();
            brush = new SolidBrush(Color.Black);
            offset = initialYPad + initialVSpace;
            startX= initialXPad; startY= initialYPad;
            vSpacing = initialVSpace;
            calcColumnWidths();
            colNum = 0;
        }

        private void calcColumnWidths()
        {
            initialColWidth = initialWidth / initialColumns;
            colWidth = initialColWidth;
        }

        public Writer insertImage(string path, double timesWidth = 0.5)
        {
            Uri u = new Uri(path);
            Image imageFile = Image.FromFile(path);
            int w = imageFile.Width;
            int h = imageFile.Height;
            int y = (int) (initialWidth * timesWidth);
            int x = (int) ( h * y / w );

            colNum = 1;
            offset = offset + vSpacing;
            int xpad = (int) (initialWidth - y ) /2;
            g.DrawImage(imageFile, 90, startY + offset, y, x);
            return this;
        }
        public Writer setFontSize(int num)
        {
            initialFontSize= num;
            return this;
        }
        public Writer setInitialColumns(int num)
        {
            initialColumns = num;
            calcColumnWidths();
            return this;
        }

        public Writer line()
        {
            string underLine = "";
            for(var i = 0; i < (initialWidth / 2); i++)
            {
                underLine = underLine + "-";
            }
            offset = offset + vSpacing;
            g.DrawString(underLine, font, brush, initialXPad, startY + offset);
            return this;
        }
        public Writer text(string text)
        {
            colNum = 1;
            offset = offset + vSpacing;
            g.DrawString( text, font, brush, startX + 15, startY + offset);
            return this;
        }

        public Writer firstColText(string text)
        {
            colNum = 1;
            offset = offset + vSpacing;
            g.DrawString(text, font, brush, startX + 15, startY + offset);
            return this;
        }
        public Writer nextColText(string text)
        {
            colNum += 1;
            g.DrawString(text, font, brush, (startX + ( colNum * colWidth ) + initialNextColXPad), startY + offset);
            return this;
        }

        public Writer vSpace(int space)
        {
            offset = offset + space;
            return this;
        }
    }
}
