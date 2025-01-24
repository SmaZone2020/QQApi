using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Text;

class Program
{

    static async Task Main(string[] args)
    {

        //https://ssl.ptlogin2.qq.com/jump?ptlang=2052&clientuin=351138806&clientkey=E2BEE5DAD70BF093BA5733D4D6BA4B8B61426F682616A277F076835AC49E52EF&u1=https:%2F%2Fuser.qzone.qq.com%2F351138806%2Finfocenter&source=panelstar
        //https://ssl.ptlogin2.qq.com/jump?ptlang=2052&clientuin=3880035049&clientkey=DBC4CD4B0F326AD0B9C8E0EF58998253566379DA375F4F59329FFD2B89D3568B&u1=https:%2F%2Fuser.qzone.qq.com%2F3880035049%2Finfocenter&source=panelstar
        //https://ssl.ptlogin2.qq.com/jump?ptlang=1033&clientuin=511413324&clientkey=f842dd0a463b26605201fdf4cef8cb9dfa078fb101c43b80ddf9de54135e2cbb4eb8cdbbb7ca2733254c5551edeb7017&u1=https:%2F%2Fqun.qq.com%2Fmember.html%2F
        //https://ssl.ptlogin2.qq.com/jump?ptlang=1033&clientuin=2431798772&clientkey=90dc60e5c754c226ec7e5ab0c3fe5775c9d26ea13db11efe364c5b9dfcc7bab8886b552c1f8319cf4814e3be20b894eb&u1=https%3A%2F%2Fuser.qzone.qq.com%2F2431798772%2Finfocenter&keyindex=19
        //https://ssl.ptlogin2.qq.com/jump?ptlang=1033&clientuin=2431798772&clientkey=068dbb13bc038f4ac68caaea576bf10f33ddf6981265c893196fc0fd9905c4b106cc1cfa86735dca5ff7e8ee2c36d67c&u1=https%3A%2F%2Fwx.mail.qq.com%2Flist%2Freadtemplate%3Fname%3Dlogin_page.html&keyindex=19
        //var qq = "2431798772";
        //var clientkey = "068dbb13bc038f4ac68caaea576bf10f33ddf6981265c893196fc0fd9905c4b106cc1cfa86735dca5ff7e8ee2c36d67c";
        var qq = "";
        var clientkey = "";
        
        var type = "";
        //Console.WriteLine(await Handle.PostQQZoneMessage(qq,"来自StarCookie客户端调试发送", clientkey,"1033"));

        //Console.WriteLine(await Handle.GetUserInfo(qq,clientkey));

        //Console.WriteLine(await Handle.GetGroupList(qq, clientkey, "1033"));

        //Console.WriteLine(await Handle.PostAnnounce(qq, clientkey, "286106964","我滴妈啊", "1033"));

        //Console.WriteLine(await Handle.SetGroupMemberNick(qq, clientkey, "286106964", "2020896116","Hacked By Krgjd", "1033"));

        //Console.WriteLine(await Handle.GetGroupMember(qq, clientkey, "286106964", "1033"));

        while (true)
        {
            Console.WriteLine(@"                       |                             
       ||||            |  ||||||   ||||         |||  
      ||  |           ||  |   |   ||  |   ||    | || 
       ||            | |     |    |   | ||  |     |  
         || ||||||| |  |    |     |   | |   |    ||| 
          |||  |  | ||||||  |   | |   | |   |      | 
      ||  | |  |  |    |   |   || ||  | ||  |      | 
      ||||| |  |  |    |  |||||||  ||| ||| |||  | || 
           |||||||||                            ||   ");

            Console.WriteLine("加入交流群N0v4 IT[286106964]");
            Console.WriteLine("免责声明：");
            Console.WriteLine(@"
1.API接口均调用腾讯公司的开放API,本人仅将其进行整理和整合。
2.凡因为使用者不正确使用方法导致的一切民事、刑事后果均由使用者自行承担。
3.本软件仅供技术交流使用，下载后请在24小时内删除。
4.使用本软件时请严格遵守《中华人民共和国网络安全法》。
5.本软件完全免费，请勿倒卖。
");


            while (clientkey.Length < 20)
            {
                Console.WriteLine("请输入正确的ClientKey");
                clientkey = Console.ReadLine();
            }

            while (qq.Length < 5)
            {
                Console.WriteLine("请输入正确的QQ");
                qq = Console.ReadLine();
            }

            while (type != "1" && type != "2" && type != "QQ" && type != "NT")
            {
                Console.WriteLine("请输入正确的KeyType序号");
                Console.WriteLine("1.QQ");
                Console.WriteLine("2.NT");
                type = Console.ReadLine();
            }

            if (type == "1") type = "2052";
            if (type == "2") type = "1033";

            Console.WriteLine("请选择进行的业务的序号");
            Console.WriteLine("1.发空间说说");
            Console.WriteLine("2.设置/取消指定好友为特别关心");
            Console.WriteLine("3.修改昵称/地址/公司信息");
            Console.WriteLine("4.获取用户信息");
            Console.WriteLine("5.获取指定群成员");
            Console.WriteLine("6.获取群列表");
            Console.WriteLine("7.获取好友列表");
            Console.WriteLine("8.发公告");
            Console.WriteLine("9.更改群内成员昵称");
            Console.WriteLine("10.删除成员");
            Console.WriteLine("11.添加管理员");
            Console.WriteLine("12.获取群验证消息");

            var work = Console.ReadLine();
            switch (work)
            {
                case "1":
                    Console.WriteLine("输入要发说说的文字内容");
                    string text = Console.ReadLine();
                    Console.WriteLine(await Handle.PostQQZoneMessage(qq, text, clientkey, type));
                    break;
                case "2":
                    Console.WriteLine("输入好友QQ");
                    string setQQ = Console.ReadLine();
                    Console.WriteLine("输入1设置为特别关心，输入0取消特别关心");
                    bool set = Console.ReadLine() == "1";
                    Console.WriteLine(await Handle.SetCare(qq, clientkey, setQQ, set, type));
                    break;
                case "3":
                    Console.WriteLine("输入新的昵称");
                    string newName = Console.ReadLine();
                    Console.WriteLine("输入新的公司信息");
                    string company = Console.ReadLine();
                    Console.WriteLine("输入新的地址");
                    string address = Console.ReadLine();
                    Console.WriteLine(await Handle.ChangeUserInfo(qq, clientkey, newName, type, company, address));
                    break;
                case "4":
                    Console.WriteLine(await Handle.GetUserInfo(qq, clientkey, type));
                    break;
                case "5":
                    Console.WriteLine("输入群ID");
                    string groupID = Console.ReadLine();
                    Console.WriteLine("输入开始的群员位置（默认为1）");
                    string start = Console.ReadLine();
                    Console.WriteLine("输入结束的群员位置（默认为1）");
                    string end = Console.ReadLine();
                    Console.WriteLine(await Handle.GetGroupMember(qq, clientkey, groupID, start, end, type));
                    break;
                case "6":
                    Console.WriteLine(await Handle.GetGroupList(qq, clientkey, type));
                    break;
                case "7":
                    Console.WriteLine(await Handle.GetFriendList(qq, clientkey, type));
                    break;
                case "8":
                    Console.WriteLine("输入群ID");
                    groupID = Console.ReadLine();
                    Console.WriteLine("输入公告内容");
                    text = Console.ReadLine();
                    Console.WriteLine(await Handle.PostAnnounce(qq, clientkey, groupID, text, type));
                    break;
                case "9":
                    Console.WriteLine("输入群ID");
                    groupID = Console.ReadLine();
                    Console.WriteLine("输入成员QQ");
                    string memberQQ = Console.ReadLine();
                    Console.WriteLine("输入新的昵称");
                    string setNick = Console.ReadLine();
                    Console.WriteLine(await Handle.SetGroupMemberNick(qq, clientkey, groupID, memberQQ, setNick, type));
                    break;
                case "10":
                    Console.WriteLine("输入群ID");
                    groupID = Console.ReadLine();
                    Console.WriteLine("输入成员QQ");
                    memberQQ = Console.ReadLine();
                    Console.WriteLine(await Handle.DelGroupMember(qq, clientkey, groupID, memberQQ, type));
                    break;
                case "11":
                    Console.WriteLine("输入群ID");
                    groupID = Console.ReadLine();
                    Console.WriteLine("输入成员QQ");
                    memberQQ = Console.ReadLine();
                    Console.WriteLine(await Handle.AddGroupAdmin(qq, clientkey, groupID, memberQQ, type));
                    break;
                case "12":
                    Console.WriteLine(await Handle.GetGroupAuthMessage(qq, clientkey, type));
                    break;
                default:
                    Console.WriteLine("无效的选项");
                    break;
            }
            Console.WriteLine("按Enter清屏并继续操作...");
            Console.ReadLine();
            Console.Clear();
        }
    }



}
