using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormShogi
{
    public struct Fugou
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Fugou(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    public partial class Form1 : Form
    {
        public List<Piece> pieces = new List<Piece>();
        public List<Piece> playerPieces = new List<Piece>();
        public List<Piece> comPieces = new List<Piece>();

        public Form1()
        {
            InitializeComponent();
            //  ReadFromFile();
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
                    e.Graphics.DrawRectangle(pen1, 20 + (i * 35), 20 + (j * 40), 35, 40); // 長方形
                }
            }
            pen1.Dispose();
        }

        //Piecesリストに駒全種類読込み
        //void ReadFromFile()
        //{
        //    var lines = File.ReadAllLines(@"Pieces.txt");

        //    foreach (var line in lines)
        //    {
        //        var pieceLine = line.Split(',');
        //        Piece piece = new Piece(this);
        //        pieces.Add(piece);
        //        piece.Name = pieceLine[0];
        //        piece.Image = Image.FromFile(pieceLine[1]);
        //        piece.Size = new Size(35, 40);
        //        piece.SizeMode = PictureBoxSizeMode.Zoom;
        //        piece.BackColor = Color.Transparent;
        //    }
        //}

        private void InitEmptySet()
        {
            int i = 0;
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    Piece piece = new Piece(this);
                    this.pieces.Add(piece);
                    piece.Size = new Size(35, 40);
                    piece.SizeMode = PictureBoxSizeMode.Zoom;
                    piece.BackColor = Color.Transparent;
                    Controls.Add(this.pieces[i]);
                    piece.Fugou = new Fugou(9 - x, y + 1);
                    //piece.Image = Image.FromFile(@"Pieces\fu.png");
                    this.pieces[i].Location = new Point(20 + x * 35, 20 + y * 40);
                    i++;
                }
            }
        }

        //スタートボタンで初期配置
        private void startButton_Click(object sender, EventArgs e)
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
                }
                else if (piece.Fugou.X == 2 && piece.Fugou.Y == 8)
                {
                    playerPieces.Add(piece);
                    piece.Name = "飛車";
                    piece.Image = Image.FromFile(@"Pieces\hisya.png");
                }
                else if (piece.Fugou.X == 8 && piece.Fugou.Y == 8)
                {
                    playerPieces.Add(piece);
                    piece.Name = "角行";
                    piece.Image = Image.FromFile(@"Pieces\kaku.png");
                }
                else if (piece.Fugou.X == 1 && piece.Fugou.Y == 9 ||
                    piece.Fugou.X == 9 && piece.Fugou.Y == 9)
                {
                    playerPieces.Add(piece);
                    piece.Name = "香車";
                    piece.Image = Image.FromFile(@"Pieces\kyou.png");
                }
                else if (piece.Fugou.X == 2 && piece.Fugou.Y == 9 ||
                    piece.Fugou.X == 8 && piece.Fugou.Y == 9)
                {
                    playerPieces.Add(piece);
                    piece.Name = "桂馬";
                    piece.Image = Image.FromFile(@"Pieces\kei.png");
                }
                else if (piece.Fugou.X == 3 && piece.Fugou.Y == 9 ||
                   piece.Fugou.X == 7 && piece.Fugou.Y == 9)
                {
                    playerPieces.Add(piece);
                    piece.Name = "銀将";
                    piece.Image = Image.FromFile(@"Pieces\gin.png");
                }
                else if (piece.Fugou.X == 4 && piece.Fugou.Y == 9 ||
                         piece.Fugou.X == 6 && piece.Fugou.Y == 9)
                {
                    playerPieces.Add(piece);
                    piece.Name = "金将";
                    piece.Image = Image.FromFile(@"Pieces\kin.png");
                }
                else if (piece.Fugou.X == 5 && piece.Fugou.Y == 9)
                {
                    playerPieces.Add(piece);
                    piece.Name = "王将";
                    piece.Image = Image.FromFile(@"Pieces\ou.png");
                }

                //相手駒
                else if (piece.Fugou.Y == 3)
                {
                    comPieces.Add(piece);
                    piece.Name = "歩兵";
                    piece.Image = Image.FromFile(@"Pieces\fu.png");
                }
                else if (piece.Fugou.X == 1 && piece.Fugou.Y == 1 ||
                         piece.Fugou.X == 9 && piece.Fugou.Y == 1)
                {
                    comPieces.Add(piece);
                    piece.Name = "香車";
                    piece.Image = Image.FromFile(@"Pieces\kyou.png");
                }
                else if (piece.Fugou.X == 2 && piece.Fugou.Y == 1 ||
                         piece.Fugou.X == 8 && piece.Fugou.Y == 1)
                {
                    comPieces.Add(piece);
                    piece.Name = "桂馬";
                    piece.Image = Image.FromFile(@"Pieces\kei.png");
                }
                else if (piece.Fugou.X == 3 && piece.Fugou.Y == 1 ||
                         piece.Fugou.X == 7 && piece.Fugou.Y == 1)
                {
                    comPieces.Add(piece);
                    piece.Name = "銀将";
                    piece.Image = Image.FromFile(@"Pieces\gin.png");
                }
                else if (piece.Fugou.X == 4 && piece.Fugou.Y == 1 ||
                         piece.Fugou.X == 6 && piece.Fugou.Y == 1)
                {
                    comPieces.Add(piece);
                    piece.Name = "金将";
                    piece.Image = Image.FromFile(@"Pieces\kin.png");
                }
                else if (piece.Fugou.X == 5 && piece.Fugou.Y == 1)
                {
                    comPieces.Add(piece);
                    piece.Name = "王将";
                    piece.Image = Image.FromFile(@"Pieces\ou.png");
                }
                else if (piece.Fugou.X == 2 && piece.Fugou.Y == 2)
                {
                    comPieces.Add(piece);
                    piece.Name = "角行";
                    piece.Image = Image.FromFile(@"Pieces\kaku.png");
                }
                else if (piece.Fugou.X == 8 && piece.Fugou.Y == 2)
                {
                    comPieces.Add(piece);
                    piece.Name = "飛車";
                    piece.Image = Image.FromFile(@"Pieces\hisya.png");
                }
            }

            foreach (var piece in comPieces)
            {
                piece.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                piece.Refresh();
            }

            TurnManager.Sengo(turnLabel);
            TurnManager.TurnStart(playerPieces, comPieces);

        }
    }
}
