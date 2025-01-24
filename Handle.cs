using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Numerics;

public static class Handle
{
    //发空间说说
    public static async Task<string> PostQQZoneMessage(string qq,string text, string clientkey, string ptlang = "2052")
    {
        string url = $"https://ssl.ptlogin2.qq.com/jump?ptlang={ptlang}&clientuin={qq}&clientkey={clientkey}&u1=https%3A%2F%2Fuser.qzone.qq.com%2F{qq}%2Finfocenter&source=panelstar&keyindex=19";

        if (ptlang == "2052")
            url = $"https://ssl.ptlogin2.qq.com/jump?ptlang={ptlang}&clientuin={qq}&clientkey={clientkey}&u1=https:%2F%2Fuser.qzone.qq.com%2F{qq}%2Finfocenter&source=panelstar";

        string skey = "";
        var cks = new CookieCollection();

        var handler = new HttpClientHandler
        {
            AllowAutoRedirect = true,
            CookieContainer = new CookieContainer(),
            UseCookies = true,
            AutomaticDecompression = DecompressionMethods.None
        };

        using (var client = new HttpClient(handler))
        {
            var response = await client.GetAsync(url);
            var finalUrl = response.RequestMessage.RequestUri.ToString();

            Console.WriteLine($"开始请求{url}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"响应成功: {response.IsSuccessStatusCode}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"最终 URL: {finalUrl}");
            Console.ResetColor();

            cks = handler.CookieContainer.GetCookies(new Uri(finalUrl));

            Console.WriteLine($"\nCookies [{cks.Count}]:");
            foreach (Cookie cookie in cks)
            {
                Console.WriteLine($"[{cookie.Domain}] {cookie.Name} = {cookie.Value}");

                if (cookie.Name.Contains("skey"))
                    skey = cookie.Value;
            }
            Console.WriteLine($"QQ:[{qq}] | Skey:[{skey}] | GTK:[{GetGTK(skey)}]");

        }

        Console.WriteLine("开始发说说");
        using (var handler_ = new HttpClientHandler())
        {
            handler_.UseCookies = true;
            handler_.CookieContainer = new CookieContainer();
            string gurl = $"https://user.qzone.qq.com/proxy/domain/taotao.qzone.qq.com/cgi-bin/emotion_cgi_publish_v6?&g_tk={GetGTK(skey)}";
            Console.WriteLine($"请求URL：{url}");

            // 设置 cookies
            var uri = new Uri(gurl);
            foreach (Cookie cookie in cks)
            {
                Console.WriteLine($"添加Cookie:[{cookie.Name} = {cookie.Value}]");
                handler_.CookieContainer.Add(uri, cookie);
            }
            using (var client = new HttpClient(handler_))
            {
                // 设置请求头
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                client.DefaultRequestHeaders.AcceptLanguage.ParseAdd("zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6");
                client.DefaultRequestHeaders.Referrer = new Uri($"https://user.qzone.qq.com/{qq}/infocenter");
                client.DefaultRequestHeaders.Add("sec-ch-ua", "\"Not/A)Brand\";v=\"8\", \"Chromium\";v=\"126\", \"Microsoft Edge\";v=\"126\"");
                client.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
                client.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");
                client.DefaultRequestHeaders.Add("sec-fetch-dest", "empty");
                client.DefaultRequestHeaders.Add("sec-fetch-mode", "cors");
                client.DefaultRequestHeaders.Add("sec-fetch-site", "same-origin");

                // 设置内容
                var textA = $"syn_tweet_verson=1" +
                    $"&paramstr=1" +
                    $"&pic_template=" +
                    $"&richtype=" +
                    $"&richval=" +
                    $"&special_url=" +
                    $"&subrichtype=" +
                    $"&who=1" +
                    $"&con={text}" +
                    $"&feedversion=1" +
                    $"&ver=1" +
                    $"&ugc_right=1" +
                    $"&to_sign=0" +
                    $"&hostuin={qq}" +
                    $"&code_version=1" +
                    $"&format=fs" +
                    $"&qzreferrer=https%3A%2F%2Fuser.qzone.qq.com%2F{qq}%2Finfocenter";
                var content = new StringContent(textA, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
                Console.WriteLine($"\n请求Content：{textA}\n");

                var response = await client.PostAsync(gurl, content);
                response.EnsureSuccessStatusCode();

                var responseHtml = await ReadResponseContentAsync(response);

                return responseHtml;
            }
        }
    }
    //设置/取消指定好友为特别关心
    public static async Task<string> SetCare(string qq, string clientkey,string setQQ,bool set = true, string ptlang = "2052")
    {
        string url = $"https://ssl.ptlogin2.qq.com/jump?ptlang={ptlang}&clientuin={qq}&clientkey={clientkey}&u1=https%3A%2F%2Fuser.qzone.qq.com%2F{qq}%2Finfocenter&source=panelstar&keyindex=19";

        if (ptlang == "2052")
            url = $"https://ssl.ptlogin2.qq.com/jump?ptlang={ptlang}&clientuin={qq}&clientkey={clientkey}&u1=https:%2F%2Fuser.qzone.qq.com%2F{qq}%2Finfocenter&source=panelstar";

        string skey = "";
        var cks = new CookieCollection();

        var handler = new HttpClientHandler
        {
            AllowAutoRedirect = true,
            CookieContainer = new CookieContainer(),
            UseCookies = true,
            AutomaticDecompression = DecompressionMethods.None
        };

        using (var client = new HttpClient(handler))
        {
            var response = await client.GetAsync(url);
            var finalUrl = response.RequestMessage.RequestUri.ToString();

            Console.WriteLine($"开始请求{url}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"响应成功: {response.IsSuccessStatusCode}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"最终 URL: {finalUrl}");
            Console.ResetColor();

            cks = handler.CookieContainer.GetCookies(new Uri(finalUrl));

            Console.WriteLine($"\nCookies [{cks.Count}]:");
            foreach (Cookie cookie in cks)
            {
                Console.WriteLine($"[{cookie.Domain}] {cookie.Name} = {cookie.Value}");

                if (cookie.Name.Contains("skey"))
                    skey = cookie.Value;
            }
            Console.WriteLine($"QQ:[{qq}] | Skey:[{skey}] | GTK:[{GetGTK(skey)}]");

        }

        Console.WriteLine("开始操作特别关心");
        using (var handler_ = new HttpClientHandler())
        {
            handler_.UseCookies = true;
            handler_.CookieContainer = new CookieContainer();
            string gurl = $"https://user.qzone.qq.com/proxy/domain/w.qzone.qq.com/cgi-bin/tfriend/specialcare_set.cgi?&g_tk={GetGTK(skey)}";
            Console.WriteLine($"请求URL：{url}");

            // 设置 cookies
            var uri = new Uri(gurl);
            foreach (Cookie cookie in cks)
            {
                Console.WriteLine($"添加Cookie:[{cookie.Name} = {cookie.Value}]");
                handler_.CookieContainer.Add(uri, cookie);
            }
            using (var client = new HttpClient(handler_))
            {
                // 设置请求头
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                client.DefaultRequestHeaders.AcceptLanguage.ParseAdd("zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6");
                client.DefaultRequestHeaders.Referrer = new Uri($"https://user.qzone.qq.com/{qq}/infocenter");
                client.DefaultRequestHeaders.Add("sec-ch-ua", "\"Not/A)Brand\";v=\"8\", \"Chromium\";v=\"126\", \"Microsoft Edge\";v=\"126\"");
                client.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
                client.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");
                client.DefaultRequestHeaders.Add("sec-fetch-dest", "empty");
                client.DefaultRequestHeaders.Add("sec-fetch-mode", "cors");
                client.DefaultRequestHeaders.Add("sec-fetch-site", "same-origin");

                // 设置内容
                var doIt = "1";
                if (set)
                    doIt = "2";

                var textA = $"uin={qq}" +
                    $"&do={doIt}" +
                    $"&fupdate=1" +
                    $"&fuin={setQQ}" +
                    $"&qzreferrer=https%3A%2F%2Fuser.qzone.qq.com%2F{qq}%2Finfocenter";
                var content = new StringContent(textA, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
                Console.WriteLine($"\n请求Content：{textA}\n");

                var response = await client.PostAsync(gurl, content);
                response.EnsureSuccessStatusCode();

                var responseHtml = await ReadResponseContentAsync(response);

                return responseHtml;
            }
        }
    }
    //修改信息
    public static async Task<string> ChangeUserInfo(string qq, string clientkey,string newName, string ptlang = "2052",string company = "奶龙园区祝你天天开心",string address = "奶龙园区祝你天天开心")
    {
        string url = $"https://ssl.ptlogin2.qq.com/jump?ptlang={ptlang}&clientuin={qq}&clientkey={clientkey}&u1=https%3A%2F%2Fuser.qzone.qq.com%2F{qq}%2Finfocenter&source=panelstar&keyindex=19";

        if (ptlang == "2052")
            url = $"https://ssl.ptlogin2.qq.com/jump?ptlang={ptlang}&clientuin={qq}&clientkey={clientkey}&u1=https:%2F%2Fuser.qzone.qq.com%2F{qq}%2Finfocenter&source=panelstar";

        string skey = "";
        var cks = new CookieCollection();

        var handler = new HttpClientHandler
        {
            AllowAutoRedirect = true,
            CookieContainer = new CookieContainer(),
            UseCookies = true,
            AutomaticDecompression = DecompressionMethods.None
        };

        using (var client = new HttpClient(handler))
        {
            var response = await client.GetAsync(url);
            var finalUrl = response.RequestMessage.RequestUri.ToString();

            Console.WriteLine($"开始请求{url}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"响应成功: {response.IsSuccessStatusCode}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"最终 URL: {finalUrl}");
            Console.ResetColor();

            cks = handler.CookieContainer.GetCookies(new Uri(finalUrl));

            Console.WriteLine($"\nCookies [{cks.Count}]:");
            foreach (Cookie cookie in cks)
            {
                Console.WriteLine($"[{cookie.Domain}] {cookie.Name} = {cookie.Value}");

                if (cookie.Name.Contains("skey"))
                    skey = cookie.Value;
            }
            Console.WriteLine($"QQ:[{qq}] | Skey:[{skey}] | GTK:[{GetGTK(skey)}]");

        }

        Console.WriteLine("开始修改昵称");
        using (var handler_ = new HttpClientHandler())
        {
            handler_.UseCookies = true;
            handler_.CookieContainer = new CookieContainer();
            string gurl = $"https://h5.qzone.qq.com/proxy/domain/w.qzone.qq.com/cgi-bin/user/cgi_apply_updateuserinfo_new?&g_tk={GetGTK(skey)}";
            Console.WriteLine($"请求URL：{url}");

            // 设置 cookies
            var uri = new Uri(gurl);
            foreach (Cookie cookie in cks)
            {
                Console.WriteLine($"添加Cookie:[{cookie.Name} = {cookie.Value}]");
                handler_.CookieContainer.Add(uri, cookie);
            }
            using (var client = new HttpClient(handler_))
            {
                // 设置请求头
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                client.DefaultRequestHeaders.AcceptLanguage.ParseAdd("zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6");
                client.DefaultRequestHeaders.Referrer = new Uri($"https://user.qzone.qq.com/{qq}/infocenter");
                client.DefaultRequestHeaders.Add("sec-ch-ua", "\"Not/A)Brand\";v=\"8\", \"Chromium\";v=\"126\", \"Microsoft Edge\";v=\"126\"");
                client.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
                client.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");
                client.DefaultRequestHeaders.Add("sec-fetch-dest", "empty");
                client.DefaultRequestHeaders.Add("sec-fetch-mode", "cors");
                client.DefaultRequestHeaders.Add("sec-fetch-site", "same-origin");

                // 设置内容
                var textA = $"nickname={newName}" +
                    $"&emoji=" +
                    $"&sex=2" +
                    $"&birthday=2019-01-01" +
                    $"&province=61" +
                    $"&city=1" +
                    $"&country=1" +
                    $"&marriage=0" +
                    $"&bloodtype=5" +
                    $"&hp=61" +
                    $"&hc=1" +
                    $"&hco=1" +
                    $"&career=奶龙园区祝你天天开心" +
                    $"&company={company}" +
                    $"&cp=0" +
                    $"&cc=0" +
                    $"&cb={address}" +
                    $"&cco=0" +
                    $"&lover=" +
                    $"&islunar=0" +
                    $"&mb=65" +
                    $"&uin={qq}" +
                    $"&pageindex=1" +
                    $"&fupdate=1" +
                    $"&qzreferrer=https%3A%2F%2Fuser.qzone.qq.com%2Fproxy%2Fdomain%2Fqzs.qq.com%2Fqzone%2Fv6%2Fsetting%2Fprofile%2Fprofile.html%3Ftab%3Dbase%26g_iframeUser%3D1";
                var content = new StringContent(textA, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
                Console.WriteLine($"\n请求Content：{textA}\n");

                var response = await client.PostAsync(gurl, content);
                response.EnsureSuccessStatusCode();

                var responseHtml = await ReadResponseContentAsync(response);

                return responseHtml;
            }
        }
    }
    //获取用户信息
    public static async Task<string> GetUserInfo(string qq, string clientkey,string ptlang = "2052")
    {
        string url = $"https://ssl.ptlogin2.qq.com/jump?ptlang={ptlang}&clientuin={qq}&clientkey={clientkey}&u1=https%3A%2F%2Fuser.qzone.qq.com%2F{qq}%2Finfocenter&source=panelstar&keyindex=19";
        
        if(ptlang == "2052")
            url = $"https://ssl.ptlogin2.qq.com/jump?ptlang={ptlang}&clientuin={qq}&clientkey={clientkey}&u1=https:%2F%2Fuser.qzone.qq.com%2F{qq}%2Finfocenter&source=panelstar";
        
        string skey = "";
        var cks = new CookieCollection();

        var handler = new HttpClientHandler
        {
            AllowAutoRedirect = true,
            CookieContainer = new CookieContainer(),
            UseCookies = true,
            AutomaticDecompression = DecompressionMethods.None
        };

        using (var client = new HttpClient(handler))
        {
            var response = await client.GetAsync(url);
            var finalUrl = response.RequestMessage.RequestUri.ToString();

            Console.WriteLine($"开始请求{url}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"响应成功: {response.IsSuccessStatusCode}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"最终 URL: {finalUrl}");
            Console.ResetColor();

            cks = handler.CookieContainer.GetCookies(new Uri(finalUrl));

            Console.WriteLine($"\nCookies [{cks.Count}]:");
            foreach (Cookie cookie in cks)
            {
                Console.WriteLine($"[{cookie.Domain}] {cookie.Name} = {cookie.Value}");

                if (cookie.Name.Contains("skey"))
                    skey = cookie.Value;
            }
            Console.WriteLine($"QQ:[{qq}] | Skey:[{skey}] | GTK:[{GetGTK(skey)}]");

        }

        Console.WriteLine("开始获取用户信息");
        using (var handler_ = new HttpClientHandler())
        {
            handler_.UseCookies = true;
            handler_.CookieContainer = new CookieContainer();
            string gurl = $"https://h5.qzone.qq.com/proxy/domain/base.qzone.qq.com/cgi-bin/user/cgi_userinfo_get_all?uin={qq}&vuin={qq}&fupdate=1&g_tk={GetGTK(skey)}";
            Console.WriteLine($"请求URL：{gurl}");

            // 设置 cookies
            var uri = new Uri(gurl);
            foreach (Cookie cookie in cks)
            {
                Console.WriteLine($"添加Cookie:[{cookie.Name} = {cookie.Value}]");
                handler_.CookieContainer.Add(uri, cookie);
            }
            using (var client = new HttpClient(handler_))
            {
                // 设置请求头
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                client.DefaultRequestHeaders.AcceptLanguage.ParseAdd("zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6");
                client.DefaultRequestHeaders.Referrer = new Uri($"https://user.qzone.qq.com/{qq}/infocenter");
                client.DefaultRequestHeaders.Add("sec-ch-ua", "\"Not/A)Brand\";v=\"8\", \"Chromium\";v=\"126\", \"Microsoft Edge\";v=\"126\"");
                client.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
                client.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");
                client.DefaultRequestHeaders.Add("sec-fetch-dest", "empty");
                client.DefaultRequestHeaders.Add("sec-fetch-mode", "cors");
                client.DefaultRequestHeaders.Add("sec-fetch-site", "same-origin");

                var response = await client.GetAsync(gurl);
                response.EnsureSuccessStatusCode();

                var responseHtml = await ReadResponseContentAsync(response);
                return responseHtml;
            }
        }
    }
    //获取群成员
    public static async Task<string> GetGroupMember(string qq, string clientkey, string groupID, string start = "1", string end = "1",string ptlang = "2052")
    {
        string loginUrl = $"https://ssl.ptlogin2.qq.com/jump?ptlang={ptlang}&clientuin={qq}&clientkey={clientkey}&u1=https%3A%2F%2Fqun.qq.com%2Fmanage.html%2F&keyindex=19";

        if (ptlang == "2052")
            loginUrl = $"https://ssl.ptlogin2.qq.com/jump?ptlang={ptlang}&clientuin={qq}&clientkey={clientkey}&u1=https:%2F%2Fqun.qq.com%2Fmanage.html%2F";

        Console.WriteLine($"登录接口 :{loginUrl}");
        string skey = "";
        CookieCollection cks = new CookieCollection();
        var handler = new HttpClientHandler
        {
            AllowAutoRedirect = true,
            CookieContainer = new CookieContainer(),
            UseCookies = true,
            AutomaticDecompression = DecompressionMethods.None
        };

        using (var client = new HttpClient(handler))
        {
            var response = await client.GetAsync(loginUrl);
            var finalUrl = response.RequestMessage.RequestUri.ToString();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"响应成功: {response.IsSuccessStatusCode}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"最终 URL: {finalUrl}");
            Console.ResetColor();

            cks = handler.CookieContainer.GetCookies(new Uri(finalUrl));

            Console.WriteLine($"\nCookies [{cks.Count}]:");
            foreach (Cookie cookie in cks)
            {
                Console.WriteLine($"[{cookie.Domain}] {cookie.Name} = {cookie.Value}");

                if (cookie.Name.Contains("skey"))
                    skey = cookie.Value;

            }
            Console.WriteLine($"QQ:[{qq}] | Skey:[{skey}] | GTK/BKN:[{GetGTK(skey)}]");
        }

        Console.WriteLine("开始获取群成员列表");
        using (var handler_ = new HttpClientHandler())
        {
            handler_.UseCookies = true;
            handler_.CookieContainer = new CookieContainer();
            string url = $"https://qun.qq.com/cgi-bin/qun_mgr/search_group_members?bkn={GetGTK(skey)}";
            Console.WriteLine($"请求URL：{url}");

            string referrerUrl = "https://qun.qq.com/";
            string requestBody = $"st={start}" +
                                $"&end={end}" +
                                $"&sort=1" +
                                $"&gc={groupID}" +
                                $"&bkn={GetGTK(skey)}";
            Console.WriteLine($"Content:{requestBody}");
            // 设置 cookies
            var uri = new Uri(url);
            foreach (Cookie cookie in cks)
            {
                Console.WriteLine($"添加Cookie:[{cookie.Name} = {cookie.Value}]");
                handler_.CookieContainer.Add(uri, cookie);
            }
            using (var client = new HttpClient(handler_))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                client.DefaultRequestHeaders.AcceptLanguage.ParseAdd("zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6");
                client.DefaultRequestHeaders.Add("sec-ch-ua", "\"Not)A;Brand\";v=\"99\", \"Microsoft Edge\";v=\"127\", \"Chromium\";v=\"127\"");
                client.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
                client.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");
                client.DefaultRequestHeaders.Add("sec-fetch-dest", "empty");
                client.DefaultRequestHeaders.Add("sec-fetch-mode", "cors");
                client.DefaultRequestHeaders.Add("sec-fetch-site", "same-origin");
                client.DefaultRequestHeaders.Referrer = new Uri(referrerUrl);

                var content = new StringContent(requestBody, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");

                var response = await client.PostAsync(url, content);

                var responseBody = await response.Content.ReadAsStringAsync();

                return(responseBody);
            }
        }
    }
    //获取群列表
    public static async Task<string> GetGroupList(string qq, string clientkey, string ptlang = "2052")
    {
        string loginUrl = $"https://ssl.ptlogin2.qq.com/jump?ptlang={ptlang}&clientuin={qq}&clientkey={clientkey}&u1=https%3A%2F%2Fqun.qq.com%2Fmanage.html%2F&keyindex=19";

        if (ptlang == "2052")
            loginUrl = $"https://ssl.ptlogin2.qq.com/jump?ptlang={ptlang}&clientuin={qq}&clientkey={clientkey}&u1=https:%2F%2Fqun.qq.com%2Fmanage.html%2F";

        Console.WriteLine($"登录接口 :{loginUrl}");
        string skey = "";
        CookieCollection cks = new CookieCollection();
        var handler = new HttpClientHandler
        {
            AllowAutoRedirect = true,
            CookieContainer = new CookieContainer(),
            UseCookies = true,
            AutomaticDecompression = DecompressionMethods.None
        };

        using (var client = new HttpClient(handler))
        {
            var response = await client.GetAsync(loginUrl);
            var finalUrl = response.RequestMessage.RequestUri.ToString();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"响应成功: {response.IsSuccessStatusCode}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"最终 URL: {finalUrl}");
            Console.ResetColor();

            cks = handler.CookieContainer.GetCookies(new Uri(finalUrl));

            Console.WriteLine($"\nCookies [{cks.Count}]:");
            foreach (Cookie cookie in cks)
            {
                Console.WriteLine($"[{cookie.Domain}] {cookie.Name} = {cookie.Value}");

                if (cookie.Name.Contains("skey"))
                    skey = cookie.Value;

            }
            Console.WriteLine($"QQ:[{qq}] | Skey:[{skey}] | GTK/BKN:[{GetGTK(skey)}]");
        }

        Console.WriteLine("开始获取群列表");
        using (var handler_ = new HttpClientHandler())
        {
            handler_.UseCookies = true;
            handler_.CookieContainer = new CookieContainer();
            string url = $"https://qun.qq.com/cgi-bin/qun_mgr/get_group_list?bkn={GetGTK(skey)}";
            Console.WriteLine($"请求URL：{url}");

            string referrerUrl = "https://qun.qq.com/";

            var uri = new Uri(url);
            foreach (Cookie cookie in cks)
            {
                Console.WriteLine($"添加Cookie:[{cookie.Name} = {cookie.Value}]");
                handler_.CookieContainer.Add(uri, cookie);
            }
            using (var client = new HttpClient(handler_))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                client.DefaultRequestHeaders.AcceptLanguage.ParseAdd("zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6");
                client.DefaultRequestHeaders.Add("sec-ch-ua", "\"Not)A;Brand\";v=\"99\", \"Microsoft Edge\";v=\"127\", \"Chromium\";v=\"127\"");
                client.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
                client.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");
                client.DefaultRequestHeaders.Add("sec-fetch-dest", "empty");
                client.DefaultRequestHeaders.Add("sec-fetch-mode", "cors");
                client.DefaultRequestHeaders.Add("sec-fetch-site", "same-origin");
                client.DefaultRequestHeaders.Referrer = new Uri(referrerUrl);

                var response = await client.GetAsync(url);

                var responseBody = await response.Content.ReadAsStringAsync();

                return (responseBody);
            }
        }
    }
    //获取好友列表
    public static async Task<string> GetFriendList(string qq, string clientkey, string ptlang = "2052")
    {
        string loginUrl = $"https://ssl.ptlogin2.qq.com/jump?ptlang={ptlang}&clientuin={qq}&clientkey={clientkey}&u1=https%3A%2F%2Fqun.qq.com%2Fmanage.html%2F&keyindex=19";

        if (ptlang == "2052")
            loginUrl = $"https://ssl.ptlogin2.qq.com/jump?ptlang={ptlang}&clientuin={qq}&clientkey={clientkey}&u1=https:%2F%2Fqun.qq.com%2Fmanage.html%2F";

        Console.WriteLine($"登录接口 :{loginUrl}");
        string skey = "";
        CookieCollection cks = new CookieCollection();
        var handler = new HttpClientHandler
        {
            AllowAutoRedirect = true,
            CookieContainer = new CookieContainer(),
            UseCookies = true,
            AutomaticDecompression = DecompressionMethods.None
        };

        using (var client = new HttpClient(handler))
        {
            var response = await client.GetAsync(loginUrl);
            var finalUrl = response.RequestMessage.RequestUri.ToString();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"响应成功: {response.IsSuccessStatusCode}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"最终 URL: {finalUrl}");
            Console.ResetColor();

            cks = handler.CookieContainer.GetCookies(new Uri(finalUrl));

            Console.WriteLine($"\nCookies [{cks.Count}]:");
            foreach (Cookie cookie in cks)
            {
                Console.WriteLine($"[{cookie.Domain}] {cookie.Name} = {cookie.Value}");

                if (cookie.Name.Contains("skey"))
                    skey = cookie.Value;

            }
            Console.WriteLine($"QQ:[{qq}] | Skey:[{skey}] | GTK/BKN:[{GetGTK(skey)}]");
        }

        Console.WriteLine("开始获取群列表");
        using (var handler_ = new HttpClientHandler())
        {
            handler_.UseCookies = true;
            handler_.CookieContainer = new CookieContainer();
            string url = $"https://qun.qq.com/cgi-bin/qun_mgr/get_friend_list?bkn={GetGTK(skey)}";
            Console.WriteLine($"请求URL：{url}");

            string referrerUrl = "https://qun.qq.com/";

            var uri = new Uri(url);
            foreach (Cookie cookie in cks)
            {
                Console.WriteLine($"添加Cookie:[{cookie.Name} = {cookie.Value}]");
                handler_.CookieContainer.Add(uri, cookie);
            }
            using (var client = new HttpClient(handler_))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                client.DefaultRequestHeaders.AcceptLanguage.ParseAdd("zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6");
                client.DefaultRequestHeaders.Add("sec-ch-ua", "\"Not)A;Brand\";v=\"99\", \"Microsoft Edge\";v=\"127\", \"Chromium\";v=\"127\"");
                client.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
                client.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");
                client.DefaultRequestHeaders.Add("sec-fetch-dest", "empty");
                client.DefaultRequestHeaders.Add("sec-fetch-mode", "cors");
                client.DefaultRequestHeaders.Add("sec-fetch-site", "same-origin");
                client.DefaultRequestHeaders.Referrer = new Uri(referrerUrl);

                var response = await client.GetAsync(url);

                var responseBody = await response.Content.ReadAsStringAsync();

                return (responseBody);
            }
        }
    }
    //发公告
    public static async Task<string> PostAnnounce(string qq, string clientkey, string groupID,string text, string ptlang = "2052")
    {
        string loginUrl = $"https://ssl.ptlogin2.qq.com/jump?ptlang={ptlang}&clientuin={qq}&clientkey={clientkey}&u1=https%3A%2F%2Fqun.qq.com%2Fmanage.html%2F&keyindex=19";

        if (ptlang == "2052")
            loginUrl = $"https://ssl.ptlogin2.qq.com/jump?ptlang={ptlang}&clientuin={qq}&clientkey={clientkey}&u1=https:%2F%2Fqun.qq.com%2Fmanage.html%2F";

        Console.WriteLine($"登录接口 :{loginUrl}");
        string skey = "";
        CookieCollection cks = new CookieCollection();
        var handler = new HttpClientHandler
        {
            AllowAutoRedirect = true,
            CookieContainer = new CookieContainer(),
            UseCookies = true,
            AutomaticDecompression = DecompressionMethods.None
        };

        using (var client = new HttpClient(handler))
        {
            var response = await client.GetAsync(loginUrl);
            var finalUrl = response.RequestMessage.RequestUri.ToString();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"响应成功: {response.IsSuccessStatusCode}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"最终 URL: {finalUrl}");
            Console.ResetColor();

            cks = handler.CookieContainer.GetCookies(new Uri(finalUrl));

            Console.WriteLine($"\nCookies [{cks.Count}]:");
            foreach (Cookie cookie in cks)
            {
                Console.WriteLine($"[{cookie.Domain}] {cookie.Name} = {cookie.Value}");

                if (cookie.Name.Contains("skey"))
                    skey = cookie.Value;

            }
            Console.WriteLine($"QQ:[{qq}] | Skey:[{skey}] | GTK/BKN:[{GetGTK(skey)}]");
        }

        Console.WriteLine("开始获取群成员列表");
        using (var handler_ = new HttpClientHandler())
        {
            handler_.UseCookies = true;
            handler_.CookieContainer = new CookieContainer();
            string url = $"https://web.qun.qq.com/cgi-bin/announce/add_qun_instruction?bkn={GetGTK(skey)}";
            Console.WriteLine($"请求URL：{url}");

            string referrerUrl = "https://qun.qq.com/";
            string requestBody = $"qid={groupID}" +
                $"&title=" +
                $"&text={text}" +
                $"&pinned=1" +
                $"&type=20" +
                $"&settings={{\"is_show_edit_card\":0,\"tip_window_type\":1,\"confirm_required\":1}}";

            // 设置 cookies
            var uri = new Uri(url);
            foreach (Cookie cookie in cks)
            {
                Console.WriteLine($"添加Cookie:[{cookie.Name} = {cookie.Value}]");
                handler_.CookieContainer.Add(uri, cookie);
            }
            using (var client = new HttpClient(handler_))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                client.DefaultRequestHeaders.AcceptLanguage.ParseAdd("zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6");
                client.DefaultRequestHeaders.Add("sec-ch-ua", "\"Not)A;Brand\";v=\"99\", \"Microsoft Edge\";v=\"127\", \"Chromium\";v=\"127\"");
                client.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
                client.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");
                client.DefaultRequestHeaders.Add("sec-fetch-dest", "empty");
                client.DefaultRequestHeaders.Add("sec-fetch-mode", "cors");
                client.DefaultRequestHeaders.Add("sec-fetch-site", "same-origin");
                client.DefaultRequestHeaders.Referrer = new Uri(referrerUrl);

                var content = new StringContent(requestBody, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");

                var response = await client.PostAsync(url, content);

                var responseBody = await response.Content.ReadAsStringAsync();

                return (responseBody);
            }
        }
    }
    //更改成员昵称
    public static async Task<string> SetGroupMemberNick(string qq, string clientkey, string groupID,string memberQQ, string Setnick, string ptlang = "2052")
    {
        string loginUrl = $"https://ssl.ptlogin2.qq.com/jump?ptlang={ptlang}&clientuin={qq}&clientkey={clientkey}&u1=https%3A%2F%2Fqun.qq.com%2Fmanage.html%2F&keyindex=19";

        if (ptlang == "2052")
            loginUrl = $"https://ssl.ptlogin2.qq.com/jump?ptlang={ptlang}&clientuin={qq}&clientkey={clientkey}&u1=https:%2F%2Fqun.qq.com%2Fmanage.html%2F";

        Console.WriteLine($"登录接口 :{loginUrl}");
        string skey = "";
        CookieCollection cks = new CookieCollection();
        var handler = new HttpClientHandler
        {
            AllowAutoRedirect = true,
            CookieContainer = new CookieContainer(),
            UseCookies = true,
            AutomaticDecompression = DecompressionMethods.None
        };

        using (var client = new HttpClient(handler))
        {
            var response = await client.GetAsync(loginUrl);
            var finalUrl = response.RequestMessage.RequestUri.ToString();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"响应成功: {response.IsSuccessStatusCode}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"最终 URL: {finalUrl}");
            Console.ResetColor();

            cks = handler.CookieContainer.GetCookies(new Uri(finalUrl));

            Console.WriteLine($"\nCookies [{cks.Count}]:");
            foreach (Cookie cookie in cks)
            {
                Console.WriteLine($"[{cookie.Domain}] {cookie.Name} = {cookie.Value}");

                if (cookie.Name.Contains("skey"))
                    skey = cookie.Value;

            }
            Console.WriteLine($"QQ:[{qq}] | Skey:[{skey}] | GTK/BKN:[{GetGTK(skey)}]");
        }

        Console.WriteLine("开始设置群成员昵称");
        using (var handler_ = new HttpClientHandler())
        {
            handler_.UseCookies = true;
            handler_.CookieContainer = new CookieContainer();
            string url = $"https://qun.qq.com/cgi-bin/qun_mgr/set_group_card?bkn={GetGTK(skey)}";
            Console.WriteLine($"请求URL：{url}");

            string referrerUrl = "https://qun.qq.com/";
            string requestBody = $"gc={groupID}" +
                $"&u={memberQQ}" +
                $"&name={Setnick}" +
                $"&bkn={GetGTK(skey)}";

            // 设置 cookies
            var uri = new Uri(url);
            foreach (Cookie cookie in cks)
            {
                Console.WriteLine($"添加Cookie:[{cookie.Name} = {cookie.Value}]");
                handler_.CookieContainer.Add(uri, cookie);
            }
            using (var client = new HttpClient(handler_))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                client.DefaultRequestHeaders.AcceptLanguage.ParseAdd("zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6");
                client.DefaultRequestHeaders.Add("sec-ch-ua", "\"Not)A;Brand\";v=\"99\", \"Microsoft Edge\";v=\"127\", \"Chromium\";v=\"127\"");
                client.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
                client.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");
                client.DefaultRequestHeaders.Add("sec-fetch-dest", "empty");
                client.DefaultRequestHeaders.Add("sec-fetch-mode", "cors");
                client.DefaultRequestHeaders.Add("sec-fetch-site", "same-origin");
                client.DefaultRequestHeaders.Referrer = new Uri(referrerUrl);

                var content = new StringContent(requestBody, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");

                var response = await client.PostAsync(url, content);

                var responseBody = await response.Content.ReadAsStringAsync();

                return (responseBody);
            }
        }
    }
    //删除成员
    public static async Task<string> DelGroupMember(string qq, string clientkey, string groupID, string memberQQ, string ptlang = "2052")
    {
        string loginUrl = $"https://ssl.ptlogin2.qq.com/jump?ptlang={ptlang}&clientuin={qq}&clientkey={clientkey}&u1=https%3A%2F%2Fqun.qq.com%2Fmanage.html%2F&keyindex=19";

        if (ptlang == "2052")
            loginUrl = $"https://ssl.ptlogin2.qq.com/jump?ptlang={ptlang}&clientuin={qq}&clientkey={clientkey}&u1=https:%2F%2Fqun.qq.com%2Fmanage.html%2F";

        Console.WriteLine($"登录接口 :{loginUrl}");
        string skey = "";
        CookieCollection cks = new CookieCollection();
        var handler = new HttpClientHandler
        {
            AllowAutoRedirect = true,
            CookieContainer = new CookieContainer(),
            UseCookies = true,
            AutomaticDecompression = DecompressionMethods.None
        };

        using (var client = new HttpClient(handler))
        {
            var response = await client.GetAsync(loginUrl);
            var finalUrl = response.RequestMessage.RequestUri.ToString();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"响应成功: {response.IsSuccessStatusCode}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"最终 URL: {finalUrl}");
            Console.ResetColor();

            cks = handler.CookieContainer.GetCookies(new Uri(finalUrl));

            Console.WriteLine($"\nCookies [{cks.Count}]:");
            foreach (Cookie cookie in cks)
            {
                Console.WriteLine($"[{cookie.Domain}] {cookie.Name} = {cookie.Value}");

                if (cookie.Name.Contains("skey"))
                    skey = cookie.Value;

            }
            Console.WriteLine($"QQ:[{qq}] | Skey:[{skey}] | GTK/BKN:[{GetGTK(skey)}]");
        }

        Console.WriteLine("开始删除群成员");
        using (var handler_ = new HttpClientHandler())
        {
            handler_.UseCookies = true;
            handler_.CookieContainer = new CookieContainer();
            string url = $"https://qun.qq.com/cgi-bin/qun_mgr/delete_group_member?bkn={GetGTK(skey)}";
            Console.WriteLine($"请求URL：{url}");

            string referrerUrl = "https://qun.qq.com/";
            string requestBody = $"gc={groupID}" +
                $"&ul={memberQQ}" +
                $"&flag=0" +
                $"&bkn={GetGTK(skey)}";

            // 设置 cookies
            var uri = new Uri(url);
            foreach (Cookie cookie in cks)
            {
                Console.WriteLine($"添加Cookie:[{cookie.Name} = {cookie.Value}]");
                handler_.CookieContainer.Add(uri, cookie);
            }
            using (var client = new HttpClient(handler_))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                client.DefaultRequestHeaders.AcceptLanguage.ParseAdd("zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6");
                client.DefaultRequestHeaders.Add("sec-ch-ua", "\"Not)A;Brand\";v=\"99\", \"Microsoft Edge\";v=\"127\", \"Chromium\";v=\"127\"");
                client.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
                client.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");
                client.DefaultRequestHeaders.Add("sec-fetch-dest", "empty");
                client.DefaultRequestHeaders.Add("sec-fetch-mode", "cors");
                client.DefaultRequestHeaders.Add("sec-fetch-site", "same-origin");
                client.DefaultRequestHeaders.Referrer = new Uri(referrerUrl);

                var content = new StringContent(requestBody, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");

                var response = await client.PostAsync(url, content);

                var responseBody = await response.Content.ReadAsStringAsync();

                return (responseBody);
            }
        }
    }
    //添加管理员
    public static async Task<string> AddGroupAdmin(string qq, string clientkey, string groupID, string memberQQ, string ptlang = "2052")
    {
        string loginUrl = $"https://ssl.ptlogin2.qq.com/jump?ptlang={ptlang}&clientuin={qq}&clientkey={clientkey}&u1=https%3A%2F%2Fqun.qq.com%2Fmanage.html%2F&keyindex=19";

        if (ptlang == "2052")
            loginUrl = $"https://ssl.ptlogin2.qq.com/jump?ptlang={ptlang}&clientuin={qq}&clientkey={clientkey}&u1=https:%2F%2Fqun.qq.com%2Fmanage.html%2F";

        Console.WriteLine($"登录接口 :{loginUrl}");
        string skey = "";
        CookieCollection cks = new CookieCollection();
        var handler = new HttpClientHandler
        {
            AllowAutoRedirect = true,
            CookieContainer = new CookieContainer(),
            UseCookies = true,
            AutomaticDecompression = DecompressionMethods.None
        };

        using (var client = new HttpClient(handler))
        {
            var response = await client.GetAsync(loginUrl);
            var finalUrl = response.RequestMessage.RequestUri.ToString();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"响应成功: {response.IsSuccessStatusCode}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"最终 URL: {finalUrl}");
            Console.ResetColor();

            cks = handler.CookieContainer.GetCookies(new Uri(finalUrl));

            Console.WriteLine($"\nCookies [{cks.Count}]:");
            foreach (Cookie cookie in cks)
            {
                Console.WriteLine($"[{cookie.Domain}] {cookie.Name} = {cookie.Value}");

                if (cookie.Name.Contains("skey"))
                    skey = cookie.Value;

            }
            Console.WriteLine($"QQ:[{qq}] | Skey:[{skey}] | GTK/BKN:[{GetGTK(skey)}]");
        }

        Console.WriteLine("开始添加管理员");
        using (var handler_ = new HttpClientHandler())
        {
            handler_.UseCookies = true;
            handler_.CookieContainer = new CookieContainer();
            string url = $"\r\nhttps://qun.qq.com/cgi-bin/qun_mgr/set_group_admin?bkn={GetGTK(skey)}";
            Console.WriteLine($"请求URL：{url}");

            string referrerUrl = "https://qun.qq.com/";
            string requestBody = $"gc={groupID}" +
                $"&ul={memberQQ}" +
                $"&op=1" +
                $"&bkn={GetGTK(skey)}";

            // 设置 cookies
            Console.WriteLine($"Content:{requestBody}");
            var uri = new Uri(url);
            foreach (Cookie cookie in cks)
            {
                Console.WriteLine($"添加Cookie:[{cookie.Name} = {cookie.Value}]");
                handler_.CookieContainer.Add(uri, cookie);
            }
            using (var client = new HttpClient(handler_))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                client.DefaultRequestHeaders.AcceptLanguage.ParseAdd("zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6");
                client.DefaultRequestHeaders.Add("sec-ch-ua", "\"Not)A;Brand\";v=\"99\", \"Microsoft Edge\";v=\"127\", \"Chromium\";v=\"127\"");
                client.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
                client.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");
                client.DefaultRequestHeaders.Add("sec-fetch-dest", "empty");
                client.DefaultRequestHeaders.Add("sec-fetch-mode", "cors");
                client.DefaultRequestHeaders.Add("sec-fetch-site", "same-origin");
                client.DefaultRequestHeaders.Referrer = new Uri(referrerUrl);

                var content = new StringContent(requestBody, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");

                var response = await client.PostAsync(url, content);

                var responseBody = await response.Content.ReadAsStringAsync();

                return (responseBody);
            }
        }
    }
    //获取群验证消息 
    public static async Task<string> GetGroupAuthMessage(string qq, string clientkey, string ptlang = "2052")
    {
        string loginUrl = $"https://ssl.ptlogin2.qq.com/jump?ptlang={ptlang}&clientuin={qq}&clientkey={clientkey}&u1=https%3A%2F%2Fqun.qq.com%2Fmanage.html%2F&keyindex=19";

        if (ptlang == "2052")
            loginUrl = $"https://ssl.ptlogin2.qq.com/jump?ptlang={ptlang}&clientuin={qq}&clientkey={clientkey}&u1=https:%2F%2Fqun.qq.com%2Fmanage.html%2F";

        Console.WriteLine($"登录接口 :{loginUrl}");
        string skey = "";
        CookieCollection cks = new CookieCollection();
        var handler = new HttpClientHandler
        {
            AllowAutoRedirect = true,
            CookieContainer = new CookieContainer(),
            UseCookies = true,
            AutomaticDecompression = DecompressionMethods.None
        };

        using (var client = new HttpClient(handler))
        {
            var response = await client.GetAsync(loginUrl);
            var finalUrl = response.RequestMessage.RequestUri.ToString();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"响应成功: {response.IsSuccessStatusCode}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"最终 URL: {finalUrl}");
            Console.ResetColor();

            cks = handler.CookieContainer.GetCookies(new Uri(finalUrl));

            Console.WriteLine($"\nCookies [{cks.Count}]:");
            foreach (Cookie cookie in cks)
            {
                Console.WriteLine($"[{cookie.Domain}] {cookie.Name} = {cookie.Value}");

                if (cookie.Name.Contains("skey"))
                    skey = cookie.Value;

            }
            Console.WriteLine($"QQ:[{qq}] | Skey:[{skey}] | GTK/BKN:[{GetGTK(skey)}]");
        }

        Console.WriteLine("开始获取验证消息");
        using (var handler_ = new HttpClientHandler())
        {
            handler_.UseCookies = true;
            handler_.CookieContainer = new CookieContainer();
            string url = $"https://async.qun.qq.com/cgi-bin/sys_msg/getmsg?show=2&bkn={GetGTK(skey)}";
            Console.WriteLine($"请求URL：{url}");

            string referrerUrl = "https://qun.qq.com/";

            var uri = new Uri(url);
            foreach (Cookie cookie in cks)
            {
                Console.WriteLine($"添加Cookie:[{cookie.Name} = {cookie.Value}]");
                handler_.CookieContainer.Add(uri, cookie);
            }
            using (var client = new HttpClient(handler_))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                client.DefaultRequestHeaders.AcceptLanguage.ParseAdd("zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6");
                client.DefaultRequestHeaders.Add("sec-ch-ua", "\"Not)A;Brand\";v=\"99\", \"Microsoft Edge\";v=\"127\", \"Chromium\";v=\"127\"");
                client.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
                client.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");
                client.DefaultRequestHeaders.Add("sec-fetch-dest", "empty");
                client.DefaultRequestHeaders.Add("sec-fetch-mode", "cors");
                client.DefaultRequestHeaders.Add("sec-fetch-site", "same-origin");
                client.DefaultRequestHeaders.Referrer = new Uri(referrerUrl);

                var response = await client.GetAsync(url);

                var responseBody = await response.Content.ReadAsStringAsync();

                return (responseBody);
            }
        }
    }

    public static async Task<string> ReadResponseContentAsync(HttpResponseMessage response)
    {
        // 处理压缩内容
        var responseStream = await response.Content.ReadAsStreamAsync();
        using (var decompressedStream = new MemoryStream())
        {
            await responseStream.CopyToAsync(decompressedStream);
            decompressedStream.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(decompressedStream, Encoding.UTF8))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
    public static int GetGTK(string skey)
    {
        int hash = 5381;
        foreach (char c in skey)
        {
            hash += (hash << 5) + c;
        }
        return hash & 0x7fffffff;
    }
}

