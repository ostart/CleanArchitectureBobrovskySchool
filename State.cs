namespace CleanArchitectureBobrovskySchool
{
    internal class State
    {
        internal Position Position { get; set; }

        private int _angleInDegrees;

        internal int AngleInDegrees
        {
            get => _angleInDegrees;
            set
            {
                if (value > 360)
                {
                    _angleInDegrees = value % 360;
                }
                else if (value < 0)
                {
                    var temp = value;
                    while (temp < 0)
                    {
                        temp = 360 + temp;
                    }
                    _angleInDegrees = temp;
                }
                else
                {
                    _angleInDegrees = value;
                }
            }
        }
        
        internal Tools Tool { get;set; }
    }

    internal struct Position
    {
        internal double X { get;set; }
        internal double Y { get;set; }
    }
}