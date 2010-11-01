function addEvent(obj, evt, fn) {
    if (typeof obj.attachEvent != 'undefined') {
        obj.attachEvent("on" + evt, fn);
    }
    else if (typeof obj.addEventListener != 'undefined') {
        obj.addEventListener(evt, fn, false);        
    } else {
        //      Do you want to support browser this old? 
        //      If so, you'll need to accomodate it here
    }
}