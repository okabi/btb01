/*
 * Main.cs
 *   概要
 *     このクラスは、メイン制御を定義します。
 *     アプリケーション実行時、実際に呼び出されるのはmain.Main()関数です。
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;

namespace BTB01
{
    public static class main
    {
        /**
         *  メイン関数
         */ 
        public static void Main()
        {
            // ウィンドウモードに切り替え
            DX.ChangeWindowMode(DX.TRUE);

            // DXライブラリの初期化に失敗したら終了
            if (DX.DxLib_Init() == -1)
            {
                return;
            }

            // テスト用
            Transition.changeScene(Scene.GAME);

            // Transitionのメイン関数を呼ぶ
            Transition.main();

            // キー入力待ち
            DX.WaitKey();

            // DXライブラリの終了
            DX.DxLib_End();
        }
    }
}
