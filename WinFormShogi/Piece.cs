using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WinFormShogi
{
    public class Piece : PictureBox
    {
        //public string Name { get; set; }  PictureBoxに存在するので削除

        public string PicAdress { get; set; }
        public int Onclick { get; set; } = 0;
        public Fugou Fugou { get; set; }
        public Owner Owner { get; set; }

        private Form1 form1;

        public Piece(Form1 form1)
        {
            this.form1 = form1;
        }

        public void eventMaking()
        {
            this.Click += new EventHandler(ClickEvent);
            this.Click -= new EventHandler(moveEvent);
        }

        public void eventSuspend()
        {
            this.Click -= new EventHandler(ClickEvent);
            this.Click += new EventHandler(moveEvent);
        }

        //駒クリックイベント
        //１度クリックされたら色変更、再度クリックで元に戻す
        public void ClickEvent(object sender, EventArgs e)
        {
            if (Onclick == 0)
            {
                form1.pieces.ForEach(x =>
                {
                    x.BackColor = Color.Transparent;
                    x.BorderStyle = BorderStyle.None;
                });
                Onclick = 1;
                ChangeBoxOn(this);
            }

            else if (Onclick == 1)
            {
                Onclick = 0;
                ChangeBoxOff(this);
            }
        }

        //どれか駒がクリックされている状態で、他のマスをクリックしたときの処理
        public void moveEvent(object sender, EventArgs e)
        {
            if (this.Owner == Owner.PLAYER)
            {
                if (form1.comPieces.Any(n => n.Onclick == 1))
                {
                    ChangePiece(form1.playerPieces, form1.comPieces);
                    if (this.Owner != Owner.EMPTY)
                    {
                        form1.comSubPieces.Add(this);
                    }
                    ClearBox(form1.comPieces);
                    TurnManager.turn = Turn.PLAYERTURN;
                    TurnManager.handlingCount++;
                }
            }

            else if (this.Owner == Owner.COMPUTER)
            {
                if (form1.playerPieces.Any(n => n.Onclick == 1))
                {
                    ChangePiece(form1.playerPieces, form1.comPieces);
                    if (this.Owner != Owner.EMPTY)
                    {
                        form1.playerSubPieces.Add(this);
                    }
                    ClearBox(form1.playerPieces);
                    TurnManager.turn = Turn.COMTURN;
                    TurnManager.handlingCount++;
                }
            }

            else if (this.Owner == Owner.EMPTY)
            {
                if (form1.playerPieces.Any(n => n.Onclick == 1))
                {
                    ChangePiece(form1.playerPieces, form1.comPieces);
                    ClearBox(form1.playerPieces);
                    TurnManager.turn = Turn.COMTURN;
                    TurnManager.handlingCount++;
                }

                else if (form1.comPieces.Any(n => n.Onclick == 1))
                {
                    ChangePiece(form1.playerPieces, form1.comPieces);
                    ClearBox(form1.comPieces);
                    TurnManager.turn = Turn.PLAYERTURN;
                    TurnManager.handlingCount++;
                }
            }
        }



        //マスの色を付ける
        public void ChangeBoxOn(Piece piece)
        {
            piece.BackColor = Color.Khaki;
            piece.BorderStyle = BorderStyle.FixedSingle;
        }

        //マスの色を消す
        public void ChangeBoxOff(Piece piece)
        {
            piece.BackColor = Color.Transparent;
            piece.BorderStyle = BorderStyle.None;
        }

        //駒インスタンスの内容を引用して入れ替える
        public void ChangePiece(List<Piece> playerPieces, List<Piece> comPieces)
        {
            if (TurnManager.turn == Turn.PLAYERTURN)
            {
                var temp = new Piece(form1);
                temp = playerPieces.FirstOrDefault(n => n.Onclick == 1);
                this.Image = temp.Image;
                this.Name = temp.Name;
                playerPieces.Add(this);
                if (form1.comPieces.Any(n => n == this))
                {
                    form1.comPieces.Remove(this);
                }
                else if (form1.emptyPieces.Any(n => n == this))
                {
                    form1.emptyPieces.Remove(this);
                }
                ChangeBoxOn(this);
            }
            else if (TurnManager.turn == Turn.COMTURN)
            {
                var temp = new Piece(form1);
                temp = comPieces.FirstOrDefault(n => n.Onclick == 1);
                this.Image = temp.Image;
                this.Name = temp.Name;
                comPieces.Add(this);
                if (form1.playerPieces.Any(n => n == this))
                {
                    form1.playerPieces.Remove(this);
                }
                else if (form1.emptyPieces.Any(n => n == this))
                {
                    form1.emptyPieces.Remove(this);
                }
                ChangeBoxOn(this);
            }
        }

        //移動前のマスをEMPTY化
        public void ClearBox(List<Piece> pieces)
        {
            var temp = new Piece(form1);
            temp = pieces.FirstOrDefault(n => n.Onclick == 1);
            temp.Image = null;
            temp.Name = null;
            ChangeBoxOff(temp);
            form1.emptyPieces.Add(temp);
            pieces.Remove(temp);
        }
    }
}

/*
1つクリックされてるとき次のクリック先が、
    ・動けるマス目かどうか
        自ゴマがいない　●
        動けるポイントか
        間に相手がいないか

    ・動いた先に相手がいるか ▲
*/
