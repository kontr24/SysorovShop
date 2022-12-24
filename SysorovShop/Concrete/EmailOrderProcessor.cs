using SysorovShop.Abstract;
using SysorovShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace SysorovShop.Concrete
{
    public class EmailSettings // Содержит все настройки, предназначенные для конфигурирования классов,
                               //которые работают с электронной почтой и требуются конструктору EmailOrderProcessor
    {
        public string MailToAddress = "sergeisysorov@mail.ru";
        public string MailFromAddress = "sysorovshop@mail.ru";
        public bool UseSsl = true;
        public string Username = "MySmtpUsername";
        public string Password = "MySmtpUserPassword";
        public string ServerName = "smtp.exemple.com";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"D:\product_store_emails";

    }


    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;
        public EmailOrderProcessor(EmailSettings setting)
        {
            
        }
        public void ProcessOrder(Cart cart, ShoppingDetails shoppingDetails)
        {
            using (var smtpClient = new SmtpClient())
            {
                   smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                .AppendLine("Новый заказ обработан")
                .AppendLine("---")
                .AppendLine("Товары:");

                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Product.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (итого: {2:c}", line.Quantity, line.Product.Brand, subtotal);
                }

                body.AppendFormat("Общая стоимость: {0:c}", cart.ComputeTotalValue())
                    .AppendLine("---")
                    .AppendLine("Доставка:")
                    .AppendLine(shoppingDetails.Name)
                    .AppendLine(shoppingDetails.Line1)
                    .AppendLine(shoppingDetails.Line2 ?? "")
                    .AppendLine(shoppingDetails.Line3 ?? "")
                    .AppendLine(shoppingDetails.City)
                    .AppendLine(shoppingDetails.Country)
                    .AppendLine("---")
                    .AppendFormat("Подарочная упаковка: {0}", shoppingDetails.GiftWrap ? "Да" : "Нет");

                MailMessage mailMessage = new MailMessage(
                    emailSettings.MailFromAddress,
                    emailSettings.MailToAddress,
                    "Новый заказ отправлен!",
                    body.ToString()
                    );

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                }

                smtpClient.Send(mailMessage);

            }
        }
    }
}