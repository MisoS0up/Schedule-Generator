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



// Function to toggle dark mode
function toggleDarkMode() {
    // Get the current mode preference from local storage
    var darkModeEnabled = localStorage.getItem('darkModeEnabled');

    // Toggle the mode preference
    darkModeEnabled = darkModeEnabled === 'true' ? 'false' : 'true';

    // Update the class of the body element
    document.body.classList.toggle('dark-mode');

    // Save the updated mode preference to local storage
    localStorage.setItem('darkModeEnabled', darkModeEnabled);
}

// Check if dark mode preference is set and apply it on page load
document.addEventListener('DOMContentLoaded', function () {
    var darkModeEnabled = localStorage.getItem('darkModeEnabled');
    if (darkModeEnabled === 'true') {
        document.body.classList.add('dark-mode');
    }
});