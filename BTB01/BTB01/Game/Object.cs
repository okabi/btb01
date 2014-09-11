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
        protected int animation;  // 表示アニメーション番号(歩行アニメーションなど)
        protected double exRate;  // 拡縮率
        protected double angle;  // 回転角(ラジアン)
        protected Func<int> behavior;  // 毎フレーム実行する行動関数
        protected double max_vel_x;  // x方向最大速度(左右とも)
        protected double max_vel_y;  // y方向最大速度(gravity_acc_yとは逆方向のみに適用)
        protected double gravity_acc_x;  // 地形効果によるx方向加速度
        protected double gravity_acc_y;  // 地形効果によるy方向加速度


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
            animation = 0;
            exRate = 1.0;
            angle = 0.0;
            Func<int> f = 
                () => 
                    {
                        return 0;
                    };
            this.behavior = f;
            max_vel_x = 4.0;
            max_vel_y = 4.0;
            gravity_acc_x = 0.0;
            gravity_acc_y = 0.1;
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
            if (Math.Abs(vel_x) < max_vel_x) vel_x += gravity_acc_x;
            else
            {
                if (vel_x < 0.0) vel_x = (-1) * max_vel_x;
                else vel_x = max_vel_x;
            }
            if (max_vel_y <= 0 && max_vel_y < vel_y) vel_y += gravity_acc_y;
            else if (max_vel_y > 0 && vel_y < max_vel_y) vel_y += gravity_acc_y;
            else vel_y = max_vel_y;
        }


        /**
         * 毎フレームの描画
         */ 
        public void draw()
        {
            DX.DrawRotaGraphF((float)pos_x, (float)pos_y, exRate, angle, Graphic.data[graphic][animation], DX.TRUE);
        }
    }
}
