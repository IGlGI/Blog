namespace BlogApp.Common.Constants
{
    public static class IdentityServerConstants
    {
        public const string ISCookieName = "IdentityServer.Cookies";

        public const string LocalApiScopeName = "IdentityServerApi";

        public const string LoginRelativePath = "/Authorization/Login";

        public const string LogoutRelativePath = "/Authorization/Logout";

        public const string TokenUrlRelativePath = "/connect/token";

        public const string ResourcesRelativePath = "/resources";

        public const string SignInOidcRelativePath = "/signin-oidc";

        public const string SignOutOidcRelativePath = "/signout-callback-oidc";

        public const string ScopeSeparator = " ";

        public const string OpenId = "openid";

        public const string Profile = "profile";

        public const string Email = "email";

        public const string Address = "address";

        public const string Phone = "phone";

        public const string Code = "code";

        public const string AccessToken = "access_token";

        public const string RefreshToken = "refresh_token";

        public const string OfflineAccess = "offline_access";

        public static class Clients
        {
            public const string AppApiName = "Blog_App_Api";

            public const string AppApiId = "BlogAppApi";

            public const string AppApiSecret = "QmxvZ0FwcEFwaUJsb2dBcHBBcGk=";

            public const string AppClientName = "Blog_App_Client";

            public const string AppClientId = "BlogAppClient";

            public const string AppClientRedirectUri = "/admin/callback";
        }
    }
}