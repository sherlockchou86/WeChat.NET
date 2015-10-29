using System;
using System.Collections.Generic;
using System.Text;

namespace WeChat.NET.Objects
{
    /// <summary>
    /// 微信消息
    /// </summary>
    public class WXMsg
    {
        /// <summary>
        /// 消息发送方
        /// </summary>
        public string From
        {
            get;
            set;
        }
        /// <summary>
        /// 消息接收方
        /// </summary>
        public string To
        {
            set;
            get;
        }
        /// <summary>
        /// 消息发送时间
        /// </summary>
        public DateTime Time
        {
            get;
            set;
        }
        /// <summary>
        /// 是否已读
        /// </summary>
        public bool Readed
        {
            get;
            set;
        }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Msg
        {
            get;
            set;
        }
        /// <summary>
        /// 消息类型
        /// </summary>
        public int Type
        {
            get;
            set;
        }
    }
}
