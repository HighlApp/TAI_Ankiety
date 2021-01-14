using System;

namespace Surveys.Infrastructure.DTO
{
    public class OptionDTO
    {
        public Guid Id { get; set; }

        public string OptionText { get; set; }

        public double? Value { get; set; }
    }
}
