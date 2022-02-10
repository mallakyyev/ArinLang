const element = document.getElementById('date');
const input = document.getElementById('date-input');
const pmu = document.querySelector('.pmu-instance');
console.log(pmu);

pickmeup.defaults.locales['ru'] = {
	days: ['Воскресенье', 'Понедельник', 'Вторник', 'Среда', 'Четверг', 'Пятница', 'Суббота'],
	daysShort: ['Вс', 'Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб'],
	daysMin: ['Вс', 'Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб'],
	months: [
		'Январь',
		'Февраль',
		'Март',
		'Апрель',
		'Май',
		'Июнь',
		'Июль',
		'Август',
		'Сентябрь',
		'Октябрь',
		'Ноябрь',
		'Декабрь',
	],
	monthsShort: ['Янв', 'Фев', 'Мар', 'Апр', 'Май', 'Июн', 'Июл', 'Авг', 'Сен', 'Окт', 'Ноя', 'Дек'],
};

let position;

if (window.innerWidth > 600 && window.innerWidth < 1024) {
	pickmeup(element, {
		locale: 'ru',
		position: 'left',
	});
} else if (window.innerWidth <= 600) {
	pickmeup(element, {
		locale: 'ru',
		position: 'bottom',
	});
} else {
	pickmeup(element, {
		locale: 'ru',
	});
}

element.addEventListener('pickmeup-change', function (e) {
	// input.value = e.detail.formatted_date;
	location.href = `/?date=${e.detail.formatted_date}`;
});
