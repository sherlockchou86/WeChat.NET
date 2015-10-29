namespace WeChat.NET
{
    partial class frmMainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.wTabControl1 = new WeChat.NET.Controls.WTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.wChatList1 = new WeChat.NET.Controls.WChatList();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.wFriendsList1 = new WeChat.NET.Controls.WFriendsList();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.wpersonalinfo = new WeChat.NET.Controls.WPersonalInfo();
            this.wTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // wTabControl1
            // 
            this.wTabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.wTabControl1.Controls.Add(this.tabPage1);
            this.wTabControl1.Controls.Add(this.tabPage2);
            this.wTabControl1.Controls.Add(this.tabPage3);
            this.wTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wTabControl1.ItemSize = new System.Drawing.Size(91, 60);
            this.wTabControl1.Location = new System.Drawing.Point(0, 0);
            this.wTabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.wTabControl1.Name = "wTabControl1";
            this.wTabControl1.Padding = new System.Drawing.Point(0, 0);
            this.wTabControl1.SelectedIndex = 0;
            this.wTabControl1.Size = new System.Drawing.Size(276, 452);
            this.wTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.wTabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.wChatList1);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(268, 384);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "微信";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // wChatList1
            // 
            this.wChatList1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.wChatList1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.wChatList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wChatList1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.wChatList1.FormattingEnabled = true;
            this.wChatList1.Location = new System.Drawing.Point(0, 0);
            this.wChatList1.Name = "wChatList1";
            this.wChatList1.Size = new System.Drawing.Size(268, 384);
            this.wChatList1.TabIndex = 0;
            this.wChatList1.StartChat += new WeChat.NET.Controls.StartChatEventHandler(this.wchatlist_StartChat);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.wFriendsList1);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(268, 384);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "通讯录";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // wFriendsList1
            // 
            this.wFriendsList1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.wFriendsList1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.wFriendsList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wFriendsList1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.wFriendsList1.FormattingEnabled = true;
            this.wFriendsList1.Location = new System.Drawing.Point(0, 0);
            this.wFriendsList1.Name = "wFriendsList1";
            this.wFriendsList1.Size = new System.Drawing.Size(268, 384);
            this.wFriendsList1.TabIndex = 0;
            this.wFriendsList1.FriendInfoView += new WeChat.NET.Controls.FriendInfoViewEventHandler(this.wfriendlist_FriendInfoView);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.wpersonalinfo);
            this.tabPage3.Location = new System.Drawing.Point(4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(268, 384);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "我";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // wpersonalinfo
            // 
            this.wpersonalinfo.BackColor = System.Drawing.Color.White;
            this.wpersonalinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wpersonalinfo.FriendUser = null;
            this.wpersonalinfo.Location = new System.Drawing.Point(0, 0);
            this.wpersonalinfo.Margin = new System.Windows.Forms.Padding(0);
            this.wpersonalinfo.Name = "wpersonalinfo";
            this.wpersonalinfo.ShowTopPanel = false;
            this.wpersonalinfo.Size = new System.Drawing.Size(268, 384);
            this.wpersonalinfo.TabIndex = 0;
            // 
            // frmMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 452);
            this.Controls.Add(this.wTabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimizeBox = false;
            this.Name = "frmMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "微信.NET";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMainForm_FormClosing);
            this.Load += new System.EventHandler(this.frmMainForm_Load);
            this.wTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.WTabControl wTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private Controls.WPersonalInfo wpersonalinfo;
        private Controls.WChatList wChatList1;
        private Controls.WFriendsList wFriendsList1;







    }
}