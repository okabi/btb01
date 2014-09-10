/*
 * Character.cs
 *   概要：
 *     このクラスは、キャラクターを定義します。
 *     キャラクターの定義は「画面上に存在する当たり判定を持つオブジェクト」です。
 *     オブジェクトを基底クラスとします。
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;

namespace BTB01.Game
{   
    class Character : Object
    {
        /**
         * コンストラクタ
         */ 
        public Character(GraphicID graphic, CharacterID character) : base(graphic)
        {
            // Func<int> behavior;
        }

        /**
         * キャラクターIDから行動を定義する
         */
        public void setCharacter(CharacterID new_character)
        {
            // character = new_character;
            
        }
    }
}
