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
            load();
            mainLoop();
            end();
        }



        /**
         * プログラム全体のロード
         */
        private static void load()
        {
            // ウィンドウモードに切り替え
            DX.ChangeWindowMode(DX.TRUE);

            // DXライブラリの初期化に失敗したら終了
            if (DX.DxLib_Init() == -1) return;

            // 裏画面描画に切り替え
            DX.SetDrawScreen(DX.DX_SCREEN_BACK);

            // ゲーム全体の初期化
            Transition.load();

            // ↓テスト用
            Transition.changeScene(SceneID.GAME);
            // ↑テスト用
        }



        /**
         * プログラムのメイン部分
         */
        private static void mainLoop()
        {
            do
            {
                // 前回描画したものを消去
                DX.ClearDrawScreen();

                // OSにメッセージを渡す
                if (DX.ProcessMessage() == -1) break;

                // デバイスの入力を取得
                Input.main();

                // Transitionのメイン関数を呼ぶ
                Transition.main();

                // 表画面に描画
                DX.ScreenFlip();
            } while (Input.data[DeviceID.KEY][DX.KEY_INPUT_LALT] == 0 || Input.data[DeviceID.KEY][DX.KEY_INPUT_F4] == 0);
        }


        
        /**
         * プログラムの終了
         */
        private static void end()
        {
            // DXライブラリの終了
            DX.DxLib_End();
        }
    }
}
