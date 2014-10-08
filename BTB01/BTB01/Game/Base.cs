/*
 * Base.cs
 *   概要：
 *     このクラスは、個体のベースを定義します。
 *     ベースの定義は「画面上に存在する、１つのグラフィックを持つ個体」です。
 *     ベースは座標とグラフィック、その描画関数のみを持ちます。
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;

namespace BTB01.Game
{
    /// <summary>画面上に存在する、１つのグラフィックを持つ個体。座標・画像・描画関数のみ定義。</summary>
    class Base
    {
        /// <summary>中心X座標</summary>
        public double PosX { get; protected set; }
        /// <summary>中心Y座標</summary>
        public double PosY { get; protected set; }
        /// <summary>画像ID</summary>
        public GraphicID Graphic { get; protected set; }
        /// <summary>表示するアニメーション番号(画像ID中)</summary>
        public int Animation { get; protected set; }
        /// <summary>画像拡縮率(0.0～1.0)</summary>
        public double ExRate { get; protected set; }
        /// <summary>画像回転角(rad)</summary>
        public double Angle { get; protected set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="graphic">グラフィックID</param>
        public Base(GraphicID graphic)
        {
            PosX = (double)Pos.CENTER_X;
            PosY = (double)Pos.CENTER_Y;
            Graphic = graphic;
            Animation = 0;
            ExRate = 1.0;
            Angle = 0.0;
        }


        /// <summary>
        /// 描画。毎フレーム呼ぶこと。
        /// </summary>
        public void draw()
        {
            DX.DrawRotaGraphF((float)PosX + (float)Game.CameraX, (float)PosY + (float)Game.CameraY, ExRate, Angle, BTB01.Graphic.data[this.Graphic][Animation], DX.TRUE);
        }

    }
}
