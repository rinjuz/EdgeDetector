using System;

namespace TrainingSetGenerator.Models.DTO
{
    class MarkedImage
    {
        public struct Range
        {
            public double Min { get; set; }
            public double Max { get; set; }
        }

        public string Name { get; set; }
        public string Number { get; set; }
        public DateTime Created { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Dpi { get; set; }

        public Range Brightness { get; set; }
        public Range Contrast { get; set; }

        public double InsideLineThickness { get; set; }
        public double OutsideLineThickness { get; set; }

        public MarkedBorder Border { get; set; }
    }
}
