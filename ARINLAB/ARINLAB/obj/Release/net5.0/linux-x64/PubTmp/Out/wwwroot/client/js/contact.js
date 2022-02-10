const tabsBar = document.getElementById('tabs-bar').children;
const tabsContent = document.getElementById('tabs-content').children;
let prevIndex = 0;
let currentIndex = 0;

const tl = gsap.timeline({ duration: 0.3, easing: 'power1.out' });

if (window.innerWidth > 600) {
	for (const c of tabsContent) {
		if (c.dataset.id == currentIndex) {
			gsap.set(c, {
				autoAlpha: 1,
			});
		} else {
			gsap.set(c, {
				autoAlpha: 0,
			});
		}
	}

	for (const tab of tabsBar) {
		tab.addEventListener('click', e => {
			if (currentIndex != e.target.dataset.id) {
				prevIndex = currentIndex;
				currentIndex = e.target.dataset.id;
				setActiveTab();
				setActiveConent();
			}
		});
	}

	function setActiveTab() {
		for (const tab of tabsBar) {
			if (tab.dataset.id == currentIndex && !tab.classList.contains('contact-tab--active')) {
				tab.classList.add('contact-tab--active');
			} else {
				if (tab.classList.contains('contact-tab--active'))
					tab.classList.remove('contact-tab--active');
			}
		}
	}

	function setActiveConent() {
		gsap.to(tabsContent[currentIndex], {
			autoAlpha: 1,
		});
		gsap.to(tabsContent[prevIndex], {
			autoAlpha: 0,
		});
	}
}
