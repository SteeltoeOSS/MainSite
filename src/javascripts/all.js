//= require vendor/particles.min
//= require vendor/svg-injector.min

document.addEventListener("DOMContentLoaded", function() {
  var SVGillustrations = document.querySelectorAll('img.svg-illustration');

  function injectSVgs() {
    SVGInjector(SVGillustrations);
  }

  function drawParticles() {
    particlesJS.load('particles', 'particle-config.json');
  }

  drawParticles();
  injectSVgs();
});