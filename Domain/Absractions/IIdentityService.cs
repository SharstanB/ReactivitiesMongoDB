using Domain.CoreServices;

namespace Domain.Absractions
{
    public interface IIdentityService
    {
        Task<OperationResult<Guid>> SignupAsync(string userName, string password, string email, string fullName , DateTime DOB );
        Task<OperationResult<string>> SigninAsync(string userName, string password);

        Task<OperationResult<bool>> SignoutAsync(string? refreshToken);
        //Task<string> GetUserIdAsync(string userName);
        //Task<(string userId, string fullName, string UserName, string email, IList<string> roles)> GetUserDetailsAsync(string userId);
        //Task<(string userId, string fullName, string UserName, string email, IList<string> roles)> GetUserDetailsByUserNameAsync(string userName);
        //Task<string> GetUserNameAsync(string userId);
        //Task<bool> DeleteUserAsync(string userId);
        //Task<bool> IsUniqueUserName(string userName);
        //Task<List<(string id, string fullName, string userName, string email)>> GetAllUsersAsync();
        //Task<List<(string id, string userName, string email, IList<string> roles)>> GetAllUsersDetailsAsync();
        //Task<bool> UpdateUserProfile(string id, string fullName, string email, IList<string> roles);

        //// Role Section
        //Task<bool> CreateRoleAsync(string roleName);
        //Task<bool> DeleteRoleAsync(string roleId);
        //Task<List<(string id, string roleName)>> GetRolesAsync();
        //Task<(string id, string roleName)> GetRoleByIdAsync(string id);
        //Task<bool> UpdateRole(string id, string roleName);

        //// User's Role section
        //Task<bool> IsInRoleAsync(string userId, string role);
        //Task<List<string>> GetUserRolesAsync(string userId);
        //Task<bool> AssignUserToRole(string userName, IList<string> roles);
        //Task<bool> UpdateUsersRole(string userName, IList<string> usersRole);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           
    }
}
