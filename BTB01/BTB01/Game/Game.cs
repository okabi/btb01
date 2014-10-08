/*
 * Game.cs
 *   概要
 *     このクラスは、メインゲームのシステム制御を定義します。
 *     Stage・Character・Object・UIを統括し、描画や当たり判定の処理を行います。
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;

namespace BTB01.Game
{
    /// <summary>座標定数</summary>
    public enum Pos
    {
        CENTER_X = 240,
        CENTER_Y = 320
    }

    /// <summary>キャラクター識別ID</summary>
    public enum CharacterID
    {
        PLAYER = 0,
    }

    /// <summary>（位置補正が必要な）当たり判定の位置。キャラクターのどの位置に存在する当たり判定か</summary>
    public enum CollisionPart
    {
        UPPER,
        LOWER,
        LEFT,
        RIGHT
    }

    /// <summary>当たり判定タイプ。それぞれを足しあわせて決定する（OR演算）</summary>
    public enum CollisionType
    {
        /// <summary>マップなど通り抜けられないモノ。ATTACK,PENETRATE属性が無いモノは全て妨害対象</summary>
        OBJECT = 0x0,
        /// <summary>ENEMYからダメージを受け、ENEMYにダメージを与える</summary>
        PLAYER = 0x1,
        /// <summary>PLAYERからダメージを受け、PLAYERにダメージを与える </summary>
        ENEMY = 0x2,
        /// <summary>OBJECTからダメージを受ける(PENETRATEが無い場合)</summary>
        ATTACK = 0x4,
        /// <summary>OBJECTを貫通する</summary>
        PENETRATE = 0x8
    }


    /// <summary>ゲーム本体を統合する静的クラス。</summary>
    public static class Game
    {
        private static List<Player> player = new List<Player>();
        private static List<Object> obj = new List<Object>();
        private static List<UI> ui = new List<UI>();


        /**
         * コンストラクタ
         */
        static Game()
        {
            // ↓テスト用
            Player item = new Player(GraphicID.PLAYER, DeviceID.KEY);
            player.Add(item);
            // ↑テスト用
        }


        // 四角形の当たり判定
        public class CollisionSquare
        {
            public double[] x = new double[2];  // 座標情報
            public double[] y = new double[2];  // | 順に左上、右下の座標
            public CollisionType type;          // 当たり判定種別

            // コンストラクタ
            public CollisionSquare(double[] x, double[] y, CollisionType type)
            {
                for (int i = 0; i < 2; ++i)
                {
                    this.x[i] = x[i];
                    this.y[i] = y[i];
                }
                this.type = type;
            }
        }


        // 線分の当たり判定 
        public class CollisionSegment
        {
            public double[] x = new double[2];  // 座標情報
            public double[] y = new double[2];  // |
            public CollisionType type;          // 当たり判定種別
            public CollisionPart part;          // キャラクターのどの位置に存在する当たり判定か

            // コンストラクタ
            public CollisionSegment(double[] x, double[] y, CollisionType type, CollisionPart part)
            {
                for (int i = 0; i < 2; ++i)
                {
                    this.x[i] = x[i];
                    this.y[i] = y[i];
                }
                this.type = type;
                this.part = part;
            }
        }


        // 円の当たり判定
        public class CollisionCircle
        {
            public double x;            // 中心の座標情報
            public double y;            // |
            public CollisionType type;  // 当たり判定種別

            // コンストラクタ
            public CollisionCircle(double x, double y, CollisionType type)
            {
                this.x = x;
                this.y = y;
                this.type = type;
            }
        }


        /**
         *  画面遷移関数が呼び出す部分
         */
        public static void main()
        {
            // オブジェクト等の行動処理
            move();

            // オブジェクト等の描画処理
            draw();
        }


        /**
         * オブジェクト等の行動処理
         */
        private static void move()
        {
            foreach (Player p in player)
            {
                p.move();
            }
        }


        /**
         * オブジェクト等の描画処理
         */
        private static void draw()
        {
            foreach (Player p in player)
            {
                p.draw();
            }
        }


        /**
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
                if (obj.type == CollisionType.OBJECT && (cha.type | CollisionType.ATTACK) == 0 && (cha.type | CollisionType.PENETRATE) == 0)
                {
                    // 当たり判定を行って意味があるパート同士かチェック
                    if ((obj.part == CollisionPart.UPPER && cha.part == CollisionPart.LOWER) || (obj.part == CollisionPart.LOWER && cha.part == CollisionPart.UPPER) ||
                        (obj.part == CollisionPart.LEFT && cha.part == CollisionPart.RIGHT) || (obj.part == CollisionPart.RIGHT && cha.part == CollisionPart.LEFT))
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
                                if (cha.part == CollisionPart.UPPER || cha.part == CollisionPart.LOWER)
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
