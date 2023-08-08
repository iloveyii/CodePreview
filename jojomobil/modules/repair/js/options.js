(function () {
  const links = document.querySelectorAll(".card-link");
  links.forEach((link) => {
    link.addEventListener("click", function (e) {
      e.preventDefault();
      e.currentTarget.className += " active";
      e.currentTarget.classList.remove("error");
      e.currentTarget.querySelector('input[type="radio"]')
        ? (e.currentTarget.querySelector('input[type="radio"]').checked = true)
        : null;
      const currentSib = e.currentTarget.parentElement;
      let nextSib = currentSib.nextElementSibling;
      while (nextSib) {
        nextSib.children[0].classList.remove("active");
        nextSib.children[0].classList.remove("error");
        nextSib = nextSib.nextElementSibling;
      }

      let prevSib = currentSib.previousElementSibling;
      while (prevSib) {
        prevSib.children[0].classList.remove("active");
        prevSib.children[0].classList.remove("error");
        prevSib = prevSib.previousElementSibling;
      }
    });
  });
})();
