using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protocol.Dto.Fight
{
    /// <summary>
    /// 表示卡牌
    /// </summary>
    [Serializable]
    public class CardDto
    {
        public int Id;
        public string Name;
        /// <summary>
        /// 花色 例如红桃
        /// </summary>
        public int Color;//红桃
        /// <summary>
        /// 数值 例如9
        /// </summary>
        public int Weight;//9

        public CardDto()
        {
        }

        public CardDto(int id, string name, int color, int weight)
        {
            Id = id;
            Name = name;
            Color = color;
            Weight = weight;
        }
    }
}
