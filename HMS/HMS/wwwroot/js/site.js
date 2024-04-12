// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function PasswordVisibility() {

    let is_visible = document.getElementById("checkbox").checked;

    if (is_visible) {

        document.getElementById("password").type = "text";

    } else {

        document.getElementById("password").type = "password";
    }
}