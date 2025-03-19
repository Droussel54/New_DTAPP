const HTMLTAG = document.querySelector("html");
const DarkModeBtn = document.getElementById("dark-mode-btn");
const LightModeBtn = document.getElementById("light-mode-btn");
const FilterOptionsDiv = document.getElementById("collapseFilters");

$(document).ready(function () {
    let AppTheme = getCookie("AppTheme");
    let FilterOptions = getCookie("FilterOptions");

    if (AppTheme != "") {
        if (AppTheme == "dark") {
            SetMode("dark");
        }
        else if (AppTheme == "light") {
            SetMode("light");
        }
    }
    else {
        setCookie("AppTheme", "light", 1);
    }

    if (FilterOptions != "") {
        SetFilterOptions(FilterOptions)
    }
    else {
        setCookie("FilterOptions", "show", 1);
    }
});

function SetMode(Mode) {
    setCookie("AppTheme", Mode, 1);
    HTMLTAG.setAttribute("data-bs-theme", Mode);

    if (Mode == "dark") {
        DarkModeBtn.setAttribute("hidden", "true");
        LightModeBtn.removeAttribute("hidden");
    }
    else if (Mode == "light") {
        LightModeBtn.setAttribute("hidden", "true");
        DarkModeBtn.removeAttribute("hidden");
    }
}

function SetFilterOptions(Mode) {
    if (FilterOptionsDiv) {
        if (Mode === undefined) {
            if (FilterOptionsDiv.classList.contains("Opened")) {
                setCookie("FilterOptions", "hide", 1);
                FilterOptionsDiv.classList.remove("Opened");
                FilterOptionsDiv.classList.add("Closed");
            }
            else {
                setCookie("FilterOptions", "show", 1);
                FilterOptionsDiv.classList.remove("Closed");
                FilterOptionsDiv.classList.add("Opened");
            }
        }
        else {
            if (Mode == "show") {
                setCookie("FilterOptions", "show", 1);
                FilterOptionsDiv.classList.remove("Closed");
                FilterOptionsDiv.classList.add("Opened");
            }
            else if (Mode == "hide") {
                setCookie("FilterOptions", "hide", 1);
                FilterOptionsDiv.classList.remove("show");
                FilterOptionsDiv.classList.remove("Opened");
                FilterOptionsDiv.classList.add("Closed");
            }
        }
    }
}

function setCookie(cname, cvalue, exdays) {
    const d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    let expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

function getCookie(cname) {
    let name = cname + "=";
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}