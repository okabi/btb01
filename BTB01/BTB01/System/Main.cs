/*
 * Main.cs
 *   概要
 *     このクラスは、メイン制御を定義します。
 *     アプリケーション実行時、実際に呼び出されるのはmain.Main()関数です。
 *   このクラスを呼び出すファイル
 *     
 *   このクラスが呼び出すファイル
 *     System/Transition.cs
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
using BTB01.System.Generic;

namespace BTB01.System
{
    class main
    {
        static void Main()
        {
            // ウィンドウモードに切り替え
            DX.ChangeWindowMode(DX.TRUE);

            // DXライブラリの初期化に失敗したら終了
            if (DX.DxLib_Init() == -1)
            {
                return;
            }

            Generic.CollisionSquare test = new Generic.CollisionSquare(new double[] { 1.0, 4.0 }, new double[] { 4.0, 3.0 }, collisionType.PLAYER);
            String st = (test.x[0] * test.y[0]).ToString() + (test.x[1] * test.y[1]).ToString();
            DX.DrawString(100, 200, st, DX.GetColor(255, 255, 255));

            // キー入力待ち
            DX.WaitKey();

            // DXライブラリの終了
            DX.DxLib_End();
        }
    }
}
