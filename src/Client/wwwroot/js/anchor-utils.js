function scrollToAnchor(anchor) {
	var selector = anchor || document.location.hash;

	if (selector && selector.length > 1) {
		var element = document.querySelector(selector);
		if (element) {
			var y = element.getBoundingClientRect().top + window.pageYOffset;
			y -= document.querySelector("header").offsetHeight;

			window.scroll(0, y);
		}
	}
	else
		window.scroll(0, 0);
}
function setPageTitle(title) {
	console.debug(title);
	document.title = title;
	console.debug(document.title);
}