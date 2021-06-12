using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Backend.Common
{
    public static class Utilities
    {
        public const string USER_ID = "user_id";
        public const string FIRST_NAME = "first_name";
        public const string LAST_NAME = "last_name";
        public const string USER_NAME = "user_name";
        public const string USER_ROLE = "user_role";
        public const string USER_EMAIL = "user_email";
        public readonly static List<string> LIST_CLAIMS = new List<string>() { USER_ID, FIRST_NAME, LAST_NAME, USER_NAME };

        public class SystemConstans
        {
            public const string USER_CONTENT_FOLDER_NAME = "user-content";
        }

        public class ProductConstants
        {
            public const string NA = "N/A";
        }
    }
}
