namespace Identity.API.Core.Abstractions
{
    public static class UserErrors
    {
        public static readonly Error DecreaseLessThanZero = 
            new("User.DecreaseLessThanZero", "You cannot decrease loyalty points cause amount is greater than points");
        public static readonly Error RoleDoesntExist = new("User.RoleDoesntExist", "Role Doesn't Exist");
        public static readonly Error NotFound = new("User.NotFound", "User Doesn't Exist");
    }
}
