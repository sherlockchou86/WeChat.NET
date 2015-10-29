using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using WeChat.NET.Controls;
using WeChat.NET.Objects;
using WeChat.NET.HTTP;
using Newtonsoft.Json.Linq;

namespace WeChat.NET
{
    /// <summary>
    /// 主界面
    /// </summary>
    public partial class frmMainForm : Form
    {
        /// <summary>
        /// 主界面等待提示
        /// </summary>
        private Label _lblWait;
        /// <summary>
        /// 聊天对话框
        /// </summary>
        private WChatBox _chat2friend;
        /// <summary>
        /// 好友信息框
        /// </summary>
        private WPersonalInfo _friendInfo;
        /// <summary>
        /// 当前登录微信用户
        /// </summary>
        private WXUser _me;

        private List<Object> _contact_all = new List<object>();
        private List<object> _contact_latest = new List<object>();

        /// <summary>
        /// 构造方法
        /// </summary>
        public frmMainForm()
        {
            InitializeComponent();

            _chat2friend = new WChatBox();
            _chat2friend.Dock = DockStyle.Fill;
            _chat2friend.Visible = false;
            _chat2friend.FriendInfoView += new FriendInfoViewEventHandler(_chat2friend_FriendInfoView);
            Controls.Add(_chat2friend);

            _friendInfo = new WPersonalInfo();
            _friendInfo.Dock = DockStyle.Fill;
            _friendInfo.Visible = false;
            _friendInfo.StartChat += new StartChatEventHandler(_friendInfo_StartChat);
            Controls.Add(_friendInfo);

            _lblWait = new Label();
            _lblWait.Text = "数据加载...";
            _lblWait.AutoSize = false;
            _lblWait.Size = this.ClientSize;
            _lblWait.TextAlign = ContentAlignment.MiddleCenter;
            _lblWait.Location = new Point(0, 0);
            Controls.Add(_lblWait);
        }


        #region  事件处理程序
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMainForm_Load(object sender, EventArgs e)
        {
            DoMainLogic();
        }
        /// <summary>
        /// 好友信息框中点击 聊天
        /// </summary>
        /// <param name="user"></param>
        void _friendInfo_StartChat(WXUser user)
        {
            _chat2friend.Visible = true;
            _chat2friend.FriendUser = user;
            _chat2friend.MeUser = _me;
            _chat2friend.BringToFront();
        }
        /// <summary>
        /// 聊天对话框中点击 好友信息
        /// </summary>
        /// <param name="user"></param>
        void _chat2friend_FriendInfoView(WXUser user)
        {
            _friendInfo.FriendUser = user;
            _friendInfo.Visible = true;
            _friendInfo.BringToFront();
        }
        /// <summary>
        /// 聊天列表点击好友   开始聊天
        /// </summary>
        /// <param name="user"></param>
        private void wchatlist_StartChat(WXUser user)
        {
            _chat2friend.Visible = true;
            _chat2friend.FriendUser = user;
            _chat2friend.MeUser = _me;
            _chat2friend.BringToFront();
        }
        /// <summary>
        /// 通讯录中点击好友 查看好友信息
        /// </summary>
        /// <param name="user"></param>
        private void wfriendlist_FriendInfoView(WXUser user)
        {
            _friendInfo.FriendUser = user;
            _friendInfo.Visible = true;
            _friendInfo.BringToFront();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
        #endregion

        #region 主逻辑
        /// <summary>
        /// 
        /// </summary>
        private void DoMainLogic()
        {
            _lblWait.BringToFront();
            ((Action)(delegate()
            {
                WXService wxs = new WXService();
                JObject init_result = wxs.WxInit();  //初始化
                
                List<object> contact_all = new List<object>();
                if (init_result != null)
                {
                    _me = new WXUser();
                    _me.UserName = init_result["User"]["UserName"].ToString();
                    _me.City = "";
                    _me.HeadImgUrl = init_result["User"]["HeadImgUrl"].ToString();
                    _me.NickName = init_result["User"]["NickName"].ToString();
                    _me.Province = "";
                    _me.PYQuanPin = init_result["User"]["PYQuanPin"].ToString();
                    _me.RemarkName = init_result["User"]["RemarkName"].ToString();
                    _me.RemarkPYQuanPin = init_result["User"]["RemarkPYQuanPin"].ToString();
                    _me.Sex = init_result["User"]["Sex"].ToString();
                    _me.Signature = init_result["User"]["Signature"].ToString();

                    foreach (JObject contact in init_result["ContactList"])  //部分好友名单
                    {
                        WXUser user = new WXUser();
                        user.UserName = contact["UserName"].ToString();
                        user.City = contact["City"].ToString();
                        user.HeadImgUrl = contact["HeadImgUrl"].ToString();
                        user.NickName = contact["NickName"].ToString();
                        user.Province = contact["Province"].ToString();
                        user.PYQuanPin = contact["PYQuanPin"].ToString();
                        user.RemarkName = contact["RemarkName"].ToString();
                        user.RemarkPYQuanPin = contact["RemarkPYQuanPin"].ToString();
                        user.Sex = contact["Sex"].ToString();
                        user.Signature = contact["Signature"].ToString();

                        _contact_latest.Add(user);
                    }
                }

                JObject contact_result = wxs.GetContact(); //通讯录
                if (contact_result != null)
                {
                    foreach (JObject contact in contact_result["MemberList"])  //完整好友名单
                    {
                        WXUser user = new WXUser();
                        user.UserName = contact["UserName"].ToString();
                        user.City = contact["City"].ToString();
                        user.HeadImgUrl = contact["HeadImgUrl"].ToString();
                        user.NickName = contact["NickName"].ToString();
                        user.Province = contact["Province"].ToString();
                        user.PYQuanPin = contact["PYQuanPin"].ToString();
                        user.RemarkName = contact["RemarkName"].ToString();
                        user.RemarkPYQuanPin = contact["RemarkPYQuanPin"].ToString();
                        user.Sex = contact["Sex"].ToString();
                        user.Signature = contact["Signature"].ToString();

                        contact_all.Add(user);
                    }
                }
                IOrderedEnumerable<object> list_all = contact_all.OrderBy(e => (e as WXUser).ShowPinYin);

                WXUser wx; string start_char;
                foreach (object o in list_all)
                {
                    wx = o as WXUser;
                    start_char = wx.ShowPinYin == "" ? "" : wx.ShowPinYin.Substring(0, 1);
                    if (!_contact_all.Contains(start_char.ToUpper()))
                    {
                        _contact_all.Add(start_char.ToUpper());
                    }
                    _contact_all.Add(o);
                }

                this.BeginInvoke((Action)(delegate()  //等待结束
                {
                    _lblWait.Visible = false;

                    wChatList1.Items.AddRange(_contact_latest.ToArray());  //近期联系人
                    wFriendsList1.Items.AddRange(_contact_all.ToArray());  //通讯录
                    wpersonalinfo.FriendUser = _me;
                }));


                string sync_flag = "";
                JObject sync_result;
                while (true)
                {
                    sync_flag = wxs.WxSyncCheck();  //同步检查
                    if (sync_flag.Contains("selector:\"0\""))  //
                    {

                    }
                    else if (sync_flag.Contains("selector:\"2\"")) //有消息
                    {
                        sync_result = wxs.WxSync();  //进行同步
                        if (sync_result != null)
                        {

                        }
                    }
                    else if (sync_flag.Contains("selector:\"0\""))
                    {

                    }
                    else
                    {

                    }
                }

            })).BeginInvoke(null, null);
        }
        #endregion

    }
}
