logger = function (container) {
    
    var _logElement = document.querySelector(container);

    this.log = log;

    function log(text) {
        if (_logElement != null) {

            _logElement.innerHTML = text + "<br/>" + _logElement.innerHTML;
        }
    }

} 