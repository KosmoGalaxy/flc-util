using System;
using UnityEngine;

namespace FullLegitCode.Util
{
  public static class Util
  {
    private class RequestPermissionsCallbackProxy : AndroidJavaProxy
    {
      private readonly RequestPermissionsCallback _callback;


      public RequestPermissionsCallbackProxy(RequestPermissionsCallback callback) : base(
        "pl.fulllegitcode.util.RequestPermissionsCallback")
      {
        _callback = callback;
      }


      // ReSharper disable once InconsistentNaming
      public void onResult(AndroidJavaObject result)
      {
        _callback(new PermissionResult(result));
      }
    }


    public delegate void RequestPermissionsCallback(PermissionResult result);


    private static bool _isInited;

#if UNITY_ANDROID && !UNITY_EDITOR
    private static AndroidJavaClass _utilClass;
#endif


    public static PermissionResult CheckPermissions(string[] permissions)
    {
      try
      {
        _Init();

#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaObject result =
          _utilClass.CallStatic<AndroidJavaObject>("checkPermissions", new object[1] { permissions });
        return new PermissionResult(result);
#endif
      }
      catch (Exception e)
      {
        Debug.LogError("[FlcUtil.CheckPermissions] error: " + e.Message);
        throw;
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
        throw;
      }
    }
    

    public static float GetBatteryLevel()
    {
      try
      {
        _Init();
        
#if UNITY_ANDROID && !UNITY_EDITOR
        return _utilClass.CallStatic<float>("getBatteryLevel");
#endif
      }
      catch (Exception e)
      {
        Debug.LogError("[FlcUtil.GetBatteryLevel] error: " + e.Message);
        throw;
      }

      return 1f;
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
        throw;
      }

      return 0f;
    }


    private static void _Init()
    {
      if (_isInited)
        return;
      
#if UNITY_ANDROID && !UNITY_EDITOR
      _utilClass = new AndroidJavaClass("pl.fulllegitcode.utilunity.UtilUnity");
#endif
      
      _isInited = true;
    }
  }
}