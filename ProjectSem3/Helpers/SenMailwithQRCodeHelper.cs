using ProjectSem3.DTOs;
using ProjectSem3.Models;
using QRCoder;

namespace ProjectSem3.Helpers;

public class SenMailwithQRCodeHelper
{
    private IConfiguration configuration;
    public SenMailwithQRCodeHelper(
        IConfiguration configuration
        )
    {
        this.configuration = configuration;
    }
    public void SenMailwithQRCode(Booking booking, List<BookingDetail> bookingDetails)
    {
        EmailHelper emailHelper = new EmailHelper(configuration);
        foreach (var bookingDetail in bookingDetails)
        {
            string ticketCode = bookingDetail.TicketCode;
            string qrCodeUrl = $"http://localhost:4200/checkticket?ticketCode={ticketCode}";

            // Generate QR code
            string qrCodePath = QRCodeHelper.GenerateQrCode(ticketCode, qrCodeUrl);

            // Generate email body from template
            string emailBody = emailHelper.GenerateEmailBody(booking, bookingDetail, qrCodePath);

            // Send the email
            emailHelper.SendEmail(booking.Email, "Congratulations on your successful ticket booking!", emailBody, qrCodePath);
        }
    }
}
