new Glider(document.querySelector('.glider'), {
    slidesToShow: 6,
    slidesToScroll: 1,
    draggable: true,
    dots: '.dots',
});

ScrollReveal().reveal('.heroReveal');
ScrollReveal().reveal('.featureReveal', { delay: 500 });
ScrollReveal().reveal('.featureProductsReveal', { delay: 300 });
ScrollReveal().reveal('.bannersReveal', { delay: 300 });
ScrollReveal().reveal('.newsletterReveal', { delay: 300 });

new Typed('#firstText', {
    strings: ['Handcrafted Elegance'],
    typeSpeed: 50,
    loop: false,
    loopCount: Infinity,
    showCursor: false,
    backSpeed: 100,
    smartBackspace: true,
});