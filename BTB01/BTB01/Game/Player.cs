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
    /// <summary>プレイヤーの入力(ジャンプ、攻撃など)</summary>
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


    /// <summary>プレイヤー。Characterを継承。</summary>
    class Player : Character
    {
        /// <summary>入力デバイス</summary>
        private DeviceID device;
        /// <summary>キーボードの併用を認めるか？</summary>
        private bool key_use;
        /// <summary>ジャンプや攻撃などがどのパッドボタンに対応するか</summary>
        private Dictionary<PlayerButtonID, PadButtonID> pad = new Dictionary<PlayerButtonID, PadButtonID>();
        /// <summary>ジャンプや攻撃などがどのキーに対応するか</summary>
        private Dictionary<PlayerButtonID, int> key = new Dictionary<PlayerButtonID, int>();
        /// <summary>ゲーム中、ジャンプや攻撃などが押し続けられたフレーム数</summary>
        private Dictionary<PlayerButtonID, int> game_input = new Dictionary<PlayerButtonID, int>();
        /// <summary>メニュー画面やポーズ画面で、決定やキャンセルなどが押し続けられたフレーム数</summary>
        private Dictionary<PlayerButtonID, int> menu_input = new Dictionary<PlayerButtonID, int>();

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="graphic">グラフィックID</param>
        /// <param name="device">入力デバイスID</param>
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
            PosX = 32;
            PosY = -32;
            // ↑試験的にデバイス・ボタン割り当てを行う
            // ↓プレイヤー特有の動作処理
            Func<int> f = 
                () =>
                    {
                        controlInGame();
                        return 0;
                    };
            this.behavior = f;
            // ↑プレイヤー特有の動作処理
        }


        /// <summary>
        /// プレイヤー操作(ゲーム中)
        /// </summary>
        private void controlInGame()
        {
            // 入力デバイスからの入力を受け取る
            inputWithController();

            // 入力に応じてキャラを動かす
            /*
            if (game_input[PlayerButtonID.UP] > 0) this.acc_y = -0.2;
            else if (game_input[PlayerButtonID.DOWN] > 0) this.acc_y = 0.2;
            else this.acc_y = 0.0;
            */
            this.VelY = 0.0;
            if (game_input[PlayerButtonID.LEFT] > 0)
            {
                if (this.VelX > (-1) * this.MaxVelX) this.AccX = -0.1;
                else
                {
                    this.VelX = (-1) * this.MaxVelX;
                    this.AccX = 0.0;
                }
            }
            else if (game_input[PlayerButtonID.RIGHT] > 0)
            {
                if (this.VelX < this.MaxVelX) this.AccX = 0.1;
                else
                {
                    this.VelX = this.MaxVelX;
                    this.AccX = 0.0;
                }
            }
            else
            {
                if (this.VelX > 0.0) this.AccX = -0.1;
                else if (this.VelX < 0.0) this.AccX = 0.1;
                else this.AccX = 0.0;
            }
        }


        /// <summary>
        /// プレイヤー入力を読み込む
        /// </summary>
        private void inputWithController()
        {
            foreach (PlayerButtonID button in Enum.GetValues(typeof(PlayerButtonID)))
            {
                if (device != DeviceID.KEY)
                {
                    if (Input.data[device][(int)pad[button]] > 0 || (key_use == true && Input.data[DeviceID.KEY][key[button]] > 0)) game_input[button]++;
                    else game_input[button] = 0;
                }
                else
                {
                    if (Input.data[DeviceID.KEY][key[button]] > 0) game_input[button]++;
                    else game_input[button] = 0;
                }
            }
        } 
    }
}
