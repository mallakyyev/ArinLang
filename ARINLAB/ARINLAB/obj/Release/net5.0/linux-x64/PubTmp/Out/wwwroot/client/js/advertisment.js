const advItemsLeft = document.querySelectorAll('.advertisment-left');
const advItemsRight = document.querySelectorAll('.advertisment-right');
const advSliderLeft = document.querySelector('.adv-slider-left');
const advSliderRight = document.querySelector('.adv-slider-right');
function doResize() {
	if (window.innerWidth > 1280) {
		advItemsRight.forEach(item => {
			item.src = item.dataset.desk;
		});
		advItemsLeft.forEach(item => {
			item.src = item.dataset.desk;
		});
	} else if (window.innerWidth <= 1280) {
		advItemsRight.forEach(item => {
			item.src = item.dataset.mobile;
		});
		advItemsLeft.forEach(item => {
			item.src = item.dataset.mobile;
		});
	}
}
doResize();

window.addEventListener('resize', () => {
	doResize();
});

const countLeft = advItemsLeft.length;
const countRight = advItemsRight.length;

const interval = 5000;

let indexLeft = 1;
let indexRight = 1;

if (countLeft > 0) {
	// advItemsLeft.forEach((item, index) => {
	// 	// if (index === 0) return;
	// 	// item.style.opacity = 0;
	// });
	setInterval(() => {
		if (indexLeft < countLeft) {
			// if (indexLeft === 0) {
			// 	// advItemsLeft[countLeft - 1].style.opacity = 0;
			// 	// advItemsLeft[indexLeft].style.opacity = 1;
			// } else {
			// 	advItemsLeft[indexLeft - 1].style.opacity = 0;
			// 	advItemsLeft[indexLeft].style.opacity = 1;
			// }
			advSliderLeft.style.transform = `translateX(-${indexLeft * 100}%)`;
			indexLeft++;
		} else if (indexLeft >= countLeft - 1) {
			indexLeft = 0;
			advSliderLeft.style.transform = `translateX(-${indexLeft * 100}%)`;
			// advItemsLeft[countLeft - 1].style.opacity = 0;
			// advItemsLeft[indexLeft].style.opacity = 1;
		}
	}, interval);
}

if (countRight > 0) {
	// advItemsRight.forEach((item, index) => {
	// 	// if (index === 0) return;
	// 	// item.style.opacity = 0;
	// });
	setInterval(() => {
		if (indexRight < countRight) {
			// if (indexRight === 0) {
			// 	// advItemsRight[countRight - 1].style.opacity = 0;
			// 	// advItemsRight[indexRight].style.opacity = 1;
			// } else {
			// 	advItemsRight[indexRight - 1].style.opacity = 0;
			// 	advItemsRight[indexRight].style.opacity = 1;
			// }
			advSliderRight.style.transform = `translateX(-${indexRight * 100}%)`;
			indexRight++;
		} else if (indexRight >= countRight - 1) {
			indexRight = 0;
			// advItemsRight[countRight - 1].style.opacity = 0;
			// advItemsRight[indexRight].style.opacity = 1;
			advSliderRight.style.transform = `translateX(-${indexRight * 100}%)`;
		}
	}, interval);
}
