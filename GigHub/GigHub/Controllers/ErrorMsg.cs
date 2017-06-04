namespace GigHub.Controllers
{
    public static class ErrorMsg
    {
        //web api controller
        public const string GeneralErrorMsg = "An error has occurred.";
        public const string InvalidLoginAttempt = "Invalid login attempt.";
        public const string InvalidCode = "Invalid code.";
        public const string PasswordHasBeenChanged = "Your password has been changed.";
        public const string PasswordHasBeenSet = "Your password has been set.";
        public const string TowFactorAuthenProviderHasBeenSet = "Your two-factor authentication provider has been set.";
        public const string PhoneNumberWasAdded = "Your phone number was added.";
        public const string PhoneNumberWasRemoved = "Your phone number was removed.";
        public const string FailedToVerifyPhone = "Failed to verify phone";
        public const string ExternalLoginWasRemoved = "The external login was removed.";

        public const string NoUserFound = "No user found";
        public const string AttendanceAlreadyExsits = "The attendance already exists.";
        public const string AttendanceDoesNotExist = "The attendance does not exist.";
        public const string FollowingAlreadyExists = "Following already exists.";
        public const string FollowingDoesNotExist = "The following does not exist.";
        public const string GigDoesNotExist = "The gig does not exist.";
        public const string NotUserDescriptionFound = "No user description found";

        public const string AuthorizedDenied = "Authorized Denied";

        public const string AccountHasLoggedIn = "Account has logged in different hosts.";
        public const string AccountIsInactivated = "Account is Inactivated.";

    }
}