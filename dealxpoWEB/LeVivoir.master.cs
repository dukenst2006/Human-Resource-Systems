using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;


public partial class LeVivoir : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        /*
        System.Drawing.Image img = System.Drawing.Image.FromFile(Server.MapPath("images/Splash_Screen.png"));

        Bitmap bmp = new Bitmap(img, 637, 346);
        int x = bmp.Size.Width;
        int y = bmp.Size.Height;

        int shade = 150;

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Color c = bmp.GetPixel(i, j);
                if (c.R > shade & c.G > shade & c.B > shade)
                {
                    //Blue Background
                    //bmp.SetPixel(i, j, Color.FromArgb(11, 80, 149));
                    if (j < 150)
                    {
                        //Transparent Background PNG
                        bmp.SetPixel(i, j, Color.FromArgb(0, 255, 255, 255));
                    }
                }
            }
        }

        

        bmp.Save(Server.MapPath("images/Splash_Screen_Vivoir.png"), System.Drawing.Imaging.ImageFormat.Png);
        */

        /*
        System.Drawing.Image img = System.Drawing.Image.FromFile(Server.MapPath("images/marker-icon_1008.png"));

        Bitmap bmp = new Bitmap(img, 54, 60);
        int x = bmp.Size.Width;
        int y = bmp.Size.Height;

        int shade = 150;

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Color c = bmp.GetPixel(i, j);
                if (c.R > shade & c.G > shade & c.B > shade)
                {
                    if (j < 150)
                    {
                        //Transparent Background PNG
                        bmp.SetPixel(i, j, Color.FromArgb(0, 255, 255, 255));
                    }
                }
            }
        }



        bmp.Save(Server.MapPath("images/output/marker-icon_1008.png"), System.Drawing.Imaging.ImageFormat.Png);
        */
    }
}
