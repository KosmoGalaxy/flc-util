package pl.fulllegitcode.utilunity;

import android.app.Fragment;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;

public class PermissionFragment extends Fragment {

  interface Callback {
    void onCreate();
    void onResult(String[] permissions, int[] grantResults);
  }


  public int requestCode;
  public Callback callback;

  @Override
  public void onCreate(@Nullable Bundle savedInstanceState) {
    super.onCreate(savedInstanceState);
    if (callback != null) {
      callback.onCreate();
    } else {
      getFragmentManager().beginTransaction().remove(this).commit();
    }
  }

  @Override
  public void onRequestPermissionsResult(int requestCode, @NonNull String[] permissions, @NonNull int[] grantResults) {
    if (callback != null && requestCode == this.requestCode) {
      callback.onResult(permissions, grantResults);
      getFragmentManager().beginTransaction().remove(this).commit();
    }
  }

}
