using FullLegitCode.Util;
using UnityEngine;

public class Main : MonoBehaviour
{
    void Start()
    {
        string[] permissions = new string[]
        {
            "android.permission.WRITE_EXTERNAL_STORAGE",
            "android.permission.READ_CONTACTS"
        };
        PermissionResult result = Util.CheckPermissions(permissions);
        if (result != null)
        {
            _LogPermissionResult("check permissions", result);
        }
        Util.RequestPermissions(permissions, result2 =>
        {
            if (result2 != null)
            {
                _LogPermissionResult("request permissions", result2);
            }
        });
    }

    void _LogPermissionResult(string title, PermissionResult result)
    {
        Debug.Log(string.Format("{0}: granted={1} denied={2}", title, string.Join(",", result.granted), string.Join(",", result.denied)));
    }
}
