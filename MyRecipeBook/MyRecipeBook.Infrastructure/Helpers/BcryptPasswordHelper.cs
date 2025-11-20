using MyRecipeBook.Application.Interfaces.Helpers;

namespace MyRecipeBook.Infrastructure.Helpers;

public class BcryptPasswordHelper : IPasswordHelper
{
    // Asla geri çözülemez
    // Aynı şifre her hash’lendiğinde farklı sonuç çıkar (her seferinde yeni salt)
    // Brute-force ve rainbow table saldırılarına dayanıklıdır
    public string Hash(string password) 
        => BCrypt.Net.BCrypt.HashPassword(password);

    // Kullanıcının girdiği şifreyi aynı salt ile yeniden hash’ler → (salt zaten hash’in içinde)
    // Ortaya çıkan hash’i veritabanındaki hash ile karşılaştırır
    public bool Verify(string password, string hash) 
        => BCrypt.Net.BCrypt.Verify(password, hash);
}