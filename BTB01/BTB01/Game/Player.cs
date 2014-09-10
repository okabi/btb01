/*
 * Player.cs
 *   概要：
 *     このクラスは、プレイヤーを定義します。
 *     操作系等もここで定義します。
 *     キャラクターを基底クラスとします。
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;

namespace BTB01.Game
{
    class Player : Character
    {
        private InputID input;  // 入力デバイス


       /**
         * コンストラクタ
         */ 
        public Player(GraphicID graphic, InputID input) : base(graphic, CharacterID.PLAYER)
        {
            this.input = input;
        }
    }
}
