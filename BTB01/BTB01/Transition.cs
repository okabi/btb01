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
    /// <summary>画面遷移用定数</summary>
    public enum SceneID
    {
        GAME = 0,
    }


    /// <summary>画面遷移用の静的クラス。</summary>
    public static class Transition
    {
        /// <summary>現在の画面</summary>
        public static SceneID Scene { get; private set; }
        /// <summary>現在の画面に変わってからのフレーム数</summary>
        public static int Count { get; private set; }

        /// <summary>
        /// メイン関数が呼び出す部分。
        /// </summary>
        public static void main()
        {
            switch (Scene)
            {
                case SceneID.GAME:
                    Game.Game.main();
                    break;
            }
            Count++;
        }

        
        /// <summary>
        /// sceneを変更する。
        /// </summary>
        /// <param name="next_scene">変更するscene</param>
        /// <returns>成功->0</returns>
        public static int changeScene(SceneID next_scene)
        {
            Scene = next_scene;
            Count = 0;
            return 0;
        }        
    }
}
