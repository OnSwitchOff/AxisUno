using AxisUno.DataBase.My100REnteties.Settings;
using HarabaSourceGenerators.Common.Attributes;

namespace AxisUno.DataBase.Repositories.Settings
{
    [Inject]
    public partial class SettingsRepository : ISettingsRepository
    {
        private readonly DatabaseContext databaseContext;

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
            Setting? setting = this.databaseContext.Settings.SingleOrDefault(s => s.Group.Equals(groupName) && s.Key.Equals(key));

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

        /// <summary>
        /// Sets value of the setting by the key and name of group.
        /// </summary>
        /// <param name="groupName">Name of settings group.</param>
        /// <param name="key">Key to search value of setting.</param>
        /// <param name="value">New value of setting.</param>
        /// <date>25.03.2022.</date>
        public void SetValue(string groupName, string key, string value)
        {
            this.databaseContext.Add(new Setting()
            {
                Group = groupName,
                Key = key,
                Value = value,
            });
            this.databaseContext.SaveChanges();
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
            Setting? setting = this.databaseContext.Settings.SingleOrDefault(s => s.Group.Equals(groupName) && s.Key.Equals(key));

            if (setting != null)
            {
                setting.Value = value;
                this.databaseContext.Update(setting);
                this.databaseContext.SaveChanges();
            }
            else
            {
                SetValue(groupName, key, value);
            }
        }
    }
}
