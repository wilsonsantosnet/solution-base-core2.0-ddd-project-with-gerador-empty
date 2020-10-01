using System;
using System.Collections.Generic;
using System.Text;

namespace Seed.CrossCuting.Auth
{
    public static class SecurityConfig
    {
        public static string GetSalt()
        {
            return "Seed321$";
        }

    }
}
