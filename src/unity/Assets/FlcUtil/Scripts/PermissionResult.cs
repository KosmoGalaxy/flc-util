using UnityEngine;

namespace FullLegitCode.Util
{
    public class PermissionResult
    {
        public string[] granted;
        public string[] denied;

        public PermissionResult(AndroidJavaObject javaObject)
        {
            granted = javaObject.Get<string[]>("granted");
            denied = javaObject.Get<string[]>("denied");
        }
    }
}
