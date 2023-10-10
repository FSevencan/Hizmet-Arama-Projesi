using Microsoft.Extensions.Options;
using Proje.OptionsModels;
using System.Net.Mail;
using System.Net;
using Proje.Services;

namespace Proje.Services
{
    public class EmailService : IEmailService
    {

        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }

        public async Task SendResetPasswordEmail(string resetPasswordEmailLink, string ToEmail)
        {
            var smptClient = new SmtpClient();

            smptClient.Host = _emailSettings.Host;
            smptClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smptClient.UseDefaultCredentials = false;
            smptClient.Port = 587;
            smptClient.Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password);
            smptClient.EnableSsl = true;

            var mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(_emailSettings.Email);
            mailMessage.To.Add(ToEmail);

            mailMessage.Subject = "www.deneme.com | Şifre Sıfırlama Linki";
            mailMessage.Body = @$"
                  <h3>Şifrenizi yenilemek için aşağıdaki linkte tıklayınız.</h3>
                  <p><a href='{resetPasswordEmailLink}'>Şifremi Yenile</a></p>";

            mailMessage.IsBodyHtml = true;


            await smptClient.SendMailAsync(mailMessage);

        }

		public string GenerateVerificationCode()
		{
			var random = new Random();
			return random.Next(1000, 9999).ToString(); // 4 HANELİ RASTGELE KOD
		}

        public async Task SendVerificationEmail(string verificationCode, string ToEmail)
        {
            var smptClient = new SmtpClient();

            smptClient.Host = _emailSettings.Host; // "mt-engine-win.guzelhosting.com"
            smptClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smptClient.UseDefaultCredentials = false;
            smptClient.Port = 587; // SMTP sunucunuzun portu
            smptClient.Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password); 
            smptClient.EnableSsl = true; 

            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_emailSettings.Email, "Proje"); // Gönderen isminde Proje yazsın
            mailMessage.To.Add(ToEmail);

            mailMessage.Subject = "www.deneme.com | Eposta Doğrulama Kodu";
            mailMessage.Body = @$"
                   <h3>Merhaba,</h3>
                   <p>Hesabınızı doğrulamak için aşağıdaki kodu kullanabilirsiniz. Kodun süresi 1 saat içerisinde dolacaktır:</p>
                   <p><strong>{verificationCode}</strong></p>
                   <p>Lütfen dikkat: E-posta adresinizi 1 saat içinde doğrulamazsanız, hesabınız silinecektir ve yeniden kayıt olmanız gerekecektir.</p>
                   <p>Teşekkür ederiz.</p>";

            mailMessage.IsBodyHtml = true;

            await smptClient.SendMailAsync(mailMessage);
        }
    }
}

