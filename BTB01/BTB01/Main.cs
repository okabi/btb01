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

            // ↓テスト用
            Transition.changeScene(Scene.GAME);
            // ↑テスト用

            do
            {
                // OSにメッセージを渡す
                if (DX.ProcessMessage() == -1) break;

                // デバイスの入力を取得
                Input.main();

                // Transitionのメイン関数を呼ぶ
                Transition.main();
            } while (Input.data[DeviceID.KEY][DX.KEY_INPUT_LALT] == 0 || Input.data[DeviceID.KEY][DX.KEY_INPUT_F4] == 0);

            // DXライブラリの終了
            DX.DxLib_End();
        }
    }
}
