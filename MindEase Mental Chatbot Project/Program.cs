using Microsoft.EntityFrameworkCore;
using MindEase_Mental_Chatbot_Project.Data;
using MindEase_Mental_Chatbot_Project.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<MindEase_Mental_Chatbot_Project.Services.IChatbotService,
    MindEase_Mental_Chatbot_Project.Services.AzureChatbotService>();
// Register AppDbContext with SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=Database/mindease.db"));

// Register AnalyticsService for dependency injection
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

builder.Services.AddScoped<ITriageService, TriageService>();

builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
    });

builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();