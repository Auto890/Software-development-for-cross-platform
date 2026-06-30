using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Services;

var builder = WebApplication.CreateBuilder(args);

// บังคับให้ฟังเฉพาะ HTTP บน localhost:5000 เท่านั้น (ไม่ใช้ HTTPS เลย)
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5000);
});

// เพิ่ม services เข้า container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ต้องเพิ่มไฟล์ config ก่อน ถึงจะอ่านค่าจากมันได้
builder.Configuration.AddJsonFile("appsettings.Database.json", optional: true, reloadOnChange: true);

// อ่านค่าจาก Config ว่าจะใช้ Database หรือ In-memory
var useDatabase = builder.Configuration.GetValue<bool>("UseDatabase");

if (useDatabase)
{
    // ✅ แก้ไข: ลงทะเบียน AppDbContext สำหรับระบบ Product
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    builder.Services.AddScoped<IProductService, ProductDbService>();

    // ✅ แก้ไข: เปลี่ยนจาก AppDbContext เป็น UserDbContext สำหรับระบบ User (ไม่ให้ซ้ำกัน)
    builder.Services.AddDbContext<UserDbContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    builder.Services.AddScoped<IUserService, UserDbService>();
}
else
{
    // แบบจำลองข้อมูลใน Memory
    builder.Services.AddSingleton<IProductService, ProductService>();
    builder.Services.AddSingleton<IUserService, UserService>();
}

// ตั้งค่า CORS เพื่อให้ Angular (http://localhost:4200) เรียก API ได้
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// ✅ แก้ไข: ลบ app.UseMvc(); ออกไป เพราะ .NET เวอร์ชันใหม่ใช้ MapControllers ด้านล่างแทนแล้ว

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ✅ แก้ไข: จัดลำดับการเปิดใช้งาน CORS ให้ถูกต้อง (ต้องเปิดก่อน MapControllers)
app.UseCors("AllowAngularApp");

app.UseAuthorization();

app.MapControllers();

app.Run();