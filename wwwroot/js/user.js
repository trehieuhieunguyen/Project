function myFunction() {
    document.getElementById("xmyDropdown").classList.toggle("show");
  }
  

  window.onclick = function(event) {
    if (!event.target.matches('.xdropbtn')) {
      var dropdowns = document.getElementsByClassName("xdropdown-content");
      var i;
      for (i = 0; i < dropdowns.length; i++) {
        var openDropdown = dropdowns[i];
        if (openDropdown.classList.contains('show')) {
          openDropdown.classList.remove('show');
        }
      }
    }
  }