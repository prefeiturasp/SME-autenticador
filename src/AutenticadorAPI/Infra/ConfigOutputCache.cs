using System;
using System.Configuration;

namespace AutenticadorAPI.Infra
{   /// <summary>
    /// Armazena as constantes para configurar o WebApi.OutputCache.V2
    /// </summary>
    public class OutputCacheConstants
    {
        public const int MinutesTimeSpan1 = 60;

        public const int MinutesTimeSpan5 = MinutesTimeSpan1 * 5;

        public const int MinutesTimeSpan15 = MinutesTimeSpan1 * 15;

        public const int MinutesTimeSpan30 = MinutesTimeSpan1 * 30;

        public const int MinutesTimeSpan60 = MinutesTimeSpan1 * 60;

        public const int MinutesTimeSpan120 = MinutesTimeSpan1 * 120;

        public const int MinutesTimeSpan240 = MinutesTimeSpan1 * 240;
    }

    public class PageConfiguration
    {
        public const int MaxPageSize = 100;
    }
}