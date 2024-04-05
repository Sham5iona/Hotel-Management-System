namespace HMS.Model
{
    public class Room
    {
        private Guid _roomId;
        public Guid RoomId { get { return _roomId; }
                            set { _roomId = value; } }

        private string _room_number;
        public string RoomNumber { get { return _room_number; }
                                set { _room_number = value; } }

        private string _room_type;
        public string RoomType { get { return _room_type; }
                                 set { _room_type = value;} }

        private int _room_capacity;
        public int RoomCapacity { get { return _room_capacity; }
                                  set { _room_capacity = value;} }

        private bool _room_is_active;
        public bool RoomIsActive { get {  return _room_is_active; }
                                   set { _room_is_active = value;} }

        private double _room_daily_cost;
        public double RoomDailyCost { get {  return _room_daily_cost; }
                                      set {  _room_daily_cost = value;} }

        public ICollection<Reservation> Reservations { get; set; }

        public Room()
        {
            
        }

        public Room(string RoomNumber, string RoomType, int RoomCapacity,
                    bool IsAvailable, double DailyCost)
        {
            this.RoomNumber = RoomNumber;
            this.RoomType = RoomType;
            this.RoomCapacity = RoomCapacity;
            this.RoomIsActive = IsAvailable;
            this.RoomDailyCost = DailyCost;
            this.Reservations = new List<Reservation>();
        }

    }
}
