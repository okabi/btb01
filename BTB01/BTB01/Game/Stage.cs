using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
using BTB01.Game.StageElements;

namespace BTB01.Game
{
    /// <summary>１つのスタート地点と１つのゴールを持つステージ。</summary>
    class Stage
    {
        /// <summary>ステージ番号</summary>
        public int Id { get; private set; }
        /// <summary>ステージ名</summary>
        public string Name { get; private set; }
        /// <summary>BGM</summary>
        public BgmID Bgm { get; private set; }
        /// <summary>現在表示しているマップ番号</summary>
        public int PresentMap { get; set; }
        /// <summary>マップデータ</summary>
        private Map[] map_data;

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="id">ステージ番号</param>
        /// <param name="present_map">最初に表示するマップ番号</param>
        public Stage(int id, int present_map = 0)
        {
            Id = id;
            readInfo(id);
            for (int i = 0; i < map_data.Length; i++)
            {
                map_data[i] = new Map(Id, i);
            }
            PresentMap = present_map;
        }

        /// <summary>
        /// info.txtの情報を読み込む。
        /// </summary>
        /// <param name="id">ステージ番号</param>
        private void readInfo(int id)
        {
            // ファイルオープン
            string file_path = "data/stage/" + id.ToString().PadLeft(3, '0') + "/info.txt";
            int file_handle = DX.FileRead_open(file_path);

            // 1行読み込む
            StringBuilder command = new StringBuilder(256);
            while (DX.FileRead_gets(command, command.Capacity, file_handle) != -1)
            {
                // commandに命令文字列が入る。
                if (command[0] != '#')
                {
                    char[] delimiters = { ' ', '=' };
                    // commandsに{"変数名", "値"}形式の配列が入る。
                    string[] commands = command.ToString().Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                    switch (commands[0])
                    {
                        case "stage_name":
                            Name = commands[1];
                            break;
                        case "map":
                            map_data = new Map[int.Parse(commands[1])];
                            break;
                        case "bgm":
                            Bgm = (BgmID)Enum.ToObject(typeof(BgmID), int.Parse(commands[1]));
                            break;
                    }
                }
            }

            // ファイルクローズ
            DX.FileRead_close(file_handle);
        }

        /// <summary>
        /// 現在指定しているマップの背景を描画する。
        /// </summary>
        public void drawBack()
        {
            map_data[PresentMap].drawBack();
        }

        /// <summary>
        /// 現在指定しているマップの地形を描画する。
        /// </summary>
        public void drawMap()
        {
            map_data[PresentMap].drawMap();
        }

        /// <summary>
        /// 現在指定しているマップの前景を描画する。
        /// </summary>
        public void drawFront()
        {
            map_data[PresentMap].drawFront();
        }

    }
}
