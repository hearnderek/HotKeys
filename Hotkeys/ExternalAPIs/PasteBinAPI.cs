using System;

namespace Hotkeys
{
    using System;
    using System.Net.Http;
    using System.Xml;

    public class PasteBinAPI
    {
        private string developerKey;
        private string username;
        private string userPassword;
        private string userKey;
        private string pasteKey;
        private string pasteTitle = "hk";
        public string pasteText;

        public PasteBinAPI(string pasteBinDeveloperAPIKey, string pasteBinAPIUsername, string pasteBinAPIUserPassword)
        {
            this.developerKey = pasteBinDeveloperAPIKey;
            this.username = pasteBinAPIUsername;
            this.userPassword = pasteBinAPIUserPassword;
            this.userKey = null;
        }

        public void Login()
        {
            // Login Post

            // Get pasteBinAPIUserKey from response

            using(var client = new HttpClient())
            {
                var payload = string.Format(
                    @"api_dev_key={0}&api_user_name={1}&api_user_password={2}",
                    System.Net.WebUtility.UrlEncode(developerKey),
                    System.Net.WebUtility.UrlEncode(username),
                    System.Net.WebUtility.UrlEncode(userPassword));

                var data = new StringContent(payload, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");

                try
                {

                    HttpResponseMessage response = client.PostAsync("https://pastebin.com/api/api_login.php", data).Result;
                    // This should just be our userKey
                    var contents = response.Content;

                    userKey = contents.ReadAsStringAsync().Result;

                }
                catch (Exception e)
                {

                    throw;
                }
            }

            UpdatePasteKeyByTitle();
            pasteText = ReadPrivate();

        }

        public void PushPrivate(string text)
        {
            // requires you to be logged in
            // planning to only use a single location

            throw new NotImplementedException();
        }

        private void UpdatePasteKeyByTitle()
        {
            using (var client = new HttpClient())
            {
                var payload = string.Format(
                    @"api_option=list&api_dev_key={0}&api_user_key={1}&api_paste_key=2bEsMpGc",
                    System.Net.WebUtility.UrlEncode(developerKey),
                    System.Net.WebUtility.UrlEncode(userKey));

                var data = new StringContent(payload, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");

                try
                {

                    HttpResponseMessage response = client.PostAsync("https://pastebin.com/api/api_post.php", data).Result;
                    // This should just be our userKey
                    var contents = response.Content;

                    string xml = contents.ReadAsStringAsync().Result;

                    var doc = new XmlDocument();
                    doc.LoadXml(xml);

                    foreach(XmlNode node in doc.SelectNodes("/paste"))
                    {
                        var finding = node.SelectSingleNode("paste_title")?.InnerText;
                        if(finding == pasteTitle)
                        {
                            pasteKey = node.SelectSingleNode("paste_key")?.InnerText;
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public string ReadPrivate()
        {
            // requires you to be logged in
            // planning to only use a single location

            // https://pastebin.com/api/api_raw.php
            using (var client = new HttpClient())
            {
                var payload = string.Format(
                    @"api_option=show_paste&api_dev_key={0}&api_user_key={1}&api_paste_key={2}",
                    System.Net.WebUtility.UrlEncode(developerKey),
                    System.Net.WebUtility.UrlEncode(userKey),
                    System.Net.WebUtility.UrlEncode(pasteKey));

                var data = new StringContent(payload, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");

                try
                {

                    HttpResponseMessage response = client.PostAsync("https://pastebin.com/api/api_raw.php", data).Result;
                    // This should just be our userKey
                    var contents = response.Content;

                    return contents.ReadAsStringAsync().Result;

                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public string DeletePaste()
        {
            using (var client = new HttpClient())
            {
                var payload = string.Format(
                    @"api_option=paste&api_dev_key={0}&api_user_key={1}&api_paste_private=2api_paste_name={2}&api_paste_expire_date=N&paste_format=None&api_paste_code{}",
                    System.Net.WebUtility.UrlEncode(developerKey),
                    System.Net.WebUtility.UrlEncode(userKey),
                    System.Net.WebUtility.UrlEncode(pasteTitle));

                var data = new StringContent(payload, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");

                try
                {

                    HttpResponseMessage response = client.PostAsync("https://pastebin.com/api/api_raw.php", data).Result;
                    // This should just be our userKey
                    var contents = response.Content;

                    return contents.ReadAsStringAsync().Result;

                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        //public string WritePaste()
        //{
        //    // TODO
        //}
    }


}