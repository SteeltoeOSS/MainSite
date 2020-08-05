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

function hov(a) {
	var s = document.getElementById("scope");
	var v = document.getElementById(a + '-items');

	unhov('why');
	unhov('learn');
	unhov('project');
	unhov('community');

	s.classList.add(a +'-scope');
	v.classList.add("active");
}
function unhovAll() {
	unhov('why');
	unhov('learn');
	unhov('project');
	unhov('community');
}
function unhov(a) {
	var s = document.getElementById("scope");
	var v = document.getElementById(a + '-items');

	if (s) { s.classList.remove(a + '-scope'); }
	if (v) { v.classList.remove("active"); }
}
function highlightCode(includeCopyBadge) {
	document.querySelectorAll('pre code').forEach((block) => { hljs.highlightBlock(block); });

	if (!includeCopyBadge)
		return;

	var options = {
		contentSelector: "#main",
		loadDelay: 10,
		// CSS class(es) used to render the copy icon.
		copyIconClass: "oi oi-document",
		// CSS class(es) used to render the done icon.
		checkIconClass: "oi oi-check text-success",
		// hook to allow modifying the text before it's pasted
		onBeforeTextCopied: function (text, codeElement) {
			return text;   //  you can fix up the text here
		}
	};
	window.highlightJsBadge(options);
}