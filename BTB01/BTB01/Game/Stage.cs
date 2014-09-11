using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTB01.Game
{
    class Stage
    {
        class Map
        {
            private int[,] map;  // 地形のグラフィックハンドル
            private int[,] back;  // 背景のグラフィックハンドル
            private int[,] front;  // 前景のグラフィックハンドル


            /**
             * コンストラクタ
             */ 
            Map()
            {
                loadMapData();
                /*
                map = new int[size_x, size_y];
                back = new int[size_x, size_y];
                front = new int[size_x, size_y];
                */
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


            /**
             * マップ番号を指定して、そのマップデータを読み込む。
             * 例外処理入れたほうが良いな…
             */
            private void loadMapData()
            {
                // ここから～～
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
