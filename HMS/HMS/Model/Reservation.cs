namespace HMS.Model
{
    public class Reservation
    {
        private Guid _reservationId;
        public Guid ReservationId { get { return _reservationId; }
                                   set { _reservationId = value; } }

        private DateTime _check_in_date;
        public DateTime CheckInDate {  get { return _check_in_date;}
                                       set { _check_in_date = value;} }

        private DateTime _check_out_date;
        public DateTime CheckOutDate {  get { return _check_out_date;}
                                        set { _check_out_date = value;} }

        public Customer Customer { get; set; }
        private Guid _customer_id;
        public Guid CustomerId { get { return _customer_id; }
                                set { _customer_id = value;} }

        public Room Room { get; set; }
        private Guid _room_id;
        public Guid RoomId
        {
            get { return _room_id; }
            set { _room_id = value; }
        }

        public Reservation()
        {
            
        }

        public Reservation(DateTime CheckInDate, DateTime CheckOutDate, 
                           Guid CustomerId, Guid RoomId)
        {
            this.CheckInDate = CheckInDate;
            this.CheckOutDate = CheckOutDate;
            this.CustomerId = CustomerId;
            this.RoomId = RoomId;
        }

    }
}
