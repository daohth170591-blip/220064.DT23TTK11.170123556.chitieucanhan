# Website Quản Lý Chi Tiêu Cá Nhân

## Tính năng chính

### Người dùng thường:
- ✅ Đăng nhập/Đăng ký
- ✅ Trang tổng quan với biểu đồ thu chi
- ✅ Quản lý giao dịch (thêm, sửa, xóa, tìm kiếm)
- ✅ Quản lý ngân sách theo danh mục
- ✅ Mục tiêu tài chính
- ✅ Đính kèm hóa đơn

### Quản trị viên:
- ✅ Trang tổng quan admin
- ✅ Quản lý người dùng
- ✅ Quản lý danh mục

## Cài đặt

### Yêu cầu:
- .NET 9.0 SDK
- SQL Server hoặc SQL Server Express

### Các bước:

1. Cập nhật connection string trong `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=ExpenseManagerDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

2. Chạy migration:
```bash
dotnet ef database update
```

3. Chạy ứng dụng:
```bash
dotnet run
```

4. Truy cập: https://localhost:5001

## Tài khoản mặc định

Sau khi đăng ký, bạn có thể tạo tài khoản admin bằng cách:
- Đăng ký tài khoản mới
- Vào database và set `IsAdmin = 1` cho user đó

## Công nghệ sử dụng

- ASP.NET Core 9.0 MVC
- Entity Framework Core
- SQL Server
- Bootstrap 5
- Chart.js
- BCrypt.Net (mã hóa mật khẩu)
