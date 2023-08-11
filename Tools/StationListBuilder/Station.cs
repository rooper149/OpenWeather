namespace StationListBuilder
{
    public readonly struct Station
    {

        public readonly string CD;      //2  chars
        public readonly string STATION; //16 chars
        public readonly string? ICAO;    //4  chars
        public readonly string? IATA;   //3  chars
        public readonly string? SYNOP;  //5  chars
        public readonly string LAT;     //6  chars
        public readonly string LON;     //7  chars
        public readonly string ELEV;    //4  chars
        public readonly string CC;      //2  chars

        public Station(string line)
        {
            CD = line[..3].Trim();
            STATION = line.Substring(3, 17).Trim();
            ICAO = line.Substring(19, 5).Trim();
            IATA = line.Substring(21, 3).Trim();
            SYNOP = line.Substring(29, 8).Trim();
            LAT = line.Substring(38, 8).Trim();
            LON = line.Substring(45, 9).Trim();
            ELEV = line.Substring(54, 5).Trim();
            CC = line.Substring(line.Length - 2, 2);
        }
    }
}
