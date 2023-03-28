namespace Middleware.Common
{
    public  static class WebUtils
    {
        
        public static readonly string PENDING_ROLE = "pending";
        public static readonly string USER_APPROVED_ROLE = "approved";
        public static readonly string ADMIN_ROLE = "admin";
        public static readonly string SUPER_ADMIN_ROLE = "super admin";
        public static readonly string TASKER_ROLE = "tasker";
        public static readonly string POSTER_ROLE = "poster";



        public static readonly string POST_APPROVED_STATUS = "approved";
        public static readonly string POST_REJECT_STATUS = "reject";
        public static readonly string POST_UNASSIGNED_STATUS = "unassigned";
        public static readonly string POST_ASSIGNED_STATUS = "assigned";
        public static readonly string POIST_EXPIRED_STATUS = "expired";

        public static bool PassAndConfirmPassMathing(string pass, string confirmPass)
        {
            return (pass == confirmPass) ? true: false;
        }

    }
}
