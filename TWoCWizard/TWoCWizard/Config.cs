using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TWoCWizard
{
    public static class Config
    {
        public static int Page;
        public static int Mode; // 0 - not selected, 1 - ps2, 2 - xbox, 3 gc
        public static int X, Y, W, H, W1, W2, H1, H2, twumpa, ttload, tmload, tblack;

        public static Bitmap wumpa, tload, mload, black;

        public static void Setup()
        {
            X = 0;
            Y = 0;
            W = 100;
            H = 100;
            W1 = 50;
            H1 = 50;
            W2 = 50;
            H2 = 50;
            twumpa = 0;
            ttload = 0;
            tmload = 0;
            tblack = 0;

            Page = 0;
            Mode = 0;

            wumpa = new Bitmap(100,100);
            tload = new Bitmap(100,100);
            mload = new Bitmap(100, 100);
            black = new Bitmap(100, 100);
        }

        public static void Save()
        {
            string text = X + "\n" + Y + "\n" + W + "\n" + H + "\n" +
                        W1 + "\n" + H2 + "\n" + H1 + "\n" + W2 + "\n" +
                        tblack + "\n" + ttload + "\n" + tmload + "\n" + twumpa + "\n";

            File.WriteAllText("settings.ini", text);

            wumpa.Save("saved4.bmp");
            tload.Save("saved2.bmp");
            mload.Save("saved3.bmp");
            black.Save("saved.bmp");

        }
    }
}
