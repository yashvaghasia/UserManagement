using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;
    private readonly IUserRepository _userRepository;
    public EmailService(IConfiguration config, IUserRepository userRepository)
    {
        _config = config;
        _userRepository = userRepository;
    }

    public void SendEmail(string to, string subject, string body)
    {
        var 
            Email = new MimeMessage();
        Email.From.Add( new MailboxAddress(
            _config["SmtpSettings:EmailSettings:From"],
            _config["SmtpSettings:EmailSettings:From"]
        ));
        Email.To.Add( MailboxAddress.Parse(to));
        Email.Subject = subject;

        var builder = new BodyBuilder { HtmlBody = body };
        Email.Body = builder.ToMessageBody();

        using var smtp = new SmtpClient();
        smtp.Connect(
            _config["SmtpSettings:EmailSettings:SmtpServer"], 
            int.Parse(_config["SmtpSettings:EmailSettings:Port"]),
           SecureSocketOptions.SslOnConnect
           );

        smtp.Authenticate(
            _config["SmtpSettings:Username"], 
            _config["SmtpSettings:Password"]);

        smtp.Send(Email);
        smtp.Disconnect(true);
    }
}
