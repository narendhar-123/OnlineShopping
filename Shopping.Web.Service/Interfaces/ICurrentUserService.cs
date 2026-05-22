namespace Shopping.Web.Service.Interfaces;

/// <summary>
/// Interface for accessing current user context information.
/// </summary>
public interface ICurrentUserService
{
    /// <summary>
    /// Gets the current user's identifier (e.g., Azure AD Object ID or email).
    /// </summary>
    string? UserId { get; }

    /// <summary>
    /// Gets the current user's display name.
    /// </summary>
    string? UserName { get; }

    /// <summary>
    /// Indicates whether the current user is authenticated.
    /// </summary>
    bool IsAuthenticated { get; }
}
