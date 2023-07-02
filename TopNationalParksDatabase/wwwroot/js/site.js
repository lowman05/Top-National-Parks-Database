// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

  // Add an event listener to the input field
    document.getElementById('rating-input').addEventListener('input', function() {
    // Get the selected rating value
    var rating = parseInt(this.value);

    // Determine the path to the star image based on the rating
    var starPath = '';

    switch(rating) {
      case 1:
    starPath = 'star-1.png';
    break;
    case 2:
    starPath = 'star-2.png';
    break;
    case 3:
    starPath = 'star-3.png';
    break;
    case 4:
    starPath = 'star-4.png';
    break;
    case 5:
    starPath = 'star-5.png';
    break;
    default:
    starPath = '';
    break;
    }

    // Set the star image source
    document.getElementById('rating-star').src = starPath;
  });
