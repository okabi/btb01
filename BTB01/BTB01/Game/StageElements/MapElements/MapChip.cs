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
        public MapChip(GraphicID graphic, int animation)
            : base(graphic)
        {
            Animation = animation;
        }
    }
}
