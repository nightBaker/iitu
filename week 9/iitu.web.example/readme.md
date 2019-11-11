# Unit testing

We start from week 6 example project.



Let's start from testing `CalculatorController`. Now it look like 

```csharp
[Route("[controller]/[action]")]
    public class CalculatorController : Controller
    {
        
        [Route("{firstNumber:int}/{secondNumber:int}")]
        public IActionResult Sum(int firstNumber, int secondNumber)
        {
            ViewData["action"] = RouteData.Values["action"].ToString();
            ViewData["mark"] = '+';
            ViewData["firstNumber"] = firstNumber;
            ViewData["secondNumber"] = secondNumber;
            ViewData["result"] = firstNumber + secondNumber;

            return View("Result");
        }

        [Route("{firstNumber:int}/{secondNumber:int:min(1)}")]
        public IActionResult Divide(int firstNumber, int secondNumber)
        {
            ViewData["action"] = RouteData.Values["action"].ToString();
            ViewData["mark"] = '/';
            ViewData["firstNumber"] = firstNumber;
            ViewData["secondNumber"] = secondNumber;
            ViewData["result"] = firstNumber / secondNumber;

            return View("Result");
        }


    }
```

It has business logic(BL) and returs view, so it violates **single responsibility principle **. Let's isolate our business logic(BL). Create new folder `Services` in the root of project and add `CalculatorService` to the folder.



![](\images\services.png)

```csharp
  public class CalculatorService
    {
        public int Sum(int firstNumber, int secondNumber)
        {
            return firstNumber + secondNumber;
        }

        public double Divide(int firstNumber, int secondNumber)
        {
            if (secondNumber == 0) throw new ArgumentException("Second number can't be 0");

            return firstNumber / secondNumber;
        }
    }
```

```csharp
 [Route("[controller]/[action]")]
    public class CalculatorController : Controller
    {

        private CalculatorService _calcService;

        public CalculatorController(CalculatorService calcService)
        {
            _calcService = calcService;
        }

        [Route("{firstNumber:int}/{secondNumber:int}")]
        public IActionResult Sum(int firstNumber, int secondNumber)
        {
            ViewData["action"] = RouteData.Values["action"].ToString();
            ViewData["mark"] = '+';
            ViewData["firstNumber"] = firstNumber;
            ViewData["secondNumber"] = secondNumber;
            ViewData["result"] = _calcService.Sum(firstNumber, secondNumber);

            return View("Result");
        }

        [Route("{firstNumber:int}/{secondNumber:int:min(1)}")]
        public IActionResult Divide(int firstNumber, int secondNumber)
        {
            ViewData["action"] = RouteData.Values["action"].ToString();
            ViewData["mark"] = '/';
            ViewData["firstNumber"] = firstNumber;
            ViewData["secondNumber"] = secondNumber;
            ViewData["result"] = _calcService.Divide(firstNumber,secondNumber);

            return View("Result");
        }


    }    
```

```csharp
public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MoviesContext>(options =>
            {
                options.UseSqlite("Filename=movies.db");
            });
            services.AddMvc();
            services.AddScoped<CalculatorService>();
        }
```

Now our contoller has dependency of  `CalculatorService` and last contains only BL and does not depend on anything. As we can see `CalculatorService` is easy testable.



## First unit test

Create folder `tests` and add new `xunit`project

![](C:\Users\Shalabaev_Y\AppData\Roaming\marktext\images\2019-11-11-10-34-43-image.png)

Add reference to `iitu.web.example` project from new created project.  Add new `CalculatorServiceTests.cs` file to newly created project.

```csharp
using iitu.web.example.Services;
using System;
using Xunit;

namespace iitu.web.example.tests
{
    public class CalculatorServiceTests
    {
        [Theory]
        [InlineData(1,2,3)]
        [InlineData(15, 33, 48)]
        public void SumTest(int firstNumber, int secondNumber, int expectedResult)
        {
            var calService = new CalculatorService();
            var resultSum = calService.Sum(firstNumber, secondNumber);
            Assert.Equal(expectedResult, resultSum);
        }

        [Fact]
        public void DevideTest()
        {
            var calService = new CalculatorService();
            var resultSum = calService.Divide(9, 3);
            Assert.Equal(3, resultSum);
        }

        [Fact]
        public void DevideExceptionTest()
        {
            var calService = new CalculatorService();
            Assert.Throws<ArgumentException>(() => calService.Divide(9, 0));
            
        }

    }
}

```



---

## How to use Mock

Let's try to test `MoviesController`. Now it has dependency on `MoviesContext` and it has BL. It violates `Single responsibility` and `Dependency inversion` principles. First of all, let's create `MovieService`

```csharp
public class MovieService
    {
        private readonly MoviesContext _dbContext;

        public MovieService(MoviesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Movie>> GetMovies()
        {
            return await _dbContext.Movies.ToListAsync();            
        }

        public async Task AddAndSave(Movie movie)
        {
            _dbContext.Movies.Add(movie);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Movie>> Search(string text)
        {
            text = text.ToLower();
            var searchedMovies = await _dbContext.Movies.Where(movie => movie.Name.ToLower().Contains(text)
                                            || movie.Genre.ToLower().Contains(text)
                                            || movie.Author.ToLower().Contains(text))
                                        .ToListAsync();

            return searchedMovies;
        }


    }
```

