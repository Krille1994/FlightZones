

// List<IRegion> map = MapParser.Parse(args[0]);

// while(true) {
//     string? rawFlights = Console.ReadLine();
//     if (rawFlights != null) {
string[] str = {"GB4562(1,12)","RE45632(2,3)"};
List<Flight> flights = FlightParser.Parse(str);
foreach(Flight f in flights) {
    Console.WriteLine(f.getFlightCode() + " " + f.getFlightPositionAsString());
}


//     }
// }


// loop 

// get user input


// parse to Flights
// check flights agains map
// send flight message


// class FlightParser (maybe static)
    // takes a string and returns a list of Flights

// class FlightController
    // constructed with Map
    // function checkFlights takes in list of Flights
        // function checkFlight checks Flight against Map and outputs message
