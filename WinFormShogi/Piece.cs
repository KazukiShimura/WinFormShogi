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
        public List<Fugou> CanMovePosList { get; set; } = new List<Fugou>();

        Form1 form1;

        List<Piece> judgeList = new List<Piece>();

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
                    x.Onclick = 0;
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
                bool canmove = CanMove(form1.playerPieces, form1.comPieces, form1.pieces);
                if (form1.comPieces.Any(n => n.Onclick == 1) && canmove)
                {
                    if (this.Owner != Owner.EMPTY)
                    {
                        OnHandyBox(Turn.COMTURN);
                    }
                    ChangePiece(form1.playerPieces, form1.comPieces);

                    ClearBox(form1.comPieces);
                    ChangeBoxOff(this);
                    turnManager.turn = Turn.PLAYERTURN;
                    turnManager.handlingCount++;
                }
            }

            else if (this.Owner == Owner.COMPUTER)
            {
                bool canmove = CanMove(form1.playerPieces, form1.comPieces, form1.pieces);
                if (form1.playerPieces.Any(n => n.Onclick == 1) && canmove)
                {
                    if (this.Owner != Owner.EMPTY)
                    {
                        OnHandyBox(Turn.PLAYERTURN);
                    }
                    ChangePiece(form1.playerPieces, form1.comPieces);

                    ClearBox(form1.playerPieces);
                    ChangeBoxOff(this);
                    turnManager.turn = Turn.COMTURN;
                    turnManager.handlingCount++;
                }
            }

            else if (this.Owner == Owner.EMPTY)
            {
                bool canmove = CanMove(form1.playerPieces, form1.comPieces, form1.pieces);
                if (form1.playerPieces.Any(n => n.Onclick == 1) && canmove)
                {
                    ChangePiece(form1.playerPieces, form1.comPieces);
                    ClearBox(form1.playerPieces);
                    ChangeBoxOff(this);
                    turnManager.turn = Turn.COMTURN;
                    turnManager.handlingCount++;
                }

                else if (form1.comPieces.Any(n => n.Onclick == 1) && canmove)
                {
                    ChangePiece(form1.playerPieces, form1.comPieces);
                    ClearBox(form1.comPieces);
                    ChangeBoxOff(this);
                    turnManager.turn = Turn.PLAYERTURN;
                    turnManager.handlingCount++;
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
            if (turnManager.turn == Turn.PLAYERTURN)
            {
                var temp = new Piece(form1);
                temp = playerPieces.FirstOrDefault(n => n.Onclick == 1);
                this.Image = temp.Image;
                this.Name = temp.Name;
                this.CanMovePosList = temp.CanMovePosList;
                this.Owner = Owner.PLAYER;
                playerPieces.Add(this);
                if (form1.comPieces.Any(n => n == this))
                {
                    form1.comPieces.Remove(this);
                }
                else if (form1.emptyPieces.Any(n => n == this))
                {
                    form1.emptyPieces.Remove(this);
                }
            }
            else if (turnManager.turn == Turn.COMTURN)
            {
                var temp = new Piece(form1);
                temp = comPieces.FirstOrDefault(n => n.Onclick == 1);
                this.Image = temp.Image;
                this.Name = temp.Name;
                this.CanMovePosList = temp.CanMovePosList;
                this.Owner = Owner.COMPUTER;
                comPieces.Add(this);
                if (form1.playerPieces.Any(n => n == this))
                {
                    form1.playerPieces.Remove(this);
                }
                else if (form1.emptyPieces.Any(n => n == this))
                {
                    form1.emptyPieces.Remove(this);
                }
            }
        }

        //取った駒を駒台に乗せる
        public void OnHandyBox(Turn turn)
        {
            if (turn == Turn.PLAYERTURN)
            {
                var piece = new Piece(form1);
                piece.Image = this.Image;
                piece.Name = this.Name;
                piece.CanMovePosList = this.CanMovePosList;
                piece.Owner = Owner.PLAYER;
                piece.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                form1.pieces.Add(piece);
                form1.playerSubPieces.Add(piece);
                form1.comPieces.Remove(this);
                form1.playerSubPieces.OrderBy(n => n.Name);

                DrawSubPiece(form1.playerSubPieces);
            }
            else if (turn == Turn.COMTURN)
            {
                var piece = new Piece(form1);
                piece.Image = this.Image;
                piece.Name = this.Name;
                piece.CanMovePosList = this.CanMovePosList;
                piece.Owner = Owner.COMPUTER;
                piece.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                form1.pieces.Add(piece);
                form1.comSubPieces.Add(piece);
                form1.playerPieces.Remove(this);
                form1.comSubPieces.OrderBy(n => n.Name);

                DrawSubPiece(form1.comSubPieces);
            }
        }

        //持ち駒描画
        public void DrawSubPiece(List<Piece> subPieces)
        {
            for (int i = 0; i < subPieces.Count; i++)
            {
                subPieces[i].Size = new Size(30, 35);
                subPieces[i].SizeMode = PictureBoxSizeMode.Zoom;
                subPieces[i].BackColor = Color.Transparent;
                subPieces[i].Onclick = 0;
                form1.Controls.Add(subPieces[i]);
                subPieces[i].BringToFront();
                if (subPieces == form1.playerSubPieces)
                {
                    form1.playerSubPieces[i].Location = new Point(370, 100 + (i * 35));
                }
                else if (subPieces == form1.comSubPieces)
                {
                    form1.comSubPieces[i].Location = new Point(10, 30 + (i * 35));
                }
            }
        }


        //移動前のマスをEMPTY化
        public void ClearBox(List<Piece> pieces)
        {
            var temp = new Piece(form1);
            temp = pieces.FirstOrDefault(n => n.Onclick == 1);
            temp.Image = null;
            temp.Name = null;
            temp.Owner = Owner.EMPTY;
            temp.CanMovePosList = null;
            temp.Onclick = 0;
            ChangeBoxOff(temp);
            form1.emptyPieces.Add(temp);
            pieces.Remove(temp);
        }

        //動ける場所か判断
        public bool CanMove(List<Piece> playerPieces, List<Piece> comPieces, List<Piece> pieces)
        {
            if (turnManager.turn == Turn.PLAYERTURN && playerPieces.Any(n => n.Onclick == 1))
            {
                var temp = new Piece(form1);
                temp = playerPieces.FirstOrDefault(n => n.Onclick == 1);
                var diff = this.Fugou - temp.Fugou;
                judgeList.Clear();

                //途中に駒があったら動けない
                if (temp.CanMovePosList.Any(n => n.X == diff.X && n.Y == diff.Y))
                {
                    if (temp.Name == "香車")
                    {
                        var tempList = pieces.Where(n => n.Fugou.X == temp.Fugou.X).ToList();
                        for (int i = 0; i < tempList.Count(); i++)
                        {
                            for (int m = this.Fugou.Y + 1; m <= temp.Fugou.Y - 1; m++)
                            {
                                if (tempList[i].Fugou.Y == m)
                                {
                                    judgeList.Add(tempList[i]);
                                }
                            }
                        }
                        if (judgeList.All(n => n.Owner == Owner.EMPTY)) return true;
                        return false;
                    }
                    else if (temp.Name == "飛車")
                    {
                        var tempListY = pieces.Where(n => n.Fugou.X == temp.Fugou.X).ToList();
                        var tempListX = pieces.Where(n => n.Fugou.Y == temp.Fugou.Y).ToList();
                        if (diff.X == 0)
                        {
                            for (int i = 0; i < tempListY.Count(); i++)
                            {
                                if (diff.Y < 0)
                                {
                                    for (int m = this.Fugou.Y + 1; m <= temp.Fugou.Y - 1; m++)
                                    {
                                        if (tempListY[i].Fugou.Y == m)
                                        {
                                            judgeList.Add(tempListY[i]);
                                        }
                                    }
                                }
                                else if (diff.Y > 0)
                                {
                                    for (int m = temp.Fugou.Y + 1; m <= this.Fugou.Y - 1; m++)
                                    {
                                        if (tempListY[i].Fugou.Y == m)
                                        {
                                            judgeList.Add(tempListY[i]);
                                        }
                                    }
                                }
                            }
                        }

                        else if (diff.Y == 0)
                        {
                            for (int i = 0; i < tempListX.Count(); i++)
                            {
                                if (diff.X < 0)
                                {
                                    for (int m = this.Fugou.X + 1; m <= temp.Fugou.X - 1; m++)
                                    {
                                        if (tempListX[i].Fugou.X == m)
                                        {
                                            judgeList.Add(tempListX[i]);
                                        }
                                    }
                                }
                                else if (diff.X > 0)
                                {
                                    for (int m = temp.Fugou.X + 1; m <= this.Fugou.X - 1; m++)
                                    {
                                        if (tempListX[i].Fugou.X == m)
                                        {
                                            judgeList.Add(tempListX[i]);
                                        }
                                    }
                                }
                            }
                        }
                        if (judgeList.All(n => n.Owner == Owner.EMPTY)) return true;
                        return false;
                    }
                    else if (temp.Name == "角行")
                    {
                        for (int i = 0; i < pieces.Count(); i++)
                        {
                            if (diff.X == 1 && diff.Y == 1 || diff.X == -1 && diff.Y == -1 || diff.X == -1 && diff.Y == 1 || diff.X == 1 && diff.Y == -1)
                            {
                                return true;
                            }

                            else if (diff.X > 0 && diff.Y > 0)
                            {
                                for (int m = 1; m <= diff.X - 1; m++)
                                {
                                    if (pieces[i].Fugou.X == temp.Fugou.X + m && pieces[i].Fugou.Y == temp.Fugou.Y + m)
                                    {
                                        judgeList.Add(pieces[i]);
                                    }
                                }
                            }
                            else if (diff.X < 0 && diff.Y < 0)
                            {
                                for (int m = diff.X + 1; m <= -1; m++)
                                {
                                    if (pieces[i].Fugou.X == temp.Fugou.X + m && pieces[i].Fugou.Y == temp.Fugou.Y + m)
                                    {
                                        judgeList.Add(pieces[i]);
                                    }
                                }
                            }
                            else if (diff.X < 0 && diff.Y > 0)
                            {
                                for (int m = diff.X + 1; m <= -1; m++)
                                {
                                    if (pieces[i].Fugou.X == temp.Fugou.X + m && pieces[i].Fugou.Y == temp.Fugou.Y - m)
                                    {
                                        judgeList.Add(pieces[i]);
                                    }
                                }
                            }
                            else if (diff.X > 0 && diff.Y < 0)
                            {
                                for (int m = 1; m <= diff.X - 1; m++)
                                {
                                    if (pieces[i].Fugou.X == temp.Fugou.X + m && pieces[i].Fugou.Y == temp.Fugou.Y - m)
                                    {
                                        judgeList.Add(pieces[i]);
                                    }
                                }
                            }
                        }
                        if (judgeList.All(n => n.Owner == Owner.EMPTY)) return true;
                        return false;
                    }
                }

                return temp.CanMovePosList.Any(n => n.X == diff.X && n.Y == diff.Y);
            }
            else if (turnManager.turn == Turn.COMTURN && comPieces.Any(n => n.Onclick == 1))
            {
                var temp = new Piece(form1);
                temp = comPieces.FirstOrDefault(n => n.Onclick == 1);
                var diff = this.Fugou - temp.Fugou;
                judgeList.Clear();

                //途中に駒があったら動けない
                if (temp.CanMovePosList.Any(n => n.X == diff.X && n.Y == diff.Y))
                {
                    if (temp.Name == "香車")
                    {
                        var tempList = pieces.Where(n => n.Fugou.X == temp.Fugou.X).ToList();
                        for (int i = 0; i < tempList.Count(); i++)
                        {
                            for (int m = temp.Fugou.Y + 1; m <= this.Fugou.Y - 1; m++)
                            {
                                if (tempList[i].Fugou.Y == m)
                                {
                                    judgeList.Add(tempList[i]);
                                }
                            }
                        }
                        if (judgeList.All(n => n.Owner == Owner.EMPTY)) return true;
                        return false;
                    }
                    else if (temp.Name == "飛車")
                    {
                        var tempListY = pieces.Where(n => n.Fugou.X == temp.Fugou.X).ToList();
                        var tempListX = pieces.Where(n => n.Fugou.Y == temp.Fugou.Y).ToList();
                        if (diff.X == 0)
                        {
                            for (int i = 0; i < tempListY.Count(); i++)
                            {
                                if (diff.Y < 0)
                                {
                                    for (int m = this.Fugou.Y + 1; m <= temp.Fugou.Y - 1; m++)
                                    {
                                        if (tempListY[i].Fugou.Y == m)
                                        {
                                            judgeList.Add(tempListY[i]);
                                        }
                                    }
                                }
                                else if (diff.Y > 0)
                                {
                                    for (int m = temp.Fugou.Y + 1; m <= this.Fugou.Y - 1; m++)
                                    {
                                        if (tempListY[i].Fugou.Y == m)
                                        {
                                            judgeList.Add(tempListY[i]);
                                        }
                                    }
                                }
                            }
                        }

                        else if (diff.Y == 0)
                        {
                            for (int i = 0; i < tempListX.Count(); i++)
                            {
                                if (diff.X < 0)
                                {
                                    for (int m = this.Fugou.X + 1; m <= temp.Fugou.X - 1; m++)
                                    {
                                        if (tempListX[i].Fugou.X == m)
                                        {
                                            judgeList.Add(tempListX[i]);
                                        }
                                    }
                                }
                                else if (diff.X > 0)
                                {
                                    for (int m = temp.Fugou.X + 1; m <= this.Fugou.X - 1; m++)
                                    {
                                        if (tempListX[i].Fugou.X == m)
                                        {
                                            judgeList.Add(tempListX[i]);
                                        }
                                    }
                                }
                            }
                        }
                        if (judgeList.All(n => n.Owner == Owner.EMPTY)) return true;
                        return false;
                    }
                    else if (temp.Name == "角行")
                    {
                        for (int i = 0; i < pieces.Count(); i++)
                        {
                            if (diff.X == 1 && diff.Y == 1 || diff.X == -1 && diff.Y == -1 || diff.X == -1 && diff.Y == 1 || diff.X == 1 && diff.Y == -1)
                            {
                                return true;
                            }

                            else if (diff.X > 0 && diff.Y > 0)
                            {
                                for (int m = 1; m <= diff.X - 1; m++)
                                {
                                    if (pieces[i].Fugou.X == temp.Fugou.X + m && pieces[i].Fugou.Y == temp.Fugou.Y + m)
                                    {
                                        judgeList.Add(pieces[i]);
                                    }
                                }
                            }
                            else if (diff.X < 0 && diff.Y < 0)
                            {
                                for (int m = diff.X + 1; m <= -1; m++)
                                {
                                    if (pieces[i].Fugou.X == temp.Fugou.X + m && pieces[i].Fugou.Y == temp.Fugou.Y + m)
                                    {
                                        judgeList.Add(pieces[i]);
                                    }
                                }
                            }
                            else if (diff.X < 0 && diff.Y > 0)
                            {
                                for (int m = diff.X + 1; m <= -1; m++)
                                {
                                    if (pieces[i].Fugou.X == temp.Fugou.X + m && pieces[i].Fugou.Y == temp.Fugou.Y - m)
                                    {
                                        judgeList.Add(pieces[i]);
                                    }
                                }
                            }
                            else if (diff.X > 0 && diff.Y < 0)
                            {
                                for (int m = 1; m <= diff.X - 1; m++)
                                {
                                    if (pieces[i].Fugou.X == temp.Fugou.X + m && pieces[i].Fugou.Y == temp.Fugou.Y - m)
                                    {
                                        judgeList.Add(pieces[i]);
                                    }
                                }
                            }
                        }
                        if (judgeList.All(n => n.Owner == Owner.EMPTY)) return true;
                        return false;
                    }
                }

                return temp.CanMovePosList.Any(n => n.X == diff.X && n.Y == diff.Y);
            }
            else
            {
                return false;
            }
        }

        //成れるか判断
        public bool CanReverse(List<Piece> playerPieces, List<Piece> comPieces)
        {
            var temp = new Piece(form1);

            if (turnManager.turn == Turn.PLAYERTURN && playerPieces.Any(n => n.Onclick == 1))
            {
                temp = playerPieces.FirstOrDefault(n => n.Onclick == 1);
                if (this.Fugou.Y == 1 || this.Fugou.Y == 2 || this.Fugou.Y == 3) return true;
                else if (temp.Fugou.Y == 1 || temp.Fugou.Y == 2 || temp.Fugou.Y == 3) return true;
                else return false;

            }
            else if (turnManager.turn == Turn.COMTURN && comPieces.Any(n => n.Onclick == 1))
            {
                temp = comPieces.FirstOrDefault(n => n.Onclick == 1);
                if (this.Fugou.Y == 7 || this.Fugou.Y == 8 || this.Fugou.Y == 9) return true;
                else if (temp.Fugou.Y == 7 || temp.Fugou.Y == 8 || temp.Fugou.Y == 9) return true;
                else return false;
            }
            else return false;
        }
    }
}
