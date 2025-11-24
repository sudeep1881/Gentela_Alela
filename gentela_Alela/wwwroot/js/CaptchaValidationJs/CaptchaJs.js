
  // Generate random captcha
    function generateCaptcha() {
    const chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    let captcha = "";
    for (let i = 0; i < 6; i++) {
        captcha += chars.charAt(Math.floor(Math.random() * chars.length));
    }
    document.getElementById("captchaText").textContent = captcha;
    return captcha;
  }

    // Initialize
    let currentCaptcha = generateCaptcha();

    // Refresh button
    document.getElementById("refreshCaptcha").addEventListener("click", function () {
        currentCaptcha = generateCaptcha();
    document.getElementById("captchaError").style.display = "none";
  });

    // Submit validation
    document.getElementById("submitBtn").addEventListener("click", function (e) {
    const userCaptcha = document.getElementById("captchaInput").value.trim();
    if (userCaptcha === "") {
        document.getElementById("captchaError").textContent = "Please enter captcha.";
    document.getElementById("captchaError").style.display = "block";
    e.preventDefault();
    return;
    }

    if (userCaptcha !== currentCaptcha) {
        document.getElementById("captchaError").textContent = "Captcha does not match!";
    document.getElementById("captchaError").style.display = "block";
    e.preventDefault();
    } else {
        document.getElementById("captchaError").style.display = "none";
   // alert("Captcha verified successfully ✅");
    }
  });

