/*
 * Generic.cs
 *   概要：
 *     この名前空間は、汎用クラス・汎用インスタンス・汎用関数・汎用変数・汎用定数を定義します。
 *   この名前空間を呼び出すファイル
 *     他のあらゆるファイル
 *   この名前空間が呼び出すファイル
 *     
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;

namespace BTB01.System.Generic
{
    /*
     * 汎用インスタンス
     */
    static class Generic
    {
        public static List<Character> character = new List<Character>();
        public static List<Object> obj = new List<Object>();
        public static List<UI> ui = new List<UI>();
    }
    



    /*
     * 汎用定数
     */ 
    // （位置補正が必要な）当たり判定の位置
    // キャラクターのどの位置に存在する当たり判定か
    enum collisionPart
    {
        UPPER,
        LOWER,
        LEFT,
        RIGHT
    }

    // 当たり判定タイプ
    // それぞれを足しあわせて決定する（OR演算）
    enum collisionType
    {
        OBJECT    = 0x0,  // マップなど通り抜けられないモノ。ATTACK,PENETRATE属性が無いモノは全て妨害対象
        PLAYER    = 0x1,  // ENEMYからダメージを受け、ENEMYにダメージを与える
        ENEMY     = 0x2,  // PLAYERからダメージを受け、PLAYERにダメージを与える 
        ATTACK    = 0x4,  // OBJECTからダメージを受ける(PENETRATEが無い場合)
        PENETRATE = 0x8   // OBJECTを貫通する
    }





    /*
     * 汎用クラス
     */ 
    // 四角形の当たり判定
    class CollisionSquare
    {
        public double[] x = new double[2];  // 座標情報
        public double[] y = new double[2];  // | 順に左上、右下の座標
        public collisionType type;          // 当たり判定種別

        // コンストラクタ
        public CollisionSquare(double[] x, double[] y, collisionType type)
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
    class CollisionSegment
    {
        public double[] x = new double[2];  // 座標情報
        public double[] y = new double[2];  // |
        public collisionType type;          // 当たり判定種別
        public collisionPart part;          // キャラクターのどの位置に存在する当たり判定か

        // コンストラクタ
        public CollisionSegment(double[] x, double[] y, collisionType type, collisionPart part)
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
    class CollisionCircle
    {
        public double x;            // 中心の座標情報
        public double y;            // |
        public collisionType type;  // 当たり判定種別

        // コンストラクタ
        public CollisionCircle(double x, double y, collisionType type)
        {
            this.x = x;
            this.y = y;
            this.type = type;
        }
    }
}
