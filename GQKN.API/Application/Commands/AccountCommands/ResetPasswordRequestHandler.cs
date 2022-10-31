using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Configuration;

namespace PVI.GQKN.API.Application.Commands.AccountCommands;

public class ResetPasswordRequestHandler : IRequestHandler<ResetPasswordRequest, bool>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly ILogger<ResetPasswordRequestHandler> logger;
    private readonly IConfiguration configurations;
    private readonly IUrlHelper urlHelper;
    private readonly IEmailSender emailSender;
    private readonly IBieuMauRepository bieuMauRepository;
    private readonly IViewRenderService viewRenderService;

    public ResetPasswordRequestHandler(
        UserManager<ApplicationUser> userManager,
        ILogger<ResetPasswordRequestHandler> logger,
        IConfiguration configurations,
        IUrlHelper urlHelper,
        IEmailSender emailSender,
        IBieuMauRepository bieuMauRepository,
        IViewRenderService viewRenderService
        )
    {
        this.userManager = userManager;
        this.logger = logger;
        this.configurations = configurations;
        this.urlHelper = urlHelper;
        this.emailSender = emailSender;
        this.bieuMauRepository = bieuMauRepository;
        this.viewRenderService = viewRenderService;
    }

    public async Task<bool> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);

        if (user == null) 
            throw new ApplicationException( $"Email {request.Email} không tồn tại");

        if (user.AccountType == AccountType.PIAS)
            throw new GQKNDomainException(ErrorsDef.INVALID_OP, $"Tác vụ không hợp lệ. Tài khoản PIAS.");

        // For more information on how to enable account confirmation and password reset please 
        // visit https://go.microsoft.com/fwlink/?LinkID=532713
        var code = await userManager.GeneratePasswordResetTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        var scheme = urlHelper.ActionContext.HttpContext.Request.Scheme;
        // reset password url
        var callbackUrl = urlHelper.Page(
            "/Account/ResetPassword",
             pageHandler: null,
             values: new { area = "Identity", code },
             protocol: scheme
        );

        // unsubcribe url
        var unsubscribeUrl = urlHelper.Page(
            "/Account/Unsubcribe",
            pageHandler: null,
            values: new { area = "Identity" },
            protocol: scheme);

        // resolve the template
        var templateId = configurations[AppSettings.KEY_EMAIL_RESET_PASSWORD];
        var template = (BieuMauEmail) await bieuMauRepository.GetByGuidAsync(templateId);

        if (template == null)
            throw new ConfigurationErrorsException($"Biểu mẫu {templateId} không tồn tại");

        // render template
        var htmlContent = await viewRenderService.RenderToStringAsync(templateId, new { callbackUrl, unsubscribeUrl });

        // send email
        await this.emailSender.SendEmailAsync(request.Email, template.TieuDe, htmlContent);

        return true;

    }
}
