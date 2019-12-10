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
	document.title = title;
}
function closeSubMenu(a) {
	var itms = document.getElementsByClassName("blazored-sub-menu-header open");

	if (!itms)
		return;

	for (var i = 0; i < itms.length; i++) {
		itms[i].classList.remove("open");
	}
}