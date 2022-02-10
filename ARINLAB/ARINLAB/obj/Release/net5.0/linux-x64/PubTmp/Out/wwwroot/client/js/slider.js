const mySwiper = new Swiper('.main-swiper-container', {
	// Optional parameters
	// direction: 'vertical',
	loop: true,
	autoplay: {
		delay: 30000
	},

	// If we need pagination
	pagination: {
		el: '.swiper-pagination',
	},

	// Navigation arrows
});

const branchSlider = new Swiper('.branch-swiper-container', {
	loop: true,
	autoplay: true,
	slidesPerView: 3,
	spaceBetween: 30,
	pagination: {
		el: '.swiper-pagination',
		dynamicBullets: true,
	},
	breakpoints: {
		// when window width is >= 320px
		300: {
			slidesPerView: 1.2,
			centeredSlides: true,
			spaceBetween: 14,
		},
		600: {
			slidesPerView: 2,

			spaceBetween: 20,
		},
		800: {
			slidesPerView: 3,
			spaceBetween: 20,
		},
	},
});

const productsSlider = new Swiper('.products-swiper-container', {
	loop: true,
	autoplay: true,
	navigation: {
		nextEl: '.next',
		prevEl: '.prev',
	},

	breakpoints: {
		// when window width is >= 320px
		300: {
			slidesPerView: 1.5,
			centeredSlides: true,
			spaceBetween: 10,
		},
		600: {
			slidesPerView: 2,

			spaceBetween: 20,
		},
		768: {
			slidesPerView: 2.5,
			spaceBetween: 10,
		},
		1024: {
			slidesPerView: 3,
			spaceBetween: 10,
		},
		1200: {
			slidesPerView: 3.6,
			spaceBetween: 10,
		},
	},
});

const pricesSlider = new Swiper('.prices-swiper-container', {
	loop: true,
	autoplay: true,
	slideToClickedSlide: true,

	breakpoints: {
		// when window width is >= 320px
		300: {
			slidesPerView: 1.3,
			centeredSlides: true,
			spaceBetween: 20,
		},
		600: {
			slidesPerView: 2,
			centeredSlides: true,
			spaceBetween: 20,
		},
		768: {
			slidesPerView: 2.2,
			centeredSlides: true,
			spaceBetween: 40,
		},
		1024: {
			slidesPerView: 3,
			centeredSlides: true,
			spaceBetween: 40,
		},
		1200: {
			slidesPerView: 3.6,
			centeredSlides: true,
			spaceBetween: 10,
		},
	},
});
