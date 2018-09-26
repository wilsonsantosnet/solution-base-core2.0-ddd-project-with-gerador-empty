using System;
using System.Collections.Generic;
using System.Text;

namespace Score.Platform.Account.CrossCuting.Auth
{
    public static class SecurityConfig
    {
        public static string GetSalt()
        {
            return "ScorePlatform321$";
        }

    }
}
