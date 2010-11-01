dndHandler = function (draggableItems, dropTarget, dbg) {

    var _draggableItems = draggableItems;
    var _dropTarget = dropTarget;
    var _dndType = "text"; //doesn't works in IE 9 preview
    var _debugger = dbg;
    var _target;
    var _cartFullImage = "/images/cartfull.png";
    var _onItemDrop = null;
    var _objLogger = null;
    var _onCartMove = null;

    this.init = init;
    this.log = log;
    this.setItemDropHandler = setItemDropHandler;
    this.attachLogger = attachLogger;
    this.setDndDone = setDndDone;
    this.setOnCartMove = setOnCartMove;


    function attachLogger(logger) {
        _objLogger = logger;
    }

    function setItemDropHandler(func) {
        _onItemDrop = func;
    }

    function log(data) {
        if (_objLogger != null) {

            _objLogger.log(data);
        }
    }

    function init() {

        var items = document.querySelectorAll(_draggableItems);
        log("found " + items.length.toString() + " to set as draggable");
        for (var iLoop = 0; iLoop < items.length; iLoop++) {
            var item = items[iLoop];

            item.setAttribute('draggable', 'true');

            addEvent(item, 'dragstart', function (e) {
                e.dataTransfer.effectAllowed = 'copy';
                var dragVal;
                var element;
                if (e.target) {
                    element = e.target;
                }
                else {
                    element = e.srcElement;
                }
                if (element.dataset) {
                    dragVal = element.value;
                }
                else {
                    dragVal = element.getAttribute("data-value");
                }
                log("drag start for " + dragVal);
                e.dataTransfer.clearData();
                e.dataTransfer.setData(_dndType, dragVal);
                _target.className = "notify";
            });
        }

        //set drop target
        _target = document.querySelector(_dropTarget);

        addEvent(_target, 'dragover', function (e) {
            if (e.preventDefault) e.preventDefault();
            this.className = 'over';
            e.dataTransfer.dropEffect = 'copy';
            return false;
        });

        addEvent(_target, 'dragenter', function (e) {
            if (e.preventDefault) e.preventDefault();
            this.className = 'over';
            return false;
        });


        addEvent(_target, 'dragleave', function (e) {
            this.className = '';
        });

        addEvent(_target, 'drop', function (e) {
            if (e.stopPropagation) e.stopPropagation();
            this.className = '';
            var data = e.dataTransfer.getData(_dndType);
            log("data: " + data);
            if (data != 'undefined') {
                if (data === "checkout") {
                    if (_onCartMove != null) {
                        _onCartMove();
                    }
                }
                else {
                    try {
                        var cartItem = JSON.parse(data);
                        log("item dropped " + cartItem.Name);
                        setDndDone();
                        if (_onItemDrop != null) {
                            _onItemDrop(cartItem);
                        }
                    }
                    catch (e) { log("error:" + e); }
                }
            }
            return false;
        });


        //set cart move for checkout
        var cartImage = _target.querySelector("#cartImage");
        addEvent(cartImage, 'dragstart', function (e) {
            e.dataTransfer.effectAllowed = 'copy';
            e.dataTransfer.clearData();
            e.dataTransfer.setData(_dndType, 'checkout');
        });

    }

    function setDndDone() {
        var cartImage = _target.querySelector("#cartImage");
        cartImage.src = _cartFullImage;
    }

    function setOnCartMove(func) {
        _onCartMove = func;
    }

}