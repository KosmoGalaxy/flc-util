using UnityEngine;

namespace FullLegitCode.Util
{
    public class PermissionResult
    {
        public string[] granted;
        public string[] denied;

        public PermissionResult(string[] granted, string[] denied)
        {
            this.granted = granted;
            this.denied = denied;
        }

        public PermissionResult(AndroidJavaObject javaObject)
        {
            granted = javaObject.Get<string[]>("granted");
            denied = javaObject.Get<string[]>("denied");
        }
    }
}
