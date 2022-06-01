# NMCNPM_QuanLyVeMayBay
***** Các bước chạy chương trình  *****
1.  Clone github về máy
2.  Vào thư mục SQL, chạy file.sql Table để tạo các bảng, sau đó chạy file Data.sql để thêm dữ liệu test phần mềm vào CSDL
          *** chạy file trên nền tảng Microsofts SQL Sever để tránh lỗi không mong muốn ***          
4.  Vào thư mục NNCNPM_QuanLyVeMayBay, mở file App.config,  thay đổi nội dung dòng connectionStrings
      		<add name="stringDatabase" connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BANVEMAYBAY;Integrated Security=True"/>
          phần "Data Source = (localdb)\MSSQLLocalDB", (localdb)\MSSQLLocalDB là "Sever name" - tên sever mà bạn đã chạy file sql 
          thay đổi (localdb)\MSSQLLocalDB thành tên sever của bạn, lưu 
4.  Vào thư mục NNCNPM_QuanLyVeMayBay, chạy file NNCNPM_QuanLyVeMayBay.sln để chạy chương trình.


*** Tài khoản     (username : admin)
                  (password : 123)
 mật khẩu của tài khoản mới được tạo mật khẩu tự động là 1
 
