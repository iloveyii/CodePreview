(function () {
  console.log("menu lib");
  var dropdownElementList = [].slice.call(
    document.querySelectorAll(".dropdown-toggler")
  );
  var dropdownList = dropdownElementList.map(function (dropdownToggleEl) {
    if(!dropdownElementList) { return false;}
    dropdownToggleEl.addEventListener("click", function (e) {
      dropdownToggleEl.nextElementSibling.classList.toggle("show");
      dropdownToggleEl.classList.toggle("show");
      e.preventDefault();
      e.stopPropagation();
      Event.stop(e);
    });
  });
  var navbarCollapseClose = document
    .getElementById("navbarCollapseClose");
    if(navbarCollapseClose) {
      navbarCollapseClose.addEventListener("click", function () {
        document.querySelector(".navbar-collapse").classList.toggle("open");
        return false;
      });
    }
})();
