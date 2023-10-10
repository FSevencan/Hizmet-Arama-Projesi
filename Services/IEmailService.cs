namespace Proje.Services
{
    public interface IEmailService
    {
        Task SendResetPasswordEmail(string resetPasswordEmailLink, string ToEmail);

		string GenerateVerificationCode();
		Task SendVerificationEmail(string verificationCode, string ToEmail);
	}
}
