using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;

namespace BTB01.Game
{
    /// <summary>１つのスタート地点と１つのゴールを持つステージ。</summary>
    class Stage
    {
        class Map
        {
            private int[,] map;  // 地形のグラフィックハンドル
            private int[,] back;  // 背景のグラフィックハンドル
            private int[,] front;  // 前景のグラフィックハンドル
            private int map_set;  // 地形のチップセット番号
            private int back_set;  // 背景のチップセット番号
            private int front_set;  // 前景のチップセット番号
            private string name;  // マップ名


            /**
             * コンストラクタ。ステージ番号とマップ番号を指定して読み込む。
             */ 
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


            /**
             * 地形をセット。引数の配列サイズは最初に指定したものにすること。
             */
            private void setMap(int[,] map)
            {
                if (this.map.GetLength(0) == map.GetLength(0) && this.map.GetLength(1) == map.GetLength(1))
                {
                    this.map = map;
                }
            }


            /**
             * 背景をセット。引数の配列サイズは最初に指定したものにすること。
             */
            private void setBack(int[,] back)
            {
                if (this.back.GetLength(0) == back.GetLength(0) && this.back.GetLength(1) == back.GetLength(1))
                {
                    this.back = back;
                }
            }


            /**
             * 前景をセット。引数の配列サイズは最初に指定したものにすること。
             */
            private void setFront(int[,] front)
            {
                if (this.front.GetLength(0) == front.GetLength(0) && this.front.GetLength(1) == front.GetLength(1))
                {
                    this.front = front;
                }
            }
        }



        class Event
        {

        }

        class Unit
        {

        }
    }
}
