## Hangfire Nedir?
Hangfire, NET ve .NET Core uygulamalarýnda arka planda iþlem (background task) yapmanýn  kolay bir yoludur.
Hangfire kullanabilmek için gerekli Nuget paketini indirmek ve bir RDBMS ya da NoSQL kullanmak yeterlidir. 
![hangfire-packages](https://github.com/snnehir/turkcellGY3/blob/main/GradedHomeworks/Homework2/images/hangfire/hangfire-packages.png)

Hangfire; türler, yöntem adlarý, argümanlar vb. gibi tüm ayrýntýlarý veri tabanýnda tutar. Bu yüzden veri tabaný baðlantýsýna ihtiyacýmýz vardýr.
![hangfire-db](https://github.com/snnehir/turkcellGY3/blob/main/GradedHomeworks/Homework2/images/hangfire/hangfire-db.png)

Hangfire ve Hangfire Dashboard'u kullanabilmemiz için Program.cs'te yapýlmasý gereken konfigürasyonlar þu þekildedir:
```c#
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

// Add database
var connectionString = builder.Configuration.GetConnectionString("HangfireConnection");
builder.Services.AddHangfire(configuration => configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_180).UseSimpleAssemblyNameTypeSerializer().UseRecommendedSerializerSettings().UseSqlServerStorage(connectionString));

builder.Services.AddHangfireServer();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseHangfireDashboard();

app.UseRouting();

app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHangfireDashboard();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

```

Konfigürasyon hakkýnda daha detaylý bilgi için: [Hangfire Documentation](https://docs.hangfire.io/en/latest/getting-started/aspnet-core-applications.html)

## Hangfire Job Türleri

### 1. Fire-and-Forget Jobs (Tek Seferlik Görevler)
Bu tür job, görevi bir kez çalýþtýrýr ve sonucunu takip etmez. Sadece görevi baþlatýr ve geri döner.

**Örnek:** Sipariþ onaylandýktan sonra e-fatura üretmek istediðimiz varsayalým. 

![fire-and-forget-jobs--1](https://github.com/snnehir/turkcellGY3/blob/main/GradedHomeworks/Homework2/images/hangfire/continuation-jobs-1.png)

Bu job'ý üretmek için `Enqueue` metodu kullanýlýr:
```cs
var jobId = BackgroundJob.Enqueue(() => CreateInvoice());
```

`Confirm` butonuna basýldýðýnda job oluþturulur:

![fire-and-forget-jobs-2](https://github.com/snnehir/turkcellGY3/blob/main/GradedHomeworks/Homework2/images/hangfire/continuation-jobs-2.png)

### 2. Continuation / Dependent Jobs  (Devam Eden Görevler)
Bu özellik, bir görevin tamamlanmasýndan sonra otomatik olarak baþka bir görevin baþlatýlmasýný saðlar. 

**Örnek:** Yukarýdaki senaryodan devam edecek olursak, fatura oluþturulduktan sonra faturanýn kullanýcýnýn mailine gönderilmesini isteyebiliriz. Bu durumda mail gönderme iþlemi fatura oluþturma iþine baðlý olmuþ olur.

Bu tür job'ý üretmek için baðýmlý olunan `jobId` ile `ContinueJobWith` metodu kullanýlýr:
```cs
var jobId = BackgroundJob.Enqueue(() => CreateInvoice());
BackgroundJob.ContinueJobWith(jobId, () => SendOrderInvoiceMail());
```

![continuation-jobs](https://github.com/snnehir/turkcellGY3/blob/main/GradedHomeworks/Homework2/images/hangfire/continuation-jobs-3.png)

### 3. Recurring Jobs  (Tekrarlanan Görevler)
Belirli aralýklarla tekrarlanan bir görevi oluþturmak için bu tür job kullanýlýr. Zamanlama formatý `Cron` ifadesiyle belirtilir.

**Örnek:** Kullanýcýlara düzenli olarak her hafta kampanyalar ve promosyonlar hakkýnda bilgilendirici mail atýlmasýný isteyebiliriz.

Bu tür job'ý aþaðýdaki gibi üretebiliriz:
```cs
RecurringJob.AddOrUpdate(() => SendPromotionMail(), recurrencePattern);
```

![recurring-jobs-2](https://github.com/snnehir/turkcellGY3/blob/main/GradedHomeworks/Homework2/images/hangfire/recurring-job-1.png)

Kullanýcý `Subscribe` butonuna bastýðýnda tekrarlanan görev oluþur:

![recurring-jobs-2](https://github.com/snnehir/turkcellGY3/blob/main/GradedHomeworks/Homework2/images/hangfire/recurring-job-2.png)

### 4. Delayed / Scheduled Jobs  (Gecikmeli / Planlý Görevler)
Bu tür job, belirli bir süre sonra görevi çalýþtýrýr. Gecikme süresi `TimeSpan` olarak belirtilir. 

**Örnek:** Kullanýcýnýn baþka bir kullanýcýya zamaný planlanmýþ mesajlar atmasýný saðlayabiliriz.

Bu tür job'ý aþaðýdaki gibi üretebiliriz:
```cs
TimeSpan timeDifference = DateTime.Now - model.DateTime;
int jobDelayInSeconds = (int)timeDifference.TotalSeconds;
var jobId = BackgroundJob.Schedule(() => SendMail(model.Message, model.To), TimeSpan.FromSeconds(jobDelayInSeconds));
```

![scheduled-jobs-1](https://github.com/snnehir/turkcellGY3/blob/master/GradedHomeworks/Homework2/images/hangfire/delayed-scheduled-jobs.png)

Kullanýcý `Send` butonuna bastýðýnda görev oluþur:

![scheduled-jobs-1](https://github.com/snnehir/turkcellGY3/blob/master/GradedHomeworks/Homework2/images/hangfire/delayed-scheduled-jobs-2.png)
