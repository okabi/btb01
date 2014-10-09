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
        /// <summary>マップデータ</summary>
        private Map[] map_data;
        /// <summary>イベントデータ</summary>
        private Event[] event_data;
        /// <summary>敵配置データ</summary>
        private Unit[] unit_data;

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="id">ステージ番号</param>
        Stage(int id)
        {
            readInfo(id);

        }

        /// <summary>
        /// info.txtの情報を読み込む。
        /// </summary>
        /// <param name="id">ステージ番号</param>
        private void readInfo(int id)
        {
            // ファイルオープン
            string file_path = "data/stage/" + id.ToString().PadLeft(3, '0') + ".dat";
            int file_handle = DX.FileRead_open(file_path);
 
            // 1行読み込む
            const int max_command_size = 256;
            StringBuilder command = new StringBuilder(max_command_size);
            while (DX.FileRead_gets(command, max_command_size, file_handle) != -1)
            {
                // commandに命令文字列が入る。
                char[] delimiters = { '=' };
                string[] commands = command.ToString().Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                // commandsに{"変数名", "値"}形式の配列が入る。
            }
            
            // ファイルクローズ
            DX.FileRead_close(file_handle);
        }

        private void _readInfoLine()
        {

        }
    }
}
