// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    // Show the error toast
    $('#errorToast').toast('show');

    // Hide the error toast after 5 seconds
    setTimeout(function () {
        $('#errorToast').toast('hide');
    }, 5000);
});