using YumBlazor.Data;

namespace YumBlazor.Repository.IRepository
{
    public interface ICategoryRepository
    {
        public Category Create(Category obj);
        public Category Update(Category obj);
        public bool Delete(int id);
        public Category Get(int id);
        public IEnumerable<Category> GetAll();
    }
}

/*
1️⃣ This is an interface. 
Interfaces define a "contract" — they say what methods a class must have,
but they do NOT provide the actual code/implementation. 
Think of it like a promise: "Any class that uses this interface must have these methods."

2️⃣ Create Method
Method to create a new Category record in the database.
(Category obj) means “this method needs a Category object as input,
and we’ll refer to it as obj inside the method.”
Returns the created Category object.

3️⃣ Update Method
Method to update an existing Category record in the database.
Takes a Category object as input (obj) and returns the updated Category.

4️⃣ Delete Method
Method to delete a Category by its Id.
Takes an integer id as input.
Returns true if deleted successfully, false otherwise.

5️⃣ Get Method
Method to fetch a single Category by its Id.
Takes an integer id as input.
Returns the matching Category object.

6️⃣ GetAll Method
Method to fetch all Category records from the database.
Returns a collection of Category objects (IEnumerable<Category>).

7️⃣ Connections / Summary
- ICategoryRepository does not contain any code, only method definitions.
- A class that implements this interface must provide actual code for Create, Update, Delete, Get, and GetAll.
- This keeps your code organized, testable, and easy to change later.
- Think of a repository as a librarian: your code asks the repository for data instead of talking directly to the database.
*/

