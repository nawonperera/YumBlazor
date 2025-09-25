using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace YumBlazor.Data
{
    
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {        
        public DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Appetizer" },
                new Category { Id = 2, Name = "Entree" },
                new Category { Id = 3, Name = "Dessert" }
            );
        }
    }

}

/*

=================================
        using statements
=================================

*using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
*using Microsoft.EntityFrameworkCore;

using → tells C# which libraries (code from other people) we want to use.

Microsoft.AspNetCore.Identity.EntityFrameworkCore → library that handles users, passwords, roles (Identity system) using EF Core.

Microsoft.EntityFrameworkCore → library that handles database stuff like tables, queries, relationships.

Connection: Without these, C# won’t know what DbContext, IdentityDbContext, or DbSet are.

=================================
   namespace YumBlazor.Data
=================================

*namespace YumBlazor.Data

namespace → a folder or container for your code.

YumBlazor.Data → we are grouping database-related code under this name.

Connection: Helps organize your project so everything related to database is in one “place”.

=============================
     Class declaration
=============================

*public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
*    : IdentityDbContext<ApplicationUser>

Let’s break this into pieces:

public → this class can be used anywhere in the project.

class ApplicationDbContext → defines a class (blueprint) called ApplicationDbContext. Think: a database manager.

(DbContextOptions<ApplicationDbContext> options) → a parameter passed to the class:

DbContextOptions → settings that tell EF Core which database to connect to and how.

ApplicationDbContext → generic parameter saying these options are for this class.

options → variable name for these settings.

: IdentityDbContext<ApplicationUser> → inheritance:

: means we are extending another class.

IdentityDbContext<ApplicationUser> → base class that already knows how to handle users, passwords, roles.

ApplicationUser → our custom user class (we can add extra info like FullName).

Connection: This line sets up a database manager that already knows about users and takes configuration options for connecting to the database.

=================================
   Property: DbSet<Category>
=================================

*public DbSet<Category> Category { get; set; }

public → everyone can see and use this property.

DbSet<Category> → represents a table in the database with rows of type Category.

Category → property name (usually same as table name).

{ get; set; } → allows reading and writing data.

Connection: EF Core will create a table called Category in the database. Each row in this table will be a Category object.

===========================
  Method: OnModelCreating
===========================

*protected override void OnModelCreating(ModelBuilder builder)

protected → only this class or classes that inherit from it can use it.

override → we are changing or adding to behavior of the base class (IdentityDbContext has a method called this too).

void → this method doesn’t return anything.

OnModelCreating → method name, used to configure database tables.

(ModelBuilder builder) → parameter used to build the structure of tables.

Inside OnModelCreating
*base.OnModelCreating(builder);

Calls the original behavior from IdentityDbContext.

Makes sure Identity tables like Users and Roles are also created.

builder.Entity<Category>().HasData(
    new Category { Id = 1, Name = "Appetizer" },
    new Category { Id = 2, Name = "Entree" },
    new Category { Id = 3, Name = "Dessert" }
);

builder.Entity<Category>() → pick the Category table.

.HasData(...) → seed default data when DB is created.

new Category { Id = 1, Name = "Appetizer" } → create a row with Id=1, Name="Appetizer".

Same for Entree and Dessert.

Connection: This ensures your Category table is not empty when the app runs the first time.

===========================
   How it all connects
===========================

ApplicationDbContext = main bridge to database.

Inherits IdentityDbContext = already knows how to handle users.

DbSet<Category> = tells EF Core to make a table for Categories.

OnModelCreating = tells EF Core how tables look and adds default data.

options = tells EF Core which database to use (connection string, SQL type, etc.).

builder.Entity<Category>().HasData = makes sure the app starts with 3 categories automatically.

✅ Summary in one line:

This class manages the database, creates tables for users and categories, and automatically fills the category table with default data when the database is created.
*/

