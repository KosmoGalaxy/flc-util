const copyDirectory = require('./copy-directory');

copyDirectory('./src/unity/Assets/FlcUtil', './dist/unity/FlcUtil', [
  'FlcUtil.aar',
  'FlcUtilUnity.aar',
  'PermissionResult.cs',
  'Util.cs'
]);
