var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Crear una lista para almacenar objectos 
var categorys = new List<Category>();

//Configura una ruta GET 
app.MapGet("/categorys", () =>
{
return categorys; //devuelve la lista 
});

// Configura una ruta GET 
app.MapGet("/categorys/{id}", (int id) =>
{
// Busca en la lista que tenga su ID especificado
var category = categorys.FirstOrDefault(c => c.Id == id);
return category;
});
//Configura una ruta Post 
app.MapPost("/categorys", (Category category) =>
{
categorys.Add(category);
return Results.Ok();//Devuelve una respuesta HTTP 200 OK
});

//Configura una rut PUT  para actualizar 
app.MapPut("/categorys/{id}", (int id, Category category) =>
{

var existingCategory = categorys.FirstOrDefault(c => c.Id == id);
if (existingCategory != null)
{
//Actualiza los datos
existingCategory.Name = category.Name;
existingCategory.description = category.description;
return Results.Ok();//Devuelve una respuesta HTTP 200 ok
}
else
{
return Results.NotFound();//Devuelve una respuesta HTTP 404 Not Fount 
}
});

//Configra una ruta DELETE par eliminar 
app.MapDelete("/categorys/{id}", (int id) =>
{
var existingCategory = categorys.FirstOrDefault(c => c.Id == id);
if (existingCategory != null)
{
//Elimina
categorys.Remove(existingCategory);
return Results.Ok();//Deuelve una respuesta HTTP 200 Ok
}
else
{
return Results.NotFound();//Devuelve una respuesta HTTP 404
}
});
//Ejecutar la aplicacion
app.Run();

//Definicion de la clase
internal class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string description { get; set; }
}
