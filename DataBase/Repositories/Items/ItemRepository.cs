using AxisUno.DataBase.My100REnteties.Items;
using Microsoft.EntityFrameworkCore;

namespace AxisUno.DataBase.Repositories.Items
{
    public class ItemRepository : IItemRepository
    {
        /// <summary>
        /// Gets item from the database by barcode.
        /// </summary>
        /// <param name="barcode">Barcode to search partner in the database.</param>
        /// <returns>Returns Item if data was searched; otherwise returns null.</returns>
        /// <date>30.03.2022.</date>
        public Task<Item?> GetItemByBarcodeAsync(string barcode)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                return dbContext.Items.FirstOrDefaultAsync(i => i.Barcode.Equals(barcode));
            }
        }

        /// <summary>
        ///  Gets item from the database by id of item.
        /// </summary>
        /// <param name="id">Id to search item in the database.</param>
        /// <returns>Returns Item if data was searched; otherwise returns null.</returns>
        /// <date>30.03.2022.</date>
        public Task<Item?> GetItemByIdAsync(int id)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                return context.Items.FirstOrDefaultAsync(i => i.Id == id);
            }
        }

        /// <summary>
        ///  Gets item from the database by name, codes and barcode.
        /// </summary>
        /// <param name="key">Key to search item in the database.</param>
        /// <returns>Returns Item if data was searched; otherwise returns null.</returns>
        /// <date>30.03.2022.</date>
        public Task<Item?> GetItemByKeyAsync(string key)
        {
            using (DatabaseContext databaseContext = new DatabaseContext())
            {
                return databaseContext.
                    Items.
                    FirstOrDefaultAsync(i =>
                    i.Name.Equals(key) ||
                    i.Code.Equals(key) ||
                    i.Barcode.Equals(key) ||
                    i.ItemsCodes.FirstOrDefault(ic => ic.Code.Equals(key)) != null);
            }
        }

        /// <summary>
        /// Gets list of items in according to name, barcode and codes of item.
        /// </summary>
        /// <param name="searchKey">Key to search data.</param>
        /// <returns>List of items.</returns>
        /// <date>30.03.2022.</date>
        public IAsyncEnumerable<Item> GetItemsAsync(string searchKey)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                return dbContext.
                    Items.
                    Where(i =>
                    string.IsNullOrEmpty(searchKey) ? 1 == 1 :
                    (i.Name.ToLower().Contains(searchKey.ToLower()) ||
                    i.Code.Contains(searchKey) ||
                    i.Barcode.Contains(searchKey) ||
                    i.ItemsCodes.Where(ic => ic.Code.Contains(searchKey)).FirstOrDefault() != null)).
                    AsAsyncEnumerable();
            }
        }

        /// <summary>
        /// Gets list of items in according to path of group, name, barcode and codes of item.
        /// </summary>
        /// <param name="groupPath">Path of group.</param>
        /// <param name="searchKey">Key to search by other fields.</param>
        /// <returns>List of items.</returns>
        /// <date>30.03.2022.</date>
        public IAsyncEnumerable<Item> GetItemsAsync(string groupPath, string searchKey)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                return dbContext.
                    Items.
                    Where(i =>
                    (groupPath.Equals("-2") ? 1 == 1 : i.Group.Path.StartsWith(groupPath)) &&
                    (string.IsNullOrEmpty(searchKey) ? 1 == 1 :
                    (i.Name.ToLower().Contains(searchKey.ToLower()) ||
                    i.Code.Contains(searchKey) ||
                    i.Barcode.Contains(searchKey) ||
                    i.ItemsCodes.Where(ic => ic.Code.Contains(searchKey)).FirstOrDefault() != null))).
                    AsAsyncEnumerable();
            }
        }

        /// <summary>
        /// Gets list of items in according to id of item group.
        /// </summary>
        /// <param name="groupId">Id of item group to search data.</param>
        /// <returns>List of items.</returns>
        /// <date>30.03.2022.</date>
        public IAsyncEnumerable<Item> GetItemsByGroupIdAsync(int groupId)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                return dbContext.
                    Items.
                    Where(i => i.Group.Id == groupId).
                    AsAsyncEnumerable();
            }
        }

        /// <summary>
        /// Adds new item to table of items.
        /// </summary>
        /// <param name="item">Item to add to table of items in the database.</param>
        /// <returns>Returns 0 if item wasn't added to database; otherwise returns real id of new record.</returns>
        /// <date>31.03.2022.</date>
        public Task<int> AddItem(Item item)
        {
            return Task.Run<int>(() =>
            {
                using (DatabaseContext dbContext = new DatabaseContext())
                {
                    dbContext.Items.Add(item);
                    dbContext.SaveChanges();

                    return item.Id;
                }
            });
        }

        /// <summary>
        /// Updates the item in the table of items.
        /// </summary>
        /// <param name="item">Item to update in the table of items in the database.</param>
        /// <returns>Returns true if item was updated; otherwise returns false.</returns>
        /// <date>31.03.2022.</date>
        public Task<bool> UpdateItem(Item item)
        {
            return Task.Run<bool>(() =>
            {
                using (DatabaseContext dbContext = new DatabaseContext())
                {
                    dbContext.Items.Update(item);
                    return dbContext.SaveChanges() > 0;
                }
            });
        }

        /// <summary>
        /// Deletes item by id.
        /// </summary>
        /// <param name="itemId">Id of item to delete.</param>
        /// <returns>Returns true if item was deleted; otherwise returns false.</returns>
        /// <date>31.03.2022.</date>
        public Task<bool> DeleteItem(int itemId)
        {
            return Task.Run<bool>(() =>
            {
                using (DatabaseContext dbContext = new DatabaseContext())
                {
                    Item? item = dbContext.Items.FirstOrDefault(i => i.Id == itemId);
                    if (item == null)
                    {
                        return false;
                    }
                    else
                    {
                        dbContext.Items.Remove(item);
                        return dbContext.SaveChanges() > 0;
                    }
                }
            });
        }

        /// <summary>
        /// Gets list of existing measures.
        /// </summary>
        /// <returns>Returns list of existing measures.</returns>
        /// <date>31.03.2022.</date>
        public async Task<List<string>> GetMeasures()
        {
            return await Task.Run<List<string>>(() =>
            {
                using (DatabaseContext dbContext = new DatabaseContext())
                {
                    List<string> list = new List<string>();
                    list.AddRange(dbContext.Items.Select(i => i.Measure).Distinct().ToList());
                    list.AddRange(dbContext.ItemsCodes.Select(ic => ic.Measure).Distinct().ToList());

                    return list.Distinct().ToList();
                }
            });
        }
    }
}
