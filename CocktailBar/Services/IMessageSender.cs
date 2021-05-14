using CocktailBar.Models;

namespace CocktailBar.Services
{
    public interface IMessageSender
    {
        void SendConfirmationMessage(User user, string emailConfirmationUrl);
    }
}