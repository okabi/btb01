/*
 * Object.cs
 *   概要：
 *     このクラスは、オブジェクトを定義します。
 *     オブジェクトの定義は「画面上に存在する、１つのグラフィックを持つ個体」です。
 *     オブジェクトは当たり判定を持ちません。当たり判定を持つオブジェクトはキャラクターと定義します。
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;

namespace BTB01.Game
{
    class Object
    {
        protected double pos_x;  // 中心座標
        protected double pos_y;  // |
        protected double vel_x;  // 速度
        protected double vel_y;  // |
        protected double acc_x;  // 加速度
        protected double acc_y;  // |
        protected GraphicID graphic;  // 画像
        protected double exRate;  // 拡縮率
        protected double angle;  // 回転角(ラジアン)
        protected Func<int> behavior;  // 毎フレーム実行する行動関数

        /**
         * コンストラクタ
         */ 
        public Object(GraphicID graphic)
        {
            pos_x = (double)Pos.CENTER_X;
            pos_y = (double)Pos.CENTER_Y;
            vel_x = 0.0;
            vel_y = 0.0;
            acc_x = 0.0;
            acc_y = 0.0;
            this.graphic = graphic;
            exRate = 1.0;
            angle = 0.0;
            Func<int> f = 
                () => 
                {
                    return 0;
                };
            this.behavior = f;
        }


        /**
         * 毎フレームの座標操作等
         */
        public void move()
        {
            behavior();
            pos_x += vel_x;
            pos_y += vel_y;
            vel_x += acc_x;
            vel_y += acc_y;
        }


        /**
         * 毎フレームの描画
         */ 
        public void draw()
        {
            DX.DrawRotaGraphF((float)pos_x, (float)pos_y, exRate, angle, Graphic.data[graphic], DX.FALSE);
        }
    }
}
