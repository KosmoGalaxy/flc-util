const component = FullLegitCode.Util.Util;

module.exports = {

  decodeImage: function(successCallback, errorCallback, bytes) {
    component.decodeImage(bytes)
    .then(bytes => successCallback(bytes))
    .catch(e => errorCallback(e));
  },

  getIp: function(successCallback, errorCallback) {
    component.getIp()
    .then(ip => {
      if (ip) {
        successCallback(ip);
      } else {
        errorCallback();
      }
    })
    .catch(e => errorCallback(e));
  }

};

require('cordova/exec/proxy').add('FlcUtil', module.exports);
