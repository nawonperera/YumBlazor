using YumBlazor.Data;
using YumBlazor.Repository.IRepository;

namespace YumBlazor.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Category Create(Category obj)
        {
            _db.Category.Add(obj);
            _db.SaveChanges();
            return obj;
        }

        public bool Delete(int id)
        {
            var obj = _db.Category.FirstOrDefault(c => c.Id == id);
            if (obj != null)
            {
                _db.Category.Remove(obj);
                return _db.SaveChanges() > 0;
            }
            return false;
        }

        public Category Get(int id)
        {
            var obj = _db.Category.FirstOrDefault(c => c.Id == id);
            if (obj == null)
            {
                return new Category();
            }
            return obj;
        }

        public IEnumerable<Category> GetAll()
        {
            return _db.Category.ToList();
        }

        public Category Update(Category obj)
        {
            var objFromDb = _db.Category.FirstOrDefault(c => c.Id == obj.Id);
            if (objFromDb is not null)
            {
                objFromDb.Name = obj.Name;
                _db.Category.Update(objFromDb);
                _db.SaveChanges();
                return objFromDb;
            }
            return obj;
        }
    }
}

/*

using YumBlazor.Data;
using YumBlazor.Repository.IRepository;


============================================================================
using: tells the compiler which namespaces to look in for types.

YumBlazor.Data → likely contains ApplicationDbContext and the Category model.
YumBlazor.Repository.IRepository → likely contains the ICategoryRepository interface.

namespace YumBlazor.Repository → groups this code under that name so other code can 
refer to it as YumBlazor.Repository.CategoryRepository.
==============================================================================

namespace YumBlazor.Repository
{
    
    ========================================================================
    public: visible to other parts of the app.
    class CategoryRepository: defines a new class named CategoryRepository.
    : ICategoryRepository: implements the interface ICategoryRepository. 
      That means the class must provide the methods the interface declares.
    ========================================================================
    
    public class CategoryRepository : ICategoryRepository
    {
        
        ==================================
        private readonly ApplicationDbContext _db;

        private → only code inside this class can use _db.
        readonly → set once (in constructor), then cannot be reassigned.
        ApplicationDbContext → EF Core class that represents the database connection and tables (DbSets).
        _db → holds the DbContext instance this repository uses.
        ==================================
        
        private readonly ApplicationDbContext _db;

        
        ==================================
        Constructor — dependency injection

        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        This is the constructor. When someone creates CategoryRepository, 
        they must give it an ApplicationDbContext called db.

        This pattern is called dependency injection: 
        the DbContext is provided from outside (by ASP.NET Core), 
        not created inside the class.
        ==================================
        
        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        
        ==================================
        1) Create

        public Category Create(Category obj)
        {
            _db.Category.Add(obj);
            _db.SaveChanges();
            return obj;
        }

        public → callable from outside.
        Category → return type (method returns a Category).
        Create → method name.
        (Category obj) → one parameter of type Category, named obj.

        _db.Category.Add(obj) → adds the obj to EF Core change tracker and 
        schedules an INSERT to the database.

        _db.SaveChanges() → executes the pending database operations 
        (runs SQL INSERT). Returns an int (rows affected).

        return obj → returns the saved Category. 
        After SaveChanges(), EF may set properties like Id.
        ==================================
        
        public Category Create(Category obj)
        {
            _db.Category.Add(obj);
            _db.SaveChanges();
            return obj;
        }

        
        ==================================
        2) Delete

        public bool Delete(int id)
        {
            var obj = _db.Category.FirstOrDefault(c => c.Id == id);
            if (obj != null)
            {
                _db.Category.Remove(obj);
                return _db.SaveChanges() > 0;
            }
            return false;
        }

        public bool Delete(int id) → returns true if record was removed, false otherwise.

        FirstOrDefault(c => c.Id == id) → LINQ expression.
          c => c.Id == id → a lambda: for each Category c, check if c.Id == id.
          FirstOrDefault → returns first matching record or null if none.

        if (obj != null) → record was found.
        _db.Category.Remove(obj) → mark entity for deletion.
        _db.SaveChanges() > 0 → run DELETE SQL, return true if row deleted.
        ==================================
        
        public bool Delete(int id)
        {
            var obj = _db.Category.FirstOrDefault(c => c.Id == id);
            if (obj != null)
            {
                _db.Category.Remove(obj);
                return _db.SaveChanges() > 0;
            }
            return false;
        }

        
        ==================================
        3) Get

        public Category Get(int id)
        {
            var obj = _db.Category.FirstOrDefault(c => c.Id == id);
            if (obj == null)
            {
                return new Category();
            }
            return obj;
        }

        FirstOrDefault(c => c.Id == id) → finds category by id or null.

        If not found → returns new Category(). 
        (Important: this can hide "not found". Many prefer returning null.)
        ==================================
        
        public Category Get(int id)
        {
            var obj = _db.Category.FirstOrDefault(c => c.Id == id);
            if (obj == null)
            {
                return new Category();
            }
            return obj;
        }

        
        ==================================
        4) GetAll

        public IEnumerable<Category> GetAll()
        {
            return _db.Category.ToList();
        }

        IEnumerable<Category> → sequence of Category objects (can loop with foreach).
        _db.Category.ToList() → executes query immediately and returns List<Category>.
        ==================================
        
        public IEnumerable<Category> GetAll()
        {
            return _db.Category.ToList();
        }

        
        ==================================
        5) Update

        public Category Update(Category obj)
        {
            var objFromDb = _db.Category.FirstOrDefault(c => c.Id == obj.Id);
            if (objFromDb is not null) 
            {
                objFromDb.Name = obj.Name;
                _db.Category.Update(objFromDb);
                _db.SaveChanges();
                return objFromDb; 
            }
            return obj;
        }

        - Finds record by obj.Id.
        - If found:
            objFromDb.Name = obj.Name → update property.
            _db.Category.Update(objFromDb) → mark as modified 
              (sometimes unnecessary if tracked).
            _db.SaveChanges() → write UPDATE SQL.
            return updated objFromDb.
        - If not found → returns the input obj (better to return null in practice).
        ==================================
        
        public Category Update(Category obj)
        {
            var objFromDb = _db.Category.FirstOrDefault(c => c.Id == obj.Id);
            if (objFromDb is not null)
            {
                objFromDb.Name = obj.Name;
                _db.Category.Update(objFromDb);
                _db.SaveChanges();
                return objFromDb;
            }
            return obj;
        }
    }
}


==================================
Glossary

public/private → who can see/use code.
class → defines a new type.
interface → promises methods must be implemented.
readonly → set once in constructor.
DbContext/ApplicationDbContext → EF Core DB connection + tables.
DbSet<T> → represents a table.
Add, Remove, Update → EF Core change tracking methods.
SaveChanges() → apply pending changes to DB.
FirstOrDefault() → get first or null.
ToList() → execute query, return list.
IEnumerable<T> → collection you can loop over.
==================================

How it connects:
- ASP.NET creates ApplicationDbContext and CategoryRepository.
- Controller asks for ICategoryRepository → gets CategoryRepository.
- Controller calls _repo.Create(category) → repository saves to DB.
- EF Core translates calls into SQL and runs on database.
==================================


*/
