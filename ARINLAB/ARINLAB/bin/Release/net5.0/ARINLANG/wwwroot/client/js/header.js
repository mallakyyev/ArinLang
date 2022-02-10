const body = document.getElementsByTagName('body');

// LANG SWITCHER
const selectBox = document.getElementById('select-box');
const selectArrow = document.getElementById('select-arrow');
const selectContent = document.getElementById('select-content');
gsap.set(selectContent, {
	y: '-100%',
	top: '100%',
	zIndex: -1,
});

selectBox.addEventListener('mouseenter', e => {
	gsap.to(selectContent, {
		duration: 0.3,
		ease: 'power1.out',
		y: 0,
	});
	gsap.to(selectArrow, {
		duration: 0.3,
		ease: 'power1.out',
		rotation: 180,
		onComplete: () => {
			isSelecting = true;
		},
	});
	selectBox.addEventListener('mouseleave', () => {
		gsap.to(selectContent, {
			duration: 0.25,
			ease: 'power1.in',
			y: '-100%',
		});
		gsap.to(selectArrow, {
			duration: 0.25,
			rotation: 0,
			ease: 'power1.in',
			onComplete: () => {
				isSelecting = false;
			},
		});
	});
});

// LANG SWITCHER

// SEARCH SECTION
const searchBtn = document.getElementById('search-btn');
const searchContainer = document.getElementById('search-container');
const searchInput = document.getElementById('search-input');
let isOpen = false;
let isSearchAnimationEnd = true;

gsap.set(searchContainer, {
	y: '-100%',
});
const openSearchBar = () => {
	if (!isSearchAnimationEnd) return;
	isSearchAnimationEnd = false;
	gsap.to(searchContainer, {
		duration: 0.7,
		ease: 'power1.out',
		y: 0,
		onComplete: () => {
			isOpen = true;
			searchInput.focus();
			isSearchAnimationEnd = true;
		},
	});
};
const closeSearchBar = () => {
	if (!isSearchAnimationEnd) return;
	isSearchAnimationEnd = false;
	gsap.to(searchContainer, {
		duration: 0.5,
		ease: 'power1.in',
		y: '-100%',
		onComplete: () => {
			isOpen = false;
			searchInput.value = '';
			searchInput.blur();
			isSearchAnimationEnd = true;
		},
	});
};
window.addEventListener('keydown', e => {
	if (e.keyCode === 27 && isOpen) {
		closeSearchBar();		
	}
	if (e.keyCode === 83 && e.shiftKey && !isOpen) {
		openSearchBar();
	}
});

searchBtn.addEventListener('click', () => {
	if (!isOpen) {
		openSearchBar();
		gsap.set(body, { height: '100vh', overflow: 'hidden' });
	} else {
		closeSearchBar();
		gsap.set(body, { height: 'auto', overflow: 'auto' });
	}
});



// SEARCH SECTION

// BURGER-MENU SECTION

const burgerMenu = document.getElementById('burger-menu');

const mobileNav = document.getElementById('mobile-nav');
let isBurgerOpen = false;
gsap.set(mobileNav, { autoAlpha: 0 });

if (window.innerWidth < 600) {
	gsap.set(burgerMenu, { zIndex: 999 });
	gsap.set(mobileNav, { autoAlpha: 0 });
	burgerMenu.addEventListener('click', () => {
		const tl = gsap.timeline();
		if (!isBurgerOpen) {
			gsap.set(body, { height: '100vh', overflow: 'hidden' });
			gsap.set(mobileNav, { zIndex: 1 });
			gsap.to(mobileNav, { duration: 0.3, autoAlpha: 1 });
			gsap.set(burgerMenu, { zIndex: 999 });
			tl.to('.burger__item.item-1', {
				top: '40%',
				duration: 0.3,
			})
				.to('.burger__item.item-1', {
					rotation: -45,
					duration: 0.2,
				})
				.to(
					'.burger__item.item-2',
					{
						bottom: '40%',
						duration: 0.3,
					},
					'-=0.6'
				)
				.to(
					'.burger__item.item-2',
					{
						rotation: 45,
						duration: 0.2,
						onComplete: () => {
							isBurgerOpen = true;
						},
					},
					'-=0.2'
				);
		} else {
			gsap.set(mobileNav, { zIndex: -1 });
			gsap.to(mobileNav, { duration: 0.3, autoAlpha: 0 });
			gsap.set(burgerMenu, { zIndex: 1 });
			gsap.set(body, { height: 'auto', overflow: 'auto' });
			tl.to('.burger__item.item-1', {
				rotation: 0,
				duration: 0.2,
			})
				.to('.burger__item.item-1', {
					top: '20%',
					duration: 0.3,
				})
				.to(
					'.burger__item.item-2',
					{
						rotation: 0,
						duration: 0.2,
					},
					'-=0.6'
				)
				.to(
					'.burger__item.item-2',
					{
						bottom: '20%',
						duration: 0.3,
						onComplete: () => {
							isBurgerOpen = false;
						},
					},
					'-=0.2'
				);
		}
	});
}

// BURGER-MENU SECTION

// DROPDOWN MENU SECTION
if (window.innerWidth > 600) {
	const menu = document.querySelector('.dropdown-links');
	const dropdownMenu = document.getElementById('dropdown-menu');
	const menuTitles = document.querySelectorAll('.menu-title');
	gsap.set(dropdownMenu, {
		y: '-200px',
		autoAlpha: 0,
		zIndex: 900,
	});

	let timerIn;
	let timerOut;
	const currentEl = {
		target: '',
		parent: '',
		child: '',
	};

	dropdownMenu.onmouseenter = () => {
		clearTimeout(timerOut);

		gsap.set(dropdownMenu, {
			y: 0,
			autoAlpha: 1,
		});
		dropdownMenu.onmouseleave = () => {
			timerOut = setTimeout(() => {
				currentEl.target.classList.remove('nav-link-active');
				currentEl.child.classList.remove('selected');
				gsap.to(dropdownMenu, {
					duration: 0.3,
					autoAlpha: 0,
					y: '-200px',
				});
			}, 300);
		};
	};

	menu.addEventListener('mouseover', mouseoverHander);

	function mouseoverHander(e) {
		const el = e.target;
		if (!el.classList.contains('dropdown')) return;

		clearTimeout(timerOut);
		if (!currentEl.target) {
			currentEl.target = el;
			currentEl.parent = el.dataset.child;
		} else if (currentEl.target !== el) {
			currentEl.target.classList.remove('nav-link-active');
			currentEl.child.classList.remove('selected');
			currentEl.target = el;
			currentEl.parent = el.dataset.child;
		}

		for (const title of menuTitles) {
			if (title.dataset.parent === currentEl.parent) currentEl.child = title;
		}

		timerIn = setTimeout(() => {
			currentEl.target.classList.add('nav-link-active');
			currentEl.child.classList.add('selected');
			gsap.to(dropdownMenu, {
				duration: 0.5,
				autoAlpha: 1,
				y: 0,
			});
		}, 100);

		el.onmouseout = () => {
			clearTimeout(timerIn);
			timerOut = setTimeout(() => {
				gsap.to(dropdownMenu, {
					duration: 0.3,
					autoAlpha: 0,
					y: '-200px',
					onComplete: () => {
						currentEl.target.classList.remove('nav-link-active');
						currentEl.child.classList.remove('selected');
					},
				});
			}, 350);
		};
	}
}

// DROPDOWN MENU SECTION
