$(document).ready(function () {
    if ($("#ShowProcessingModel") != null) {

        $("#ShowProcessingModel").hide();
    }
});

function ShowAjaxSuccess(data) {
    //alert(data.code + " " + data.message);
    if (data.code == '1') {
        $("#dvMessaheAlert").html("<div class='alert alert-" + data.css + " alert-dismissible fade in' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button>" + data.message + "</div>");
    }
    //Alert for open report in new windows with ajax call response
    if (data.code == '3') {
        $("#dvMessaheAlert").html("<div class='alert alert-" + data.css + " alert-dismissible fade in' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button>" + data.message + "</div>");
        window.open(data.linkUrl)
    }
}
function ShowAjaxFailure(data) {
    $("#dvMessaheAlert").html("<div class='alert alert-" + data.css + " alert-dismissible fade in' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button>" + data.message + "</div>");
}
function ShowAjaxComplete() {
    var $confirm = $("#ShowProcessingModel");
    $confirm.modal('hide');
}
function ShowAjaxProcessing() {
    var $confirm = $("#ShowProcessingModel");
    $confirm.modal('show');
}