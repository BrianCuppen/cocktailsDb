using System;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
namespace CocktailDb.Services;

public interface IEmailService
{
    Task SendMail(string to, Drink drink);
}

public class EmailService : IEmailService
{
    public async Task SendMail(string to, Drink drink)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Jhon Doe", "JhonDoe@Gmail.com"));
        message.To.Add(new MailboxAddress("Mr Brian", to));
        message.Subject = "Here's your drink recipe!";

        message.Body = new TextPart("plain")
        {
            Text = @"Hello, here is the recipe for your drink: " + drink.Name + "(" + drink.Category +")" + "\n" +
                    "glass type: " + drink.GlassType.Name + "\n" +
                   "Ingredients: " + drink.Ingredient + "\n" +
                   "Instructions: " + drink.Instructions + "\n" +
                   "url: "+ drink.ImageUrl + "\n" +
                   "Enjoy!"
        };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync("mailserver", 25, false);

            // Note: only needed if the SMTP server requires authentication
            await client.AuthenticateAsync("joey", "password");

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }

}