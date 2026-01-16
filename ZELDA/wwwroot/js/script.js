const bar = document.getElementById('bar');
const close = document.getElementById('close');
const nav = document.getElementById('navbar');

if (bar) {
  bar.addEventListener('click', () => {
    nav.classList.add('active')
  });
}

if (close) {
  close.addEventListener('click', () => {
    nav.classList.remove('active')
  });
}

// VIDEO AND AUDIO//
const vid = document.getElementById("vid");
const audio = document.getElementById("bgMusic");

if (vid && audio) {
  vid.addEventListener("play", () => {
    audio.play();
  });

  vid.addEventListener("pause", () => {
    audio.pause();
  });
}

// Email button at the bottom pages not contact form
const btnSignUp = document.getElementById("btn-sign-up");

if (btnSignUp) {
  btnSignUp.addEventListener("click", () => {
    const email = document.getElementById("input-email").value;

    const atpos = email.indexOf("@");
    const dotpos = email.lastIndexOf(".");

    if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 > email.length) {
      alert("Invalid email address.");
      return false;
    }

    window.location.reload()
  });
}

// Cuppon button at shop page
const btnCupon = document.getElementById("cupon-btn");

if (btnCupon) {
  btnCupon.addEventListener("click", () => {
    const cuponValue = document.getElementById("cupon-value").value;

    if (cuponValue === "") {
      alert("Cupon empty.");
      return false;
    }

    window.location.reload()
  });
}

// Header hide when scrolling
let lastScrollY = window.scrollY;
const navbar = document.querySelector('#header');

window.addEventListener('scroll', () => {
  if (window.scrollY > lastScrollY) {
    navbar.classList.add('hidden');
  } else {
    navbar.classList.remove('hidden');
  }
  lastScrollY = window.scrollY;
});

// Hamburger hide when scrolling
let lastScrollYHamburger = window.scrollY;
const navbarHamburger = document.querySelector('#hamburger');

window.addEventListener('scroll', () => {
  if (window.scrollY > lastScrollYHamburger) {
    navbarHamburger.classList.add('hidden');
  } else {
    navbarHamburger.classList.remove('hidden');
  }
  lastScrollYHamburger = window.scrollY;
});

// Hamburger menu

const hamburgerIcon = document.getElementById('hamburger-icon');
const hamburgerMenu = document.getElementById('hamburger-menu');

if (hamburgerIcon && hamburgerMenu) {
  hamburgerIcon.addEventListener('click', () => {
    hamburgerMenu.classList.toggle('active');
    hamburgerIcon.textContent = hamburgerMenu.classList.contains('active') ? '✖' : '☰';
  });
}
