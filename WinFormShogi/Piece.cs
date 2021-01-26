using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormShogi
{
    public class Piece : PictureBox
    {
        //public string Name { get; set; }  PictureBoxに存在するので削除

        public string PicAdress { get; set; }
        public int Onclick { get; set; } = 0;
        public int[,] dirs = new int[3, 3];
        private Form1 form1;

        public Piece(Form1 form1)
        {
            this.form1 = form1;
        }

        public void eventMaking()
        {
            this.Click += new EventHandler(doClickEvent);
        }

        public void eventSuspend()
        {
            this.Click -= new EventHandler(doClickEvent);
        }

        //１度クリックされたら色変更、再度クリックで元に戻す
        public void doClickEvent(object sender, EventArgs e)
        {
            if (Onclick == 0)
            {
                form1.pieces.ForEach(x =>
                {
                    x.BackColor = Color.Transparent;
                    x.BorderStyle = BorderStyle.None;
                });
                Onclick = 1;
                this.BackColor = Color.Khaki;
                this.BorderStyle = BorderStyle.FixedSingle;
            }

            else if (Onclick == 1)
            {
                Onclick = 0;
                this.BackColor = Color.Transparent;
                this.BorderStyle = BorderStyle.None;
            }
        }
    }
}

//if (name == "歩")
//{
//    this.dirs[0, 0] = 0;
//    this.dirs[0, 1] = 1;
//    this.dirs[0, 2] = 0;
//    this.dirs[1, 0] = 0;
//    this.dirs[1, 1] = 0;
//    this.dirs[1, 2] = 0;
//    this.dirs[2, 0] = 0;
//    this.dirs[2, 1] = 0;
//    this.dirs[2, 2] = 0;
//}
