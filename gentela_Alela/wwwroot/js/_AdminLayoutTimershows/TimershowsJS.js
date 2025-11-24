
    // Start from 30 minutes
    let totalTime = 30 * 60;  // 1800 seconds

    function startSessionTimer() {
        const minLabel = document.getElementById("sessionTimerValue");
        const secLabel = document.getElementById("secondsTimer");

        const timer = setInterval(() => {

            let minutes = Math.floor(totalTime / 60);
            let seconds = totalTime % 60;

            // Update UI
            minLabel.textContent = minutes.toString().padStart(2, '0');
            secLabel.textContent = seconds.toString().padStart(2, '0');

            // Timer end → redirect to login
            if (totalTime <= 0) {
                clearInterval(timer);
                window.location.href = "/Login/Index";   // CHANGE URL if needed
            }

            totalTime--;

        }, 1000);
    }

    window.onload = startSessionTimer;

