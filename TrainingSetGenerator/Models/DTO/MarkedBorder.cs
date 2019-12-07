namespace TrainingSetGenerator.Models.DTO
{
    class MarkedBorder
    {
        public struct Point
        {
            public int X { get; set; }
            public int Y { get; set; }
        }
        public struct Edge
        {
            public int I { get; set; }
            public int J { get; set; }
        }

        public Point[] Points { get; set; }
        public Edge[] Edges { get; set; }
    }
}
