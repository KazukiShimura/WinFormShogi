﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormShogi
{

    public partial class Form1 : Form
    {
        public List<Piece> pieces = new List<Piece>();
        public List<Piece> emptyPieces = new List<Piece>();
        public List<Piece> playerPieces = new List<Piece>();
        public List<Piece> comPieces = new List<Piece>();
        public List<Piece> playerSubPieces = new List<Piece>();
        public List<Piece> comSubPieces = new List<Piece>();

        public Form1()
        {
            InitializeComponent();
            //  ReadFromFile();
            Piece piece = new Piece(this);
            InitEmptySet();
        }

        //盤の描画
        protected override void OnPaint(PaintEventArgs e)
        {
            Pen pen1 = new Pen(Color.Black, 1);
            int grid = 9;
            for (int i = 0; i < grid; i++)
            {
                for (int j = 0; j < grid; j++)
                {
                    e.Graphics.DrawRectangle(pen1, 50 + (i * 35), 20 + (j * 40), 35, 40); // 長方形
                }
            }
            pen1.Dispose();
        }
        private void InitEmptySet()
        {
            int i = 0;
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    Piece piece = new Piece(this);
                    this.pieces.Add(piece);
                    piece.Owner = WinFormShogi.Owner.EMPTY;
                    piece.Size = new Size(35, 40);
                    piece.SizeMode = PictureBoxSizeMode.Zoom;
                    piece.BackColor = Color.Transparent;
                    Controls.Add(this.pieces[i]);
                    piece.Fugou = new Fugou(9 - x, y + 1);
                    this.pieces[i].Location = new Point(50 + x * 35, 20 + y * 40);
                    i++;
                }
            }
        }

        //スタートボタンで初期配置
        private async void startButton_Click(object sender, EventArgs e)
        {
            startButton.Enabled = false;

            foreach (var piece in pieces)
            {
                //自駒
                if (piece.Fugou.Y == 7)
                {
                    playerPieces.Add(piece);
                    piece.Name = "歩兵";
                    piece.Image = Image.FromFile(@"Pieces\fu.png");
                    piece.CanMovePosList.Add(new Fugou(0, -1));
                }
                else if (piece.Fugou.X == 1 && piece.Fugou.Y == 9 ||
                    piece.Fugou.X == 9 && piece.Fugou.Y == 9)
                {
                    playerPieces.Add(piece);
                    piece.Name = "香車";
                    piece.Image = Image.FromFile(@"Pieces\kyou.png");
                    for (int i = -8; i <= -1; i++)
                    {
                        piece.CanMovePosList.Add(new Fugou(0, i));
                    }
                }
                else if (piece.Fugou.X == 2 && piece.Fugou.Y == 9 ||
                    piece.Fugou.X == 8 && piece.Fugou.Y == 9)
                {
                    playerPieces.Add(piece);
                    piece.Name = "桂馬";
                    piece.Image = Image.FromFile(@"Pieces\kei.png");
                    piece.CanMovePosList.Add(new Fugou(1, -2));
                    piece.CanMovePosList.Add(new Fugou(-1, -2));
                }
                else if (piece.Fugou.X == 3 && piece.Fugou.Y == 9 ||
                   piece.Fugou.X == 7 && piece.Fugou.Y == 9)
                {
                    playerPieces.Add(piece);
                    piece.Name = "銀将";
                    piece.Image = Image.FromFile(@"Pieces\gin.png");
                    piece.CanMovePosList.Add(new Fugou(-1, -1));
                    piece.CanMovePosList.Add(new Fugou(0, -1));
                    piece.CanMovePosList.Add(new Fugou(1, -1));
                    piece.CanMovePosList.Add(new Fugou(1, 1));
                    piece.CanMovePosList.Add(new Fugou(-1, 1));

                }
                else if (piece.Fugou.X == 4 && piece.Fugou.Y == 9 ||
                         piece.Fugou.X == 6 && piece.Fugou.Y == 9)
                {
                    playerPieces.Add(piece);
                    piece.Name = "金将";
                    piece.Image = Image.FromFile(@"Pieces\kin.png");
                    for (int i = -1; i <= 1; i++)
                    {
                        piece.CanMovePosList.Add(new Fugou(i, -1));
                        piece.CanMovePosList.Add(new Fugou(i, 0));
                    }
                    piece.CanMovePosList.Add(new Fugou(0, 1));

                }
                else if (piece.Fugou.X == 2 && piece.Fugou.Y == 8)
                {
                    playerPieces.Add(piece);
                    piece.Name = "飛車";
                    piece.Image = Image.FromFile(@"Pieces\hisya.png");

                    for (int i = -8; i <= 8; i++)
                    {
                        piece.CanMovePosList.Add(new Fugou(0, i));
                        piece.CanMovePosList.Add(new Fugou(i, 0));
                    }
                }
                else if (piece.Fugou.X == 8 && piece.Fugou.Y == 8)
                {
                    playerPieces.Add(piece);
                    piece.Name = "角行";
                    piece.Image = Image.FromFile(@"Pieces\kaku.png");
                    for (int i = -8; i <= 8; i++)
                    {
                        piece.CanMovePosList.Add(new Fugou(i, i));
                        piece.CanMovePosList.Add(new Fugou(i, -i));
                    }
                }
                else if (piece.Fugou.X == 5 && piece.Fugou.Y == 9)
                {
                    playerPieces.Add(piece);
                    piece.Name = "王将";
                    piece.Image = Image.FromFile(@"Pieces\ou.png");
                    for (int i = -1; i <= 1; i++)
                    {
                        piece.CanMovePosList.Add(new Fugou(i, -1));
                        piece.CanMovePosList.Add(new Fugou(i, 0));
                        piece.CanMovePosList.Add(new Fugou(i, 1));
                    }
                }

                //相手駒
                else if (piece.Fugou.Y == 3)
                {
                    comPieces.Add(piece);
                    piece.Name = "歩兵";
                    piece.Image = Image.FromFile(@"Pieces\fu.png");
                    piece.CanMovePosList.Add(new Fugou(0, 1));
                }
                else if (piece.Fugou.X == 1 && piece.Fugou.Y == 1 ||
                         piece.Fugou.X == 9 && piece.Fugou.Y == 1)
                {
                    comPieces.Add(piece);
                    piece.Name = "香車";
                    piece.Image = Image.FromFile(@"Pieces\kyou.png");
                    for (int i = 1; i <= 8; i++)
                    {
                        piece.CanMovePosList.Add(new Fugou(0, i));
                    }
                }
                else if (piece.Fugou.X == 2 && piece.Fugou.Y == 1 ||
                         piece.Fugou.X == 8 && piece.Fugou.Y == 1)
                {
                    comPieces.Add(piece);
                    piece.Name = "桂馬";
                    piece.Image = Image.FromFile(@"Pieces\kei.png");
                    piece.CanMovePosList.Add(new Fugou(1, 2));
                    piece.CanMovePosList.Add(new Fugou(-1, 2));
                }
                else if (piece.Fugou.X == 3 && piece.Fugou.Y == 1 ||
                         piece.Fugou.X == 7 && piece.Fugou.Y == 1)
                {
                    comPieces.Add(piece);
                    piece.Name = "銀将";
                    piece.Image = Image.FromFile(@"Pieces\gin.png");
                    piece.CanMovePosList.Add(new Fugou(-1, 1));
                    piece.CanMovePosList.Add(new Fugou(0, 1));
                    piece.CanMovePosList.Add(new Fugou(1, 1));
                    piece.CanMovePosList.Add(new Fugou(1, -1));
                    piece.CanMovePosList.Add(new Fugou(-1, -1));
                }
                else if (piece.Fugou.X == 4 && piece.Fugou.Y == 1 ||
                         piece.Fugou.X == 6 && piece.Fugou.Y == 1)
                {
                    comPieces.Add(piece);
                    piece.Name = "金将";
                    piece.Image = Image.FromFile(@"Pieces\kin.png");
                    for (int i = -1; i <= 1; i++)
                    {
                        piece.CanMovePosList.Add(new Fugou(i, 1));
                        piece.CanMovePosList.Add(new Fugou(i, 0));
                    }
                    piece.CanMovePosList.Add(new Fugou(0, -1));
                }
                else if (piece.Fugou.X == 2 && piece.Fugou.Y == 2)
                {
                    comPieces.Add(piece);
                    piece.Name = "角行";
                    piece.Image = Image.FromFile(@"Pieces\kaku.png");
                    for (int i = -8; i <= 8; i++)
                    {
                        piece.CanMovePosList.Add(new Fugou(i, i));
                        piece.CanMovePosList.Add(new Fugou(i, -i));
                    }
                }
                else if (piece.Fugou.X == 8 && piece.Fugou.Y == 2)
                {
                    comPieces.Add(piece);
                    piece.Name = "飛車";
                    piece.Image = Image.FromFile(@"Pieces\hisya.png");
                    for (int i = -8; i <= 8; i++)
                    {
                        piece.CanMovePosList.Add(new Fugou(0, i));
                        piece.CanMovePosList.Add(new Fugou(i, 0));
                    }
                }
                else if (piece.Fugou.X == 5 && piece.Fugou.Y == 1)
                {
                    comPieces.Add(piece);
                    piece.Name = "王将";
                    piece.Image = Image.FromFile(@"Pieces\ou.png");
                    for (int i = -1; i <= 1; i++)
                    {
                        piece.CanMovePosList.Add(new Fugou(i, -1));
                        piece.CanMovePosList.Add(new Fugou(i, 0));
                        piece.CanMovePosList.Add(new Fugou(i, 1));
                    }
                }
                else
                {
                    emptyPieces.Add(piece);
                    piece.Owner = WinFormShogi.Owner.EMPTY;
                }
            }

            foreach (var piece in playerPieces)
            {
                piece.Owner = WinFormShogi.Owner.PLAYER;
            }

            foreach (var piece in comPieces)
            {
                piece.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                piece.Refresh();
                piece.Owner = WinFormShogi.Owner.COMPUTER;
            }

            turnManager turnManager = new turnManager(this);
            turnManager.TurnShuffle(turnLabel);
            await Task.Run(() => turnManager.RoundTurn(playerPieces, comPieces, emptyPieces, turnLabel, countLabel, playerList, comList, emptyList, playerSubList, comSubList, playerSubPieces, comSubPieces));
        }
    }
}
