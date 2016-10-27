
        $(document).ready(function () {
            function disabletext(e) {
                return false
            }
            function reEnable() {
                return true
            }
            document.onselectstart = new Function("return false")
            if (window.sidebar) {
                document.onmousedown = disabletext
                document.onclick = reEnable
            }

            document.onkeydown = keydown;

            function keydown(evt) {
                if (!evt) evt = event;
                if (evt.keyCode == 116) { //F5
                    evt.stopPropagation();
                    evt.preventDefault();
                }
            }
        });
function nobackbutton() {
    //2
    window.location.hash = "";
    //3
    window.location.hash = "Again-No-back-button" //chrome
    //4
    window.onhashchange = function () { window.location.hash = ""; }
    //5
    window.onbeforeunload = function () { window.location.hash = ""; };
    // window.onbeforeunload = function () { return "Si resfreca la APP se podrian perder algunos datos" };
}
