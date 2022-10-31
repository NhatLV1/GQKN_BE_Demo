namespace PVI.GQKN.API.Application.Commands.VaiTroCommands;

public class DeleteVaiTroRequest: IRequest<bool>
{
    public string Id { get; set; }
}

public class DeleteVaiTroRequestHandler : IRequestHandler<DeleteVaiTroRequest, bool>
{
	private readonly RoleManager<ApplicationRole> roleManager;

	public DeleteVaiTroRequestHandler(RoleManager<ApplicationRole> roleManager)
	{
		this.roleManager = roleManager;
	}

	public async Task<bool> Handle(DeleteVaiTroRequest request, CancellationToken cancellationToken)
	{
		var vaiTro = await roleManager.FindByIdAsync(request.Id);
		var result = await roleManager.DeleteAsync(vaiTro);
		return result.Succeeded;
	}
}
