using Proje.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Proje.Services
{
    // Veritabanında belirli bir süre geçtikten sonra onaylanmamış kullanıcıları silen bir servis.
    public class VerificationExpiryService : IHostedService, IDisposable
    {
        private Timer _timer; // Zamanlayıcı nesnemiz
        private readonly IServiceScopeFactory _serviceScopeFactory; // Servis kapsamı oluşturabilmek için IServiceScopeFactory nesnesi

        // Dependency Injection ile IServiceScopeFactory nesnesini alıyoruz
        public VerificationExpiryService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        // Servis başlatıldığında çağrılır
        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Zamanlayıcıyı başlatıyoruz. İlk çalışma anında ve sonrasında her saat başı DoWork metodu çağrılır
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromHours(1));

            return Task.CompletedTask;
        }

        // Zamanlayıcı tarafından çağrılan metot
        // NOT: Eğer proje büyürde kullanıcı sayısı cok fazla artarsa performans sorunu olmasın diye
        // AppUser e kayıt tarihi ekleyip. tüm kullanıcıları taramadan kayıt tarihine gore tarama yapıp onaysız kullanıcıları silebiliriz //
        private async void DoWork(object state)
        {
            // Yeni bir servis kapsamı oluşturuyoruz
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                // Kapsam içerisinden UserManager<AppUser> nesnesini alıyoruz
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                // Onaylanmamış ve süresi dolmuş kullanıcıları getiriyoruz
                var users = await userManager.Users.Where(u => u.IsVerified == false && u.VerificationCodeExpiration <= DateTime.Now).ToListAsync();

                // Bu kullanıcıları tek tek siliyoruz
                foreach (var user in users)
                {
                    await userManager.DeleteAsync(user);
                }
            }
        }

        // Servis durdurulduğunda çağrılır
        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Zamanlayıcıyı durduruyoruz
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        // IDisposable gerekliliklerini yerine getiriyoruz
        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
