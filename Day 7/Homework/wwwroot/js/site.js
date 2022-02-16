// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function PassRowIndexModal (action, index){
    // var action = $('#deleteModal').find('form').attr('action');
    $('#deleteModal').find('form').attr('action', `${action}?index=${index}`);
    $('#deleteModal').modal('show');
}