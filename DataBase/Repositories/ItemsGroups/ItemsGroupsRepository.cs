using AxisUno.DataBase.My100REnteties.ItemsGroups;

namespace AxisUno.DataBase.Repositories.ItemsGroups
{
    public partial class ItemsGroupsRepository : IItemsGroupsRepository
    {
        private readonly DatabaseContext dbContext = new DatabaseContext();

        /// <summary>
        /// Gets path of group by id of group.
        /// </summary>
        /// <param name="groupId">Id of group.</param>
        /// <returns>Returns "-2" if group is absent; otherwise returns path of group.</returns>
        /// <date>31.03.2022.</date>
        public Task<string> GetPathByIdAsync(int groupId)
        {
            return Task.Run<string>(() =>
            {
                ItemsGroup? res = this.dbContext.ItemsGroups.Where(pg => pg.Id == groupId).FirstOrDefault();
                if (res == null)
                {
                    return "-2";
                }

                return res.Path;
            });
        }

        /// <summary>
        /// Adds new group of items to table with groups of items.
        /// </summary>
        /// <param name="itemsGroup">Group of items.</param>
        /// <returns>Returns 0 if group of items wasn't added to database; otherwise returns real id of new record.</returns>
        /// <date>31.03.2022.</date>
        public Task<int> AddGroup(ItemsGroup itemsGroup)
        {
            return Task.Run<int>(() =>
            {
                this.dbContext.ItemsGroups.Add(itemsGroup);
                this.dbContext.SaveChanges();

                return itemsGroup.Id;
            });
        }

        /// <summary>
        /// Updates the group of items in the table with groups of items.
        /// </summary>
        /// <param name="itemsGroup">Group of items.</param>
        /// <returns>Returns true if group of items was updated; otherwise returns false.</returns>
        /// <date>31.03.2022.</date>
        public Task<bool> UpdateGroup(ItemsGroup itemsGroup)
        {
            return Task.Run<bool>(() =>
            {
                this.dbContext.ItemsGroups.Update(itemsGroup);
                return this.dbContext.SaveChanges() > 0;
            });
        }

        /// <summary>
        /// Deletes group of items by id.
        /// </summary>
        /// <param name="groupId">Id of group of item.</param>
        /// <returns>Returns true if group of items was deleted; otherwise returns false.</returns>
        /// <date>31.03.2022.</date>
        public Task<bool> DeleteGroup(int groupId)
        {
            return Task.Run<bool>(() =>
            {
                ItemsGroup? itemsGroup = this.dbContext.ItemsGroups.FirstOrDefault(i => i.Id == groupId);
                if (itemsGroup == null)
                {
                    return false;
                }
                else
                {
                    this.dbContext.ItemsGroups.Remove(itemsGroup);
                    return this.dbContext.SaveChanges() > 0;
                }
            });
        }

        /// <summary>
        /// Gets list with groups of items.
        /// </summary>
        /// <returns>Returns list with groups of items.</returns>
        /// <date>01.04.2022.</date>
        public async Task<List<ItemsGroup>> GetItemsGroupsAsync()
        {
            return await Task.Run(() =>
            {
                return this.dbContext.ItemsGroups.ToList();
            });
        }
    }
}