```csharp
  public class MoviesController : Controller
    {
        private readonly MovieService _movieService;

        public MoviesController(MovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _movieService.GetMovies();
            return View(movies);
        }

        [HttpGet]
        public IActionResult Add()
        {            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Movie movie)
        {

            await _movieService.AddAndSave(movie);

            var movies = await _movieService.GetMovies();

            return View("Index",movies);
        }

        public async Task<IActionResult> Search(string text)
        {
            var searchedMovies = await _movieService.Search(text);
            return View("Index", searchedMovies);
        }

    }
```

```csharp
 public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MoviesContext>(options =>
            {
                options.UseSqlite("Filename=movies.db");
            });
            services.AddMvc();
            services.AddScoped<CalculatorService>();
            services.AddScoped<MovieService>();
        }
```



Now `MovieService` still has direct dependency on MoviesContext. It violates `Dependency inversion principle`. So we have to fix it. Let's create abstraction as `IMovieRepository`

```csharp
 public interface IMovieRepository
    {
        void Add(Movie movie);
        Task Save();
        Task<List<Movie>> GetAll();
        Task<List<Movie>> GetMovies(Expression<Func<Movie, bool>> predicate);

    }
```

```csharp
 public class MovieService
    {
        private readonly IMovieRepository _movieRepo;

        public MovieService(IMovieRepository movieRepo)
        {
            _movieRepo = movieRepo;
        }

        public async Task<List<Movie>> GetMovies()
        {
            return await _movieRepo.GetAll();            
        }

        public async Task AddAndSave(Movie movie)
        {
            _movieRepo.Add(movie);
            await _movieRepo.Save();
        }

        public async Task<List<Movie>> Search(string text)
        {
            text = text.ToLower();
            var searchedMovies = await _movieRepo.GetMovies(movie => movie.Name.ToLower().Contains(text)
                                            || movie.Genre.ToLower().Contains(text)
                                            || movie.Author.ToLower().Contains(text));

            return searchedMovies;
        }


    }
```

`MovieService` became *Testable* . Let's add new test `MovieServiceTests`.

Firstly install mocking framework `iitu.web.example.tests` project

```powershell
Install-Package Moq -Version 4.13.1
```

[Details about moq dotnet](https://github.com/moq/moq4)



```csharp
 public class MovieServiceTests
    {
        [Fact]
        public async Task AddTest()
        {
            var fakeRepository = Mock.Of<IMovieRepository>();
            var movieService = new MovieService(fakeRepository);

            var movie = new Movie() { Name = "test movie" };
            await movieService.AddAndSave(movie);
        }

        [Fact]
        public async Task GetMoviesTest()
        {
            var movies = new List<Movie>
            {
                new Movie() { Name = "test movie 1" },
                new Movie() { Name = "test movie 2" },
            };

            var fakeRepositoryMock = new Mock<IMovieRepository>();
            fakeRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(movies);


            var movieService = new MovieService(fakeRepositoryMock.Object);

            var resultMovies = await movieService.GetMovies();

            Assert.Collection(resultMovies, movie =>
            {
                Assert.Equal("test movie 1", movie.Name);
            },
            movie =>
            {
                Assert.Equal("test movie 2", movie.Name);
            });
        }

        [Fact]
        public async Task SearchTest()
        {
            var movies = new List<Movie>
            {
                new Movie() { Name = "test movie 1" },
                new Movie() { Name = "test movie 2" },
            };

            var fakeRepositoryMock = new Mock<IMovieRepository>();
            fakeRepositoryMock.Setup(x => x.GetMovies(It.IsAny<Expression<Func<Movie,bool>>>())).ReturnsAsync(movies);


            var movieService = new MovieService(fakeRepositoryMock.Object);

            var resultMovies = await movieService.Search("TEST");

            Assert.Collection(resultMovies, movie =>
            {
                Assert.Equal("test movie 1", movie.Name);
            },
            movie =>
            {
                Assert.Equal("test movie 2", movie.Name);
            });
        }
    }
```



Don't forget to implement `IMoviesRepository` .

```csharp
public class MovieRepository : IMovieRepository
    {
        readonly MoviesContext _context;

        public MovieRepository(MoviesContext context)
        {
            _context = context;
        }

        public void Add(Movie movie)
        {
            _context.Add(movie);
        }

        public Task<List<Movie>> GetAll()
        {
            return _context.Movies.ToListAsync();
        }

        public  Task<List<Movie>> GetMovies(Expression<Func<Movie, bool>> predicate)
        {
            return  _context.Movies.Where(predicate).ToListAsync();
        }

        public Task Save()
        {
            return _context.SaveChangesAsync();
        }
    }
```

```csharp
  public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MoviesContext>(options =>
            {
                options.UseSqlite("Filename=movies.db");
            });
            services.AddMvc();
            services.AddScoped<CalculatorService>();
            services.AddScoped<MovieService>();
            services.AddScoped<IMovieRepository,MovieRepository>();
        }
```

![](C:\Users\Shalabaev_Y\AppData\Roaming\marktext\images\2019-11-11-12-04-49-image.png)
