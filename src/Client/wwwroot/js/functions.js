function setPageTitle(title) {
	document.title = title;
}
function highlightCode(includeCopyBadge) {
	//console.info('highlighting code');
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