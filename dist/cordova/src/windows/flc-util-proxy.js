const component = FullLegitCode.Util.Util;

module.exports = {

  decodeImage: function(successCallback, errorCallback, bytes) {
    component.decodeImage(bytes).then(successCallback, errorCallback);
  },

  getIp: function(successCallback, errorCallback) {
    component.getIp().then(successCallback, errorCallback);
  }

};

require('cordova/exec/proxy').add('FlcUtil', module.exports);
