
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protocol.Dto
{
    /// <summary>
    /// 房间数据对应的传输模型
    /// </summary>
    [Serializable]
    public class MatchRoomDto
    {
        /// <summary>
        /// 用户id对应的用户数据的传输模型
        /// </summary>
        public Dictionary<int, UserDto> UIdUserDict;

        /// <summary>
        /// 准备的玩家id列表
        /// </summary>
        public List<int> ReadyUIdList;

        /// <summary>
        /// 存储玩家进入的顺序
        /// </summary>
        private List<int> uIdList;

        public MatchRoomDto()
        {
            this.UIdUserDict = new Dictionary<int, UserDto>();
            this.ReadyUIdList = new List<int>();
            //this.uIdList = new List<int>();
        }

        public void Add(UserDto newUser)
        {
            this.UIdUserDict.Add(newUser.Id, newUser);
            this.uIdList.Add(newUser.Id);
        }

        public void Leave(int userId)
        {
            this.UIdUserDict.Remove(userId);
            this.uIdList.Remove(userId);
        }

        public void Ready(int userId)
        {
            this.ReadyUIdList.Add(userId);
        }

        public int LeftId;//左边玩家的id
        public int RightId;//右边玩家的id

        /// <summary>
        /// 重置位置
        ///     在每次玩家进入或者离开房间是时候 都需要调整一下位置
        /// </summary>
        public void ResetPosition(int myUserId)
        {
            LeftId = -1;
            RightId = -1;

            //房间有1个人
            if(uIdList.Count == 1)
            {
                return;
            }
            //房间有2个人 a和x
            else if (uIdList.Count == 2)
            {
                //x a
                if(uIdList[0] == myUserId)
                {
                    RightId = uIdList[1];
                }
                //a x
                if (uIdList[1] == myUserId)
                {
                    LeftId = uIdList[0];
                }
            }
            //房间有3个人 x a b
            else if(uIdList.Count == 3)
            {
                //x a b
                if (uIdList[0] == myUserId)
                {
                    LeftId = uIdList[2];
                    RightId = uIdList[1];
                }
                //a x b
                if (uIdList[1] == myUserId)
                {
                    LeftId = uIdList[0];
                    RightId = uIdList[2];
                }
                //a b x
                if (uIdList[2] == myUserId)
                {
                    LeftId = uIdList[1];
                    RightId = uIdList[0];
                }
            }
        }

    }
}
