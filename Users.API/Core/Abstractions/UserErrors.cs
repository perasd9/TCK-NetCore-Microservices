namespace Identity.API.Core.Abstractions
{
    public static class UserErrors
    {
        public static readonly Error DecreaseLessThanZero =
            Error.Validation("User.DecreaseLessThanZero", "You cannot decrease loyalty points cause amount is greater than points");
        public static readonly Error RoleDoesntExist = Error.NotFound("User.RoleDoesntExist", "Role Doesn't Exist");
        public static readonly Error NotFound = Error.NotFound("User.NotFound", "User Doesn't Exist");
    }
}
