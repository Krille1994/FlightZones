class Flight {
    private string _flightCode;
    private (int x,int y) _position;
    public Flight(string flightCode, (int,int) position) {
        _flightCode = flightCode;
        _position = position;
    }
    public string getFlightCode() {
        return _flightCode;
    }
    public string getFlightPositionAsString() {
        return $"({_position.x},{_position.y})";
    }
}