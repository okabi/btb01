using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;

namespace BTB01.Game.StageElements
{
    /// <summary>ステージ中の1マップ。</summary>
    class Map
    {
        /// <summary>地形のグラフィックデータ</summary>
        private int[,] map;
        /// <summary>背景のグラフィックデータ</summary>
        private int[,] back;
        /// <summary>前景のグラフィックデータ</summary>
        private int[,] front;
        /// <summary>地形のチップセット番号</summary>
        private int map_set;
        /// <summary>背景のチップセット番号</summary>
        private int back_set;
        /// <summary>前景のチップセット番号</summary>
        private int front_set;
        /// <summary>マップ名</summary>
        private string name;


        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="stage_index">読み込むステージ番号</param>
        /// <param name="map_index">読み込むマップ番号</param>
        Map(int stage_index, int map_index)
        {
            // ファイルを開く
            string file_path = "data/stage/stage" + stage_index.ToString("D3") + "/map" + map_index.ToString("D3") + ".dat";
            int file_handle = DX.FileRead_open(file_path);

            // 必要な変数の宣言
            int size_x = 0;  // マップの横サイズ
            int size_y = 0;  // マップの縦サイズ

            // 1行ずつ読み込んでいく
            for (int i = 0; ; i++)
            {
                StringBuilder st = new StringBuilder("");
                st.Capacity = 1024;
                if (DX.FileRead_gets(st, st.Capacity, file_handle) == -1) break;
                if (i == 0) name = st.ToString();
                else if (i == 1) map_set = int.Parse(st.ToString());
                else if (i == 2) back_set = int.Parse(st.ToString());
                else if (i == 3) front_set = int.Parse(st.ToString());
                else if (i == 4) size_x = int.Parse(st.ToString());
                else if (i == 5)
                {
                    size_y = int.Parse(st.ToString());
                    map = new int[size_x, size_y];
                    back = new int[size_x, size_y];
                    front = new int[size_x, size_y];
                }
                else if (i >= 6 && i < 6 + size_y)
                {
                    string[] st_array = st.ToString().Split(',');
                    for (int j = 0; j < st_array.Length; j++)
                    {
                        map[j, i - 6] = int.Parse(st_array[j]);
                    }
                }
                else if (i >= 6 + size_y && i < 6 + 2 * size_y)
                {
                    string[] st_array = st.ToString().Split(',');
                    for (int j = 0; j < st_array.Length; j++)
                    {
                        back[j, i - 6 - size_y] = int.Parse(st_array[j]);
                    }
                }
                else if (i >= 6 + 2 * size_y && i < 6 + 3 * size_y)
                {
                    string[] st_array = st.ToString().Split(',');
                    for (int j = 0; j < st_array.Length; j++)
                    {
                        front[j, i - 6 - 2 * size_y] = int.Parse(st_array[j]);
                    }
                }
            }

            // ファイルを閉じる
            DX.FileRead_close(file_handle);
        }


        /// <summary>
        /// 地形のセット。
        /// </summary>
        /// <param name="map">地形データ(配列サイズはコンストラクタで指定したものにすること)。</param>
        private void setMap(int[,] map)
        {
            if (this.map.GetLength(0) == map.GetLength(0) && this.map.GetLength(1) == map.GetLength(1))
            {
                this.map = map;
            }
        }


        /// <summary>
        /// 背景のセット。
        /// </summary>
        /// <param name="back">背景データ(配列サイズはコンストラクタで指定したものにすること)。</param>
        private void setBack(int[,] back)
        {
            if (this.back.GetLength(0) == back.GetLength(0) && this.back.GetLength(1) == back.GetLength(1))
            {
                this.back = back;
            }
        }


        /// <summary>
        /// 前景のセット。
        /// </summary>
        /// <param name="front">前景データ(配列サイズはコンストラクタで指定したものにすること)。</param>
        private void setFront(int[,] front)
        {
            if (this.front.GetLength(0) == front.GetLength(0) && this.front.GetLength(1) == front.GetLength(1))
            {
                this.front = front;
            }
        }
    }
}
