# CNPM-2021
Selling Clothes Web Online <br />

Database trong dtb folder: database.sql / ShopOnline.bak 

chỉnh sửa file web.config trong project, chỉnh sửa
```c sharp
  <connectionStrings>
    <add name="ShopOnlineEntities" connectionString="metadata=res://*/Models.Model1.csdl|res://*/Models.Model1.ssdl|res://*/Models.Model1.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=DESKTOP-5HMSITJ\SQLEXPRESS;initial catalog=ShopOnline;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
  </connectionStrings>
```

Với data source là tên server SQL 

-> chạy demo

Trang thông thường đăng nhập với <br />
	tên đăng nhập: test <br />
	mật khẩu: 123

Trang admin có đường dẫn /Admin/Index <br />
	tên đăng nhập: admin <br />
	mật khẩu: 123
  
