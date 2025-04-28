using System;

namespace UserManagement.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class RequiresPermissionAttribute : Attribute
    {
        public string Permission { get; }

        public RequiresPermissionAttribute(string permission)
        {
            Permission = permission;
        }
    }
}
