using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.IO;
using System.Net;

namespace WeChat.NET.HTTP
{
    /// <summary>
    /// 微信主要业务逻辑服务类
    /// </summary>
    class WXService
    {
        private static Dictionary<string, string> _syncKey = new Dictionary<string, string>();

        //微信初始化url
        private static string _init_url = "https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxinit?r=1377482058764";
        //获取好友头像
        private static string _geticon_url = "https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxgeticon?username=";
        //获取群聊（组）头像
        private static string _getheadimg_url = "https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxgetheadimg?username=";
        //获取好友列表
        private static string _getcontact_url = "https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxgetcontact";
        //同步检查url
        private static string _synccheck_url = "https://webpush.weixin.qq.com/cgi-bin/mmwebwx-bin/synccheck?sid={0}&uin={1}&synckey={2}&r={3}&skey={4}&deviceid={5}";
        //同步url
        private static string _sync_url = "https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxsync?sid=";
        //发送消息url
        private static string _sendmsg_url = "https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxsendmsg?sid=";

        /// <summary>
        /// 微信初始化
        /// </summary>
        /// <returns></returns>
        public JObject WxInit()
        {
            string init_json = "{{\"BaseRequest\":{{\"Uin\":\"{0}\",\"Sid\":\"{1}\",\"Skey\":\"\",\"DeviceID\":\"e1615250492\"}}}}";
            Cookie sid = BaseService.GetCookie("wxsid");
            Cookie uin = BaseService.GetCookie("wxuin");

            if (sid != null && uin != null)
            {
                init_json = string.Format(init_json, uin.Value, sid.Value);
                byte[] bytes = BaseService.SendPostRequest(_init_url + "&pass_ticket=" + LoginService.Pass_Ticket, init_json);
                string init_str = Encoding.UTF8.GetString(bytes);

                JObject init_result = JsonConvert.DeserializeObject(init_str) as JObject;

                foreach (JObject synckey in init_result["SyncKey"]["List"])  //同步键值
                {
                    _syncKey.Add(synckey["Key"].ToString(), synckey["Val"].ToString());
                }
                return init_result;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取好友头像
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Image GetIcon(string username)
        {
            byte[] bytes = BaseService.SendGetRequest(_geticon_url + username);

            return Image.FromStream(new MemoryStream(bytes));
        }
        /// <summary>
        /// 获取微信讨论组头像
        /// </summary>
        /// <param name="usename"></param>
        /// <returns></returns>
        public Image GetHeadImg(string usename)
        {
            byte[] bytes = BaseService.SendGetRequest(_getheadimg_url + usename);

            return Image.FromStream(new MemoryStream(bytes));
        }
        /// <summary>
        /// 获取好友列表
        /// </summary>
        /// <returns></returns>
        public JObject GetContact()
        {
            byte[] bytes = BaseService.SendGetRequest(_getcontact_url);
            string contact_str = Encoding.UTF8.GetString(bytes);

            return JsonConvert.DeserializeObject(contact_str) as JObject;     
        }
        /// <summary>
        /// 微信同步检测
        /// </summary>
        /// <returns></returns>
        public string WxSyncCheck()
        {
            string sync_key = "";
            foreach (KeyValuePair<string, string> p in _syncKey)
            {
                sync_key += p.Key + "_" + p.Value + "%7C";
            }
            sync_key = sync_key.TrimEnd('%','7','C');

            Cookie sid = BaseService.GetCookie("wxsid");
            Cookie uin = BaseService.GetCookie("wxuin");

            if (sid != null && uin != null)
            {
                _synccheck_url = string.Format(_synccheck_url, sid.Value, uin.Value, sync_key, (long)(DateTime.Now.ToUniversalTime() - new System.DateTime(1970, 1, 1)).TotalMilliseconds, LoginService.SKey.Replace("@", "%40"), "e1615250492");

                byte[] bytes = BaseService.SendGetRequest(_synccheck_url +"&_=" + DateTime.Now.Ticks);
                if (bytes != null)
                {
                    return Encoding.UTF8.GetString(bytes);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 微信同步
        /// </summary>
        /// <returns></returns>
        public JObject WxSync()
        {
            string sync_json = "{{\"BaseRequest\" : {{\"DeviceID\":\"e1615250492\",\"Sid\":\"{1}\", \"Skey\":\"{5}\", \"Uin\":\"{0}\"}},\"SyncKey\" : {{\"Count\":{2},\"List\":[{3}]}},\"rr\" :{4}}}";
            Cookie sid = BaseService.GetCookie("wxsid");
            Cookie uin = BaseService.GetCookie("wxuin");

            string sync_keys = "";
            foreach (KeyValuePair<string, string> p in _syncKey)
            {
                sync_keys += "{\"Key\":" + p.Key + ",\"Val\":" + p.Value + "},";
            }
            sync_keys = sync_keys.TrimEnd(',');
            sync_json = string.Format(sync_json, uin.Value, sid.Value, _syncKey.Count, sync_keys, (long)(DateTime.Now.ToUniversalTime() - new System.DateTime(1970, 1, 1)).TotalMilliseconds, LoginService.SKey);

            if (sid != null && uin != null)
            {
                byte[] bytes = BaseService.SendPostRequest(_sync_url + sid.Value + "&lang=zh_CN&skey=" + LoginService.SKey + "&pass_ticket=" + LoginService.Pass_Ticket, sync_json);
                string sync_str = Encoding.UTF8.GetString(bytes);

                JObject sync_resul = JsonConvert.DeserializeObject(sync_str) as JObject;

                if (sync_resul["SyncKey"]["Count"].ToString() != "0")
                {
                    _syncKey.Clear();
                    foreach (JObject key in sync_resul["SyncKey"]["List"])
                    {
                        _syncKey.Add(key["Key"].ToString(), key["Val"].ToString());
                    }
                }
                return sync_resul;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="type"></param>
        public void SendMsg(string msg, string from, string to, int type)
        {
            string msg_json = "{{" +
            "\"BaseRequest\":{{" +
                "\"DeviceID\" : \"e441551176\"," +
                "\"Sid\" : \"{0}\"," +
                "\"Skey\" : \"{6}\"," +
                "\"Uin\" : \"{1}\"" +
            "}}," +
            "\"Msg\" : {{" +
                "\"ClientMsgId\" : {8}," +
                "\"Content\" : \"{2}\"," +
                "\"FromUserName\" : \"{3}\"," +
                "\"LocalID\" : {9}," +
                "\"ToUserName\" : \"{4}\"," +
                "\"Type\" : {5}" +
            "}}," +
            "\"rr\" : {7}" +
            "}}";

            Cookie sid = BaseService.GetCookie("wxsid");
            Cookie uin = BaseService.GetCookie("wxuin");

            if (sid != null && uin != null)
            {
                msg_json = string.Format(msg_json, sid.Value, uin.Value, msg, from, to, type, LoginService.SKey, DateTime.Now.Millisecond, DateTime.Now.Millisecond, DateTime.Now.Millisecond);

                byte[] bytes = BaseService.SendPostRequest(_sendmsg_url + sid.Value + "&lang=zh_CN&pass_ticket="+LoginService.Pass_Ticket, msg_json);

                string send_result = Encoding.UTF8.GetString(bytes);
            }
        }

    }
}
