var buttonTap = 0;
//toggle side navbar
function toggleNav() {
    if (buttonTap % 2 == 0) {
        document.getElementById("mySidenav").style.width = "225px";
        document.getElementById("main").style.marginLeft = "225px";
        buttonTap++;
    }
    else {
        document.getElementById("mySidenav").style.width = "0";
        document.getElementById("main").style.marginLeft = "0";
        buttonTap++;
    }
}
// get total height for <header>, including padding
var navBarEl = document.getElementById("navBar");
var navBarHeight = +navBarEl.offsetHeight;

// set margin-top to sideNav depending on <header> height
var sideNav = document.getElementById("mySidenav");
sideNav.style.marginTop = navBarHeight - 2 + 'px';
