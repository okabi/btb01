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


            /* 線分と線分の当たり判定（位置補正有り）
             * 引数：
             *     CollisionSegment cha => 線分の当たり判定（位置補正対象）
             *     CollisionSegment obj => 線分の当たり判定（マップなど動かない方）
             * 戻り値：
             *     return[0]==0.0 => 当たってない
             *              ==1.0 => chaの上下に当たってる(return[1]は補正でchaのy座標に加算すべき値)
             *              ==2.0 => chaの左右に当たってる(return[1]は補正でchaのx座標に加算すべき値)
             */
            public static double[] segmentSegment(CollisionSegment cha, CollisionSegment obj)
            {
                // 当たり判定を行って意味があるタイプ同士かチェック
                if (obj.type == collisionType.OBJECT && (cha.type | collisionType.ATTACK) == 0 && (cha.type | collisionType.PENETRATE) == 0)
                {
                    // 当たり判定を行って意味があるパート同士かチェック
                    if ((obj.part == collisionPart.UPPER && cha.part == collisionPart.LOWER) || (obj.part == collisionPart.LOWER && cha.part == collisionPart.UPPER) ||
                        (obj.part == collisionPart.LEFT && cha.part == collisionPart.RIGHT) || (obj.part == collisionPart.RIGHT && cha.part == collisionPart.LEFT))
                    {
                        // それぞれの直線方程式 ax + by + c = 0 の定数値
                        double[] a = new double[] { cha.y[1] - cha.y[0], obj.y[1] - obj.y[0] };
                        double[] b = new double[] { cha.x[1] - cha.x[0], obj.x[1] - obj.x[0] };
                        double[] c = new double[] { cha.x[1] * cha.y[0] - cha.x[0] * cha.y[1], obj.x[1] * obj.y[0] - obj.x[0] * obj.y[1] };

                        // 直線が平行か同じなら当たり判定を行わない
                        double delta = a[0] * b[1] - a[1] * b[0];
                        if (delta != 0)
                        {
                            // 直線と直線の交点（線分ではない）
                            double line_intersection_x = (b[0] * c[1] - b[1] * c[0]) / delta;
                            double line_intersection_y = (a[1] * c[0] - a[0] * c[1]) / delta;

                            // 線分と線分が交わっているか
                            if (cha.x[0] < line_intersection_x && line_intersection_x < cha.x[1] && obj.x[0] < line_intersection_x && line_intersection_x < obj.x[1] &&
                                cha.y[0] < line_intersection_y && line_intersection_y < cha.y[1] && obj.y[0] < line_intersection_y && line_intersection_y < obj.y[1])
                            {
                                if (cha.part == collisionPart.UPPER || cha.part == collisionPart.LOWER)
                                {
                                    return new double[] { 1.0, line_intersection_y - cha.y[1] };
                                }
                                else
                                {
                                    return new double[] { 2.0, line_intersection_x - cha.x[1] };
                                }
                            }
                        }
                    }
                }
                return new double[] { 0.0, 0.0 };
            }

        }
    }
}
