/*
 * Transition.cs
 *   概要：
 *     このクラスは、画面遷移を定義します。
 *     Transitionディレクトリ内に登録された画面から別の画面に切り替える制御を行います。
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;

namespace BTB01
{
    // 画面遷移用定数
    public enum Scene
    {
        GAME = 0,
    }


    public static class Transition
    {
        public static Scene scene {get; private set; }  // 現在の画面
        private static int count;  // 現在の画面に変わってからのフレーム数

        /**
         *  メイン関数が呼び出す部分
         */
        public static void main()
        {
            switch (scene)
            {
                case Scene.GAME:
                    Game.Game.main();
                    break;
            }
            count++;
        }

        /**
         *  sceneを変更する。
         *  @params next_scene:変更するscene
         *  @return 成功:0
         */ 
        public static int changeScene(Scene next_scene)
        {
            scene = next_scene;
            count = 0;
            return 0;
        }        
    }
}
