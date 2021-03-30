using System;

namespace Hotkeys
{
    public class PasteBinAPI
    {
        private string pasteBinDeveloperAPIKey;
        private string pasteBinAPIUsername;
        private string pasteBinAPIUserPassword;
        private string pasteBinAPIUserKey;

        public PasteBinAPI(string pasteBinDeveloperAPIKey, string pasteBinAPIUsername, string pasteBinAPIUserPassword)
        {
            this.pasteBinDeveloperAPIKey = pasteBinDeveloperAPIKey;
            this.pasteBinAPIUsername = pasteBinAPIUsername;
            this.pasteBinAPIUserPassword = pasteBinAPIUserPassword;
            this.pasteBinAPIUserKey = null;
        }

        public void Login()
        {
            // Login Post

            // Get pasteBinAPIUserKey from response

            throw new NotImplementedException();

        }

        public void PushPrivate(string text)
        {
            // requires you to be logged in
            // planning to only use a single location

            throw new NotImplementedException();
        }

        public string ReadPrivate()
        {
            // requires you to be logged in
            // planning to only use a single location

            throw new NotImplementedException();
        }
    }


}