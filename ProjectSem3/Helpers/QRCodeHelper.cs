using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ProjectSem3.Helpers;

public static class QRCodeHelper
{
    // Method to create and save QR code based on a given URL and TicketCode
    public static string GenerateQrCode(string ticketCode, string url)
    {
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "client", "ticket-qr-code");

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string qrCodePath = Path.Combine(folderPath, $"{ticketCode}.png");
        // Tạo QR code

        // Create QR code using QRCoder
        using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
        {
            // ECCLevel.Q is the error correction level (Q is 25% error correction)
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);

            using (QRCode qrCode = new QRCode(qrCodeData))
            {
                using (Bitmap qrCodeImage = qrCode.GetGraphic(20)) // 20 is the pixel size
                {
                    // Save the QR code as a PNG image
                    qrCodeImage.Save(qrCodePath, ImageFormat.Png);
                }
            }
        }

        return qrCodePath;
    }
}
