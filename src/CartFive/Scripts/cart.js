cart = function () {
    var _cartItems = [];
    var _cartStorageKey = "Cart5_Items";
    var _objLogger = null;

    this.addToCart = addToCart;
    this.attachLogger = attachLogger;
    this.clearCart = clearCart;
    this.loadCart = loadCart;
    this.getCartItemsJSON = getCartItemsJSON;
    this.getCartItems = getCartItems;
    this.removeFromCart = removeFromCart;    

    function log(data) {
        if (_objLogger != null) {

            _objLogger.log(data);
        }
    }

    function attachLogger(logger) {
        _objLogger = logger;
    }

    function loadCart() {
        var val = localStorage.getItem(_cartStorageKey);
        if (val == null) {
            _cartItems = [];
        }
        else {
            _cartItems = JSON.parse(val);
        }
        log(_cartItems.length + ' item(s) loaded ');
    }

    function saveCart() {
        localStorage.setItem(_cartStorageKey, JSON.stringify(_cartItems));
    }

    function clearCart() {
        localStorage.removeItem(_cartStorageKey);
    }

    function addToCart(item) {
        var alreadyExists = false;
        for (var iLoop = 0; iLoop < _cartItems.length; iLoop++) {
            var cartItem = _cartItems[iLoop];
            if (cartItem.Id === item.Id) {
                cartItem.Qty++;
                alreadyExists = true;
                log("Item " + cartItem.Name + " found. Qty:" + cartItem.Qty);
                break;
            }
        }

        if (!alreadyExists) {
            item.Qty = 1;
            _cartItems.push(item);
        }

        //save cart to local storage
        saveCart();
    }

    function getCartItemsJSON() {
        return _cartItems;
    }

    function getCartItems() {
        return JSON.stringify(_cartItems);
    }

    function removeFromCart(productId) {        
        for (var iLoop = 0; iLoop < _cartItems.length; iLoop++) {
            var cartItem = _cartItems[iLoop];
            if (cartItem.Id === productId) {
                if (cartItem.Qty > 1) {
                    cartItem.Qty--;
                    log("Item " + cartItem.Name + " Quantity reduced to:"+ cartItem.Qty.toString());
                }
                else {
                    _cartItems.splice(iLoop, 1)
                    log("Item " + cartItem.Name + " removed from your cart.");
                }
                break;
            }
        }

        //save cart to local storage
        saveCart();
    }

    //clearCart();
    //loadCart();

}