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
        public Fugou Fugou { get; set; }

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

        //駒クリックイベント
        //１度クリックされたら色変更、再度クリックで元に戻す
        public void doClickEvent(object sender, EventArgs e)
        {
            if (this.Onclick == 0)
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

            else if (this.Onclick == 1)
            {
                Onclick = 0;
                this.BackColor = Color.Transparent;
                this.BorderStyle = BorderStyle.None;
            }
        }
    }
}
