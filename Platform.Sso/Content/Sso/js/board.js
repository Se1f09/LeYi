$(document).ready(function () {
	$(".signLink").each(function (i, obj) {
		$(obj).click(function () {
		    window.open($(this).attr('data-url'));
		});
	});
	doTada();
});

function doTada() {
	$(".square200").each(function (i, obj) {
		$(this).transition("pulse");
	});

	$(".signLink").each(function (i, obj) {
		$(obj).mouseenter(function () {
			$(this).transition("pulse");
		});
	});
}
