using Microsoft.AspNetCore.Authorization;

namespace VShow_BackEnd.Configs
{
    /// <summary>
    /// สำหรับกำหนดสิทธิ์ให้กับ User
    /// </summary>
    public class PolicyConfig
    {
        public const string Admin = "Admin";
        public const string Employee = "Employee";
        public const string User = "User";

        /// <summary>
        /// สำหรับ เพิ่ม Role ให้กับ User
        /// </summary>
        public static void RegisterPolicies(AuthorizationOptions authorization)
        {
            authorization.AddPolicy(Admin, AddPolicy(Admin));
            authorization.AddPolicy(Employee, AddPolicy(Admin, Employee));
        }

        /// <summary>
        /// สำหรับเพิ่มสิทธิ์ให้กับ User ในระบบ
        /// </summary>
        /// <param name="roles">สิทธิ์ของผู้ใช้งานใส่ได้เรื่อยๆ</param>
        /// <returns></returns>
        protected static AuthorizationPolicy AddPolicy(params string[] roles)
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(roles).Build();
        }
    }
}

