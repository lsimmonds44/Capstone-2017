$( document ).ready(function() {
    headSlideOne();
    headSlideTwo();
});


function headSlideOne() {
        var slide1 = document.getElementById('topOne');
        slide1.setAttribute('data-x', 'center');
        slide1.setAttribute('data-y', 'center');
        slide1.setAttribute('data-customin', 'x:-50;y:50;z:0;rotationX:0;rotationY:0;rotationZ:0;scaleX:1;scaleY:1;skewX:0;skewY:0;opacity:0;transformPerspective:1200;transformOrigin:50% 50%;');
        slide1.setAttribute('data-customout', 'x:0;y:0;z:0;rotationX:0;rotationY:0;rotationZ:0;scaleX:1;scaleY:1;skewX:0;skewY:0;opacity:0;transformPerspective:600;transformOrigin:50% 50%;');
        slide1.setAttribute('data-start','1300');
        slide1.setAttribute('data-speed', '1800');
        slide1.setAttribute('data-easing', 'Expo.easOut');
        slide1.setAttribute('data-elementdelay', '0.5');
        slide1.setAttribute('data-endspeed', '600');
        slide1.setAttribute('data-endeasing', 'Expo.easeInOut');
        headSlideOneImage();
}

function headSlideOneImage(){
        var slideOneImg = document.getElementById('sOne');
        slideOneImg.setAttribute('data-when','exit');
        slideOneImg.setAttribute('data-from', '0');
        slideOneImg.setAttribute('data-to', '0.4');
        slideOneImg.setAttribute('data-opacity', '0');
        slideOneImg.setAttribute('data-translatey', '-60');
}

function headSlideTwo() {
        var slide2 = document.getElementById('topTwo');
        slide2.setAttribute('data-x', 'center');
        slide2.setAttribute('data-y', 'center');
        slide2.setAttribute('data-customin', 'x:-50;y:50;z:0;rotationX:0;rotationY:0;rotationZ:0;scaleX:1;scaleY:1;skewX:0;skewY:0;opacity:0;transformPerspective:1200;transformOrigin:50% 50%;');
        slide2.setAttribute('data-customout', 'x:0;y:0;z:0;rotationX:0;rotationY:0;rotationZ:0;scaleX:1;scaleY:1;skewX:0;skewY:0;opacity:0;transformPerspective:600;transformOrigin:50% 50%;');
        slide2.setAttribute('data-start','1300');
        slide2.setAttribute('data-speed', '1800');
        slide2.setAttribute('data-easing', 'Expo.easOut');
        slide2.setAttribute('data-elementdelay', '0.5');
        slide2.setAttribute('data-endspeed', '600');
        slide2.setAttribute('data-endeasing', 'Expo.easeInOut');
        headSlideTwoImage();
}

function headSlideTwoImage(){
        var slideTwoImg = document.getElementById('sTwo');
        slideTwoImg.setAttribute('data-when','exit');
        slideTwoImg.setAttribute('data-from', '0');
        slideTwoImg.setAttribute('data-to', '0.4');
        slideTwoImg.setAttribute('data-opacity', '0');
        slideTwoImg.setAttribute('data-translatey', '-60');
}