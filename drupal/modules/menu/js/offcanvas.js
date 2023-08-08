(function () {
  'use strict'

  var navbarSideCollapse = document.querySelector('#navbarSideCollapse');
  if(navbarSideCollapse) {
    navbarSideCollapse.addEventListener('click', function () {
      document.querySelector('.offcanvas-collapse').classList.toggle('open')
    })
  }
  
})()
