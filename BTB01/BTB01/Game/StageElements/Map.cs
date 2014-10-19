using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
using BTB01.Game.StageElements.MapElements;

namespace BTB01.Game.StageElements
{
    /// <summary>ステージ中の1マップ。</summary>
    public class Map
    {
        /// <summary>地形のマップチップ</summary>
        private MapChip[,] map;
        /// <summary>背景のマップチップ</summary>
        private MapChip[,] back;
        /// <summary>前景のマップチップ</summary>
        private MapChip[,] front;
        /// <summary>マップ名</summary>
        private string name;


        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="stage_index">読み込むステージ番号</param>
        /// <param name="map_index">読み込むマップ番号</param>
        public Map(int stage_index, int map_index)
        {
            // ファイルを開く
            string file_path = "data/stage/stage" + stage_index.ToString("D3") + "/map" + map_index.ToString("D3") + ".dat";
            int file_handle = DX.FileRead_open(file_path);

            // 必要な変数の宣言
            int size_x = 0;  // マップの横サイズ
            int size_y = 0;  // マップの縦サイズ
            int map_set = 0;  // 使用する地形のマップチップ番号
            int back_set = 0;  // 使用する背景のマップチップ番号
            int front_set = 0;  // 使用する前景のマップチップ番号

            // 1行ずつ読み込んでいく
            for (int i = 0; ; i++)
            {
                StringBuilder st = new StringBuilder(1024);
                if (DX.FileRead_gets(st, st.Capacity, file_handle) == -1) break;
                if (i == 0) name = st.ToString();
                else if (i == 1) map_set = int.Parse(st.ToString());
                else if (i == 2) back_set = int.Parse(st.ToString());
                else if (i == 3) front_set = int.Parse(st.ToString());
                else if (i == 4) size_x = int.Parse(st.ToString());
                else if (i == 5)
                {
                    size_y = int.Parse(st.ToString());
                    map = new MapChip[size_x, size_y];
                    back = new MapChip[size_x, size_y];
                    front = new MapChip[size_x, size_y];
                }
                else if (i >= 6 && i < 6 + size_y)
                {
                    string[] st_array = st.ToString().Split(',');
                    for (int j = 0; j < st_array.Length; j++)
                    {
                        map[j, i - 6] = new MapChip((GraphicID)Enum.ToObject(typeof(GraphicID), map_set), int.Parse(st_array[j]));
                    }
                }
                else if (i >= 6 + size_y && i < 6 + 2 * size_y)
                {
                    string[] st_array = st.ToString().Split(',');
                    for (int j = 0; j < st_array.Length; j++)
                    {
                        back[j, i - 6 - size_y] = new MapChip((GraphicID)Enum.ToObject(typeof(GraphicID), back_set), int.Parse(st_array[j]));
                    }
                }
                else if (i >= 6 + 2 * size_y && i < 6 + 3 * size_y)
                {
                    string[] st_array = st.ToString().Split(',');
                    for (int j = 0; j < st_array.Length; j++)
                    {
                        front[j, i - 6 - 2 * size_y] = new MapChip((GraphicID)Enum.ToObject(typeof(GraphicID), front_set), int.Parse(st_array[j]));
                    }
                }
            }

            // ファイルを閉じる
            DX.FileRead_close(file_handle);
        }

        /// <summary>
        /// 背景を描画する。
        /// </summary>
        public void drawBack()
        {
            foreach (MapChip elem in back)
            {
                elem.draw();
            }
        }

        /// <summary>
        /// 地形を描画する。
        /// </summary>
        public void drawMap()
        {
            foreach (MapChip elem in map)
            {
                elem.draw();
            }
        }

        /// <summary>
        /// 前景を描画する。
        /// </summary>
        public void drawFront()
        {
            foreach (MapChip elem in front)
            {
                elem.draw();
            }
        }
    }
}
