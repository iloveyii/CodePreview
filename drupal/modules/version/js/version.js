

(function() {
    function copyTextFrom(e) {
        /* Get the text field */
        var el = document.getElementById('env_version');
         /* Copy the text inside the text field */
        navigator.clipboard.writeText(el.innerHTML.trim());
        /* Alert the copied text */
        console.log("Copied the text: " + el.innerHTML.trim());
        e.target.value = 'Copied';
    }

    Drupal.behaviors.copy_version = {
        attach: function (context, settings) {
          // Attach a click listener to the clear button.
          var copyButton = document.getElementById('copy-button');
          if(copyButton) {
            copyButton.addEventListener('click', function(e) {
              // Do something!
              console.log('Clear button clicked!');
              copyTextFrom(e);
            }, false);
          }
        }
      };

})();