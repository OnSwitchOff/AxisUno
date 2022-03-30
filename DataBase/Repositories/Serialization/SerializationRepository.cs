namespace AxisUno.DataBase.Repositories.Serialization
{
    public partial class SerializationRepository : ISerializationRepository
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
                My100REnteties.Serializations.Serialization? serialization = databaseContext.Serializations.FirstOrDefault(s => s.Group.Equals(groupName) && s.Key.Equals(key));

                if (serialization != null)
                {
                    return serialization.Value;
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
            using (DatabaseContext databaseContext = new DatabaseContext())
            {
                databaseContext.Add(new My100REnteties.Serializations.Serialization()
                {
                    Group = groupName,
                    Key = key,
                    Value = value,
                });
                databaseContext.SaveChanges();
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
            using (DatabaseContext databaseContext = new DatabaseContext())
            {
                My100REnteties.Serializations.Serialization? serialization = databaseContext.Serializations.FirstOrDefault(s => s.Group.Equals(groupName) && s.Key.Equals(key));

                if (serialization != null)
                {
                    serialization.Value = value;
                    databaseContext.Update(serialization);
                    databaseContext.SaveChanges();
                }
                else
                {
                    SetValue(groupName, key, value);
                }
            }
        }
    }
}
