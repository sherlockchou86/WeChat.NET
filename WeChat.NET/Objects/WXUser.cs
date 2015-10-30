using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Linq;
using WeChat.NET.HTTP;

namespace WeChat.NET.Objects
{
    /// <summary>
    /// 微信用户
    /// </summary>
    public class WXUser
    {
        //用户id
        private string _userName;
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
            }
        }
        //昵称
        private string _nickName;
        public string NickName
        {
            get
            {
                return _nickName;
            }
            set
            {
                _nickName = value;
            }
        }
        //头像url
        private string _headImgUrl;
        public string HeadImgUrl
        {
            get
            {
                return _headImgUrl;
            }
            set
            {
                _headImgUrl = value;
            }
        }
        //备注名
        private string _remarkName;
        public string RemarkName
        {
            get
            {
                return _remarkName;
            }
            set
            {
                _remarkName = value;
            }
        }
        //性别 男1 女2 其他0
        private string _sex;
        public string Sex
        {
            get
            {
                return _sex;
            }
            set
            {
                _sex = value;
            }
        }
        //签名
        private string _signature;
        public string Signature
        {
            get
            {
                return _signature;
            }
            set
            {
                _signature = value;
            }
        }
        //城市
        private string _city;
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
            }
        }
        //省份
        private string _province;
        public string Province
        {
            get
            {
                return _province;
            }
            set
            {
                _province = value;
            }
        }
        //昵称全拼
        private string _pyQuanPin;
        public string PYQuanPin
        {
            get
            {
                return _pyQuanPin;
            }
            set
            {
                _pyQuanPin = value;
            }
        }
        //备注名全拼
        private string _remarkPYQuanPin;
        public string RemarkPYQuanPin
        {
            get
            {
                return _remarkPYQuanPin;
            }
            set
            {
                _remarkPYQuanPin = value;
            }
        }
        //头像
        private bool _loading_icon = false;
        private Image _icon;
        public Image Icon
        {
            get
            {
                if (_icon == null && !_loading_icon)
                {
                    _loading_icon = true;
                    ((Action)(delegate()
                    {
                        WXService wxs = new WXService();
                        if (_userName.Contains("@@"))  //讨论组
                        {
                            _icon = wxs.GetHeadImg(_userName);
                        }
                        else if (_userName.Contains("@"))  //好友
                        {
                            _icon = wxs.GetIcon(_userName);
                        }
                        else
                        {
                            _icon = wxs.GetIcon(_userName);
                        }
                        _loading_icon = false;
                    })).BeginInvoke(null, null);
                }
                return _icon;
            }
        }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string ShowName
        {
            get
            {
                return (_remarkName == null || _remarkName == "") ? _nickName : _remarkName;
            }
        }
        /// <summary>
        /// 显示的拼音全拼
        /// </summary>
        public string ShowPinYin
        {
            get
            {
                return (_remarkPYQuanPin == null || _remarkPYQuanPin == "") ? _pyQuanPin : _remarkPYQuanPin;
            }
        }

        //发送给对方的消息  
        private Dictionary<DateTime, WXMsg> _sentMsg = new Dictionary<DateTime, WXMsg>();
        public Dictionary<DateTime, WXMsg> SentMsg
        {
            get
            {
                return _sentMsg;
            }
        }
        //收到对方的消息
        private Dictionary<DateTime, WXMsg> _recvedMsg = new Dictionary<DateTime, WXMsg>();
        public Dictionary<DateTime, WXMsg> RecvedMsg
        {
            get
            {
                return _recvedMsg;
            }
        }

        public event MsgSentEventHandler MsgSent;
        public event MsgRecvedEventHandler MsgRecved;

        /// <summary>
        /// 接收来自该用户的消息
        /// </summary>
        /// <param name="msg"></param>
        public void ReceiveMsg(WXMsg msg)
        {
            _recvedMsg.Add(msg.Time, msg);
            if (MsgRecved != null)
            {
                MsgRecved(msg);
            }
        }
        /// <summary>
        /// 向该用户发送消息
        /// </summary>
        /// <param name="msg"></param>
        public void SendMsg(WXMsg msg, bool showOnly)
        {
            //发送
            if (!showOnly)
            {
                WXService wxs = new WXService();
                wxs.SendMsg(msg.Msg, msg.From, msg.To, msg.Type);
            }

            _sentMsg.Add(msg.Time, msg);
            if (MsgSent != null)
            {
                MsgSent(msg);
            }
        }
        /// <summary>
        /// 获取该用户发送的未读消息
        /// </summary>
        /// <returns></returns>
        public List<WXMsg> GetUnReadMsg()
        {
            List<WXMsg> list = null;
            foreach (KeyValuePair<DateTime, WXMsg> p in _recvedMsg)
            {
                if (!p.Value.Readed)
                {
                    if (list == null)
                    {
                        list = new List<WXMsg>();
                    }
                    list.Add(p.Value);
                }
            }

            return list;
        }
        /// <summary>
        /// 获取最近的一条消息
        /// </summary>
        /// <returns></returns>
        public WXMsg GetLatestMsg()
        {
            WXMsg msg = null;
            if (_sentMsg.Count > 0 && _recvedMsg.Count>0)
            {
                msg = _sentMsg.Last().Value.Time > _recvedMsg.Last().Value.Time ? _sentMsg.Last().Value : _recvedMsg.Last().Value;
            }
            else if (_sentMsg.Count > 0)
            {
                msg = _sentMsg.Last().Value;
            }
            else if (_recvedMsg.Count > 0)
            {
                msg = _recvedMsg.Last().Value;
            }
            else
            {
                msg = null;
            }
            return msg;
        }
    }
    /// <summary>
    /// 表示处理消息发送完成事件的方法
    /// </summary>
    /// <param name="msg"></param>
    public delegate void MsgSentEventHandler(WXMsg msg);
    /// <summary>
    /// 表示处理接收到新消息事件的方法
    /// </summary>
    /// <param name="msg"></param>
    public delegate void MsgRecvedEventHandler(WXMsg msg);
}
