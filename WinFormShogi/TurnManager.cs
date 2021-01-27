using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinFormShogi
{
    public static class TurnManager
    {
        public static Turn turn = Turn.PLAYERTURN;
        public static int handlingCount = 0;
        public static int preCount = 0;
        static Random random = new Random();


        //先後決定
        public static void TurnShuffle(Control control)
        {
            //turn = random.Next(2);  現状は先手番開始で固定

            if (turn == Turn.PLAYERTURN)
            {
                control.Text = "手番：あなた";
                MessageBox.Show("あなたが先手です");
            }
            else if (turn == Turn.COMTURN)
            {
                control.Text = "手番：コンピュータ";
                MessageBox.Show("あなたは後手です");
            }
        }

        //待機（考慮中）時
        public static void RoundTurn(List<Piece> playerPieces, List<Piece> comPieces, List<Piece> emptyPieces)
        {
            TurnStart(playerPieces, comPieces, emptyPieces);
            Console.WriteLine(turn);

            while (true)
            {
                //if (詰みならば)
                //{
                //    break;
                //}
                //手番カウントが進めば手番変更処理
                if (handlingCount > preCount)
                {
                    TurnStart(playerPieces, comPieces, emptyPieces);
                    preCount = handlingCount;
                    Console.WriteLine(turn);
                }
            }
        }


        //手番変更処理
        public static void TurnStart(List<Piece> playerPieces, List<Piece> comPieces, List<Piece> emptyPieces)
        {
            //プレイヤー手番の時
            if (turn == Turn.PLAYERTURN)
            {
                foreach (var playerPiece in playerPieces)
                {
                    playerPiece.eventMaking();
                }
                foreach (var comPiece in comPieces)
                {
                    comPiece.eventSuspend();
                }
                foreach (var emptyPiece in emptyPieces)
                {
                    emptyPiece.eventSuspend();
                }
            }
            else if (turn == Turn.COMTURN)//COM手番の時
            {
                foreach (var playerPiece in playerPieces)
                {
                    playerPiece.eventSuspend();
                }
                foreach (var comPiece in comPieces)
                {
                    comPiece.eventMaking();
                }
                foreach (var emptyPiece in emptyPieces)
                {
                    emptyPiece.eventSuspend();
                }
            }
        }

        //選択された駒が動いたかどうか
        public static void Move()
        {

        }

        //選択された駒が動けるかどうかの判断
        public static void CanMove()
        {

        }
    }
}
