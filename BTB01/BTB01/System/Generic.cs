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
    // （位置補正が必要な）当たり判定用
    // キャラクターのどの位置に存在する当たり判定か
    enum collisionPart
    {
        UPPER,
        LOWER,
        SIDE
    }





    /*
     * 汎用クラス
     */ 
    // 四角形の当たり判定
    class CollisionSquare
    {
        // 座標情報
        // 順に左上、右下の座標
        public double[] x = new double[2];
        public double[] y = new double[2];

        // コンストラクタ
        public CollisionSquare(double[] x, double[] y)
        {
            for (int i = 0; i < 2; ++i)
            {
                this.x[i] = x[i];
                this.y[i] = y[i];
            }
        }
    }


    //直線の当たり判定 
    class CollisionLine
    {
        // 座標情報
        public double[] x = new double[2];
        public double[] y = new double[2];

        // キャラクターのどの位置に存在する当たり判定か
        public collisionPart part;

        // コンストラクタ
        public CollisionLine(double[] x, double[] y, collisionPart part)
        {
            for (int i = 0; i < 2; ++i)
            {
                this.x[i] = x[i];
                this.y[i] = y[i];
            }
            this.part = part;
        }
    }
}
