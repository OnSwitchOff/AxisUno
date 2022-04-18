using HarabaSourceGenerators.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.Services.Translation
{
    public class TranslationServiceCommutator
    {
        private readonly ITranslationService translationService;

        public ITranslationService TranslationService => translationService;
    }
}
