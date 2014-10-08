/*
 * Character.cs
 *   概要：
 *     このクラスは、キャラクターを定義します。
 *     キャラクターの定義は「画面上に存在する、当たり判定を持つオブジェクト」です。
 *     Objectを基底クラスとします。
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;

namespace BTB01.Game
{
    /// <summary>画面上に存在する、当たり判定と振る舞いを持つ個体。Objectを継承。</summary>
    class Character : Object
    {
        /// <summary>
        /// 【不完全】コンストラクタ。
        /// </summary>
        /// <param name="graphic">グラフィックID</param>
        /// <param name="character">キャラクターID</param>
        public Character(GraphicID graphic, CharacterID character) : base(graphic)
        {
            // Func<int> behavior;
        }


    }
}
