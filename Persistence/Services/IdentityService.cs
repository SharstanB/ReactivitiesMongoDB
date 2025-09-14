using Domain.Absractions;
using Domain.CoreServices;
using Domain.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.IdentityEnitities;

namespace Persistence.Services
{
    public class IdentityService(SignInManager<AppUser> signInManager, AppDBContext dbContext,
        UserManager<AppUser> userManager, IAuthTokenProcessor authTokenProcessor, IDeviceContext deviceContext,
        ITokenBlacklistService tokenBlacklistService) : IIdentityService
    {
        public async Task<OperationResult<Guid>> SignupAsync(string userName, string password, string email, string fullName, DateTime DOB)
        {
            var operationResult = new OperationResult<Guid>();


            var user = await userManager.FindByNameAsync(userName);

            if (user != null)
                return operationResult.FailedResult("user already exist");

            user = new AppUser()
            {
                UserName = userName,
                Email = email,
                Name = fullName,
                DOB = DOB
            };

            var userCreationResult = await userManager.CreateAsync(user, password);
            if (!userCreationResult.Succeeded)
                return operationResult.FailedResult("Field to create user");

            deviceContext.Initialize(user.Id);
           

            user.UserDevices.Add(new UserDevice()
            {
                FingerPrint = deviceContext.FingerPrint,
                DeviceId = deviceContext.DeviceId,
                UserId = user.Id,
            });
            await dbContext.SaveChangesAsync();

            await RefreshToken(user);

            return operationResult.SuccessResult(user.Id);
        }

        public async Task<OperationResult<string>> SigninAsync(string userName, string password)
        {
            var operationResult = new OperationResult<string>();
            var user = await userManager.Users.Include(user => user.UserDevices)
                .FirstOrDefaultAsync(user => user.UserName == userName);
            if (user == null)
                return operationResult.NotExistResult();

            var result = await signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);

            if (!result.Succeeded)
                return operationResult.FailedResult();
            
            await RefreshToken(user);

            return operationResult.SuccessResult(deviceContext.DeviceId);
        }

        //when the tocken is expired to refresh it (upadte it)
        public async Task<OperationResult<bool>> RefreshTokenAsync(string? refreshToken)
        {
            var operationResult = new OperationResult<bool>();
            if (string.IsNullOrEmpty(refreshToken) )
                return operationResult.UnauthorizedResult("unauthorized access attempt");

            var user = dbContext.Users
                .Include(user => user.UserDevices)
                .FirstOrDefault(user => user.UserDevices!
                .Any(token => token.DeviceId == deviceContext.DeviceId && token.RefreshToken == refreshToken));


            if (user == null)
                return operationResult.FailedResult("Unable to retrieve the user");

            var token = user!.UserDevices.FirstOrDefault(ut=> ut.DeviceId == deviceContext.DeviceId);

            if (token is not null || token!.RefreshTokenExpiration < DateTime.UtcNow)
                      return operationResult.FailedResult("Refresh Token is expired");

            await RefreshToken(user, token!.Id);

            return operationResult.SuccessResult(true);
        }

        // update option for for the token record is only for Refresh Token
        private async Task RefreshToken(AppUser user, Guid? tokenId = null)
        {
            // Generate new tokens
            var (accessToken, accessTokenExpiration) = authTokenProcessor.GenerateToken(user);
            var refreshToken = authTokenProcessor.GenerateRefreshToken();
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(7);

            // Update existing token or add new one
            var userToken = tokenId.HasValue
                ? user.UserDevices.FirstOrDefault(t => t.Id == tokenId)
                : null;

            if (userToken != null)
            {
                userToken.RefreshToken = refreshToken;
                userToken.RefreshTokenExpiration = refreshTokenExpiration;
            }
            else
            {
                deviceContext.Initialize(user.Id);
                user.UserDevices.Add(new UserDevice
                {
                    UserId = user.Id,
                    RefreshToken = refreshToken,
                    RefreshTokenExpiration = refreshTokenExpiration,
                    FingerPrint = deviceContext.FingerPrint,
                    DeviceId = deviceContext.DeviceId
                });
            }

            await dbContext.SaveChangesAsync();

            // Write tokens to secure cookies
            authTokenProcessor.WriteTokenAsHttpOnlyCookies("Access_Token", accessToken, accessTokenExpiration);
            authTokenProcessor.WriteTokenAsHttpOnlyCookies("Refresh_Token", refreshToken, refreshTokenExpiration);
        }


        public async Task<OperationResult<bool>> SignoutAsync(string? refreshToken)
        {
            var operationResult = new OperationResult<bool>();

            if (string.IsNullOrEmpty(refreshToken))
                return operationResult.UnauthorizedResult("unauthorized access attempt");

            var user = dbContext.Users
                .Include(user => user.UserDevices)
                .FirstOrDefault(user => user.UserDevices!
                .Any(token => token.DeviceId == deviceContext.DeviceId && token.RefreshToken == refreshToken))!
                .UserDevices.FirstOrDefault();
            var token = dbContext.UserDevices.FirstOrDefault(token => token.DeviceId == deviceContext.DeviceId && token.RefreshToken == refreshToken );
            if (token is null)
                return operationResult.UnauthorizedResult("unauthorized access attempt");

            await tokenBlacklistService.BlacklistAsync(token!.Id, token.RefreshTokenExpiration);

            token.DeletedAt = DateTime.UtcNow; 

            await dbContext.SaveChangesAsync();
            //await RefreshTokenAsync("");
            return operationResult;
        }
    }
}
