/*
 * Object.cs
 *   概要：
 *     このクラスは、オブジェクトを定義します。
 *     オブジェクトの定義は「画面上に存在する、動き(振る舞い)を持つ個体」です。
 *     オブジェクトは当たり判定を持ちません。当たり判定を持つオブジェクトはキャラクターと定義します。
 *     Baseを基底クラスとします。
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;

namespace BTB01.Game
{
    /// <summary>画面上に存在する、動き(振る舞い)を持つ個体。当たり判定を持たない。Baseを継承。</summary>
    class Object : Base
    {
        /// <summary>X速度</summary>
        public double VelX { get; protected set; }
        /// <summary>Y速度</summary>
        public double VelY { get; protected set; }
        /// <summary>X加速度</summary>
        public double AccX { get; protected set; }
        /// <summary>Y加速度</summary>
        public double AccY { get; protected set; }
        /// <summary>毎フレーム実行する行動関数</summary>
        protected Func<int> behavior;
        /// <summary>X最大速度(左右とも)</summary>
        public double MaxVelX { get; protected set; }
        /// <summary>Y最大速度(重力加速度とは逆方向のみに適用)</summary>
        public double MaxVelY { get; protected set; }
        /// <summary>地形効果によるX加速度</summary>
        public double GravityAccX { get; protected set; }
        /// <summary>地形効果によるY加速度</summary>
        public double GravityAccY { get; protected set; }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="graphic">グラフィックID</param>
        public Object(GraphicID graphic) : base(graphic)
        {
            VelX = 0.0;
            VelY = 0.0;
            AccX = 0.0;
            AccY = 0.0;
            Func<int> f = 
                () => 
                    {
                        return 0;
                    };
            behavior = f;
            MaxVelX = 4.0;
            MaxVelY = 4.0;
            GravityAccX = 0.0;
            GravityAccY = 0.1;
        }


        /// <summary>
        /// 振る舞い関数を呼びそれに従って、座標を動かす。毎フレーム呼ぶこと。
        /// </summary>
        public void move()
        {
            behavior();
            PosX += VelX;
            PosY += VelY;
            VelX += AccX;
            VelY += AccY;
            if (Math.Abs(VelX) < MaxVelX) VelX += GravityAccX;
            else
            {
                if (VelX < 0.0) VelX = (-1) * MaxVelX;
                else VelX = MaxVelX;
            }
            if (MaxVelY <= 0 && MaxVelY < VelY) VelY += GravityAccY;
            else if (MaxVelY > 0 && VelY < MaxVelY) VelY += GravityAccY;
            else VelY = MaxVelY;
        }


        /// <summary>
        /// 振る舞い定義。
        /// </summary>
        /// <param name="behavior"></param>
        public void setBehavior(Func<int> behavior)
        {
            this.behavior = behavior;
        }
    }
}
