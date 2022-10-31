namespace PVI.GQKN.API.Application.Validations;

public class CreateDonViCommandValidator: AbstractValidator<CreatePhongBanCommand>
{
    public CreateDonViCommandValidator(ILogger<CreateDonViCommandValidator> logger)
    {
        RuleFor(command => command.TenPhongBan).NotEmpty();

        logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
    }
}
