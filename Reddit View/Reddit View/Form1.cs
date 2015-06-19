using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Windows;

namespace Reddit_View
{
    public partial class RedditView : Form
    {
        utilities util;
        System.Net.WebClient wc;
        System.Net.WebRequest wr;
        string URL;

        public RedditView()
        {
            InitializeComponent();
            util = new utilities();
            wc = new System.Net.WebClient();
            URL = "http://www.reddit.com";

           // this.ControlBox = false;
           // this.MaximizeBox = false;
           // this.MinimizeBox = false;
           // this.FormBorderStyle = FormBorderStyle.None;

            Timer timer = new Timer();
            timer.Interval = 6000;
            timer.Tick +=new EventHandler(timer_Tick);
            timer.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string webData = wc.DownloadString("http://www.reddit.com");
            string image = util.Search(webData);
            WebRequest requestPic = WebRequest.Create(image);
            WebResponse responsePic = requestPic.GetResponse();
            Image webImage = Image.FromStream(responsePic.GetResponseStream());
            Image resizedImage = util.resizeImage(webImage, webImage.Size);
            pictureBox1.Height = resizedImage.Height;
            pictureBox1.Width = resizedImage.Width;
            pictureBox1.Image = resizedImage;
            
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            string webData = wc.DownloadString(URL);
            string image = util.Search(webData);

            if (image.Equals(""))
            {

            }

            else
            {
                WebRequest requestPic = WebRequest.Create(image);
                WebResponse responsePic = requestPic.GetResponse();
                Image webImage = Image.FromStream(responsePic.GetResponseStream());
                Image resizedImage = util.resizeImage(webImage, pictureBox1.Size);
                pictureBox1.Height = resizedImage.Height;
                pictureBox1.Width = resizedImage.Width;
                pictureBox1.Image = resizedImage;
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Height = 418;
            pictureBox1.Width = 600;
            string webData = wc.DownloadString(URL);
            string image = util.Search(webData);

            if (image.Equals(""))
            {

            }

            else
            {
                WebRequest requestPic = WebRequest.Create(image);
                WebResponse responsePic = requestPic.GetResponse();
                Image webImage = Image.FromStream(responsePic.GetResponseStream());
                Image resizedImage = util.resizeImage(webImage, pictureBox1.Size);
                pictureBox1.Height = resizedImage.Height;
                pictureBox1.Width = resizedImage.Width;
                pictureBox1.Image = resizedImage;
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void quitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void removeBordersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            this.ControlBox = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void addBordersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ControlBox = true;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.FormBorderStyle = FormBorderStyle.Sizable;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            URL = "http://www.reddit.com/";
            
        }

        private void rfunnyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            URL = "http://www.reddit.com/r/funny";
        }

        private void rpicsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            URL = "http://www.reddit.com/r/pics";
        }

        private void rToolStripMenuItem_Click(object sender, EventArgs e)
        {
            URL = "http://www.reddit.com/r/WTF";
        }

        private void rawwToolStripMenuItem_Click(object sender, EventArgs e)
        {
            URL = "http://www.reddit.com/r/aww";
        }
    }
}
