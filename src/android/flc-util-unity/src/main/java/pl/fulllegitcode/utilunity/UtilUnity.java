package pl.fulllegitcode.utilunity;

import android.app.FragmentManager;
import android.app.FragmentTransaction;

import com.unity3d.player.UnityPlayer;

import pl.fulllegitcode.util.PermissionResult;
import pl.fulllegitcode.util.RequestPermissionsCallback;
import pl.fulllegitcode.util.RequestPermissionsDelegate;
import pl.fulllegitcode.util.Util;

public class UtilUnity {

  public static PermissionResult checkPermissions(String[] permissions) {
    return Util.checkPermissions(UnityPlayer.currentActivity, permissions);
  }

  public static void requestPermissions(String[] permissions, RequestPermissionsCallback callback) {
    final RequestPermissionsDelegate delegate = Util.requestPermissions(permissions, callback);
    final PermissionFragment fragment = new PermissionFragment();
    fragment.requestCode = delegate.requestCode();
    fragment.callback = new PermissionFragment.Callback() {
      @Override
      public void onCreate() {
        delegate.run(fragment);
      }

      @Override
      public void onResult(String[] permissions, int[] grantResults) {
        delegate.onRequestPermissionsResult(permissions, grantResults);
      }
    };
    FragmentManager fragmentManager = UnityPlayer.currentActivity.getFragmentManager();
    FragmentTransaction transaction = fragmentManager.beginTransaction();
    transaction.add(fragment, "permission" + Math.random());
    transaction.commit();
  }

  public static float getTemperature() {
    return Util.getTemperature(UnityPlayer.currentActivity);
  }

}
