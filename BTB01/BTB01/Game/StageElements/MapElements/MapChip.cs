using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;

namespace BTB01.Game.StageElements.MapElements
{
    /// <summary>マップ上に存在する1チップ。</summary>
    class MapChip : Base
    {
        public MapChip(GraphicID graphic, int animation, int x_index, int y_index)
            : base(graphic)
        {
            Animation = animation;
            PosX = 32.0 * x_index;
            PosY = 32.0 * y_index;
        }
    }
}
