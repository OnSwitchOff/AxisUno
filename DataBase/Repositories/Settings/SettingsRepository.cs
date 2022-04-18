using AxisUno.DataBase.My100REnteties.Settings;

namespace AxisUno.DataBase.Repositories.Settings
{
    public partial class SettingsRepository : ISettingsRepository
    {
        /// <summary>
        /// Gets value of the setting by the key and name of group.
        /// </summary>
        /// <param name="groupName">Name of settings group.</param>
        /// <param name="key">Key to search value of setting.</param>
        /// <param name="defaultValue">Default value of setting.</param>
        /// <returns>Returns value of the setting.</returns>
        /// <date>25.03.2022.</date>
        public string GetValue(string groupName, string key, string defaultValue)
        {
            using (DatabaseContext databaseContext = new DatabaseContext())
            {
                Setting? setting = databaseContext.Settings.SingleOrDefault(s => s.Group.Equals(groupName) && s.Key.Equals(key));

                if (setting != null)
                {
                    return setting.Value;
                }
                else
                {
                    this.SetValue(groupName, key, defaultValue);

                    return defaultValue;
                }
            }
        }

        /// <summary>
        /// Sets value of the setting by the key and name of group.
        /// </summary>
        /// <param name="groupName">Name of settings group.</param>
        /// <param name="key">Key to search value of setting.</param>
        /// <param name="value">New value of setting.</param>
        /// <date>25.03.2022.</date>
        public void SetValue(string groupName, string key, string value)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                dbContext.Add(new Setting()
                {
                    Group = groupName,
                    Key = key,
                    Value = value,
                });
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Updates value of the setting by the key and name of group.
        /// </summary>
        /// <param name="groupName">Name of settings group.</param>
        /// <param name="key">Key to search value of setting.</param>
        /// <param name="value">New value of setting.</param>
        /// <date>25.03.2022.</date>
        public void UpdateValue(string groupName, string key, string value)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                Setting? setting = context.Settings.SingleOrDefault(s => s.Group.Equals(groupName) && s.Key.Equals(key));

                if (setting != null)
                {
                    setting.Value = value;
                    context.Update(setting);
                    context.SaveChanges();
                }
                else
                {
                    SetValue(groupName, key, value);
                }
            }
        }
    }
}
