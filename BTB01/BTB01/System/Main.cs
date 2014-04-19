/*
 * Main.cs
 *   このクラスは、メイン制御を定義します。
 *   実際に呼び出されるのはこのMain.Main()関数です。
 *   
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;

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

            DX.DrawString(100, 200, "ちんこｗ", DX.GetColor(255, 255, 255));

            // キー入力待ち
            DX.WaitKey();

            // DXライブラリの終了
            DX.DxLib_End();
        }
    }
}
