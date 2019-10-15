//= require vendor/tocbot.min
//= require vendor/highlight.min

document.addEventListener("DOMContentLoaded", function() {
	var codeSnippets = document.querySelectorAll('pre code');

	tocbotinit();
	highlightCodeSnippets();
	
	function tocbotinit() {
		tocbot.init({
			// Where to render the table of contents.
			tocSelector: '.js-toc',
			// Where to grab the headings to build the table of contents.
			contentSelector: '.js-toc-content',
			ignoreSelector: '.js-toc-ignore',
			// Which headings to grab inside of the contentSelector element.
			headingSelector: 'h1, h2, h3, h4, h5',
			positionFixedSelector: '#toc',
			// Fixed position class to add to make sidebar fixed after scrolling down past the fixedSidebarOffset.
			positionFixedClass: 'is-position-fixed',
			// fixedSidebarOffset can be any number but by default is set to auto which sets the fixedSidebarOffset to the sidebar element's offsetTop from the top of the document on init.
			fixedSidebarOffset: '150',
		});
	}

	function highlightCodeSnippets() {
		for(var i = 0; i < codeSnippets.length; i++) {
		  hljs.highlightBlock(codeSnippets[i]);
		}
	}
});