// <copyright file="SettingsItemModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AxisUno.Services.Settings
{
    using System;
    using AxisUno.Enums;
    using Microinvest.CommonLibrary.Enums;

    /// <summary>
    /// Describes data of settings item.
    /// </summary>
    public class SettingsItemModel
    {
        private ESettingGroups group;
        private ESettingKeys key;
        private string? value;
        private string defaultValue;
        private bool valueIsChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsItemModel"/> class.
        /// </summary>
        /// <param name="key">Key to save and load setting from the database.</param>
        /// <param name="group">Group to classify setting in the database.</param>
        /// <param name="defaultValue">Default setting.</param>
        public SettingsItemModel(ESettingKeys key, ESettingGroups group, string defaultValue)
        {
            this.key = key;
            this.group = group;
            this.value = null;
            this.defaultValue = defaultValue;
        }

        /// <summary>
        /// Gets or sets settings value.
        /// </summary>
        /// <date>16.03.2022.</date>
        public string Value
        {
            get
            {
                if (this.value == null)
                {
                    this.value = this.defaultValue;
                }

                return (string)this.value;
            }

            set
            {
                if (this.value == null || !this.Value.Equals(value))
                {
                    this.value = value;

                    this.valueIsChanged = true;
                }
            }
        }

        /// <summary>
        /// Convert SettingsItemModel object to string.
        /// </summary>
        /// <param name="settingsItem">SettingsItemModel object.</param>
        /// <date>16.03.2022.</date>
        public static implicit operator string(SettingsItemModel settingsItem)
        {
            return settingsItem.Value;
        }

        /// <summary>
        /// Convert SettingsItemModel object to bool.
        /// </summary>
        /// <param name="settingsItem">SettingsItemModel object.</param>
        /// <date>16.03.2022.</date>
        public static explicit operator bool(SettingsItemModel settingsItem)
        {
            bool result;

            if (bool.TryParse(settingsItem.Value, out result))
            {
                return result;
            }

            throw new FormatException();
        }

        /// <summary>
        /// Convert SettingsItemModel object to int.
        /// </summary>
        /// <param name="settingsItem">SettingsItemModel object.</param>
        /// <date>16.03.2022.</date>
        public static explicit operator int(SettingsItemModel settingsItem)
        {
            int result;
            if (int.TryParse(settingsItem.Value, out result))
            {
                return result;
            }

            throw new FormatException();
        }

        /// <summary>
        /// Convert SettingsItemModel object to DateTime.
        /// </summary>
        /// <param name="settingsItem">SettingsItemModel object.</param>
        /// <date>16.03.2022.</date>
        public static explicit operator DateTime(SettingsItemModel settingsItem)
        {
            DateTime result;
            if (DateTime.TryParse(settingsItem.Value, out result))
            {
                return result;
            }

            throw new FormatException();
        }

        /// <summary>
        /// Convert SettingsItemModel object to ELanguages.
        /// </summary>
        /// <param name="settingsItem">SettingsItemModel object.</param>
        /// <date>16.03.2022.</date>
        public static explicit operator ELanguages(SettingsItemModel settingsItem)
        {
            if (ELanguages.IsDefined(settingsItem.Value))
            {
                return ELanguages.TryParse(settingsItem.Value);
            }

            return ELanguages.Bulgarian;
        }

        /// <summary>
        /// Convert SettingsItemModel object to ECountries.
        /// </summary>
        /// <param name="settingsItem">SettingsItemModel object.</param>
        /// <date>16.03.2022.</date>
        public static explicit operator ECountries(SettingsItemModel settingsItem)
        {
            if (ECountries.IsDefined(settingsItem.Value))
            {
                return ECountries.TryParse(settingsItem.Value);
            }

            return ECountries.Bulgaria;
        }

        /// <summary>
        /// Convert SettingsItemModel object to EOnlineShopTypes.
        /// </summary>
        /// <param name="settingsItem">SettingsItemModel object.</param>
        /// <date>16.03.2022.</date>
        public static explicit operator EOnlineShopTypes(SettingsItemModel settingsItem)
        {
            int value;
            if (int.TryParse(settingsItem.Value, out value) && Enum.IsDefined(typeof(EOnlineShopTypes), value))
            {
                return (EOnlineShopTypes)value;
            }
            else if (Enum.IsDefined(typeof(EOnlineShopTypes), settingsItem.Value))
            {
                return (EOnlineShopTypes)Enum.Parse(typeof(EOnlineShopTypes), settingsItem.Value);
            }

            throw new FormatException();
        }

        /// <summary>
        /// Returns a string that represents the SettingsDataHelper object.
        /// </summary>
        /// <returns>String value.</returns>
        /// <date>16.03.2022.</date>
        public override string ToString()
        {
            return this.Value;
        }

        /// <summary>
        /// Update data in the database if data was changed.
        /// </summary>
        /// <date>16.03.2022.</date>
        public void UpdateData()
        {
            if (this.valueIsChanged)
            {
                // TODO: write code to update settings item value
                this.valueIsChanged = false;
            }
        }
    }
}
