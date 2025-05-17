
# CQRS Nedir?

**CQRS**, özünde Command Query Responsibility Segregation‘ın kısaltılmış halidir. Açılımından da anlaşılacağı üzere ‘Command’ ve ‘Query’ sorumluluklarının ayrılması prensibini esas alan bir yaklaşımı savunmaktadır.

* Command

Olmayan veriyi oluşturan ya da var olan bir veri üzerinde güncelleme veya silme işlemi yapan isteklerdir.

`INSERT UPDATE DELETE`

* Query

Mevcut verileri sadece listelemek, okumak yahut sunmak için read işlemi yapan isteklerdir.

`SELECT`


İşte CQRS, uygulamalarımızda bu istekleri karşılayacak olan yapılanmaları birbirinden ayırmamızı önermektedir.


## CQRS Pattern Uygulaması

![image](https://github.com/user-attachments/assets/04cce575-a1f9-4a07-b488-77fe2772c5b4)

CQRS, verileri güncellemek için ‘Command’ sınıflarını, okumak için ise ‘Query’ sınıflarını kullanmaktadır. Tabi burada öncelikle CQRS pattern’ının tasarımını ele alacağız. Ardından bu tasarımı daha hızlı ve dinamik bir şekilde uygulamamızı sağlayacak olan MediatR kütüphanesiyle kurmaya çalışacağız.

İlk olarak ilgili uygulama ya da katman içerisinde ‘CQRS’ isminde bir klasör oluşturunuz. Ardından içerisine ‘Commands’, ‘Queries’ ve ‘Handlers’ isimlerinde klasörler oluşturunuz. Ardından tüm klasörlerin içerisine yandaki görseldeki gibi gelecek olan request’leri ve cevap olarak döndürülecek olan response’ları tanımlayacağımız ‘Request’ ve ‘Response’ klasörlerini oluşturunuz.

![image](https://github.com/user-attachments/assets/a33b8a93-943b-4e74-9ff1-60841a5f63cf)

- **Commands**

Uygulamada yapılacak olan tüm Command’leri tarif edecek sınıfları barındırmaktadır.

`Request`

Yapılacak Command isteklerini karşılayacak olan sınıfları barındırmaktadır.

`Response`

Yapılan Command isteklerine karşılık verilecek olan response sınıflarını barındırmaktadır.

- **Queries**

Uygulamada yapılacak olan tüm Query’leri tarif edecek sınıfları barındırmaktadır.

`Request`

Yapılacak Query isteklerini karşılayacak olan sınıfları barındırmaktadır.

`Response`

Yapılan Query isteklerine karşılık verilecek olan response sınıflarını barındırmaktadır.

- **Handlers**

Uygulamada yapılacak olan tüm Command ya da Query isteklerini işleyecek ve sonuç olarak respose nesnelerini dönecek olan sınıfları barındırmaktadır.

`CommandHandlers`

Yapılan Command isteklerini işler ve response’larını döner.

`QueryHandlers`

Yapılan Query isteklerini işler ve response’larını döner.

***CQRS, okuma ve yazma iş yüklerinin bağımsız olarak ölçeklendirilebilmesine olanak tanır.***

# Mediator Pattern Nedir?
Mediator, tek bir aracı(mediator) nesnesi içerisinde çeşitli nesneler arasındaki karmaşık ilişkiler ağını yönetmenize olanak tanıyan bir tasarım desenidir.

## MediatR Kütüphanesi Nedir?
Mediator pattern’dan faydalanmak için kendimizce manuel bir tasarım yapabileceğimiz gibi bu pattern’ı benimseyen ve işlemleri daha hızlı ve efektif bir şekilde gerçekleştirmemizi sağlayacak olan [MediatR Kütüphanesini](https://github.com/jbogard/MediatR)‘de kullanabiliriz. Biz bu içeriğimizde MediatR kütüphanesinden istifade edecek ve konumuza bu nitelikle devam edeceğiz…

Öncelikle ilgili uygulamaya MediatR Kütüphanesi eşliğinde Asp.NET Core projesi olmasından dolayı dependency injection paketi olan [MediatR.Extensions.Microsoft.DependencyInjection](https://www.nuget.org/packages/MediatR.Extensions.Microsoft.DependencyInjection/) kütüphanelerini yükleyiniz.

![image](https://github.com/user-attachments/assets/752ccbf9-2343-46d2-b8aa-795940d708c5)

Ardından Asp.NET Core uygulamasına aşağıdaki gibi MediatR servisini ekleyiniz.

```csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        //Burada Command ve Query'lerin oluşturulduğu projedeki herhangi bir classın verilmesi kafidir.

        services.AddMediatR(typeof(ApplicationDbContext));
        .
        .
        .
    }
}
```

Ardından Command, Query ve Handler sınıflarını aşağıdaki gibi bu sefer MediatR kütüphanesinin getirisi olan IRequest ve IRequestHandler arayüzleriyle tekrardan tanımlayalım.

### IRequest
![image](https://github.com/user-attachments/assets/84f7b5a4-3813-42d5-9865-46ca3150727a)
IRequest, command yahut query requestlerini karşılayacak olan sınıflar tarafından implemente edilecek olan bir arayüzdür. Generic olarak bu request karşılığında hangi nesnenin döndürüleceğini bildirmemizi ister.

### IRequestHandler
![image](https://github.com/user-attachments/assets/028378ee-6827-4efb-abe8-49ff4ecbc0e4)
IRequestHandler ise command yahut query requestlerinin işlenmesini sağlayacak olan Handler sınıflarının arayüzüdür. Generic olarak request ve response sınıflarının bildirilmesini ister ve ilgili sınıfa içerisindeki ‘Handle’ isimli fonksiyonu implemente ettirir.

## Commands
* `CreateProductCommandRequest` & `CreateProductCommandResponse`

```csharp
namespace DAL.CQRS.Commands.Request
{
    public class CreateProductCommandRequest : IRequest<CreateProductCommandResponse>
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
```
```csharp
namespace DAL.CQRS.Commands.Response
{
    public class CreateProductCommandResponse
    {
        public bool IsSuccess { get; set; }
        public Guid ProductId { get; set; }
    }
}
```


* `DeleteProductCommandRequest` & `DeleteProductCommandResponse`
```csharp
namespace DAL.CQRS.Commands.Request
{
    public class DeleteProductCommandRequest : IRequest<DeleteProductCommandResponse>
    {
        public Guid Id { get; set; }
    }
}
```
```csharp
namespace DAL.CQRS.Commands.Response
{
    public class DeleteProductCommandResponse
    {
        public bool IsSuccess { get; set; }
    }
}
```

## Queries

* `GetAllProductQueryRequest` & `GetAllProductQueryResponse`
```csharp
namespace DAL.CQRS.Queries.Request
{
    public class GetAllProductQueryRequest : IRequest<List<GetAllProductQueryResponse>>
    {
 
    }
}
```
```csharp
namespace DAL.CQRS.Queries.Response
{
    public class GetAllProductQueryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
```

* `GetByIdProductQueryRequest` & `GetByIdProductQueryResponse`
```csharp
namespace DAL.CQRS.Queries.Request
{
    public class GetByIdProductQueryRequest : IRequest<GetByIdProductQueryResponse>
    {
        public Guid Id { get; set; }
    }
}
```
```csharp
namespace DAL.CQRS.Queries.Response
{
    public class GetByIdProductQueryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
```

## Handlers

* `CreateProductCommandHandler`
```csharp
namespace DAL.CQRS.Handlers.CommandHandlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var id = Guid.NewGuid();
            ApplicationDbContext.ProductList.Add(new()
            {
                Id = id,
                Name = request.Name,
                Price = request.Price,
                Quantity = request.Quantity,
                CreateTime = DateTime.Now
            });
            return new CreateProductCommandResponse
            {
                IsSuccess = true,
                ProductId = id
            };
        }
    }
}
```

* `DeleteProductCommandHandler`
```csharp
namespace DAL.CQRS.Handlers.CommandHandlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, DeleteProductCommandResponse>
    {
        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            var deleteProduct = ApplicationDbContext.ProductList.FirstOrDefault(p => p.Id == request.Id);
            ApplicationDbContext.ProductList.Remove(deleteProduct);
            return new DeleteProductCommandResponse
            {
                IsSuccess = true
            };
        }
    }
}
```

* `GetAllProductQueryHandler`
```csharp
namespace DAL.CQRS.Handlers.QueryHandlers
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, List<GetAllProductQueryResponse>>
    {
        public async Task<List<GetAllProductQueryResponse>> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            return ApplicationDbContext.ProductList.Select(product => new GetAllProductQueryResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                CreateTime = product.CreateTime
            }).ToList();
        }
    }
}
```
* `GetByIdProductQueryHandler`
```csharp
namespace DAL.CQRS.Handlers.QueryHandlers
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {
        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
            var product = ApplicationDbContext.ProductList.FirstOrDefault(p => p.Id == request.Id);
            return new GetByIdProductQueryResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                CreateTime = product.CreateTime
            };
        }
    }
}
```

MediatR kütüphanesinin en can alıcı noktası bu command ve query sınıflarını controller üzerinde kullanırken oldukça kolay ve sade bir implementasyon gerektirmesidir.
Şöyle ki;

```csharp
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    IMediator _mediator;
 
    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }
 
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest requestModel)
    {
        List<GetAllProductQueryResponse> allProducts = await _mediator.Send(requestModel);
        return Ok(allProducts);
    }
 
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromQuery] GetByIdProductQueryRequest requestModel)
    {
        GetByIdProductQueryResponse product = await _mediator.Send(requestModel);
        return Ok(product);
    }
 
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateProductCommandRequest requestModel)
    {
        CreateProductCommandResponse response = await _mediator.Send(requestModel);
        return Ok(response);
    }
 
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromQuery] DeleteProductCommandRequest requestModel)
    {
        DeleteProductCommandResponse response = await _mediator.Send(requestModel);
        return Ok(response);
    }
}
```

Görüldüğü üzere tüm implementasyon ‘IMediator’ referansı üzerinden kolayca gerçekleştirilebilmektedir.

### Kaynak
[🌐 Gençay Yıldız - CQRS](https://www.gencayyildiz.com/blog/cqrs-pattern-nedir-mediatr-kutuphanesi-ile-nasil-uygulanir/)
