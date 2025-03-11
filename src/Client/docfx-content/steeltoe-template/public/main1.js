$(function () {
    if (window.location.href.indexOf("v4") > -1) {
        $("#v4").prop("checked", true);
        $("#v4").parent().addClass('active');
    }
    else if (window.location.href.indexOf("v3") > -1) {
        $("#v3").prop("checked", true);
        $("#v3").parent().addClass('active');
    }
    else if (window.location.href.indexOf("v2") > -1) {
        $("#v2").prop("checked", true);
        $("#v2").parent().addClass('active');
    }
    else if (window.location.href.indexOf("/articles") > -1) {
        $('#docsNavLink').removeClass('active');
        $('#blogNavLink').addClass('active');
    }
    else if (window.location.href.indexOf("/guides") > -1) {
        $('#docsNavLink').removeClass('active');
        $('#guidesNavLink').addClass('active');
    }
});

