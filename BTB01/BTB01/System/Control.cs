/*
 * Control.cs
 *   概要
 *     このクラスは、システム制御を定義します。
 *     描画や当たり判定の処理を行います。
 *   このクラスを呼び出すファイル
 *     
 *   このクラスが呼び出すファイル
 *     
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
using BTB01.System.Generic;

namespace BTB01.System
{
    class Control
    {
        /*
         * 当たり判定の処理
         */ 
        public static class Collision
        {
            /* 四角形と四角形の当たり判定（位置補正無し）
             * 引数：
             *     CollisionSquare a => 四角形の当たり判定
             *     CollisionSquare b => 四角形の当たり判定
             * 戻り値：
             *     0 => 当たってない
             *     1 => 当たってる
             */
            public static int squareSquare(CollisionSquare a, CollisionSquare b)
            {
                if (a.x[0] < b.x[1] && b.x[0] < a.x[1] && a.y[0] < b.y[1] && b.y[0] < a.y[0])
                {
                    return 1;
                }
                return 0;
            }
        }
    }
}
