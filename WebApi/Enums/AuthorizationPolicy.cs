namespace WebApi.Enums;

public enum AuthorizationPolicy
{
    ONLY_ADMIN = 1,
    ADMIN_AND_OWNER = 2,
    ONLY_OWNER = 3,
    AUTHENTICATED_USER = 4
}