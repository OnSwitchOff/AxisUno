// <copyright file="SerializationService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AxisUno.Services.Serialization
{
    using System;
    using System.Collections.Generic;
    using AxisUno.DataBase.Repositories.Serialization;
    using AxisUno.Enums;
    using HarabaSourceGenerators.Common.Attributes;

    /// <summary>
    /// Describes serialization service.
    /// </summary>
    [Inject]
    public partial class SerializationService : ISerializationService
    {
        private ISerializationRepository serialization;
        private Dictionary<ESerializationKeys, SerializationItemModel> serializationData;

        /// <summary>
        /// Gets or sets serialization value by key.
        /// </summary>
        /// <param name="key">Key to search serialization value.</param>
        /// <returns>SerializationItemModel.</returns>
        /// <date>28.03.2022.</date>
        public SerializationItemModel this[ESerializationKeys key]
        {
            get
            {
                if (this.ContainsKey(key))
                {
                    return this.serializationData[key];
                }

                throw new KeyNotFoundException();
            }

            set
            {
                if (this.ContainsKey(key))
                {
                    this.serializationData[key] = value;
                }
            }
        }

        /// <summary>
        /// Update serialization values in the database.
        /// </summary>
        /// <date>28.03.2022.</date>
        public void Update()
        {
            if (this.serialization == null)
            {
                throw new Exception("Data doesn't initialized!");
            }

            foreach (KeyValuePair<ESerializationKeys, SerializationItemModel> keyValue in this.serializationData)
            {
                keyValue.Value.UpdateData();
            }
        }

        /// <summary>
        /// Determines whether the serialization data contains the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the serialization data.</param>
        /// <returns>Returns true if the serialization data contains an element with the specified key; otherwise returns false.</returns>
        /// <date>28.03.2022.</date>
        public bool ContainsKey(ESerializationKeys key)
        {
            return this.serializationData.ContainsKey(key);
        }

        /// <summary>
        /// Set serialization data property.
        /// </summary>
        /// <param name="group">Key to search serialization value.</param>
        /// <date>28.03.2022.</date>
        public void InitSerializationData(ESerializationGroups group)
        {
            this.serializationData = new Dictionary<ESerializationKeys, SerializationItemModel>();

            switch (group)
            {
                case ESerializationGroups.Sale:
                    this.serializationData.Add(
                        ESerializationKeys.AddColumns,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.AddColumns, group, "0"));
                    this.serializationData.Add(
                        ESerializationKeys.TbPartnerEnabled,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.TbPartnerEnabled, group, "false"));
                    this.serializationData.Add(
                        ESerializationKeys.TbPartnerText,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.TbPartnerText, group, string.Empty));
                    this.serializationData.Add(
                        ESerializationKeys.TbPartnerID,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.TbPartnerID, group, "-1"));
                    this.serializationData.Add(
                        ESerializationKeys.ColRowNumberWidth,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.ColRowNumberWidth, group, "35"));
                    this.serializationData.Add(
                        ESerializationKeys.ColCodeWidth,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.ColCodeWidth, group, "25"));
                    this.serializationData.Add(
                        ESerializationKeys.ColBarcodeWidth,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.ColBarcodeWidth, group, "40"));
                    this.serializationData.Add(
                        ESerializationKeys.ColNameWidth,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.ColNameWidth, group, "50"));
                    this.serializationData.Add(
                        ESerializationKeys.ColMeasureWidth,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.ColMeasureWidth, group, "45"));
                    this.serializationData.Add(
                        ESerializationKeys.ColQuantityWidth,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.ColQuantityWidth, group, "66"));
                    this.serializationData.Add(
                        ESerializationKeys.ColPriceWidth,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.ColPriceWidth, group, "66"));
                    this.serializationData.Add(
                        ESerializationKeys.ColDiscountWidth,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.ColDiscountWidth, group, "40"));
                    this.serializationData.Add(
                        ESerializationKeys.ColTotalSumWidth,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.ColTotalSumWidth, group, "66"));
                    this.serializationData.Add(
                        ESerializationKeys.ColNoteWidth,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.ColNoteWidth, group, "66"));
                    break;
                case ESerializationGroups.Invoice:
                case ESerializationGroups.Proform:
                case ESerializationGroups.DebitNote:
                case ESerializationGroups.CreditNote:
                    this.serializationData.Add(
                        ESerializationKeys.ColAcctWidth,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.ColAcctWidth, group, "75"));
                    this.serializationData.Add(
                        ESerializationKeys.ColCompanyWidth,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.ColCompanyWidth, group, "150"));
                    this.serializationData.Add(
                        ESerializationKeys.ColCityWidth,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.ColCityWidth, group, "50"));
                    this.serializationData.Add(
                        ESerializationKeys.ColAddressWidth,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.ColAddressWidth, group, "70"));
                    this.serializationData.Add(
                        ESerializationKeys.ColPhoneWidth,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.ColPhoneWidth, group, "50"));
                    this.serializationData.Add(
                        ESerializationKeys.ColDateWidth,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.ColDateWidth, group, "100"));
                    this.serializationData.Add(
                        ESerializationKeys.ColSumWidth,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.ColSumWidth, group, "66"));
                    this.serializationData.Add(
                        ESerializationKeys.ColDocumentNumberWidth,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.ColDocumentNumberWidth, group, "85"));
                    this.serializationData.Add(
                        ESerializationKeys.ColDocumentDateWidth,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.ColDocumentDateWidth, group, "100"));
                    this.serializationData.Add(
                        ESerializationKeys.AddColumns,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.AddColumns, group, "0"));
                    this.serializationData.Add(
                        ESerializationKeys.SelectedPeriod,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.SelectedPeriod, group, "0"));
                    this.serializationData.Add(
                        ESerializationKeys.DateFrom,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.DateFrom, group, DateTime.Now.ToString()));
                    this.serializationData.Add(
                        ESerializationKeys.DateTo,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.DateTo, group, DateTime.Now.ToString()));
                    break;
                case ESerializationGroups.Report:
                    this.serializationData.Add(
                        ESerializationKeys.SelectedGroupNodeTag,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.SelectedGroupNodeTag, group, "-1"));
                    break;
                case ESerializationGroups.ItemsNomenclature:
                    this.serializationData.Add(
                        ESerializationKeys.AddColumns,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.AddColumns, group, "0"));
                    this.serializationData.Add(
                        ESerializationKeys.SelectedGroupNodeTag,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.SelectedGroupNodeTag, group, "-2"));
                    this.serializationData.Add(
                        ESerializationKeys.ColNameWidth,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.ColNameWidth, group, "50"));
                    this.serializationData.Add(
                        ESerializationKeys.ColCodeWidth,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.ColCodeWidth, group, "35"));
                    this.serializationData.Add(
                        ESerializationKeys.ColBarcodeWidth,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.ColBarcodeWidth, group, "90"));
                    this.serializationData.Add(
                        ESerializationKeys.ColMeasureWidth,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.ColMeasureWidth, group, "60"));
                    this.serializationData.Add(
                        ESerializationKeys.ColPriceWidth,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.ColPriceWidth, group, "60"));
                    this.serializationData.Add(
                        ESerializationKeys.ColVATGroupWidth,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.ColVATGroupWidth, group, "35"));
                    break;
                case ESerializationGroups.PartnersNomenclature:
                    this.serializationData.Add(
                        ESerializationKeys.AddColumns,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.AddColumns, group, "0"));
                    this.serializationData.Add(
                        ESerializationKeys.SelectedGroupNodeTag,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.SelectedGroupNodeTag, group, "-2"));
                    this.serializationData.Add(
                        ESerializationKeys.ColCompanyWidth,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.ColCompanyWidth, group, "90"));
                    this.serializationData.Add(
                        ESerializationKeys.ColPrincipalWidth,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.ColPrincipalWidth, group, "90"));
                    this.serializationData.Add(
                        ESerializationKeys.ColCityWidth,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.ColCityWidth, group, "100"));
                    this.serializationData.Add(
                        ESerializationKeys.ColAddressWidth,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.ColAddressWidth, group, "150"));
                    this.serializationData.Add(
                        ESerializationKeys.ColPhoneWidth,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.ColPhoneWidth, group, "75"));
                    this.serializationData.Add(
                        ESerializationKeys.ColEMailWidth,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.ColEMailWidth, group, "75"));
                    this.serializationData.Add(
                        ESerializationKeys.ColTaxNumberWidth,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.ColTaxNumberWidth, group, "65"));
                    this.serializationData.Add(
                        ESerializationKeys.ColVATNumberWidth,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.ColVATNumberWidth, group, "80"));
                    this.serializationData.Add(
                        ESerializationKeys.ColDiscountCardWidth,
                        new SerializationItemModel(ref this.serialization, ESerializationKeys.ColVATGroupWidth, group, "90"));
                    break;
            }
        }
    }
}
