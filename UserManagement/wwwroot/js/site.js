$(".password-toggle-icon").on("click", function () {
  var passwordField = $(this).siblings(".login-password");

  if (passwordField.attr("type") === "password") {
    passwordField.attr("type", "text");
    $(this).removeClass("fa-eye-slash").addClass("fa-eye");
  } else {
    passwordField.attr("type", "password");
    $(this).removeClass("fa-eye").addClass("fa-eye-slash");
  }
});