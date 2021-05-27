# 将棋（Winフォームアプリ）
## 更新履歴
- 2021/02/03(初回)
## 使い方
- ZIPダウンロード(画面上部の緑色Codeボタン→Download ZIP)
- WinFormShogi.sinファイルをVisualStudioで起動
- デバッグ無しで開始
- 一度エラーが出ますが、一旦終了
- VIsualStudioも終了
- フォルダ「Pieces」を、WinFormShogi → bin → Debugフォルダ内に移動
- DebugフォルダのWinFormShogiアプリケーションを起動してスタート

## 概要
C#を学習して初めて製作したアプリです
- 将棋ゲームアプリ
- COM側自動着手は未実装

## コード説明
### Form1
- 盤の描画
- 9\*9=81マスに空のPieceを配置
- 対局持ち時間選択後、対局開始ボタンで駒を初期配置（指定Pieceに駒情報を渡す）
- 手番・手数・持ち時間表示
- 持ち時間管理
### TurnManager
- 先手・後手の決定
- 先手・後手のターン管理
- 勝敗決定（王将奪取or時間切れ）
### Piece
- 駒クラス（PictureBoxを継承）
- 駒の操作・ルールは全てこのクラスに記載
### Turn
- enum 先手・後手
### Owner
- enum 自分・相手・空
### Fugou
- Struct 符号（将棋ルールと同等右上が(1,1)）
