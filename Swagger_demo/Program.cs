using Microsoft.EntityFrameworkCore;
using Swagger_demo.DB;
using Swagger_demo.DBContext;
using Swagger_demo.Repository;

var builder = WebApplication.CreateBuilder(args);
//很重要沒有這個swagger不會動
builder.Services.AddEndpointsApiExplorer();
//controller + json patch註冊
builder.Services.AddControllers().AddNewtonsoftJson();
//swagger註冊
/*builder.Services.AddSwaggerGen(c =>
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PizzaStore API", Description = "Making the Pizzas you love", Version = "v1" })
);*/
builder.Services.AddSwaggerGen();
//DB註冊
builder.Services.AddDbContext<MyDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("MyDB")));
//註冊自己的服務(repository設計模式)
builder.Services.AddTransient<ICRUD, CRUD_Test>();
/************************************************************************/
var app = builder.Build();
//swagger
app.UseSwagger();
/*app.UseSwaggerUI(c =>
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API V1")
);*/
app.UseSwaggerUI();
app.UseRouting();

//手動加路由
app.MapGet("/pizzas", () => PizzaDB.GetPizzas());
app.MapGet("/pizzas/{id}", (int id) => PizzaDB.GetPizza(id));
app.MapPost("/pizzas", (Pizza pizza) => PizzaDB.CreatePizza(pizza));
app.MapPut("/pizzas", (Pizza pizza) => PizzaDB.UpdatePizza(pizza));
app.MapDelete("/pizzas/{id}", (int id) => PizzaDB.RemovePizza(id));
//controller路由
app.MapControllers();

app.Run();
