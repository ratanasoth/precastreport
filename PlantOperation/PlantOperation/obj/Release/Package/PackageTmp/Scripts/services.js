// show popup form
// first arg: overlay div
// second arg: main box
function showDialog(overlay, box) {
    $(overlay).css("display", "block");
    $(box).css("display", "block");
}
// highlight row
function rS(tr) {
    var t = $(tr);
    var sel = $(t).attr("row-selected");
    if (sel == 'true') {
        $(t).css("background", "none");
        $(t).attr("row-selected", "false");
    }
    else {
        $(t).siblings().css("background", "none");
        $(t).siblings().attr("row-selected", 'false');
        $(t).css("background", "#aaa");
        $(t).attr("row-selected", "true");
    }
}
// function to add auto number
function autNumber(tbl) {
    var trs = $(tbl);
    var x = 1;
    for (var i = 0; i < trs.length; i++) {
        $(trs[i]).children("td:nth-child(1)").html(x);
        x++;
    }
}
// alert message box function
function setAlert(message) {
    // set title
    $(".alert-title").html("[សារព្រមាន]");
    // construct alert body
    var html = "<div class='row'>";
    html += "<div class='col-sm-3 col-xs-3'>";
    html += "<img src='" + burl + "content/img/warning.png' alt='Warning' width='63' />";
    html += "</div>";
    html += "<div class='col-sm-8 col-xs-8 text-justify' style='padding-top:9px'>" + message + "</div></div>";
    html += "<div class='row'><div class='col-sm-12 text-right'>";
    html += "<button class='btn btn-danger btn-xs alert-close' type='button'>ចាកចេញ</button>";
    html += "</div></div>";
    html += "</div>";
    $(".alert-overlay,.alert-box").css("display", "block");
    $(".alert-body").html(html);
    $(".alert-close").click(function () {
        $('.alert-box,.alert-overlay').css('display', 'none');
    });
}
// confirm message box
function setConfirm(message, callback) {
    // set title
    $(".confirm-title").html("[សារព្រមាន]");
    // construct confirm body
    var html = "<div class='row'>";
    html += "<div class='col-sm-3 col-xs-3'>";
    html += "<img src='" + burl + "content/img/question.png' alt='Warning' width='63' />";
    html += "</div>";
    html += "<div class='col-sm-8 col-xs-8 text-justify' style='padding-top:9px'>" + message + "</div></div>";
    html += "<div class='row'><div class='col-sm-12 text-right'>";
    html += "<button class='btn btn-primary btn-xs confirm-ok' type='button'>យល់ព្រម</button>&nbsp;&nbsp;";
    html += "<button class='btn btn-danger btn-xs confirm-close' type='button'>បោះបង់</button>";
    html += "</div></div>";
    html += "</div>";
    $(".confirm-body").html(html);
    $(".confirm-overlay,.confirm-box").css("display", "block");
    $(".confirm-close").click(function () {
        $('.confirm-box,.confirm-overlay').css('display', 'none');
        callback(false);
    });
    $(".confirm-ok").click(function () {
        $('.confirm-box,.confirm-overlay').css('display', 'none');
        callback(true);
    });

}