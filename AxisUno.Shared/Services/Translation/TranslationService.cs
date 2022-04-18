// <copyright file="TranslationService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AxisUno.Services.Translation
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using AxisUno.Extensions;
    using static AxisUno.Services.Translation.DictionaryModel;

    /// <summary>
    /// Gets tools to localize the application.
    /// </summary>
    public class TranslationService : ITranslationService
    {
        private static ITranslationService instance;
        private Dictionary<string, string> mainDictionary;
        private Dictionary<string, string> helpDictionary;
        private Dictionary<int, string> wishesDictionary;
        private Dictionary<string, string> supportedLanguages;

        /// <summary>
        /// Creates and returns TranslationService object (Singleton pattern).
        /// </summary>
        /// <returns>Returns TranslationService object.</returns>
        /// <date>12.04.2022.</date>
        public static ITranslationService CreateInstance()
        {
            if (instance == null)
            {
                instance = new TranslationService();
            }

            return instance;
        }

        /// <summary>
        /// Gets languages suppotred by the application.
        /// </summary>
        /// <date>12.04.2022.</date>
        public Dictionary<string, string> SupportedLanguages
        {
            get
            {
                if (this.supportedLanguages == null)
                {
                    throw new NotImplementedException(nameof(this.InitializeDictionary));
                }

                return this.supportedLanguages;
            }
        }

        /// <summary>
        /// Gets dictionary with localized strings.
        /// </summary>
        /// <date>12.04.2022.</date>
        private Dictionary<string, string> MainDictionary => this.mainDictionary == null ? this.mainDictionary = new Dictionary<string, string>() : this.mainDictionary;

        /// <summary>
        /// Gets dictionary with explanation strings.
        /// </summary>
        /// <date>12.04.2022.</date>
        private Dictionary<string, string> HelpDictionary => this.helpDictionary == null ? this.helpDictionary = new Dictionary<string, string>() : this.helpDictionary;

        /// <summary>
        /// Gets dictionary with wish strings.
        /// </summary>
        /// <date>12.04.2022.</date>
        private Dictionary<int, string> WishesDictionary => this.wishesDictionary == null ? this.wishesDictionary = new Dictionary<int, string>() : this.wishesDictionary;

        /// <summary>
        /// LanguageChanged event.
        /// </summary>
        /// <date>12.04.2022.</date>
        public event Action LanguageChanged;

        /// <summary>
        /// Gets localized string in according by key.
        /// </summary>
        /// <param name="key">Key to get localized string.</param>
        /// <returns>Localized string.</returns>
        /// <date>12.04.2022.</date>
        public string Localize(string key)
        {
            if (this.mainDictionary == null)
            {
                throw new NotImplementedException(nameof(this.InitializeDictionary));
            }

            if (this.MainDictionary.ContainsKey(key))
            {
                return this.MainDictionary[key];
            }
            else
            {
                return string.Format("There is no translation for the keyword \"{0}\"!", key);
            }
        }

        /// <summary>
        /// Gets localized explanation string in according by key.
        /// </summary>
        /// <param name="key">Key to get explanation string.</param>
        /// <returns>Explanation string.</returns>
        /// <date>12.04.2022.</date>
        public string GetExplanation(string key)
        {
            if (this.helpDictionary == null)
            {
                throw new NotImplementedException(nameof(this.InitializeDictionary));
            }

            if (this.HelpDictionary.ContainsKey(key))
            {
                return this.HelpDictionary[key];
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets localized wish string in according by key.
        /// </summary>
        /// <param name="key">Key to get wish string.</param>
        /// <returns>Wish string.</returns>
        /// <date>12.04.2022.</date>
        public string GetWish(string key)
        {
            if (this.wishesDictionary == null)
            {
                throw new NotImplementedException(nameof(this.InitializeDictionary));
            }

            return this.WishesDictionary[new Random().Next(1, this.WishesDictionary.Count) - 1];
        }

        /// <summary>
        /// Initialize dictionary with values to localize the application.
        /// </summary>
        /// <param name="languageCode">Combine code of item of Elanguages to fill dictionaries.</param>
        /// <date>12.04.2022.</date>
        public void InitializeDictionary(string languageCode)
        {
            bool firstInitialize = false;
            if (this.supportedLanguages == null)
            {
                this.supportedLanguages = new Dictionary<string, string>();
                firstInitialize = true;
            }

            this.MainDictionary.Clear();
            this.HelpDictionary.Clear();
            this.WishesDictionary.Clear();
            this.SupportedLanguages.Clear();

            Assembly assembly = this.GetType().GetTypeInfo().Assembly;
            string[] names = assembly.GetManifestResourceNames();

            foreach (string dictionaryPath in names)
            {
                using (Stream? stream = assembly.GetManifestResourceStream(names[0]))
                {
                    if (stream != null)
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            RootClass? fullDictionary = XmlExtensions.DeserializeData<RootClass>(reader.ReadToEnd());
                            if (fullDictionary != null)
                            {
                                foreach (Dict dict in fullDictionary.Dictionaries)
                                {
                                    if (dict.Product == "Axis My100R")
                                    {
                                        foreach (Language lang in dict.Languages)
                                        {
                                            this.SupportedLanguages.Add(lang.Text, lang.DisplayName);
                                        }

                                        foreach (Data data in dict.Data)
                                        {
                                            foreach (Value value in data.Values)
                                            {
                                                if (value.Lang.ToUpper().Equals(languageCode.ToUpper()) && !this.MainDictionary.ContainsKey(data.Key))
                                                {
                                                    this.MainDictionary.Add(data.Key, value.Text);
                                                    break;
                                                }
                                            }
                                        }
                                    }

                                    if (dict.Product == "Axis My100R Help")
                                    {
                                        foreach (Data data in dict.Data)
                                        {
                                            foreach (Value value in data.Values)
                                            {
                                                if (value.Lang.ToUpper().Equals(languageCode.ToUpper()) && !this.HelpDictionary.ContainsKey(data.Key))
                                                {
                                                    this.HelpDictionary.Add(data.Key, value.Text);
                                                    break;
                                                }
                                            }
                                        }
                                    }

                                    if (dict.Product == "Axis My100R RandomDictionary")
                                    {
                                        foreach (Data data in dict.Data)
                                        {
                                            foreach (Value value in data.Values)
                                            {
                                                int wishKey = int.Parse(data.Key);
                                                if (value.Lang.ToUpper().Equals(languageCode.ToUpper()) && !this.WishesDictionary.ContainsKey(wishKey))
                                                {
                                                    this.WishesDictionary.Add(wishKey, value.Text);
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (!firstInitialize)
            {
                if (this.LanguageChanged != null)
                {
                    this.LanguageChanged.Invoke();
                }
            }
        }
    }
}
