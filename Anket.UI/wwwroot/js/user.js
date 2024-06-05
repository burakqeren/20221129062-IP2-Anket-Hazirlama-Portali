var token = localStorage.getItem("token")

var userRoles = [];

if (token == "" || token == null) {
    $("#buttonLogin").attr("hidden", false);
    $("#buttonRegister").attr("hidden", false);
    $(".buttonLogout").attr("hidden", true);
}
 else {
    var payload = parseJwt(token);
    var username = payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
    var userId = payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
    userRoles = payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

    $("#topbarUsername").html(username);
    $("#wrapper").removeAttr("hidden");
    $("#sessionUserId").val(userId);

    $("#buttonLogin").attr("hidden", true);
    $("#buttonRegister").attr("hidden", true);
    $(".buttonLogout").attr("hidden", false);

}

function parseJwt(token) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(window.atob(base64).split('').map(function (c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);

}

//$("#Logout").click(function () {
//    localStorage.removeItem("token");
//    location.href = "/Login";
//});

$("#Logout").on("click",function () {
    localStorage.removeItem("token");
    location.href = "/";
});