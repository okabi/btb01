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
    // プレイヤーの入力ボタン
    public enum PlayerButtonID
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        JUMP,
        SHOT,
        PAUSE
    }

    
    class Player : Character
    {
        private DeviceID device;  // 入力デバイス
        private bool key_use;  // キーボードの併用を認めるか
        private Dictionary<PlayerButtonID, PadButtonID> pad = new Dictionary<PlayerButtonID, PadButtonID>();  // それぞれの機能がどのパッドボタンに対応するか
        private Dictionary<PlayerButtonID, int> key = new Dictionary<PlayerButtonID, int>();  // それぞれの機能がどのキーに対応するか
        private Dictionary<PlayerButtonID, int> game_input = new Dictionary<PlayerButtonID, int>();  // ゲーム中、そのボタンが押し続けられたフレーム数
        private Dictionary<PlayerButtonID, int> menu_input = new Dictionary<PlayerButtonID, int>();  // メニュー画面やポーズ画面で、そのボタンが押し続けられたフレーム数


       /**
        * コンストラクタ
        */ 
        public Player(GraphicID graphic, DeviceID device) : base(graphic, CharacterID.PLAYER)
        {
            this.device = device;
            key_use = true;
            foreach (PlayerButtonID i in Enum.GetValues(typeof(PlayerButtonID)))
            {
                pad[i] = PadButtonID.BUTTON1;
                key[i] = 0;
                game_input[i] = 0;
                menu_input[i] = 0;
            }
            // ↓試験的にデバイス・ボタン割り当てを行う
            pad[PlayerButtonID.UP] = PadButtonID.UP;
            pad[PlayerButtonID.DOWN] = PadButtonID.DOWN;
            pad[PlayerButtonID.LEFT] = PadButtonID.LEFT;
            pad[PlayerButtonID.RIGHT] = PadButtonID.RIGHT;
            pad[PlayerButtonID.JUMP] = PadButtonID.BUTTON1;
            pad[PlayerButtonID.SHOT] = PadButtonID.BUTTON2;
            pad[PlayerButtonID.PAUSE] = PadButtonID.BUTTON8;
            key[PlayerButtonID.UP] = DX.KEY_INPUT_UP;
            key[PlayerButtonID.DOWN] = DX.KEY_INPUT_DOWN;
            key[PlayerButtonID.LEFT] = DX.KEY_INPUT_LEFT;
            key[PlayerButtonID.RIGHT] = DX.KEY_INPUT_RIGHT;
            key[PlayerButtonID.JUMP] = DX.KEY_INPUT_Z;
            key[PlayerButtonID.SHOT] = DX.KEY_INPUT_X;
            key[PlayerButtonID.PAUSE] = DX.KEY_INPUT_ESCAPE;
            // ↑試験的にデバイス・ボタン割り当てを行う
        }


        /**
         * Game.main()が呼び出す部分
         */
        public void main()
        {
            controlInGame();
        }


        /**
         * プレイヤーの操作(ゲーム中)
         */
        private void controlInGame()
        {
            // ここから書いてこーーー
        }
    }
}
