using ProjectSem3.DTOs;
using System.Net.Mail;
using System.Net;
using ProjectSem3.Models;
using QRCoder;

namespace ProjectSem3.Helpers;

public class EmailHelper
{
    private IConfiguration configuration;
    public EmailHelper(
        IConfiguration configuration
        )
    {
        this.configuration = configuration;
    }

    // Method to load email template
    public string LoadTemplate(string templatePath)
    {
        if (File.Exists(templatePath))
        {
            return File.ReadAllText(templatePath);
        }
        return string.Empty;
    }

    // Method to generate email body from template
    public string GenerateEmailBody(Booking booking, BookingDetail bookingDetail, string qrCodePath)
    {
        string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "emailTemplate.html");
        string emailTemplate = LoadTemplate(templatePath);

        if (!string.IsNullOrEmpty(emailTemplate))
        {
            return emailTemplate
                .Replace("{{DepartureLocationName}}", booking.BusTrip.Trip.DepartureLocation.Name)
                .Replace("{{ArrivalLocationName}}", booking.BusTrip.Trip.ArrivalLocation.Name)
                .Replace("{{DateStart}}", booking.BusTrip.Trip.DateStart.ToString("HH:mm:ss dd/MM/yyyy"))
                .Replace("{{DateEnd}}", booking.BusTrip.Trip.DateEnd.ToString("HH:mm:ss dd/MM/yyyy"))
                .Replace("{{FullName}}", booking.FullName)
                .Replace("{{BusTypeName}}", booking.BusTrip.Bus.BusType.Name)
                .Replace("{{LicensePlate}}", booking.BusTrip.Bus.LicensePlate)
                .Replace("{{SeatName}}", bookingDetail.SeatName)
                .Replace("{{BookingDate}}", booking.BookingDate.ToString("HH:mm:ss dd/MM/yyyy"))
                .Replace("{{BusTripId}}", booking.BusTripId.ToString())
                .Replace("{{TicketCode}}", bookingDetail.TicketCode);
        }

        return string.Empty;
    }

    // Method to send the email
    public void SendEmail(string to, string subject, string body, string qrCodePath)
    {
        var host = configuration["Gmail:Host"];
        var port = int.Parse(configuration["Gmail:Port"]);
        var username = configuration["Gmail:Username"];
        var password = configuration["Gmail:Password"];
        var enable = bool.Parse(configuration["Gmail:SMTP:starttls:enable"]);
        var smtpClient = new SmtpClient
        {
            Host = host,
            Port = port,
            EnableSsl = enable,
            Credentials = new NetworkCredential(username, password),

        };

        var from = configuration["Gmail:Username"];
        if (!string.IsNullOrEmpty(qrCodePath) && File.Exists(qrCodePath))
        {
            body = body.Replace("{{QRCodePath}}", "cid:QRCodeImage");
        }

        AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");

        if (!string.IsNullOrEmpty(qrCodePath) && File.Exists(qrCodePath))
        {
            LinkedResource qrImage = new LinkedResource(qrCodePath)
            {
                ContentId = "QRCodeImage",
                TransferEncoding = System.Net.Mime.TransferEncoding.Base64
            };
            qrImage.ContentType = new System.Net.Mime.ContentType("image/png");

            // Thêm ảnh QR vào nội dung email
            htmlView.LinkedResources.Add(qrImage);

        }

        var mailMessage = new MailMessage
        {
            From = new MailAddress(from),
            Subject = subject,
            IsBodyHtml = true,
            Body = body
        };

        // Gửi email đến người nhận
        mailMessage.To.Add(to);
        mailMessage.AlternateViews.Add(htmlView);

        smtpClient.Send(mailMessage);


    }
}
