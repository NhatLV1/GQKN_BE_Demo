using Microsoft.AspNetCore.Identity;
using Polly;

namespace PVI.GQKN.API.Application.Commands.UserCommands;

public class UpdateUserRequestHandler : IRequestHandler<UpdateUserRequest, ApplicationUser>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly ILogger<UpdateUserRequest> logger;
    private readonly RoleManager<ApplicationRole> roleManager;
    private readonly IMapper mapper;

    public UpdateUserRequestHandler(
        UserManager<ApplicationUser> userManager,
        ILogger<UpdateUserRequest> logger,
        RoleManager<ApplicationRole> roleManager,
        IMapper mapper
        )
    {
        this.userManager = userManager;
        this.logger = logger;
        this.roleManager = roleManager;
        this.mapper = mapper;
    }

    public async Task<ApplicationUser> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.__UserId);

        if (user != null)
        {
            user = mapper.Map(request, user);
            var userUpdateResult = await userManager.UpdateAsync(user);
            if (!userUpdateResult.Succeeded)
            {
                IdentityError err = userUpdateResult.Errors.First();
                throw new GQKNDomainException(err.Code, err.Description);
            }

            var updateRoleResult = await roleManager.UpdateUserRoles(userManager, request.VaiTro, user);
           
        }

        return user;
    }

    //private async Task<string> UpdateUserRoles(
    //    string[] selectedIds, ApplicationUser user)
    //{
    //    var roles = await roleManager.Roles.ToArrayAsync();
    //    var allRoles = roles.Select(r => r.Name).ToArray();
    //    var selectedRoles = roles.Where(e => selectedIds.Contains(e.Id))
    //        .Select(e => e.Name).ToArray();

    //    var userRoles = await userManager.GetRolesAsync(user);

    //    foreach (var role in allRoles)
    //    {
    //        if (selectedRoles.Contains(role))
    //        {
    //            if (!userRoles.Contains(role)) // add
    //            {
    //                await userManager.AddToRoleAsync(user, role);
    //            }
    //        }
    //        else
    //        {
    //            if (userRoles.Contains(role))
    //            {
    //                await userManager.RemoveFromRoleAsync(user, role);
    //            }
    //        }
    //    }

    //    return null;

    //}
}
