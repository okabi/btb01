/*
 * Generic.cs
 *   概要：
 *     この名前空間は、汎用クラス・汎用インスタンス・汎用関数・汎用変数・汎用定数を定義します。
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;

namespace BTB01
{
    /**
     * ゲーム内汎用定数の宣言
     */
    // 画像識別ID
    public enum GraphicID
    {
        PLAYER = 0,
    }

    // BGM識別ID
    public enum BgmID
    {
        TEST = 0,
    }

    // 効果音識別ID
    public enum SoundEffectID
    {
        TEST = 0,
    }

    // 入力デバイス定数
    public enum DeviceID
    {
        PAD1 = DX.DX_INPUT_PAD1,
        PAD2 = DX.DX_INPUT_PAD2,
        PAD3 = DX.DX_INPUT_PAD3,
        PAD4 = DX.DX_INPUT_PAD4,
        KEY = DX.DX_INPUT_KEY
    }

    // パッド入力定数
    public enum PadButtonID
    {
        UP = 0,
        DOWN,
        LEFT,
        RIGHT,
        BUTTON1,
        BUTTON2,
        BUTTON3,
        BUTTON4,
        BUTTON5,
        BUTTON6,
        BUTTON7,
        BUTTON8,
        BUTTON9,
        BUTTON10,
        BUTTON11,
        BUTTON12,
        BUTTON13,
        BUTTON14,
        BUTTON15,
        BUTTON16,
        BUTTON17,
        BUTTON18,
        BUTTON19,
        BUTTON20,
        BUTTON21,
        BUTTON22,
        BUTTON23,
        BUTTON24,
        BUTTON25,
        BUTTON26,
        BUTTON27,
        BUTTON28,
        length
    }


    /**
     *  入力関連クラス
     */
    public static class Input
    {
        // 各入力デバイスの各ボタンが押され続けたフレーム数
        public static Dictionary<DeviceID, int[]> data { get; private set; }

        /**
         * コンストラクタ
         */ 
        static Input()
        {
            data = new Dictionary<DeviceID, int[]>();
            data[DeviceID.PAD1] = new int[(int)PadButtonID.length];
            data[DeviceID.PAD2] = new int[(int)PadButtonID.length];
            data[DeviceID.PAD3] = new int[(int)PadButtonID.length];
            data[DeviceID.PAD4] = new int[(int)PadButtonID.length];
            data[DeviceID.KEY] = new int[256];
        }

        
        /**
         * 入力状態の取得。毎フレーム実行される。
         */
        public static void main()
        {
            updatePadData();
            updateKeyData();
        }
        

        /**
         * パッド入力状態の取得と内部データの更新
         */
        private static void updatePadData()
        {
            for (int i = 0; i < 4; i++)
            {
                // 入力状態を取得するデバイスを指定
                DeviceID device = DeviceID.KEY;
                if (i == 0) device = DeviceID.PAD1;
                else if (i == 1) device = DeviceID.PAD2;
                else if (i == 2) device = DeviceID.PAD3;
                else if (i == 3) device = DeviceID.PAD4;

                // 入力状態を取得
                int state = DX.GetJoypadInputState((int)device);

                // 内部データの更新
                for (int j = 0; j < (int)PadButtonID.length; j++)
                {
                    int dx_button = DX.PAD_INPUT_UP;
                    if (j == 0) dx_button = DX.PAD_INPUT_UP;
                    else if (j == 1) dx_button = DX.PAD_INPUT_DOWN;
                    else if (j == 2) dx_button = DX.PAD_INPUT_LEFT;
                    else if (j == 3) dx_button = DX.PAD_INPUT_RIGHT;
                    else if (j == 4) dx_button = DX.PAD_INPUT_1;
                    else if (j == 5) dx_button = DX.PAD_INPUT_2;
                    else if (j == 6) dx_button = DX.PAD_INPUT_3;
                    else if (j == 7) dx_button = DX.PAD_INPUT_4;
                    else if (j == 8) dx_button = DX.PAD_INPUT_5;
                    else if (j == 9) dx_button = DX.PAD_INPUT_6;
                    else if (j == 10) dx_button = DX.PAD_INPUT_7;
                    else if (j == 11) dx_button = DX.PAD_INPUT_8;
                    else if (j == 12) dx_button = DX.PAD_INPUT_9;
                    else if (j == 13) dx_button = DX.PAD_INPUT_10;
                    else if (j == 14) dx_button = DX.PAD_INPUT_11;
                    else if (j == 15) dx_button = DX.PAD_INPUT_12;
                    else if (j == 16) dx_button = DX.PAD_INPUT_13;
                    else if (j == 17) dx_button = DX.PAD_INPUT_14;
                    else if (j == 18) dx_button = DX.PAD_INPUT_15;
                    else if (j == 19) dx_button = DX.PAD_INPUT_16;
                    else if (j == 20) dx_button = DX.PAD_INPUT_17;
                    else if (j == 21) dx_button = DX.PAD_INPUT_18;
                    else if (j == 22) dx_button = DX.PAD_INPUT_19;
                    else if (j == 23) dx_button = DX.PAD_INPUT_20;
                    else if (j == 24) dx_button = DX.PAD_INPUT_21;
                    else if (j == 25) dx_button = DX.PAD_INPUT_22;
                    else if (j == 26) dx_button = DX.PAD_INPUT_23;
                    else if (j == 27) dx_button = DX.PAD_INPUT_24;
                    else if (j == 28) dx_button = DX.PAD_INPUT_25;
                    else if (j == 29) dx_button = DX.PAD_INPUT_26;
                    else if (j == 30) dx_button = DX.PAD_INPUT_27;
                    else if (j == 31) dx_button = DX.PAD_INPUT_28;

                    if ((state & dx_button) != 0) data[device][j]++;
                    else data[device][j] = 0;

                }
            }
        }


        /**
         * キーボード入力状態の取得と内部データの更新
         */
        private static void updateKeyData()
        {
            // 入力状態を取得
            for (int i = 0; i < data[DeviceID.KEY].Length; i++)
            {
                if (DX.CheckHitKey(i) != 0) data[DeviceID.KEY][i]++;
                else data[DeviceID.KEY][i] = 0;
            }
        }
    }


    /**
      * 画像統合クラス
      */
    public static class Graphic
    {
        public static Dictionary<GraphicID, int[]> data { get; private set; }  // 画像データ(連想配列)
        
        /**
         * コンストラクタ
         */ 
        static Graphic()
        {
            data = new Dictionary<GraphicID, int[]>();
            data[GraphicID.PLAYER] = new int[1];
            // ここから～～
            data[GraphicID.PLAYER][0] = DX.LoadGraph("data/img/player0.png");
        }
    }

    
    /**
      * 音楽統合クラス
      */
    public static class Sound
    {
        public static Dictionary<BgmID, int> bgm { get; private set; }  // BGMデータ(連想配列)
        public static Dictionary<SoundEffectID, int> sound_effect { get; private set; }  // 効果音データ(連想配列)

        /**
         * コンストラクタ
         */
        static Sound()
        {
            bgm = new Dictionary<BgmID, int>();
            sound_effect = new Dictionary<SoundEffectID, int>();
            // bgm[BgmID.TEST] = DX.LoadSoundMem("test.wav");
            // sound_effect[SoundEffectID.TEST] = DX.LoadSoundMem("test.wav");
        }
    }

}
