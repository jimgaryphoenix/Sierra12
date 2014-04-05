using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web.Security;
using log4net;
using ProgressTen.Domain.Entities;
using ProgressTen.Infrastructure.ViewModels;
using ProgressTen.Repository;

namespace ProgressTen.Services
{
	public class SiteService : ServiceBase
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(ClubService).Name);

		public ContactViewModel SendContactEmail(ContactViewModel contact)
		{
			using (SmtpClient _mailClient = new SmtpClient(ConfigurationManager.AppSettings["SmtpServer"]))
			{
				MailMessage message = new MailMessage();

				string toAddress = ConfigurationManager.AppSettings["ContactToAddress"];

				message.To.Add(new MailAddress(toAddress));
				message.From = new MailAddress(contact.EmailAddress, contact.Name);
				message.Subject = "ProgressTen - Contact Request";
				message.Body = contact.Comments;

				_mailClient.Send(message);
			}
			
			return AddThankYouMessage(contact);
		}

		public ContactViewModel AddThankYouMessage(ContactViewModel contact)
		{
			contact.ThankYouMessage = "Thank you for your comments. If you have sent a question or comments on a matter that requires our follow up, you will hear from us soon.";

			return contact;
		}

		public bool ValidateLogin(LoginViewModel loginViewModel)
		{
			bool authenticated = Membership.ValidateUser(loginViewModel.EmailAddress, loginViewModel.Password);

			loginViewModel.IsAuthenticated = authenticated;

			return authenticated;
		}

		public bool ResetPassword(LoginViewModel loginViewModel)
		{
			var user = Membership.GetUser(loginViewModel.EmailAddress);

			//var availableChars = "ABCDEFGHIJKLMNPQRSTUVWXYZabcdefghijklmnpqrstuvwxyz123456789";

			//var random = new Random();
			//var tempPassword = new string(Enumerable.Repeat(availableChars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
			
			
			string tempPassword = "New Password Not Set";

			if(user != null)
			{
				try
				{
					tempPassword = user.ResetPassword();

					using (SmtpClient _mailClient = new SmtpClient(ConfigurationManager.AppSettings["SmtpServer"]))
					{
						MailMessage message = new MailMessage();

						string toAddress = loginViewModel.EmailAddress;

						message.To.Add(new MailAddress(toAddress));
						message.From = new MailAddress("noReply@progressten.com");
						message.Subject = "ProgressTen - Password Reset";
						message.Body =
							@"
Your password has been reset throught the ProgressTen.com Password Reset page. Your new password is:

" + tempPassword + @"

This password is case sensitive, so enter it exactly as you see it here. Once you log in, be sure to go to your Profile tab and change your password to something of your own.

Thanks, 
ProgressTen.com";

						_mailClient.Send(message);
					}

					log.Info("Password for " + user.UserName + " has been reset to " + tempPassword);

					return true;
				}
				catch (Exception ex)
				{
					log.Error("There was a problem resetting a user's password. User: " + user.UserName + " New Password: " + tempPassword, ex);
					throw;
				}
			}
			
			loginViewModel.FailureMessage = "No user was found with this email address. For further assistance, contact the ProgressTen.com Site Admin.";

			return false;
		}
	}
}
