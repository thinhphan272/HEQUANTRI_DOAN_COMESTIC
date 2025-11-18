
var fulltime = new Date("december 11, 2025 22:36:00").getTime();
setInterval(function () {
	var now = new Date().getTime();
	var Duration = fulltime - now;
	var days = Math.floor(Duration / (1000 * 60 * 60 * 24));
	var hours = Math.floor(Duration / (1000 * 60 * 60));
	var minutes = Math.floor(Duration / (1000 * 60));
	var seconds = Math.floor(Duration / 1000);

	hours %= 24;
	minutes %= 60;
	seconds %= 60;

	document.getElementById("days").innerHTML = days;
	document.getElementById("hours").innerHTML = hours;
	document.getElementById("minutes").innerHTML = minutes;
	document.getElementById("seconds").innerHTML = seconds;

	if (Duration <= 0) {
		document.getElementById("days").innerHTML = 0;
		document.getElementById("hours").innerHTML = 0;
		document.getElementById("minutes").innerHTML = 0;
		document.getElementById("seconds").innerHTML = 0;

		const end = document.querySelector(".end");
		end.style.display = "block";
		end.style.color = "red";
		end.style.textAlign = "center";
	}

}, 1000);