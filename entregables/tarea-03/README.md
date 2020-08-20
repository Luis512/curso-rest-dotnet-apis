# Tarea

 1. Investigar sobre el significado y concepto de CQRS en ASP.NET Core

    Command and Query Responsibility Segregation(CQRS) es un patron utilizado para separar o segregar las operaciones de lectura o escritura en base de datos de una aplicacion. Los operaciones de lectura son llamados Queries mientras que los llamados de escritura son llamdos Commands.

    Beneficios de utilizar CQRS
    -   Modularidad de codigo, lo que permite clases y modelos mas simples.
    -   Mayor escalabilidad dado que se puede separar los llamados en distintos microservicios.
    -   No existe gran dependencia en modelos unicos dado que se pueden crean n cantidad. 

    Desventajas de utilizar CQRS
    - En sistemas grandes y/o complejos, existe interdependecia(por ende agrega complejidad) entre llamados, por lo que muchas veces un llamado de lectura necesite hacer una escritura o viceversa.
    
 2. Investigar sobre el uso de la libreria FluentAssertions para ASP.NET Core y aplicarlo en el ejercicio hecho en clase de la sesion 08 (endpoints de Customer y Product)
 Ver CustomerTest.cs y ProductTest.cs en WebApi > Test
 
 ```csharp
 public class CustomerTest
 {
     DbContextOptions<AdventureworksContext> options = new DbContextOptionsBuilder<AdventureworksContext>()
                                                         .UseInMemoryDatabase(databaseName: "AdventureworksContext")
                                                         .Options;
     [OneTimeSetUp]
     public void SetUP()
     {            
         using (var context = new AdventureworksContext(options))
         {
             List<Customer> customers = new List<Customer>();

             customers.Add(new Customer()
             {
                 CustomerId = 888,
                 FirstName = "Luis",
                 MiddleName = "Esteban",
                 LastName = "Alvarez"
             });
             customers.Add(new Customer()
             {
                 CustomerId = 456,
                 FirstName = "Marco",
                 MiddleName = "Antonio",
                 LastName = "Alvarezq"
             });
             customers.Add(new Customer()
             {
                 CustomerId = 789,
                 FirstName = "DELETE",
                 MiddleName = "DELETE",
                 LastName = "DELETE"
             });
             context.AddRange(customers);
             context.SaveChanges();
         }
     }

     [Test,Order(1)]
     public void CustomerController_GetById()
     {
         using (var context = new AdventureworksContext(options))
         {
             var repository = new CustomerRepository(context);
             var controller = new CustomerController(repository);
             var result = controller.Get(888);
             result.Should().NotBeNull();
             result.Should().BeEquivalentTo(new
             {
                 Id = 888,
                 FirstName = "Luis",
                 MiddleName = "Esteban",
                 LastName = "Alvarez"
             });
         }
     }

     [Test, Order(2)]
     public void CustomerController_GetAllCustomer()
     {
         using (var context = new AdventureworksContext(options))
         {
             var repository = new CustomerRepository(context);
             var controller = new CustomerController(repository);
             var result = controller.Get();
             result.Should().NotBeNull();
             var response = result.Result as OkObjectResult;
             var customers = (IEnumerable<dynamic>)response.Value;
             response.StatusCode.Should().Be(200);             
             customers.Count().Should().Be(3);
         }
     }

     [Test, Order(3)]
     public void CustomerController_Delete()
     {
         using (var context = new AdventureworksContext(options))
         {
             var repository = new CustomerRepository(context);
             var controller = new CustomerController(repository);
             var result = controller.Delete(789);
             result.Should().NotBeNull();
             Assert.That(result, Is.InstanceOf<IActionResult>());
         }
     }
 }     
 
 ```
 
 
 3. Subir un TXT con la respuesta al item 1 de la tarea y el ejercicio resuelto a la carpeta /entregables/tarea-02 correspondiente en su repositorio (NO el del profesor)    
