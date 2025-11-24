
    // Simple client-side validation
    document.getElementById("passwordForm").addEventListener("submit", function (event) {
        const newPass = document.getElementById("newPassword").value;
    const confirmPass = document.getElementById("confirmPassword").value;
    const errorMsg = document.getElementById("passwordError");

    if (newPass !== confirmPass) {
        event.preventDefault(); // stop form from submitting
    errorMsg.style.display = "block";
        } else {
        errorMsg.style.display = "none";
        }
    });

    // Hide error when user types again
    document.getElementById("confirmPassword").addEventListener("input", function () {
        document.getElementById("passwordError").style.display = "none";
    });
