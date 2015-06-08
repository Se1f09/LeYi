
$(document).ready(function () {

    $("#sidebar").sidebar("show");

	$("#sidebarSwitcher").click(function () {
		$("#sidebar").sidebar("toggle");
	});

	$("#sidebar .circular.button").each(function (i, obj) {
	    $(obj).click(function () {
		});
	});
});

function nameClick() {
	$("#sidebar").sidebar("toggle");
}
