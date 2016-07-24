
$(document).ready(function () {
    LeftMenuGenerate();
    //$(function () {
    //    setTimeout(function () {
    //        LeftMenuGenerate();
    //    }, 100);
    //});
});

function LeftMenuGenerate() {
    debugger;
    $.ajax({

        url: '../../MerchantMenu.ashx',
        method: 'get',
        datatype: 'json',
        success: function (data) {
            buildMenu($('#side-menu'), $.parseJSON(data));
            //buildMenu($('ul.nav'), $.parseJSON(data));
        }
    });
};

function buildMenu(parent, item) {
    debugger;
    $(item).each(function () {
        var li = $('<li ></li>');
        var liInnerHtml = $('<a><i class="' + this.CssClass + '"></i>' + this.MenuText + '<span class="fa fa-chevron-down"></span></a>');
        //var liInnerHtml = $('<a href="' + this.URL + '"><i class="' + this.CssClass + '"></i>&nbsp<span class="hidden-tablet">' + this.MenuText + '</span></a>');
        liInnerHtml.appendTo(li);
        //if ($(this.List).length == 0) {
        //    liInnerHtml.appendTo(li);
        //}

        //if (!this.IsActive) {
        //    li.addClass('ui-state-disabled');
        //}

        //if ($(this.List).length == 0) {
        //    $(li).find('span').removeClass('arrow');
        //}

        li.appendTo(parent);

        if ($(this.List).length > 0) {
            //var liSubMenuInnerHtml = $('<a class="dropmenu" href="#"><i class="' + this.CssClass + '"></i>&nbsp<span class="hidden-tablet">' + this.MenuText + '</span></a>');
            //liSubMenuInnerHtml.appendTo(li);
            var ul = $('<ul class="nav child_menu"></ul>');
            ul.appendTo(li);
            build2ndLevelMenu(ul, $(this.List));
        }
    });
};

function build2ndLevelMenu(parent, item) {
    $(item).each(function () {

        var li = $('<li ></li>');
        //var liInnerHtml = $('<a class="submenu" href="' + this.URL + '"><i class="' + this.CssClass + '"></i><span class="hidden-tablet">' + this.MenuText + '</span></a>');
        var liInnerHtml = $('<a href="' + this.URL + '">' + this.MenuText + '</a>');
        liInnerHtml.appendTo(li);

        //if (!this.IsActive) {
        //    li.addClass('ui-state-disabled');
        //}
        li.appendTo(parent);
    });
};