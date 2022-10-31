namespace PVI.GQKN.API.Application.Validations;

public class CreateVaiTroRequestValidator: AbstractValidator<CreateVaiTroRequest>
{
    private readonly IAuthService authService;

    public CreateVaiTroRequestValidator(IAuthService authService)
    {
        this.authService = authService;
    }

    public CreateVaiTroRequestValidator(ILogger<CreateDonViCommandValidator> logger)
    {
       
        logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
    }
}
