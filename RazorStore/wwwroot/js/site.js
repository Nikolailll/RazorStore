// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// $(window).load(function() {
//     $('.flexslider').flexslider({
//         animation: "slide"
//     });
// });

class HvrSlider {
    constructor(selector) {
        const elements = document.querySelectorAll(selector);
        elements.forEach((el) => {
            if (el.querySelectorAll('img').length > 1) {
                const hvr = document.createElement('div');
                hvr.classList.add('hvr');

                const hvrImages = document.createElement('div');
                hvrImages.classList.add('hvr__images');
                hvr.appendChild(hvrImages);

                const hvrSectors = document.createElement('div');
                hvrSectors.classList.add('hvr__sectors');
                hvrImages.appendChild(hvrSectors);

                const hvrDots = document.createElement('div');
                hvrDots.classList.add('hvr__dots');
                hvr.appendChild(hvrDots);

                el.parentNode.insertBefore(hvr, el);
                hvrImages.prepend(el);

                const hvrImagesArray = hvr.querySelectorAll('img');
                hvrImagesArray.forEach(() => {
                    hvrSectors.insertAdjacentHTML('afterbegin', '<div class="hvr__sector"></div>');
                    hvrDots.insertAdjacentHTML('afterbegin', '<div class="hvr__dot"></div>');
                });
                hvrDots.firstChild.classList.add('hvr__dot--active');
                const setActiveEl = function (targetEl) {
                    const index = [...hvrSectors.children].indexOf(targetEl);
                    hvrImagesArray.forEach((img, idx) => {
                        if (index == idx) {
                            img.style.display = 'block';
                        } else {
                            img.style.display = 'none';
                        }
                    });
                    hvr.querySelectorAll('.hvr__dot').forEach((dot, idx) => {
                        if (index == idx) {
                            dot.classList.add('hvr__dot--active');
                        } else {
                            dot.classList.remove('hvr__dot--active');
                        }
                    });
                };
                hvrSectors.addEventListener('mouseover', function (e) {
                    if (e.target.matches('.hvr__sector')) {
                        setActiveEl(e.target);
                    }
                });
                hvrSectors.addEventListener('touchmove', function (e) {
                    const position = e.changedTouches[0];
                    const target = document.elementFromPoint(position.clientX, position.clientY);
                    if (target.matches('.hvr__sector')) {
                        setActiveEl(target);
                    }
                });
            }
        });
    }
}

new HvrSlider('.images');



let slider = document.querySelector('.slider'),
    sliderList = slider.querySelector('.slider-list'),
    sliderTrack = slider.querySelector('.slider-track'),
    slides = slider.querySelectorAll('.slide'),
    arrows = slider.querySelector('.slider-arrows'),
    prev = arrows.children[0],
    next = arrows.children[1],
    slideWidth = slides[0].offsetWidth,
    slideIndex = 0,
    posInit = 0,
    posX1 = 0,
    posX2 = 0,
    posY1 = 0,
    posY2 = 0,
    posFinal = 0,
    isSwipe = false,
    isScroll = false,
    allowSwipe = true,
    transition = true,
    nextTrf = 0,
    prevTrf = 0,
    lastTrf = --slides.length * slideWidth,
    posThreshold = slides[0].offsetWidth * 0.35,
    trfRegExp = /([-0-9.]+(?=px))/,
    swipeStartTime,
    swipeEndTime,
    getEvent = function() {
        return (event.type.search('touch') !== -1) ? event.touches[0] : event;
    },
    slide = function() {
        if (transition) {
            sliderTrack.style.transition = 'transform .5s';
        }
        sliderTrack.style.transform = `translate3d(-${slideIndex * slideWidth}px, 0px, 0px)`;

        prev.classList.toggle('disabled', slideIndex === 0);
        next.classList.toggle('disabled', slideIndex === --slides.length);
    },
    swipeStart = function() {
        let evt = getEvent();

        if (allowSwipe) {

            swipeStartTime = Date.now();

            transition = true;

            nextTrf = (slideIndex + 1) * -slideWidth;
            prevTrf = (slideIndex - 1) * -slideWidth;

            posInit = posX1 = evt.clientX;
            posY1 = evt.clientY;

            sliderTrack.style.transition = '';

            document.addEventListener('touchmove', swipeAction);
            document.addEventListener('mousemove', swipeAction);
            document.addEventListener('touchend', swipeEnd);
            document.addEventListener('mouseup', swipeEnd);

            sliderList.classList.remove('grab');
            sliderList.classList.add('grabbing');
        }
    },
    swipeAction = function() {

        let evt = getEvent(),
            style = sliderTrack.style.transform,
            transform = +style.match(trfRegExp)[0];

        posX2 = posX1 - evt.clientX;
        posX1 = evt.clientX;

        posY2 = posY1 - evt.clientY;
        posY1 = evt.clientY;

        if (!isSwipe && !isScroll) {
            let posY = Math.abs(posY2);
            if (posY > 7 || posX2 === 0) {
                isScroll = true;
                allowSwipe = false;
            } else if (posY < 7) {
                isSwipe = true;
            }
        }

        if (isSwipe) {
            if (slideIndex === 0) {
                if (posInit < posX1) {
                    setTransform(transform, 0);
                    return;
                } else {
                    allowSwipe = true;
                }
            }

            // запрет ухода вправо на последнем слайде
            if (slideIndex === --slides.length) {
                if (posInit > posX1) {
                    setTransform(transform, lastTrf);
                    return;
                } else {
                    allowSwipe = true;
                }
            }

            if (posInit > posX1 && transform < nextTrf || posInit < posX1 && transform > prevTrf) {
                reachEdge();
                return;
            }

            sliderTrack.style.transform = `translate3d(${transform - posX2}px, 0px, 0px)`;
        }

    },
    swipeEnd = function() {
        posFinal = posInit - posX1;

        isScroll = false;
        isSwipe = false;

        document.removeEventListener('touchmove', swipeAction);
        document.removeEventListener('mousemove', swipeAction);
        document.removeEventListener('touchend', swipeEnd);
        document.removeEventListener('mouseup', swipeEnd);

        sliderList.classList.add('grab');
        sliderList.classList.remove('grabbing');

        if (allowSwipe) {
            swipeEndTime = Date.now();
            if (Math.abs(posFinal) > posThreshold || swipeEndTime - swipeStartTime < 300) {
                if (posInit < posX1) {
                    slideIndex--;
                } else if (posInit > posX1) {
                    slideIndex++;
                }
            }

            if (posInit !== posX1) {
                allowSwipe = false;
                slide();
            } else {
                allowSwipe = true;
            }

        } else {
            allowSwipe = true;
        }

    },
    setTransform = function(transform, comapreTransform) {
        if (transform >= comapreTransform) {
            if (transform > comapreTransform) {
                sliderTrack.style.transform = `translate3d(${comapreTransform}px, 0px, 0px)`;
            }
        }
        allowSwipe = false;
    },
    reachEdge = function() {
        transition = false;
        swipeEnd();
        allowSwipe = true;
    };

sliderTrack.style.transform = 'translate3d(0px, 0px, 0px)';
sliderList.classList.add('grab');

sliderTrack.addEventListener('transitionend', () => allowSwipe = true);
slider.addEventListener('touchstart', swipeStart);
slider.addEventListener('mousedown', swipeStart);

arrows.addEventListener('click', function() {
    let target = event.target;

    if (target.classList.contains('next')) {
        slideIndex++;
    } else if (target.classList.contains('prev')) {
        slideIndex--;
    } else {
        return;
    }

    slide();
});