namespace API.Domain.Users.Auth.JWT
{
    public static class Roles
    {
        public const string ROLE_ADMIN = "RoleAdmin";

        public static (string Name, string Id) ROLE_ADMIN_CREATE = (ROLE_ADMIN, "23d9d409-d7aa-4966-9047-48c04b41f0a1".ToUpper());
    }
}
