using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormShogi
{
    public partial class Form1 : Form
    {
        public List<Piece> pieces = new List<Piece>();
        public List<Piece> playerPieces = new List<Piece>();
        public List<Piece> comPieces = new List<Piece>();

        public Form1()
        {
            InitializeComponent();
            ReadFromFile();
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
        void ReadFromFile()
        {
            var lines = File.ReadAllLines(@"Pieces.txt");

            foreach (var line in lines)
            {
                var pieceLine = line.Split(',');
                Piece piece = new Piece(this);
                pieces.Add(piece);
                piece.Name = pieceLine[0];
                piece.Image = Image.FromFile(pieceLine[1]);
                piece.Size = new Size(35, 40);
                piece.SizeMode = PictureBoxSizeMode.Zoom;
                piece.BackColor = Color.Transparent;
            }
        }

        //スタートボタンで初期配置
        private void startButton_Click(object sender, EventArgs e)
        {
            startButton.Enabled = false;

            playerPieces.Add(pieces[0]);
            playerPieces.Add(pieces[1]);
            playerPieces.Add(pieces[2]);
            playerPieces.Add(pieces[3]);
            playerPieces.Add(pieces[4]);
            playerPieces.Add(pieces[5]);
            playerPieces.Add(pieces[6]);
            playerPieces.Add(pieces[7]);
            playerPieces.Add(pieces[8]);
            playerPieces.Add(pieces[9]);
            playerPieces.Add(pieces[11]);
            playerPieces.Add(pieces[13]);
            playerPieces.Add(pieces[15]);
            playerPieces.Add(pieces[17]);
            playerPieces.Add(pieces[16]);
            playerPieces.Add(pieces[14]);
            playerPieces.Add(pieces[12]);
            playerPieces.Add(pieces[10]);
            playerPieces.Add(pieces[18]);
            playerPieces.Add(pieces[19]);

            for (int i = 0; i < playerPieces.Count; i++)
            {
                Controls.Add(this.playerPieces[i]);
                playerPieces[i].Visible = true;
                if (i < 9)
                {
                    this.playerPieces[i].Location = new Point(20 + i * 35, 260);
                }
                else if (i < 18)
                {
                    this.playerPieces[i].Location = new Point(20 + (i - 9) * 35, 340);
                }
                else
                {
                    this.playerPieces[i].Location = new Point(55 + (i - 18) * 210, 300);
                }
            }

            comPieces.Add(pieces[29]);
            comPieces.Add(pieces[31]);
            comPieces.Add(pieces[33]);
            comPieces.Add(pieces[35]);
            comPieces.Add(pieces[37]);
            comPieces.Add(pieces[36]);
            comPieces.Add(pieces[34]);
            comPieces.Add(pieces[32]);
            comPieces.Add(pieces[30]);
            comPieces.Add(pieces[20]);
            comPieces.Add(pieces[21]);
            comPieces.Add(pieces[22]);
            comPieces.Add(pieces[23]);
            comPieces.Add(pieces[24]);
            comPieces.Add(pieces[25]);
            comPieces.Add(pieces[26]);
            comPieces.Add(pieces[27]);
            comPieces.Add(pieces[28]);
            comPieces.Add(pieces[39]);
            comPieces.Add(pieces[38]);

            for (int i = 0; i < comPieces.Count; i++)
            {
                this.Controls.Add(this.comPieces[i]);

                if (i < 9)
                {
                    this.comPieces[i].Location = new Point(20 + i * 35, 20);
                }
                else if (i < 18)
                {
                    this.comPieces[i].Location = new Point(20 + (i - 9) * 35, 100);
                }
                else
                {
                    this.comPieces[i].Location = new Point(55 + (i - 18) * 210, 60);
                }
            }

            foreach (var comPiece in comPieces)
            {
                comPiece.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                comPiece.Refresh();
            }

            //await Taikyoku();
            TurnManager.Sengo(turnLabel);
            TurnManager.TurnStart(playerPieces, comPieces);


        }

        public async Task Taikyoku()
        {
            //先後決定
            TurnManager.Sengo(turnLabel);

            //考慮中
            await Task.Run(() => { TurnManager.TurnStart(playerPieces, comPieces); });
        }

    }

    public static class TurnManager
    {
        static int turn = 0;
        static Random random = new Random();

        //先後決定
        public static void Sengo(Control control)
        {
            //turn = random.Next(2);  現状は先手番開始で固定

            if (turn == 0)
            {
                control.Text = "手番：あなた";
                MessageBox.Show("あなたが先手です");
            }
            else if (turn == 1)
            {
                control.Text = "手番：コンピュータ";
                MessageBox.Show("あなたは後手です");
            }
        }

        //待機（考慮中）時
        public static void TurnStart(List<Piece> playerPieces, List<Piece> comPieces)
        {
            bool move = false;

            while (true)
            {
                if (move)
                {
                    break;
                }

                //プレイヤー手番の時
                if (turn == 0)
                {
                    foreach (var playerPiece in playerPieces)
                    {
                        playerPiece.eventMaking();
                    }
                    foreach (var comPiece in comPieces)
                    {
                        comPiece.eventSuspend();
                    }
                    Console.WriteLine("プレイヤ・まだ");
                }
                else if (turn == 1)//COM手番の時
                {
                    foreach (var playerPiece in playerPieces)
                    {
                        playerPiece.eventSuspend();
                    }
                    foreach (var comPiece in comPieces)
                    {
                        comPiece.eventMaking();
                    }
                }

                move = true;
                //move = TurnManager.Move();
            }

        }

        //選択された駒が動いたかどうか
        public static bool Move()
        {
            //if (移動可能なマスをクリックしたら)
            //{
            //return true;
            //}
            return false;
        }

        //選択された駒が動けるかどうかの判断
        public static void CanMove()
        {

        }
    }
}
