using AphilyServer;
using AphilyServer.Concurrent;
using GameServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Cache
{
    /// <summary>
    /// 账号缓存
    ///     简单来说：存账号的
    /// </summary>
    public class AccountCache
    {
        /// <summary>
        /// 账号 对应的 账号数据模型
        /// </summary>
        public Dictionary<string, AccountModel> accModelDict = new Dictionary<string, AccountModel>();

        /// <summary>
        /// 是否存在账号
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool IsExist(string account)
        {
            return accModelDict.ContainsKey(account);
        }

        /// <summary>
        /// 用来存储账号的id
        ///     后期是数据库来处理的
        /// </summary>
        private ConcurrentInt id = new ConcurrentInt(-1);

        /// <summary>
        /// 创建账号数据模型
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        public void Creat(string account, string password)
        {
            AccountModel model = new AccountModel(id.Add_Get(), account, password);
            accModelDict.Add(model.Account, model);
        }

        /// <summary>
        /// 获取账号对应的数据模型
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public AccountModel GetModel(string account)
        {
            return accModelDict[account];
        }

        /// <summary>
        /// 账号密码是否匹配
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool IsMatch(string account, string password)
        {
            AccountModel model = accModelDict[account];
            return model.Password == password;
        }

        /// <summary>
        /// 账号 对应 连接对象
        /// </summary>
        public Dictionary<string, ClientPeer> accClientDict = new Dictionary<string, ClientPeer>();
        /// <summary>
        /// 连接对象 对应 账号
        /// </summary>
        public Dictionary<ClientPeer, string> clinetAccDict = new Dictionary<ClientPeer, string>();

        /// <summary>
        /// 是否在线
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool IsOnline(string account)
        {
            return accClientDict.ContainsKey(account);
        }

        /// <summary>
        /// 是否在线
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool IsOnline(ClientPeer client)
        {
            return clinetAccDict.ContainsKey(client);
        }

        /// <summary>
        /// 用户上线
        /// </summary>
        /// <param name="client"></param>
        /// <param name="account"></param>
        public void Online(ClientPeer client, string account)
        {
            accClientDict.Add(account, client);
            clinetAccDict.Add(client, account);
        }

        /// <summary>
        /// 下线
        /// </summary>
        /// <param name="client"></param>
        public void Offlint(ClientPeer client)
        {
            string account = clinetAccDict[client];
            clinetAccDict.Remove(client);
            accClientDict.Remove(account);
        }

        /// <summary>
        /// 下线
        /// </summary>
        /// <param name="client"></param>
        public void Offlint(string account)
        {
            ClientPeer client = accClientDict[account];
            accClientDict.Remove(account);
            clinetAccDict.Remove(client);
        }

        /// <summary>
        /// 获取在线玩家的id
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public int GetId(ClientPeer client)
        {
            string account = clinetAccDict[client];
            AccountModel model = accModelDict[account];
            return model.Id;
        }
    }
}
