using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinFormShogi
{
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
