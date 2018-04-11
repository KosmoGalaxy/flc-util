using System;
using UnityEngine;

namespace FullLegitCode.Util
{
    public class Util
    {
        class RequestPermissionsCallbackProxy : AndroidJavaProxy
        {
            private RequestPermissionsCallback _callback;

            public RequestPermissionsCallbackProxy(RequestPermissionsCallback callback) : base("pl.fulllegitcode.util.RequestPermissionsCallback")
            {
                _callback = callback;
            }

            public void onResult(AndroidJavaObject result)
            {
                _callback(new PermissionResult(result));
            }
        }


        public delegate void RequestPermissionsCallback(PermissionResult result);


        static bool _isInited;

#if UNITY_ANDROID && !UNITY_EDITOR
        static AndroidJavaClass _utilClass;
#endif

        public static PermissionResult CheckPermissions(string[] permissions)
        {
            try
            {
                _Init();
#if UNITY_ANDROID && !UNITY_EDITOR
                AndroidJavaObject result = _utilClass.CallStatic<AndroidJavaObject>("checkPermissions", new object[1] { permissions });
                return new PermissionResult(result);
#endif
            }
            catch (Exception e)
            {
                Debug.LogError("[FlcUtil.CheckPermissions] error: " + e.Message);
                throw e;
            }
            return new PermissionResult(permissions, new string[0]);
        }

        public static void RequestPermissions(string[] permissions, RequestPermissionsCallback callback)
        {
            try
            {
                _Init();
#if UNITY_ANDROID && !UNITY_EDITOR
                RequestPermissionsCallbackProxy callbackProxy = new RequestPermissionsCallbackProxy(callback);
                _utilClass.CallStatic("requestPermissions", new object[2] { permissions, callbackProxy });
#endif
            }
            catch (Exception e)
            {
                Debug.LogError("[FlcUtil.RequestPermissions] error: " + e.Message);
                throw e;
            }
        }

        public static float GetTemperature()
        {
            try
            {
                _Init();
#if UNITY_ANDROID && !UNITY_EDITOR
                return _utilClass.CallStatic<float>("getTemperature");
#endif
            }
            catch (Exception e)
            {
                Debug.LogError("[FlcUtil.GetTemperature] error: " + e.Message);
                throw e;
            }
            return 0f;
        }

        static void _Init()
        {
            if (!_isInited)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                _utilClass = new AndroidJavaClass("pl.fulllegitcode.utilunity.UtilUnity");
#endif
                _isInited = true;
            }
        }
    }
}
