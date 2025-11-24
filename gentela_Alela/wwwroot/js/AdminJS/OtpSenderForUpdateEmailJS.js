
    $("#updateEmailBtn").click(function () {
        var email = $("#email").val();

    if (!email) {
        Swal.fire({
            icon: "warning",
            title: "Email Required",
            text: "Please enter an email address."
        });
    return;
        }

    $.post("/Admin/SendOtp", {email: email }, function (res) {
            if (res.success) {
        Swal.fire({
            title: "OTP Sent!",
            text: "An OTP has been sent to " + email,
            icon: "success",
            confirmButtonText: "Enter OTP"
        }).then(() => {
            askForOtp(); // open OTP input popup
        });
            } else {
        Swal.fire({
            icon: "error",
            title: "Failed to Send OTP",
            text: res.message
        });
            }
        });
    });

    function askForOtp() {
        Swal.fire({
            title: "Enter OTP",
            input: "text",
            inputPlaceholder: "Enter the 6-digit OTP",
            showCancelButton: true,
            confirmButtonText: "Verify",
            preConfirm: (otp) => {
                if (!otp) {
                    Swal.showValidationMessage("Please enter the OTP!");
                }
                return otp;
            }
        }).then((result) => {
            if (result.isConfirmed) {
                var otp = result.value;
                $.post("/Admin/VerifyOtp", { enteredOtp: otp }, function (res) {
                    if (res.success) {
                        Swal.fire({
                            icon: "success",
                            title: "Success",
                            text: res.message
                        });
                    } else {
                        Swal.fire({
                            icon: "error",
                            title: "Invalid OTP",
                            text: res.message
                        }).then(() => {
                            askForOtp(); // let user try again
                        });
                    }
                });
            }
        });
    }

