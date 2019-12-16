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
function toggleSubMenu(a) {
	var itms = document.getElementsByClassName("blazored-sub-menu-header");

	if (!itms)
		return;

	for (var i = 0; i < itms.length; i++) {
		if (a.indexOf(itms[i].id) == -1) {
			itms[i].classList.remove("open");
			continue;
		}

		if (!itms[i].classList.contains('open'))
			itms[i].classList.add("open");
	}
}