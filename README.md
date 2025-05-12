
---

# 📘 Hướng Dẫn Cài Đặt Backend - Dự Án ViserBus (.NET Core)

Tài liệu này cung cấp hướng dẫn chi tiết các bước để thiết lập và chạy phần backend của dự án **ViserBus** sử dụng ASP.NET Core và SQL Server.

---

## 📥 1. Clone Dự Án

Sử dụng Git để tải mã nguồn dự án về máy:

```bash
git clone https://github.com/nguyengiabao91753/ProjectSem3.git
cd ProjectSem3
```

---

## ⚙️ 2. Cấu Hình Kết Nối Cơ Sở Dữ Liệu

### a. Sửa file `appsettings.json`

Mở file `appsettings.json` và thay đổi chuỗi kết nối như sau:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=ProjectSem3DB;User Id=sa;Password=YourPassword123;trusted_connection=true;encrypt=false"
}
```

> 🔔 **Lưu ý:**  
> - Thay `localhost` bằng tên SQL Server trên máy bạn nếu khác.
> - Thay `ProjectSem3DB` bằng tên Database trên máy bạn nếu khác. 
> - Đảm bảo tài khoản `sa` và `Password` đang được bật và có đúng mật khẩu.

---

### b. Kiểm tra file `DataBaseContext.cs`

Vào thư mục `Models` → Mở file `DataBaseContext.cs`.  
Nếu có chuỗi kết nối được viết trực tiếp (hardcoded), hãy cập nhật thông tin đăng nhập tương ứng tại đây.

---

## 🗄️ 3. Khởi Tạo Cơ Sở Dữ Liệu

### Thực hiện các bước sau trong SQL Server:

1. Mở phần mềm **SQL Server Management Studio (SSMS)**  
2. Mở file `BusTicket.sql` (có thể nằm ở thư mục gốc dự án)  
3. Chạy toàn bộ script để tạo cơ sở dữ liệu và các bảng cần thiết.

---

## ▶️ 4. Chạy Backend

Mở terminal và chạy các lệnh sau trong thư mục gốc:

```bash
dotnet restore
dotnet build
dotnet run
```

Mặc định ứng dụng sẽ chạy tại:  
- `https://localhost:7273`

---

## 🔐 5. Tài Khoản Mặc Định

| Vai Trò | Username             | Password     |
|---------|------------------------|--------------|
| Admin   | `admin`     | `12345`  |
| Staff   | `test`     | `12345`  |

> Bạn có thể đăng nhập bằng các tài khoản trên để kiểm tra chức năng đăng nhập, phân quyền.

---

## 📌 Ghi Chú

- Đảm bảo SQL Server đã được bật và cho phép kết nối qua TCP/IP.  
- Nếu bạn dùng SQL Express, có thể cần thay `Server=localhost` thành `Server=.\SQLEXPRESS`.  
- Nếu xảy ra lỗi khi kết nối CSDL, hãy kiểm tra tường lửa, xác thực SQL và mật khẩu.

---

## 📄 Giấy Phép

Dự án thuộc đồ án học phần và chỉ sử dụng cho mục đích học tập.

---
