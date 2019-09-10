using FullLegitCode.Util;
using UnityEngine;

public class Main : MonoBehaviour
{
  private void Start()
  {
    Debug.Log($"(batteryLevel)={Util.GetBatteryLevel()}");
  }
}