# WeChat.NET #
--------------------
WeChat.NET is based on web wechat.http://wx.qq.com

see more: http://www.cnblogs.com/xiaozhi_5638

## overview ##
nothing here but a few screenshots:

- ![][0]
- ![][1]
- ![][2]
- ![][3]
- ![][4]
- ![][5]
- ![][6]

## can do and cant do ##
**can do**
- login with qrcode;
- load latest contacts;
- load all friends;
- chat with friends;
- view friends' simple infomation(nickname, sex, signature etc.).

**cant do**
- cant receive msg except string("hello"、"你好" etc.);
- cant send msg except string;
- cant scan friends' status;

## step by step ##

1. get session id. like "gZ3bQtahGg==";
2. get qrcode using session id;
3. show the qrcode;
4. check weather the user scan the qrcode or not;
5. initialize latest contacts;
6. loading all friends;
7. check the status continuously.

## thanks and references ##
1. json.net
2. webkit.net
3. wx.qq.com
4. chrome

**all source code follow the [MIT][1] license.**


[0]: https://github.com/sherlockchou86/WeChat.NET/blob/master/assert/p1.png
[1]: https://github.com/sherlockchou86/WeChat.NET/blob/master/assert/p2.png
[2]: https://github.com/sherlockchou86/WeChat.NET/blob/master/assert/p3.png
[3]: https://github.com/sherlockchou86/WeChat.NET/blob/master/assert/p4.png
[4]: https://github.com/sherlockchou86/WeChat.NET/blob/master/assert/p5.png
[5]: https://github.com/sherlockchou86/WeChat.NET/blob/master/assert/p6.png
[6]: https://github.com/sherlockchou86/WeChat.NET/blob/master/assert/p7.png
